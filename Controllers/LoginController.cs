using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GroceryOrdering.Models;
using System.Web.UI.WebControls;

namespace GroceryOrdering.Controllers
{
    public class LoginController : ApiController
    {
        [HttpGet]
        [Route("api/verifylogin")]
        public buyer verifylogin(string username)
        {
            DataTable tbl = new DataTable();


            buyer b = new buyer();
            //database name, server name, driver, credentails - connection string


            //sql server connection string
            string connectionstring = "data source=DESKTOP-8HK3EKH\\SQLEXPRESS;initial catalog=groceriesordering;integrated security=sspi";

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionstring;

            //con.Open();

            //command
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = con;

            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.CommandText = "[dbo].[VerifyLogin]";

            SqlParameter name = new SqlParameter("@name", SqlDbType.VarChar, 200);
            name.Value = username;

            sqlCommand.Parameters.Add(name);

            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
            da.Fill(tbl);

            for (int row = 0; row < tbl.Rows.Count; row++)
            {

                b.name = tbl.Rows[row]["name"].ToString();
                b.id = Convert.ToInt32(tbl.Rows[row]["id"]);


            }


            //con.Close();

            return b;
        }



        [HttpPost]
        [Route("api/createbuyer")]
        public IHttpActionResult CreateBuyer(buyer newBuyer)
        {
            if (newBuyer == null)
            {
                return BadRequest("Invalid buyer data.");
            }

            string connectionstring = "data source=DESKTOP-8HK3EKH\\SQLEXPRESS;initial catalog=groceriesordering;integrated security=sspi";
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = con;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = "[dbo].[CreateBuyer]";

                sqlCommand.Parameters.AddWithValue("@name", newBuyer.name);
                sqlCommand.Parameters.AddWithValue("@mobileno", newBuyer.mobileno);
                // Add any additional parameters for the buyer entity based on your database schema

                sqlCommand.ExecuteNonQuery();
            }

            return Ok();
        }

    }
}
