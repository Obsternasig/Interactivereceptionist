using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Interaktivreceptionist.Pages
{
    public class AboutModel : PageModel
    {
        public string Message { get; set; }

        public void OnGet()
        {
            Message = "Hej Bloggen, så skal der kodes";
        }
    }
}