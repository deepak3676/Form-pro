using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Web_proj.Pages.Clients
{
    public class createModel : PageModel
    {
        public alpha indo = new alpha();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            indo.Name = Request.Form["Name"];
            indo.Age = Request.Form["Age"];
            indo.Sex = Request.Form["Sex"];
            indo.Hobby = Request.Form["Hobby"];
            indo.EmailId = Request.Form["EmailId"];

            if (string.IsNullOrEmpty(indo.Name) || string.IsNullOrEmpty(indo.Age) ||
                string.IsNullOrEmpty(indo.Sex) || string.IsNullOrEmpty(indo.Hobby) ||
                string.IsNullOrEmpty(indo.EmailId))
            {
                errorMessage = "All fields are required";
                return Page();
            }

            try
            {
                string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=white;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO whiteform (Name, Age, Sex, Hobby, EmailId) VALUES " +
                        "(@Name, @Age, @Sex, @Hobby, @EmailId)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Name", indo.Name);
                        command.Parameters.AddWithValue("@Age", indo.Age);
                        command.Parameters.AddWithValue("@Sex", indo.Sex);
                        command.Parameters.AddWithValue("@Hobby", indo.Hobby);
                        command.Parameters.AddWithValue("@EmailId", indo.EmailId);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return Page();
            }

            return RedirectToPage("/Clients/read");
        }
    }

}