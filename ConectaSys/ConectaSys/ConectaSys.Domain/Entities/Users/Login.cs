using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Data;
using System.Xml.Linq;
namespace ConectaSys.ConectaSys.Domain.Entities.Users
{
    [Table("Users")]
    public class Login
    {
        [Column("Email")]
        public string Email { get; set; }

        [Column("Password")]
        public string Password { get; set; }


        public Login(string email, string password)
        {
            Email = email;
            Password = password;


        }

    }
}

