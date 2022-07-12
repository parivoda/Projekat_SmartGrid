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
        public bool Blocked { get; set; }

        public List<Order> CurrentOrder { get; set; }

        public User()
        {

        }

        public User(int id,string username,string email,string password,string name,string lastname,string dateOfBirth,string address,UserType userType,string image,bool blocked)
        {
            Id = id;
            Username = username;
            Email = email;
            Password = password;
            Name = name;
            Lastname = lastname;
            DateOfBirth = dateOfBirth;
            Address = address;
            UserType = userType;
            Image = image;
            Blocked = blocked;

            CurrentOrder = new List<Order>();
        }
        public User(int id, string username, string email, string password, string name, string lastname, string dateOfBirth, string address, UserType userType, bool blocked)
        {
            Id = id;
            Username = username;
            Email = email;
            Password = password;
            Name = name;
            Lastname = lastname;
            DateOfBirth = dateOfBirth;
            Address = address;
            UserType = userType;
            Blocked = blocked;

            CurrentOrder = new List<Order>();
        }
        public User(string username, string email, string password, string name, string lastname, string dateOfBirth, string address, UserType userType, bool blocked)
        {
            Username = username;
            Email = email;
            Password = password;
            Name = name;
            Lastname = lastname;
            DateOfBirth = dateOfBirth;
            Address = address;
            UserType = userType;
            Blocked = blocked;

            CurrentOrder = new List<Order>();
        }
    }
}