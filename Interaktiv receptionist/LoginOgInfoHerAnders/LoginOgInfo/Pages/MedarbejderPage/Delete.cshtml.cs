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
    public class DeleteModel : PageModel
    {
        private readonly LoginOgInfo.Models.LoginOgInfoContext _context;

        public DeleteModel(LoginOgInfo.Models.LoginOgInfoContext context)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MedarbejderInfo = await _context.MedarbejderInfo.FindAsync(id);

            if (MedarbejderInfo != null)
            {
                _context.MedarbejderInfo.Remove(MedarbejderInfo);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
