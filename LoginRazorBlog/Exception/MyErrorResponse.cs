using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginRazorBlog.Exception
{
    public class MyErrorResponse
    {
        public string Type { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }

        public MyErrorResponse(System.Exception ex)
        {
            Type = ex.GetType().Name;
            Message = ex.Message;
            StackTrace = ex.ToString();
        }
        //throw new HttpStatusCodeException(HttpStatusCode.NotFound, "User not found");
    }
}
