﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LoginRazorBlog.Exception
{
    public class HttpStatusException : System.Exception
    {
        public HttpStatusCode Status { get; private set; }

        public HttpStatusException(HttpStatusCode status, string msg) : base(msg)
        {
            Status = status;
        }

        //throw new HttpStatusCodeException(HttpStatusCode.NotFound, "User not found");
    }
}
