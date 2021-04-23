﻿using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class EmployeeController : ApiController
    {
        // GET: Employee
        public HttpResponseMessage Get()
        {
            string query = "select EmployeeId,EmployeeName,Department,DateofJoining,PhotoFileName from dbo.Employee";
            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeAppDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }
            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        public string Post(Employee emp)
        {
            try
            {
                string query = @"insert into dbo.Employee values
('" + emp.EmployeeName + @"','" + emp.Department + @"','" + emp.DateOfJoining + @"','" + emp.PhotoFileName + @"')";
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
        public string Put(Employee emp)
        {
            try
            {
                string query = @"update   dbo.Employee set 
                          EmployeeName='" + emp.EmployeeName + @"'
                          ,Department='" + emp.Department + @"'
                          ,DateofJoining='" + emp.DateOfJoining + @"'
                          ,PhotoFileName='" + emp.PhotoFileName + @"'
                          Where EmployeeId=" + emp.EmployeeId + @"";
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

        [Route("api/Employee/SaveFile")]
        public string SaveFile()
        {
            try
            {
                var HttpRequest = HttpContext.Current.Request;
                var PostedFile = HttpRequest.Files[0];
                string FileName = PostedFile.FileName;
                var PhysicalPath = HttpContext.Current.Server.MapPath("~/Photos/" + FileName);
                PostedFile.SaveAs(PhysicalPath);
                return FileName;
            }
            catch (Exception Ex)
            {
                return "Save FIle Failed";
            }
        }

        public string Delete(int id)
        {
            try
            {
                string query = @"delete from   dbo.Employee where EmployeeId=" + id + @"";
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