using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using LoginOgInfo.BrugerParkering;
using LoginOgInfo.Models;

namespace LoginOgInfo.Pages.ParkeringPage
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
        public ParkeringsInfo ParkeringsInfo { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ParkeringsInfo.Add(ParkeringsInfo);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}