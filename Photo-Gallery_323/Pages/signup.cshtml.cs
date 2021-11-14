using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            var username = Request.Form["username"];
            var password = Request.Form["password"];

            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "cmpg323-server.database.windows.net";
                builder.UserID = "Coffeebean";
                builder.Password = "Simpel projek!";
                builder.InitialCatalog = "cmpg323-db";

            }
            catch (SqlException e)
            {

            }
        }
    }
}
