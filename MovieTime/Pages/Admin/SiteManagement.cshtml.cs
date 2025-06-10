using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MovieTime.Pages
{
    public class SiteManagementModel : PageModel
    {
        public IActionResult OnGet()
        {
            //בדיקה אם המשתמש הוא מנהל
            //בגלל שזה נשמר בזיכרון זמני של המשתמש בזמן הגלישה
            if (HttpContext.Session.GetString("IsAdmin") != "true")
            {
                return RedirectToPage("/AdminLogin");
            }

            return Page();
        }
    }
}
