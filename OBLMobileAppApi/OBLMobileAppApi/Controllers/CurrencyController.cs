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
    [RoutePrefix("api/Currency")]
    public class CurrencyController : ApiController
    {
        public static string TAG_EXCHANGE_RATE_TYPE = "CASH";
        OBLAPPEntities dbContext = new OBLAPPEntities();
        OracleDBAccessServiceLayer oOracleDBAccessServiceLayer = new OracleDBAccessServiceLayer();

        [Route("GetRateOnlyCashList")]
        [HttpGet]
        public EXCHANGERATELIST GetRateOnlyCashList()
        {
            var Result = new EXCHANGERATELIST();
            try
            {

                List<EXCHANGERATEINFO> rateInfoList = new List<EXCHANGERATEINFO>();
                rateInfoList = oOracleDBAccessServiceLayer.QueryExchangeRateList();

                Result.RATELIST = rateInfoList.Where(t => t.RATETYPE == TAG_EXCHANGE_RATE_TYPE).ToList();
                Result.STATUS = true;
                Result.REQMSG = rateInfoList.Count.ToString();
            }
            catch (Exception ex)
            {
                Result.STATUS = false;
                Result.REQMSG = (ex.InnerException != null) ? ex.Message : ex.InnerException.Message;

            }
            return Result;
        }


    }
}