using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OBLMobileAppApi.Entity;
using System.Text;
using OBLMobileAppApi.Models;

namespace OBLMobileAppApi.Controllers
{
    [RoutePrefix("api/Loan")]
    public class LoanController : ApiController
    {
        
        OBLAPPEntities dbContext = new OBLAPPEntities();
        // GET api/values
        private string RandomString(int Size)
        {
            Random random = new Random();
            string input = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < Size; i++)
            {
                ch = input[random.Next(0, input.Length)];
                builder.Append(ch);
            }
            return builder.ToString();
        }
        

    }
}