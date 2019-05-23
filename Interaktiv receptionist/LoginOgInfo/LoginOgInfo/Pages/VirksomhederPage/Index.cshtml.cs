using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LoginOgInfo.Models;
using LoginOgInfo.Virksomheder;

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

        public async Task OnGetAsync()
        {
            VirksomhederInfo = await _context.VirksomhederInfo.ToListAsync();
        }
    }
}
