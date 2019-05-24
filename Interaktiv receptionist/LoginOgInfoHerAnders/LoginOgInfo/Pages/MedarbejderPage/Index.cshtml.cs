using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LoginOgInfo.Medarbejdere;
using LoginOgInfo.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LoginOgInfo.Pages.MedarbejderPage
{
    public class IndexModel : PageModel
    {
        private readonly LoginOgInfo.Models.LoginOgInfoContext _context;

        public IndexModel(LoginOgInfo.Models.LoginOgInfoContext context)
        {
            _context = context;
        }

        public IList<MedarbejderInfo> MedarbejderInfo { get;set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        // Requires using Microsoft.AspNetCore.Mvc.Rendering;
        public SelectList Navn { get; set; }
        [BindProperty(SupportsGet = true)]
        public string MedarbejderInfoNavn { get; set; }

        public async Task OnGetAsync()
        {
            var MedarbejderPage = from m in _context.MedarbejderInfo
                         select m;
            if (!string.IsNullOrEmpty(SearchString))
            {
                MedarbejderPage = MedarbejderPage.Where(s => s.Navn.Contains(SearchString));
            }

            MedarbejderInfo = await MedarbejderPage.ToListAsync();
        }
    }
}
