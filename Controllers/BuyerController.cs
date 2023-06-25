using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GroceryOrdering.Models;

namespace GroceryOrdering.Controllers
{
    public class BuyerController : ApiController
    {

        [HttpGet]
        [Route("api/buyerlist")]
        public List<buyer> buyerlist()
        {
            DataTable tbl = new DataTable();


            List<buyer> buyers = new List<buyer>();
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
            sqlCommand.CommandText = "[dbo].[GetBuyersList]";

            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
            da.Fill(tbl);

            for (int row = 0; row < tbl.Rows.Count; row++)
            {

                buyer b = new buyer();
                b.name = tbl.Rows[row]["name"].ToString();
                b.id = Convert.ToInt32(tbl.Rows[row]["id"]);
                buyers.Add(b);
            }


            //con.Close();

            return buyers;
        }


        [HttpPost]
        [Route("api/updatebuyer")]
        public List<buyer> updatebuyer(buyer b)
        {
            DataTable tbl = new DataTable();

            List<buyer> buyers = new List<buyer>();

            //sql server connection string
            string connectionstring = "data source=DESKTOP-8HK3EKH\\SQLEXPRESS;initial catalog=groceriesordering;integrated security=sspi";

            SqlConnection con = new SqlConnection();
            con.ConnectionString = connectionstring;

            //command
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = con;

            sqlCommand.CommandType = CommandType.StoredProcedure;
            //  sqlCommand.CommandText = "INSERT INTO [dbo].[student]([name],[age],[address])VALUES ('shireesha',25,'hyd')";

            sqlCommand.CommandText = "insupddelbuyer";

            SqlParameter nameparam = new SqlParameter("@name", SqlDbType.VarChar, 100);

            nameparam.Value = b.name;

            SqlParameter idparam = new SqlParameter("@id", SqlDbType.Int);
            idparam.Value = b.id;

            SqlParameter mobilenoparam = new SqlParameter("@mobileno", SqlDbType.VarChar, 100);
            mobilenoparam.Value = b.mobileno;

            SqlParameter totalparam = new SqlParameter("@total", SqlDbType.VarChar, 100);
            totalparam.Value = b.total;

            SqlParameter flag = new SqlParameter("@flag", SqlDbType.VarChar, 5);
            flag.Value = b.flag;

            sqlCommand.Parameters.Add(nameparam);
            sqlCommand.Parameters.Add(idparam);
            sqlCommand.Parameters.Add(mobilenoparam);
            sqlCommand.Parameters.Add(totalparam);
            sqlCommand.Parameters.Add(flag);

            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
            da.Fill(tbl);

            for (int row = 0; row < tbl.Rows.Count; row++)
            {

                buyer b1 = new buyer();
                b1.name = tbl.Rows[row]["name"].ToString();
                b1.id = Convert.ToInt32(tbl.Rows[row]["id"]);
                buyers.Add(b1);
            }


            //con.Close();

            return buyers;


        }
    }


   
}
