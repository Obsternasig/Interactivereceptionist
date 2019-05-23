using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LoginOgInfo.Medarbejdere;
using LoginOgInfo.Models;

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

        public async Task OnGetAsync()
        {
            MedarbejderInfo = await _context.MedarbejderInfo.ToListAsync();
        }
    }
}
