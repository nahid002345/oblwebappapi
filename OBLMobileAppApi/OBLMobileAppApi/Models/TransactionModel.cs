using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OBLMobileAppApi.Entity;

namespace OBLMobileAppApi.Models
{
    public class AccountInfoResult
    {
        public bool Status { get; set; }
        public string RequestMsg { get; set; }
        public List<APPACINFO> AcList { get; set; }
    }
}