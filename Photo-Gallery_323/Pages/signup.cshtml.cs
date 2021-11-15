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
            var username = Request.Form["username"];
            var password = Request.Form["password"];

            //string we use to salt our hash
            byte[] salt = Convert.FromBase64String("Kem+zsDZYAl8PxvnVbd6+g==");

            // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8
                ));


            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "cmpg323-server.database.windows.net";
                builder.UserID = "Coffeebean";
                builder.Password = "Simpel projek!";
                builder.InitialCatalog = "cmpg323-db";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    string sql = "INSERT INTO dbo.[User] (Username, Password) VALUES ('" + username + "', '" + hashed + "');";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
            catch (SqlException e)
            {

            }


        }
    }
}
