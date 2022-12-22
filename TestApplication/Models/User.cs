using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TestApplication.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Login { get; set; }
        public string PassWord { get; set; }

        public User(int id, string fullName, string login, string passWord)
        {
            Id = id;
            FullName = fullName;
            Login = login;
            PassWord = passWord;
        }
    }
}
