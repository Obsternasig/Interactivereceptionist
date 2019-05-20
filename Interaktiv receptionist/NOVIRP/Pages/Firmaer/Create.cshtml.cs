using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NOVIRP.Models;


namespace NOVIRP.Pages.Firmaer
{
    public class CreateModel : PageModel
    {
        private readonly NOVIRP.Models.NOVIRPContext _context;

        public CreateModel(NOVIRP.Models.NOVIRPContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public NOVI NOVI { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.NOVI.Add(NOVI);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}