using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LoginOgInfo.Lokaler;
using LoginOgInfo.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LoginOgInfo.Pages.LokalerPage
{
    public class IndexModel : PageModel
    {
        private readonly LoginOgInfo.Models.LoginOgInfoContext _context;

        public IndexModel(LoginOgInfo.Models.LoginOgInfoContext context)
        {
            _context = context;
        }

        public IList<LokalerInfo> LokalerInfo { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        // Requires using Microsoft.AspNetCore.Mvc.Rendering;
        public SelectList Lokalenummer { get; set; }
        [BindProperty(SupportsGet = true)]
        public string LokalerInfoLokalenummer { get; set; }


        public async Task OnGetAsync()
        {
            var LokalerPage = from m in _context.LokalerInfo
                         select m;
            if (!string.IsNullOrEmpty(SearchString))
            {
                LokalerPage = LokalerPage.Where(s => s.Lokalenummer.Contains(SearchString));
                
            }
            // LokalerInfo = await LokalerInfo.ToListAsync();
            LokalerInfo = await LokalerPage.ToListAsync();
            
        }
    }
}


