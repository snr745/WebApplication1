using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Http;
using System.Net.Http;
using System.Net;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class DepartMentController:ApiController
    {
        public HttpResponseMessage Get()
        {
            string query = "select DepartmentId,DepartmentName from dbo.Department";
            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd=new SqlCommand(query,con))
                using (var da=new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }
            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        public string Post(Department dep)
        {
            try
            {
                string query = @"insert into dbo.Department value
('" + dep.DepartMentName + @"')
";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "Added Succesfully!!!";
            }
            catch (Exception ex)
            {
                return "Failed To Add!!!";
            }
        }
        public string Put(Department dep)
        {
            try
            {
                string query = @"update   dbo.Department set DepartmentName='" + dep.DepartMentName + @"'
 Where DepartMentId="+dep.DepartmentId+@"";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "Updated Succesfully!!!";
            }
            catch (Exception ex)
            {
                return "Update Failed!!!";
            }
        }

        public string Delete(int id)
        {
            try
            {
                string query = @"delete from   dbo.Department where DepartmentId="+id+@"";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "Deleted Succesfully!!!";
            }
            catch (Exception ex)
            {
                return "Delete  Failed!!!";
            }
        }
    }
}