using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using LoginOgInfo.Models;
using LoginOgInfo.Virksomheder;

namespace LoginOgInfo.Pages.VirksomhederPage
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
        public VirksomhederInfo VirksomhederInfo { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.VirksomhederInfo.Add(VirksomhederInfo);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}