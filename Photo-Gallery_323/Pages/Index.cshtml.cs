using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Data.SqlClient;

namespace Photo_Gallery_323.Pages
{
    public class IndexModel : PageModel
    {
        Database database;

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        public void OnPost()
        {
            var username = Request.Form["username"];
            var password = Request.Form["password"];

            database = new Database();
            database.InsertUser(username, password);
        }

        public void SignUp()
        {

        }

    }
}
