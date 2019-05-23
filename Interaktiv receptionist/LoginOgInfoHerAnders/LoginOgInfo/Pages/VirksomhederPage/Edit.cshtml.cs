using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LoginOgInfo.Models;
using LoginOgInfo.Virksomheder;

namespace LoginOgInfo.Pages.VirksomhederPage
{
    public class EditModel : PageModel
    {
        private readonly LoginOgInfo.Models.LoginOgInfoContext _context;

        public EditModel(LoginOgInfo.Models.LoginOgInfoContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(VirksomhederInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VirksomhederInfoExists(VirksomhederInfo.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool VirksomhederInfoExists(int id)
        {
            return _context.VirksomhederInfo.Any(e => e.ID == id);
        }
    }
}
