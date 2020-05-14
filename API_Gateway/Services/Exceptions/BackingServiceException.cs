using System;
using System.Collections.Generic;
using System.Text;

namespace BackingServices.Exceptions
{
    public class BackingServiceException : Exception
    {
        public int Code { get { return 502; } }

        public BackingServiceException(string message) : base(message) 
        {

        }
    }
    public class BadRequestException : Exception
    {
        public int Code { get { return 500; } }

        public BadRequestException(string message) : base(message)
        {

        }
    }
}
