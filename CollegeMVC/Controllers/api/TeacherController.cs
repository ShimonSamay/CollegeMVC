using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CollegeMVC.Models;

namespace CollegeMVC.Controllers.api
{
    public class TeacherController : ApiController
    {
        string connectionString = "Data Source=SHIMONSAMAY;Initial Catalog=CollegeDB;Integrated Security=True;Pooling=False";

        public IHttpActionResult Get()
        { 
            try
            {
                List<Teacher> teachersList = getTechersData(connectionString);
                return Ok(new { teachersList });
            }
            catch(SqlException sqlErr)
            {
                return BadRequest(sqlErr.Message);
            }
            catch (Exception x)
            {
                return BadRequest(x.Message);
            }

        }

        public IHttpActionResult Get(int id)
        {
            try
            {
                Teacher teacher = getTeacherData(connectionString, id);
                return Ok(new { teacher });
            }
            catch (SqlException sqlErr)
            {
                return BadRequest(sqlErr.Message);
            }
            catch (Exception x)
            {
                return BadRequest(x.Message);
            }

        }

       [HttpPost]
        public IHttpActionResult Post([FromBody]Teacher value)
        {
            try
            {
                createTeacher(connectionString, value);
                List<Teacher> list = getTechersData(connectionString);
                return Ok(new { list });
            }
            catch (SqlException sqlErr)
            {
                return BadRequest(sqlErr.Message);
            }
            catch(Exception x)
            {
                return BadRequest(x.Message);
            }
        }

       [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]Teacher value)
        {
            try
            {
                updateTeacher(connectionString, value, id);
                List<Teacher> list = getTechersData(connectionString);
                return Ok(new { list });
            }
            catch (SqlException sqlErr)
            {
                return BadRequest(sqlErr.Message);
            }
            catch (Exception x)
            {
                return BadRequest(x.Message);
            }
        }

       [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                delteTeacher(connectionString, id);
                List<Teacher> teachersList = getTechersData(connectionString);
                return Ok(new { teachersList });
            }
            catch (SqlException sqlErr)
            {
                return BadRequest(sqlErr.Message);
            }
            catch (Exception x)
            {
                return BadRequest(x.Message);
            }
        }
















        private static List<Teacher> getTechersData(string connection)
        { 
                List<Teacher> teachersList = new List<Teacher>();
                using (SqlConnection DBconnection = new SqlConnection(connection))
                {
                    DBconnection.Open();
                    string query = "SELECT * FROM Teachers";
                    SqlCommand command = new SqlCommand(query, DBconnection);
                    SqlDataReader data = command.ExecuteReader();
                    if (data.HasRows)
                    {
                        while (data.Read())
                        {
                            teachersList.Add(new Teacher(data.GetString(1),
                                                           data.GetString(2),
                                                           data.GetString(3),
                                                           data.GetString(4),
                                                           data.GetInt32(5)));
                        }
                        return teachersList;
                    }
                    return teachersList;
                }
        }

        private static Teacher getTeacherData (string connection , int id)
        {
            using (SqlConnection DBconnection = new SqlConnection( connection))
            {
                DBconnection.Open ();
                string query = $@"SELECT * FROM Teachers WHERE Id = {id}";
                SqlCommand command = new SqlCommand(query,DBconnection);
                SqlDataReader DATA = command.ExecuteReader();
                if (DATA.HasRows)
                {
                    while (DATA.Read())
                    {
                        Teacher someTeacher = new Teacher(DATA.GetString(1),
                                                            DATA.GetString(2),
                                                            DATA.GetString(3),
                                                            DATA.GetString(4),
                                                            DATA.GetInt32(5));
                        return someTeacher;
                    }
                }
                DBconnection.Close();
                return new Teacher();
            }
        }

        private static void createTeacher (string connection , Teacher teacher)
        {
            using(SqlConnection DBconnection = new SqlConnection(connection))
            {
                DBconnection.Open();
                string query = @"INSERT INTO Teachers (FirstName , LastName , Section , Email , Wage) " +
                               $"VALUES('{teacher.FirstName}' , '{teacher.LastName}' , '{teacher.Section}', '{teacher.Email}' , '{teacher.wage}')";
                SqlCommand command = new SqlCommand(query, DBconnection);
                command.ExecuteNonQuery();
                DBconnection.Close ();
            }
        }

        private static void updateTeacher (string connection, Teacher teacher , int id)
        {
            using (SqlConnection DBconnection = new SqlConnection( connection ))
            {
                DBconnection.Open ();
                string query = $@"UPDATE Teachers
                                SET FirstName = '{teacher.FirstName}' , 
                                LastName = '{teacher.LastName}' , 
                                Section = '{teacher.Section}', 
                                Email = '{teacher.Email}' , 
                                Wage = {teacher.wage} 
                                WHERE Id = {id} ";

                SqlCommand command = new SqlCommand (query, DBconnection);
                command.ExecuteNonQuery ();
                DBconnection.Close();
            }
        }

        private static void delteTeacher (string connection , int id)
        {
            using( SqlConnection DBconnection = new SqlConnection(connection))
            {
                DBconnection.Open();
                string query = $@"DELETE FROM Teachers WHERE Id = {id}";
                SqlCommand command = new SqlCommand(query, DBconnection);
                command.ExecuteNonQuery ();
                DBconnection.Close();
            }
        }

    }


}
