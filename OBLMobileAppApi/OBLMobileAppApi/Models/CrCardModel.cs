using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OBLMobileAppApi.Entity;
using OBLMobileAppApi.FCUBSAccService;

namespace OBLMobileAppApi.Models
{

    public class CARDINFOLIST
    {
        public string CARDNO { get; set; }
        public string CARDHOLDERNAME { get; set; }
        public string CARDOUTSTANDING { get; set; }
    }

    public class CARDLISTREQ
    {
        public bool STATUS { get; set; }
        public string REQMSG { get; set; }
        public List<CARDINFOLIST> CARDLIST { get; set; }
    }

    public class CARDDETAIL
    {
       public string  CARDNO{ get; set; }
       public string MASKCARDNO{ get; set; }
       public string CLIENTNAME{ get; set; }
       public string ADDRESS{ get; set; }
       public string PAYMENTDATE{ get; set; }      
       public string BILLINGDATE{ get; set; }

//For BDT
       public string BDTCURRENCY { get; set; }
       public string BDTCREDITLIMIT { get; set; }
       public string BDTAVAILABLECRLIMIT { get; set; }
       public string BDTCASHLIMIT { get; set; }
       public string BDTAVAILABLECASHLIMIT { get; set; }
       public string BDTMINAMNTDUE { get; set; }
       public string BDTPREVBALANCE { get; set; }
       public string BDTCURRBALANCE { get; set; }
       public string BDTTOTALPURCHASE { get; set; }
       public string BDTTOTALINTR { get; set; }
       public string BDTPAYMENTS { get; set; }
       public string BDTCREDITS { get; set; }
       public string BDTCASHADV { get; set; }

       //For USD
       public string USDCURRENCY { get; set; }
       public string USDCREDITLIMIT { get; set; }
       public string USDAVAILABLECRLIMIT { get; set; }
       public string USDCASHLIMIT { get; set; }
       public string USDAVAILABLECASHLIMIT { get; set; }
       public string USDMINAMNTDUE { get; set; }
       public string USDPREVBALANCE { get; set; }
       public string USDCURRBALANCE { get; set; }
       public string USDTOTALPURCHASE { get; set; }
       public string USDTOTALINTR { get; set; }
       public string USDPAYMENTS { get; set; }
       public string USDCREDITS { get; set; }
       public string USDCASHADV { get; set; }
       
    }

    public class CARDDETAILREQ
    {
        public bool STATUS { get; set; }
        
        public string REQMSG { get; set; }
        public CARDDETAIL CARDINFO { get; set; }
    }
}