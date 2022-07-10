using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projekat_SmartGrid.Models
{
    public enum UserType { ADMIN,DELIVERER,USER}
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string DateOfBirth { get; set; }
        public string Address { get; set; }
        public UserType UserType { get; set; }
        public string Image { get; set; }
    }
}