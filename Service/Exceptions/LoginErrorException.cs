﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Exceptions
{
    public class LoginErrorException : Exception
    {
        public LoginErrorException() : base("LOGINERROR")
        {

        }
    }
}
