using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;


namespace Photo_Gallery_323.Pages
{
    public class signupModel : PageModel
    {
        public void OnGet()
        {

        }

        public void OnPost()
        {

        }

        public IActionResult CreateUser()
        {
            var username = Request.Form["username"];
            var password = Request.Form["password"];

            Database database = new Database();

            database.InsertUser(username, password);
            return RedirectToPage("Index");
        }
    }
}
