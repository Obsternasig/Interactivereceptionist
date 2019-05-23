using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LoginOgInfo.Models;
using LoginOgInfo.Virksomheder;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LoginOgInfo.Pages.VirksomhederPage
{
    public class IndexModel : PageModel
    {
        private readonly LoginOgInfo.Models.LoginOgInfoContext _context;

        public IndexModel(LoginOgInfo.Models.LoginOgInfoContext context)
        {
            _context = context;
        }

        public IList<VirksomhederInfo> VirksomhederInfo { get;set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        // Requires using Microsoft.AspNetCore.Mvc.Rendering;
        public SelectList Navn { get; set; }
        [BindProperty(SupportsGet = true)]
        public string VirksomhederInfoNavn { get; set; }

        public async Task OnGetAsync()
        {
            var VirksomhederPage = from m in _context.VirksomhederInfo
                                  select m;
            if (!string.IsNullOrEmpty(SearchString))
            {
                VirksomhederPage = VirksomhederPage.Where(s => s.Navn.Contains(SearchString));
            }

            VirksomhederInfo = await VirksomhederPage.ToListAsync();
        }
    }
}

