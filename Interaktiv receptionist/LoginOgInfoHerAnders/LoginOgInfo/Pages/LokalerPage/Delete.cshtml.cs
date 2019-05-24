using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LoginOgInfo.Lokaler;
using LoginOgInfo.Models;

namespace LoginOgInfo.Pages.LokalerPage
{
    public class DeleteModel : PageModel
    {
        private readonly LoginOgInfo.Models.LoginOgInfoContext _context;

        public DeleteModel(LoginOgInfo.Models.LoginOgInfoContext context)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            LokalerInfo = await _context.LokalerInfo.FindAsync(id);

            if (LokalerInfo != null)
            {
                _context.LokalerInfo.Remove(LokalerInfo);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
