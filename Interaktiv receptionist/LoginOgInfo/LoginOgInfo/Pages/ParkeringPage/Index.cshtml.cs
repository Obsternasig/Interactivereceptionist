using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LoginOgInfo.BrugerParkering;
using LoginOgInfo.Models;

namespace LoginOgInfo.Pages.ParkeringPage
{
    public class IndexModel : PageModel
    {
        private readonly LoginOgInfo.Models.LoginOgInfoContext _context;

        public IndexModel(LoginOgInfo.Models.LoginOgInfoContext context)
        {
            _context = context;
        }

        public IList<ParkeringsInfo> ParkeringsInfo { get;set; }

        public async Task OnGetAsync()
        {
            ParkeringsInfo = await _context.ParkeringsInfo.ToListAsync();
        }
    }
}
