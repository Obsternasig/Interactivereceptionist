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
    public class DetailsModel : PageModel
    {
        private readonly LoginOgInfo.Models.LoginOgInfoContext _context;

        public DetailsModel(LoginOgInfo.Models.LoginOgInfoContext context)
        {
            _context = context;
        }

        public VirksomhederInfo VirksomhederInfo { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            VirksomhederInfo = await _context.VirksomhederInfo.FirstOrDefaultAsync(m => m.ID == id);

            if (VirksomhederInfo == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
