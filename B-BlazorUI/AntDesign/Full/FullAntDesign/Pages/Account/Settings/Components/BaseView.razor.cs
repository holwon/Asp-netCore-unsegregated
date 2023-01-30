using System.Threading.Tasks;
using FullAntDesign.Models;
using FullAntDesign.Services;
using Microsoft.AspNetCore.Components;

namespace FullAntDesign.Pages.Account.Settings
{
    public partial class BaseView
    {
        private CurrentUser _currentUser = new CurrentUser();

        [Inject] protected IUserService UserService { get; set; }

        private void HandleFinish()
        {
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            _currentUser = await UserService.GetCurrentUserAsync();
        }
    }
}