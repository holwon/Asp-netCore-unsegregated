﻿using System.Threading.Tasks;
using Blazored.Toast.Services;
using Bunit;
using Bunit.TestDoubles;
using LinkDotNet.Blog.Domain;
using LinkDotNet.Blog.TestUtilities;
using LinkDotNet.Blog.Web;
using LinkDotNet.Blog.Web.Features.Components;
using LinkDotNet.Blog.Web.Features.Services;
using LinkDotNet.Blog.Web.Features.ShowBlogPost;
using LinkDotNet.Blog.Web.Features.ShowBlogPost.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LinkDotNet.Blog.IntegrationTests.Web.Features.ShowBlogPost;

public class ShowBlogPostPageTests : SqlDatabaseTestBase<BlogPost>
{
    [Fact]
    public async Task ShouldAddLikeOnEvent()
    {
        var publishedPost = new BlogPostBuilder().WithLikes(2).IsPublished().Build();
        await Repository.StoreAsync(publishedPost);
        using var ctx = new TestContext();
        ctx.JSInterop.Mode = JSRuntimeMode.Loose;
        RegisterComponents(ctx);
        ctx.AddTestAuthorization().SetAuthorized("s");
        var cut = ctx.RenderComponent<ShowBlogPostPage>(
            p => p.Add(b => b.BlogPostId, publishedPost.Id));
        var likeComponent = cut.FindComponent<Like>();
        likeComponent.SetParametersAndRender(c => c.Add(p => p.BlogPost, publishedPost));

        likeComponent.Find("span").Click();

        var fromDb = await DbContext.BlogPosts.AsNoTracking().SingleAsync(d => d.Id == publishedPost.Id);
        fromDb.Likes.Should().Be(3);
    }

    [Fact]
    public async Task ShouldSubtractLikeOnEvent()
    {
        var publishedPost = new BlogPostBuilder().WithLikes(2).IsPublished().Build();
        await Repository.StoreAsync(publishedPost);
        using var ctx = new TestContext();
        var localStorage = new Mock<ILocalStorageService>();
        var hasLikedStorage = $"hasLiked/{publishedPost.Id}";
        localStorage.Setup(l => l.ContainKeyAsync(hasLikedStorage)).ReturnsAsync(true);
        localStorage.Setup(l => l.GetItemAsync<bool>(hasLikedStorage)).ReturnsAsync(true);
        ctx.JSInterop.Mode = JSRuntimeMode.Loose;
        RegisterComponents(ctx, localStorage.Object);
        ctx.AddTestAuthorization().SetAuthorized("s");
        var cut = ctx.RenderComponent<ShowBlogPostPage>(
            p => p.Add(b => b.BlogPostId, publishedPost.Id));
        var likeComponent = cut.FindComponent<Like>();
        likeComponent.SetParametersAndRender(c => c.Add(p => p.BlogPost, publishedPost));

        likeComponent.Find("span").Click();

        var fromDb = await DbContext.BlogPosts.AsNoTracking().SingleAsync(d => d.Id == publishedPost.Id);
        fromDb.Likes.Should().Be(1);
    }

    [Fact]
    public async Task ShouldSetTagsWhenAvailable()
    {
        var publishedPost = new BlogPostBuilder().IsPublished().WithTags("Tag1,Tag2").Build();
        await Repository.StoreAsync(publishedPost);
        using var ctx = new TestContext();
        ctx.JSInterop.Mode = JSRuntimeMode.Loose;
        ctx.AddTestAuthorization();
        RegisterComponents(ctx);
        var cut = ctx.RenderComponent<ShowBlogPostPage>(
            p => p.Add(b => b.BlogPostId, publishedPost.Id));

        var ogData = cut.FindComponent<OgData>();

        ogData.Instance.Keywords.Should().Be("Tag1,Tag2");
    }

    private void RegisterComponents(TestContextBase ctx, ILocalStorageService localStorageService = null)
    {
        ctx.Services.AddScoped(_ => Repository);
        ctx.Services.AddScoped(_ => localStorageService ?? Mock.Of<ILocalStorageService>());
        ctx.Services.AddScoped(_ => Mock.Of<IToastService>());
        ctx.Services.AddScoped(_ => Mock.Of<IUserRecordService>());
        ctx.Services.AddScoped(_ => new AppConfiguration());
    }
}