using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OBLMobileAppApi.Entity;
using OBLMobileAppApi.FCUBSAccService;

namespace OBLMobileAppApi.Models
{
    public class AccountInfoResult
    {
        public bool Status { get; set; }
        public string RequestMsg { get; set; }
        public List<APPACINFO> AcList { get; set; }
    }

    public class USERACINFO
    {
        public string ACNO {get; set;}
        public string CUSTNO {get; set;}
        public string ACNAME {get; set;}
        public string ACTYPE {get; set;}
        public decimal ACAVAILBAL {get; set;}
        public decimal ACCURRENTBAL {get; set;}
        
    }

    public class ACTRANSACTION
    {
        public string ACNO { get; set; }
        public string ACCURRENCY { get; set; }
        public string TRANDATE { get; set; }
        public DateTime TRANDATETIME { get; set; }
        public string TRANREFNO { get; set; }
        public string TRANDES { get; set; }
        public string TRANTYPE { get; set; }
        public decimal TRANAMOUNT { get; set; }
        public decimal TRANACBAL { get; set; }
        public long ACENTRYSERIAL { get; set; }

    }

    public class ACREQUEST
    {
        public bool STATUS { get; set; }
        public string CUSTOMERNO { get; set; }
        public string REQMSG { get; set; }
        public List<USERACINFO> ACLIST { get; set; }
    }


    public class TRANSACTIONREQUEST
    {
        public bool STATUS { get; set; }
        public string ACNO { get; set; }
        public string REQMSG { get; set; }
        public List<ACTRANSACTION> TRANLIST { get; set; }
    }
}