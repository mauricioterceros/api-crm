using System;
using System.Collections.Generic;
using System.Text;

namespace BackingServices
{
    public interface IUsersDB : IDBManager
    {
        public bool UserExists(string user, string pass);

    }
}
