using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DBapp.Models;
using System.Data.SqlClient;

namespace DBapp.Controllers
{
    public class accountController : Controller
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        // GET: account
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        void connectionString()
        {
            con.ConnectionString = "Data Source=DESKTOP-2NJDVQ3;Initial Catalog=MYDBproject;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";
        }
        [HttpPost]
        public ActionResult Verify(account acc)
        {
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "Select * from login where Username='" + acc.Name + "' and Password='" + acc.password + "'";

            dr = com.ExecuteReader();
            if (dr.Read())
            {
                return View("../Home/Index");
            }
            else
            {
                con.Close();
                return View("Error");
            }



        }
    }
}