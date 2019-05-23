using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LoginOgInfo.Medarbejdere;
using LoginOgInfo.Models;

namespace LoginOgInfo.Pages.MedarbejderPage
{
    public class EditModel : PageModel
    {
        private readonly LoginOgInfo.Models.LoginOgInfoContext _context;

        public EditModel(LoginOgInfo.Models.LoginOgInfoContext context)
        {
            _context = context;
        }

        [BindProperty]
        public MedarbejderInfo MedarbejderInfo { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MedarbejderInfo = await _context.MedarbejderInfo.FirstOrDefaultAsync(m => m.ID == id);

            if (MedarbejderInfo == null)
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

            _context.Attach(MedarbejderInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedarbejderInfoExists(MedarbejderInfo.ID))
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

        private bool MedarbejderInfoExists(int id)
        {
            return _context.MedarbejderInfo.Any(e => e.ID == id);
        }
    }
}
