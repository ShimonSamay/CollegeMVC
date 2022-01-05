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
    public class StudentController : ApiController
    {
        string connectionString = "Data Source=SHIMONSAMAY;Initial Catalog=CollegeDB;Integrated Security=True;Pooling=False";
        
        public IHttpActionResult Get()
        {
            
            List<Student> studentList = getStudentsData(connectionString);
            return Ok (new{studentList});
        }

        
        public IHttpActionResult Get(int id)
        {
            Student student = getStudentData(connectionString, id);
            return Ok (new {student});
        }

       [HttpPost]
        public IHttpActionResult Post([FromBody] Student anyStudent)
        {
            addStudentTotable(connectionString, anyStudent);
            List<Student> studentList = getStudentsData(connectionString);
            return Ok(new {studentList});
        }

        
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody] Student anyStudent)
        {
            updateStudent(connectionString, anyStudent, id);
            List<Student> studentList = getStudentsData(connectionString);
            return Ok( new {studentList} );
        }

        
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            deleteStudent(connectionString, id);
            List<Student> studentList = getStudentsData(connectionString);
            return Ok(new { studentList });
        }











        public static List<Student> getStudentsData(string connection)
        {
            List<Student> studentsList = new List<Student>();
            using(SqlConnection DBconnection = new SqlConnection(connection))
            {
                DBconnection.Open();
                string query = "SELECT * FROM Students";
                SqlCommand command = new SqlCommand(query, DBconnection);
                SqlDataReader DATA = command.ExecuteReader();
                if (DATA.HasRows)
                {
                    while (DATA.Read())
                    {
                        studentsList.Add(new Student(DATA.GetString(1),
                                                             DATA.GetString(2),
                                                             DATA.GetDateTime(3),
                                                             DATA.GetString(4),
                                                             DATA.GetInt32(5)));
                    }
                    return studentsList;
                }
                DBconnection.Close();
                return studentsList;
            }
            
        }
        public static Student getStudentData (string connection , int id )
        {

            using (SqlConnection DBconnection = new SqlConnection( connection))
            {
                DBconnection.Open ();
                string query = $@"SELECT * FROM Students WHERE Id = {id}";
                SqlCommand command = new SqlCommand(query,DBconnection);
                SqlDataReader data = command.ExecuteReader();   
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        Student student = new Student(data.GetString(1)
                                             , data.GetString(2)
                                             , data.GetDateTime(3)
                                             , data.GetString(4)
                                             , data.GetInt32(5));
                        return student;
                    }
             
                }
                return new Student();



            }


        }

        public static void addStudentTotable (string connection , Student somestudent)
        {
            using (SqlConnection DBconnection = new SqlConnection(connection))
            {
                DBconnection.Open();
                string query = $@"INSERT INTO Students (FirstName , LastName , BirthDate , Email ,TeachYear )
                               Values ('{somestudent.FirstName}' , '{somestudent.LastName}' , '{somestudent.BirthDate}' , '{somestudent.Email}' , {somestudent.TeachYear})";
                SqlCommand command = new SqlCommand(query, DBconnection);
                command.ExecuteNonQuery();
                DBconnection.Close();
            }
        }
        
        public static void updateStudent (string connection, Student student , int id)
        {
            using(SqlConnection DBconnection = new SqlConnection(connection))
            {
                DBconnection.Open();
                string query = $@"UPDATE Students 
                                SET FirstName = '{student.FirstName}' , 
                                LastName = '{student.LastName}' , 
                                BirthDate = '{student.BirthDate}', 
                                Email = '{student.Email}' , 
                                TeachYear = {student.TeachYear} 
                                WHERE Id = {id} " ;

                SqlCommand command = new SqlCommand (query, DBconnection);  
                command.ExecuteNonQuery ();
                DBconnection.Close();
            }
        }

        public static void deleteStudent (string connection ,  int id)
        {
            using (SqlConnection DBconnection = new SqlConnection(connection))
            {
                DBconnection.Open ();
                string query = $@"DELETE FROM Students WHERE Id = {id} ";
                SqlCommand command = new SqlCommand (query,DBconnection);
                command.ExecuteNonQuery();
                DBconnection.Close ();


            }
        }

    }

   
}
