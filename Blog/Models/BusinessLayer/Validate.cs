using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Model;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;

namespace Blog.Models.BusinessLayer
{
    public class Validate
    {
        public static IConfiguration Configuration { get; set; }

        //To Read ConnectionString from appsettings.json file  
        private static string GetConnectionString()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            string connectionString = Configuration["ConnectionStrings:BlogDB"];

            return connectionString;

        }


        string connectionString = GetConnectionString();

        //public string ValidateLogin(User user)
        //{
        //    using (SqlConnection con = new SqlConnection(connectionString))
        //    {
        //        SqlCommand cmd = new SqlCommand("spValidateLogin", con);
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        cmd.Parameters.AddWithValue("@LoginID", user.Username);
        //        cmd.Parameters.AddWithValue("@LoginPassword", user.Salt);

        //        con.Open();
        //        string result = cmd.ExecuteScalar().ToString();
        //        con.Close();

        //        return result;
        //    }
        //}

        public static List<SelectListItem> PopulateCategories()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            string constr = GetConnectionString();
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = " SELECT Name,Category_ID FROM Category";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            items.Add(new SelectListItem
                            {
                                Text = sdr["Name"].ToString(),
                                Value = sdr["Category_ID"].ToString()
                            });
                        }
                    }
                    con.Close();
                }
            }

            return items;
        }
    }
}
