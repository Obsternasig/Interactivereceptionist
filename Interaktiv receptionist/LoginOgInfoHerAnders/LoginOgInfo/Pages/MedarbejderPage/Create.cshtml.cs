using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using LoginOgInfo.Medarbejdere;
using LoginOgInfo.Models;

namespace LoginOgInfo.Pages.MedarbejderPage
{
    public class CreateModel : PageModel
    {
        private readonly LoginOgInfo.Models.LoginOgInfoContext _context;

        public CreateModel(LoginOgInfo.Models.LoginOgInfoContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public MedarbejderInfo MedarbejderInfo { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.MedarbejderInfo.Add(MedarbejderInfo);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}