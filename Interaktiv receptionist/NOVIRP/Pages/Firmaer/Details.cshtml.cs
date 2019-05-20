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
    public class DetailsModel : PageModel
    {
        private readonly NOVIRP.Models.NOVIRPContext _context;

        public DetailsModel(NOVIRP.Models.NOVIRPContext context)
        {
            _context = context;
        }

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
    }
}
