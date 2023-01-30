using System.Collections.Generic;
using FullAntDesign.Models;
using Microsoft.AspNetCore.Components;

namespace FullAntDesign.Pages.Account.Center
{
    public partial class Articles
    {
        [Parameter] public IList<ListItemDataType> List { get; set; }
    }
}