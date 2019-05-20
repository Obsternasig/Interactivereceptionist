using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NOVIRP.Models;


namespace NOVIRP.Pages.Firmaer
{
    public class EditModel : PageModel
    {
        private readonly NOVIRP.Models.NOVIRPContext _context;

        public EditModel(NOVIRP.Models.NOVIRPContext context)
        {
            _context = context;
        }

        [BindProperty]
        public NOVI NOVI { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            NOVI = await _context.NOVI.FirstOrDefaultAsync(m => m.ID == id);

            if (NOVI == null)
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

            _context.Attach(NOVI).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NOVIExists(NOVI.ID))
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

        private bool NOVIExists(int id)
        {
            return _context.NOVI.Any(e => e.ID == id);
        }
    }
}
