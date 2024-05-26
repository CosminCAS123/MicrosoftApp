using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MicrosoftApp.Data
{
    [PrimaryKey("ID")]
    public class User
    {
        public int ID { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string FullName { get; set;} = string.Empty;

        public User() { }
        public User(string email , string password, string username , string fullname)
        {
            this.Email = email;
            this.Password = password;
            this.Username = username;
            this.FullName = fullname;
        }

    }
}
