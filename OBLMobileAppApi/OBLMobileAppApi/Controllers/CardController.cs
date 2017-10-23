using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OBLMobileAppApi.Entity;
using System.Text;
using System.Runtime.Serialization.Json;
using OBLMobileAppApi.Models;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using OBLMobileAppApi.WebService;
using OBLMobileAppApi.FCUBSAccService;
using System.Data;


namespace OBLMobileAppApi.Controllers
{
    [RoutePrefix("api/Card")]
    public class CardController : ApiController
    {
        private static int TranQueryDays = 30;

        OBLAPPEntities dbContext = new OBLAPPEntities();


        [Route("GetCardList/{UserId}")]
        [HttpGet]
        public CARDLISTREQ GetCardList(Int32 UserId)
        {
            var Result = new CARDLISTREQ();
            try
            {
                dbContext.Configuration.ProxyCreationEnabled = false;
                var cardList = dbContext.APPCREDITCARDINFOes.Where(t => t.USERID == UserId && t.ISACTIVE == true).ToList();
                List<CARDINFOLIST> cardInfoList = new List<CARDINFOLIST>();
                if (cardList != null && cardList.Count > 0)
                {
                    foreach (APPCREDITCARDINFO oCard in cardList)   
                    {
                        var cardDBInfo = dbContext.CARDDETAILINFOes.FirstOrDefault(t => t.CARDNO == oCard.CCARDNO);
                        if (cardDBInfo != null)
                        {
                            CARDINFOLIST cInfo = new CARDINFOLIST();
                            cInfo.CARDNO = cardDBInfo.CARDNO;
                            cInfo.CARDHOLDERNAME = cardDBInfo.CLIENTNAME;
                            cInfo.CARDOUTSTANDING = cardDBInfo.CURRBALANCE.ToString();
                            cardInfoList.Add(cInfo);
                        }
                    }
                }
                Result.CARDLIST = cardInfoList;
                Result.STATUS = true;
                Result.REQMSG = cardList.Count.ToString();
            }
            catch (Exception ex)
            {
                Result.STATUS = false;
                Result.REQMSG = (ex.InnerException != null) ? ex.Message : ex.InnerException.Message;

            }
            return Result;
        }

