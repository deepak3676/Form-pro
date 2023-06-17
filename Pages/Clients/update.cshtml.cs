using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Web_proj.Pages.Clients
{
    public class updateModel : PageModel
    {
        public alpha indo=new alpha();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
            string id = Request.Query["Id"];
            try
            {
                string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=white;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM whiteform" +
                        " WHERE Id=@Id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if(reader.Read())
                            {
                                indo.Id = "" + reader.GetInt32(0);
                                indo.Name = reader.GetString(1);
                                indo.Age = reader.GetString(2);
                                indo.Sex = reader.GetString(3);
                                indo.Hobby = reader.GetString(4);
                                indo.EmailId= reader.GetString(5);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message+"asdadasdadwdadawd";
                return;
            }
        }
        public void OnPost()
        {
            indo.Id = Request.Form["Id"];
            indo.Name = Request.Form["Name"];
            indo.Age = Request.Form["Age"];
            indo.Sex = Request.Form["Sex"];
            indo.Hobby = Request.Form["Hobby"];
            indo.EmailId = Request.Form["EmailId"];

            if (indo.Name.Length==0|| indo.Sex.Length == 0||indo.Hobby.Length==0)
            {
                errorMessage = "All fields are required";
                return;
            }
            try
            {
                string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=white;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "UPDATE whiteform " +
                        " SET  Name=@Name, Age=@Age, Sex =@Sex, Hobby=@Hobby ,EmailId=@EmailId " +
                        " WHERE Id=@Id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Name", indo.Name);
                        command.Parameters.AddWithValue("@Age", indo.Age);
                        command.Parameters.AddWithValue("@Sex", indo.Sex);
                        command.Parameters.AddWithValue("@Hobby", indo.Hobby);
                        command.Parameters.AddWithValue("@EmailId", indo.EmailId);
                        command.Parameters.AddWithValue("@Id", indo.Id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {

                errorMessage = ex.Message;
                return;
            }
            successMessage = "All fields are saveed";
            Response.Redirect("/Clients/read");

        }
    }
}
