using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OBLMobileAppApi.Entity;
using OBLMobileAppApi.FCUBSAccService;

namespace OBLMobileAppApi.Models
{
    public class EXCHANGERATEINFO
    {
        public string CURRENCY { get; set; }
        public string RATETYPE { get; set; }
        public string RATEDATE { get; set; }
        public decimal BUYRATE { get; set; }
        public decimal SALERATE { get; set; }
    }

    public class EXCHANGERATELIST
    {
        public bool STATUS { get; set; }
        public string REQMSG { get; set; }
        public List<EXCHANGERATEINFO> RATELIST { get; set; }
    }

}