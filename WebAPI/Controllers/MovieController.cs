using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Odbc;
using System.Data;

using Microsoft.Data.SqlClient;


namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : Controller
    {
  
        [HttpGet]
        public JsonResult Index()
        {
            DataTable dataTable = new DataTable();
            string connString = @"Data Source=MSI\MSSQLSERVER02;Initial Catalog=Movie;Integrated Security=True;Encrypt=false;TrustServerCertificate=true";

            string query = "select * from Movie";

            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();

            // create data adapter
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            // this will query your database and return the result to your datatable
            da.Fill(dataTable);
            conn.Close();
            da.Dispose();
            return new JsonResult(dataTable);

        }
    }
}
