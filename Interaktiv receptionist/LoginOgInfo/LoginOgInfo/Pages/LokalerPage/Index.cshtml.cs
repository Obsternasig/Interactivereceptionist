using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LoginOgInfo.Lokaler;
using LoginOgInfo.Models;

namespace LoginOgInfo.Pages.LokalerPage
{
    public class IndexModel : PageModel
    {
        private readonly LoginOgInfo.Models.LoginOgInfoContext _context;

        public IndexModel(LoginOgInfo.Models.LoginOgInfoContext context)
        {
            _context = context;
        }

        public IList<LokalerInfo> LokalerInfo { get;set; }

        public async Task OnGetAsync()
        {
            LokalerInfo = await _context.LokalerInfo.ToListAsync();
        }
    }
}
