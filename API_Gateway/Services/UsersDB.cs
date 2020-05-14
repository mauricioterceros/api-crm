using System;
using System.Collections.Generic;
using System.Text;
using BackingServices.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.IO;


namespace BackingServices
{
    public class UsersDB : IUsersDB
    {
        private readonly IConfiguration _configuration;

        private string _dbPath;
        private List<User> _userList;
        private DBContext _dbContext;

        public UsersDB(IConfiguration configuration)
        {
            _configuration = configuration;

            InitDBContext();
        }

        public void InitDBContext()
        {

            _dbPath = _configuration.GetSection("Database").GetSection("connectionString").Value;

            _dbContext = JsonConvert.DeserializeObject<DBContext>(File.ReadAllText(_dbPath));

            _userList = _dbContext.Users;

        }

        public bool UserExists(string user, string pass)
        {
            User userFound = _userList.Find(user1 => user1.Username == user && user1.Password == pass);
            if (userFound != null)
                return true;

            else
                return false;

        }

    }
}
