using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Interaktivreceptionist.Pages
{
    public class AboutModel : PageModel
    {
        public string Message { get; set; }

        public void OnGet()
        {
            Message = "Hej Bloggen, så skal der kneppes, høhø - prøv at skrive æ ø og å min pik er hoar ";
        }
    }
    using Microsoft.AspNetCore.Mvc;

    namespace Kendo.Mvc.Examples.Controllers
    {
        public partial class ButtonController : Controller
        {
            [Demo]
            public IActionResult Events()
            {
                return View();
            }
        }
    }
}