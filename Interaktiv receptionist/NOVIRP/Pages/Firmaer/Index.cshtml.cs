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
    public class IndexModel : PageModel
    {
        private readonly NOVIRP.Models.NOVIRPContext _context;

        public IndexModel(NOVIRP.Models.NOVIRPContext context)
        {
            _context = context;
        }

        public IList<NOVI> NOVI { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        // Requires using Microsoft.AspNetCore.Mvc.Rendering;
        public SelectList CVR { get; set; }
        [BindProperty(SupportsGet = true)]
        public string NOVICVR { get; set; }

        public async Task OnGetAsync()
        {
            // Use LINQ to get list of genres.
            IQueryable<int> CVRQuery = from m in _context.NOVI
                                       orderby m.CVR
                                       select m.CVR;

            var firmaer = from m in _context.NOVI
                          select m;

            if (!string.IsNullOrEmpty(SearchString))
            {
                firmaer = firmaer.Where(s => s.Navn.Contains(SearchString));
            }

            if (!string.IsNullOrEmpty(NOVICVR))
            {
                firmaer = firmaer.Where(x => x.CVR == Convert.ToInt32(NOVICVR)); //Denne her linje giver måske lidt bøvl, mht til at søge ,
            }
            CVR = new SelectList(await CVRQuery.Distinct().ToListAsync());
            NOVI = await firmaer.ToListAsync();
        }
    }
}

