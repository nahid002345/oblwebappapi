using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OBLMobileAppApi.Models
{
   

    public class LOGINRESULT
    {
        public bool STATUS { get; set; }
        public string USERID { get; set; }
        public string TOKENID { get; set; }
        public string RES_MSG { get; set; }
        public string CUSTOMER_NO { get; set; }
        public string CUSTOMER_NAME { get; set; }
        public string CUSTOMER_MOBILE { get; set; }
    }
}