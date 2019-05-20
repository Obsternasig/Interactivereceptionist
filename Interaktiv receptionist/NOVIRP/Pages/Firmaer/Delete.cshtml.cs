using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NOVIRP.Models;


namespace NOVIRP.Pages.Firmaer
{
    public class DeleteModel : PageModel
    {
        private readonly NOVIRP.Models.NOVIRPContext _context;

        public DeleteModel(NOVIRP.Models.NOVIRPContext context)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            NOVI = await _context.NOVI.FindAsync(id);

            if (NOVI != null)
            {
                _context.NOVI.Remove(NOVI);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
