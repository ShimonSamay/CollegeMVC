using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeMVC.Models
{
    public class Student
    {
        public string FirstName;
        public string LastName;
        public DateTime BirthDate;
        public string Email;
        public int TeachYear;

        public Student (string _firstname , string _lastname , DateTime _birthdate , string _email , int _teachyear)
        {
            this.FirstName = _firstname;    
            this.LastName = _lastname;
            this.BirthDate = _birthdate;
            this.Email = _email;
            this.TeachYear = _teachyear;    
        }
        public Student () { }

        
    }
}