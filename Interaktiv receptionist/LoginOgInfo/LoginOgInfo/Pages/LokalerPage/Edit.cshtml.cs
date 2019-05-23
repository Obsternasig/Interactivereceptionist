using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LoginOgInfo.Lokaler;
using LoginOgInfo.Models;

namespace LoginOgInfo.Pages.LokalerPage
{
    public class EditModel : PageModel
    {
        private readonly LoginOgInfo.Models.LoginOgInfoContext _context;

        public EditModel(LoginOgInfo.Models.LoginOgInfoContext context)
        {
            _context = context;
        }

        [BindProperty]
        public LokalerInfo LokalerInfo { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            LokalerInfo = await _context.LokalerInfo.FirstOrDefaultAsync(m => m.ID == id);

            if (LokalerInfo == null)
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

            _context.Attach(LokalerInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LokalerInfoExists(LokalerInfo.ID))
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

        private bool LokalerInfoExists(int id)
        {
            return _context.LokalerInfo.Any(e => e.ID == id);
        }
    }
}