        [Route("GetCardInfo/{CardNo}")]
        [HttpGet]
        public CARDDETAILREQ GetCardInfo(string CardNo)
        {
            var Result = new CARDDETAILREQ();
            try
            {
                var cardInfo = dbContext.CARDDETAILINFOes.Where(t => t.CARDNO == CardNo).ToList();
                CARDDETAIL cardDetail = new CARDDETAIL();
                if (cardInfo != null && cardInfo.Count > 0)
                {
                    cardDetail.CARDNO = cardInfo[0].CARDNO;
                    cardDetail.MASKCARDNO = cardInfo[0].MASKCARDNO;
                    cardDetail.CLIENTNAME = cardInfo[0].CLIENTNAME;
                    cardDetail.ADDRESS = cardInfo[0].ADDRESS;
                    cardDetail.PAYMENTDATE = cardInfo[0].PAYMENTDATE;
                    cardDetail.BILLINGDATE = cardInfo[0].BILLINGDATE.ToString("dd/MM/yyyy");

                    var cardBdt = cardInfo.FirstOrDefault(t => t.CURRENCY == "BDT");
                    var cardUsd = cardInfo.FirstOrDefault(t => t.CURRENCY == "USD");
                    if (cardBdt != null)
                    {
                        cardDetail.BDTCURRENCY = cardBdt.CURRENCY;
                        cardDetail.BDTCREDITLIMIT = cardBdt.CREDITLIMIT.ToString();
                        cardDetail.BDTAVAILABLECRLIMIT = cardBdt.AVAILABLECRLIMIT.ToString();
                        cardDetail.BDTCASHLIMIT = cardBdt.CASHLIMIT.ToString();
                        cardDetail.BDTAVAILABLECASHLIMIT = cardBdt.AVAILABLECASHLIMIT.ToString();
                        cardDetail.BDTMINAMNTDUE = cardBdt.MINAMNTDUE.ToString();
                        cardDetail.BDTPREVBALANCE = cardBdt.PREVBALANCE.ToString();
                        cardDetail.BDTCURRBALANCE = cardBdt.CURRBALANCE.ToString();
                        cardDetail.BDTTOTALPURCHASE = cardBdt.TOTALPURCHASE.ToString();
                        cardDetail.BDTTOTALINTR = cardBdt.TOTALINTR.ToString();
                        cardDetail.BDTPAYMENTS = cardBdt.PAYMENTS.ToString();
                        cardDetail.BDTCREDITS = cardBdt.CREDITS.ToString();
                        cardDetail.BDTCASHADV = cardBdt.CASHADV.ToString();
                    }
                    else
                    {
                        cardDetail.BDTCURRENCY = string.Empty;
                        cardDetail.BDTCREDITLIMIT = string.Empty;
                        cardDetail.BDTAVAILABLECRLIMIT = string.Empty;
                        cardDetail.BDTCASHLIMIT = string.Empty;
                        cardDetail.BDTAVAILABLECASHLIMIT = string.Empty;
                        cardDetail.BDTMINAMNTDUE = string.Empty;
                        cardDetail.BDTPREVBALANCE = string.Empty;
                        cardDetail.BDTCURRBALANCE = string.Empty;
                        cardDetail.BDTTOTALPURCHASE = string.Empty;
                        cardDetail.BDTTOTALINTR = string.Empty;
                        cardDetail.BDTPAYMENTS = string.Empty;
                        cardDetail.BDTCREDITS = string.Empty;
                        cardDetail.BDTCASHADV = string.Empty;
                    }

                    if (cardUsd != null)
                    {
                        cardDetail.USDCURRENCY = cardUsd.CURRENCY;
                        cardDetail.USDCREDITLIMIT = cardUsd.CREDITLIMIT.ToString();
                        cardDetail.USDAVAILABLECRLIMIT = cardUsd.AVAILABLECRLIMIT.ToString();
                        cardDetail.USDCASHLIMIT = cardUsd.CASHLIMIT.ToString();
                        cardDetail.USDAVAILABLECASHLIMIT = cardUsd.AVAILABLECASHLIMIT.ToString();
                        cardDetail.USDMINAMNTDUE = cardUsd.MINAMNTDUE.ToString();
                        cardDetail.USDPREVBALANCE = cardUsd.PREVBALANCE.ToString();
                        cardDetail.USDCURRBALANCE = cardUsd.CURRBALANCE.ToString();
                        cardDetail.USDTOTALPURCHASE = cardUsd.TOTALPURCHASE.ToString();
                        cardDetail.USDTOTALINTR = cardUsd.TOTALINTR.ToString();
                        cardDetail.USDPAYMENTS = cardUsd.PAYMENTS.ToString();
                        cardDetail.USDCREDITS = cardUsd.CREDITS.ToString();
                        cardDetail.USDCASHADV = cardUsd.CASHADV.ToString();
                    }
                    else
                    {
                        cardDetail.USDCURRENCY = string.Empty;
                        cardDetail.USDCREDITLIMIT = string.Empty;
                        cardDetail.USDAVAILABLECRLIMIT = string.Empty;
                        cardDetail.USDCASHLIMIT = string.Empty;
                        cardDetail.USDAVAILABLECASHLIMIT = string.Empty;
                        cardDetail.USDMINAMNTDUE = string.Empty;
                        cardDetail.USDPREVBALANCE = string.Empty;
                        cardDetail.USDCURRBALANCE = string.Empty;
                        cardDetail.USDTOTALPURCHASE = string.Empty;
                        cardDetail.USDTOTALINTR = string.Empty;
                        cardDetail.USDPAYMENTS = string.Empty;
                        cardDetail.USDCREDITS = string.Empty;
                        cardDetail.USDCASHADV = string.Empty;
                    }

                }
                Result.CARDINFO = cardDetail;
                Result.STATUS = true;
                Result.REQMSG = cardInfo.Count.ToString();
            }
            catch (Exception ex)
            {
                Result.STATUS = false;
                Result.REQMSG = (ex.InnerException != null) ? ex.Message : ex.InnerException.Message;

            }
            return Result;
        }
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

    }
}