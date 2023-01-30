﻿using System;
using System.Linq;
using System.Threading.Tasks;
using LinkDotNet.Blog.Domain;
using LinkDotNet.Blog.Infrastructure.Persistence;

namespace LinkDotNet.Blog.Web.Features.Admin.Dashboard.Services;

public class DashboardService : IDashboardService
{
    private readonly IRepository<UserRecord> userRecordRepository;

    public DashboardService(
        IRepository<UserRecord> userRecordRepository)
    {
        this.userRecordRepository = userRecordRepository;
    }

    public async Task<DashboardData> GetDashboardDataAsync()
    {
        var records = await userRecordRepository.GetAllAsync();
        var users = records.GroupBy(r => r.UserIdentifierHash).Count();
        var thirtyDaysAgo = DateTime.UtcNow.AddDays(-30).Date;
        var users30Days = records
            .Where(r => r.DateTimeUtcClicked >= thirtyDaysAgo)
            .GroupBy(r => r.UserIdentifierHash).Count();

        var clicks = records.Count;
        var clicks30Days = records.Count(r => r.DateTimeUtcClicked >= thirtyDaysAgo);

        var aboutMeClicks = records.Count(r => r.UrlClicked.Contains("AboutMe"));
        var aboutMeClicksLast30Days = records.Count(r => r.UrlClicked.Contains("AboutMe") && r.DateTimeUtcClicked >= thirtyDaysAgo);

        return new DashboardData
        {
            TotalAmountOfUsers = users,
            AmountOfUsersLast30Days = users30Days,
            TotalPageClicks = clicks,
            PageClicksLast30Days = clicks30Days,
            TotalAboutMeClicks = aboutMeClicks,
            AboutMeClicksLast30Days = aboutMeClicksLast30Days,
        };
    }
}