using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Photo_Gallery_323
{
    public class Database
    {
        private SqlConnectionStringBuilder builder;

        public SqlConnection connection;

        public Database() 
        {
            builder = new SqlConnectionStringBuilder();
            builder.DataSource = "cmpg323-server.database.windows.net";
            builder.UserID = "Coffeebean";
            builder.Password = "Simpel projek!";
            builder.InitialCatalog = "cmpg323-db";
            connection = new SqlConnection(builder.ConnectionString);
        }

        public void InsertUser(string username, string password)
        {

            //Generate a 128-bit salt using a cryptographically strong random sequence of nonzero values
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
                if (UserExist(username))
                {
                    return;
                }

                using (connection)
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

        public Boolean UserExist(string username)
        {
            try
            {
                using (connection)
                {
                    string sql = $"SELECT * FROM dbo.[User] WHERE [Username]={username};";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (!reader.HasRows)
                            {
                                return false;
                            }
                            else
                            {
                                return true;
                            }
                        }
                        connection.Close();
                    }
                }
            }
            catch (SqlException e)
            {

            }

            return true;
        }


    }
}
