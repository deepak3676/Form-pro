using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Web_proj.Pages.Clients
{
    public class readModel : PageModel
    {
        public List<alpha> rover = new List<alpha>();
        public string errorMessage = "";

        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=white;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM whiteform";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                alpha beta = new alpha();
                                beta.Id = reader.GetInt32(0).ToString();
                                beta.Name = reader.GetString(1);
                                beta.Age = reader.GetString(2);
                                beta.Sex = reader.GetString(3);
                                beta.Hobby = reader.GetString(4);
                                beta.EmailId = reader.GetString(5);

                                rover.Add(beta);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }
    }
    public class alpha
    {
        public string Id;
        public string Name;
        public string Age;
        public string Sex;
        public string Hobby;
        public string EmailId;
    }

}
