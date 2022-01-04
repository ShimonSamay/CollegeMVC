using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeMVC.Models
{
    public class Teacher
    {
        public string FirstName;
        public string LastName;
        public string Section;
        public string Email;
        public int wage;

        public Teacher (string _firstname , string _lastname , string _section , string _email , int _wage)
        {
            this.FirstName = _firstname;
            this.LastName = _lastname;
            this.Section = _section;
            this.Email = _email;
            this.wage = _wage;
        }
        public Teacher () { }
    }
}