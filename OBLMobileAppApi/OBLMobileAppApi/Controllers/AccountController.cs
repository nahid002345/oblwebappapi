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
using OBLMobileAppApi.Class;


namespace OBLMobileAppApi.Controllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private static int TranQueryDays = 30;
        
        OBLAPPEntities dbContext = new OBLAPPEntities();
        FCUBSACCServiceLayer oFCUBSACCServiceLayer = new FCUBSACCServiceLayer();
        OracleDBAccessServiceLayer oOracleDBAccessServiceLayer = new OracleDBAccessServiceLayer();

        [Route("GetAcInfo/{UId}")]
        [HttpGet]
        public AccountInfoResult GetAcInfo(Int32 UId)
        {
            var Result = new AccountInfoResult();
            try
            {
                dbContext.Configuration.LazyLoadingEnabled = false;
                var oAclist = dbContext.APPACINFOes.Where(t => t.USERID == UId).ToList();
                if (oAclist != null)
                {
                    Result.Status = true;
                    Result.RequestMsg = oAclist.Count + " result/s found.";
                    Result.AcList = new List<APPACINFO>(oAclist);
                }
                else
                {
                    Result.Status = false;
                    Result.RequestMsg = " 0 result found.";
                    Result.AcList = null;
                }
            }
            catch (Exception ex)
            {
                Result.Status = false;
                Result.RequestMsg = (ex.InnerException != null) ? ex.Message : ex.InnerException.Message;
                Result.AcList = null;
            }
            return Result;
        }

        [Route("GetFCUBSCustomerInfo/{TId}/{CustNo}")]
        [HttpGet]
        public ACREQUEST GetFCUBSCustomerInfo(string TId, string CustNo)
        {
            var Result = new ACREQUEST();
            var userStatus = dbContext.APPUSERLOGs.FirstOrDefault(t => t.Token == TId.Trim());
            try
            {
                if (userStatus != null)
                {
                    Result = oFCUBSACCServiceLayer.QueryCustomerSummary(CustNo);
                    Result.STATUS = true;
                    Result.REQMSG = Result.ACLIST.ToList().Count.ToString();
                }
                else
                {
                    Result.STATUS = false;
                    Result.REQMSG = SystemMessage.MSG_INVALID_TOKEN;
                }

            }
            catch (Exception ex)
            {
                Result.STATUS = false;
                Result.REQMSG = (ex.InnerException != null) ? ex.Message : ex.InnerException.Message;
                
            }
            return Result;
        }

        [Route("GetFCUBSAcInfo/{TId}/{CustNo}/{AcNo}")]
        [HttpGet]
        public ACREQUEST GetFCUBSAcInfo(string TId,string CustNo, string AcNo)
        {
            var Result = new ACREQUEST();
            var userStatus = dbContext.APPUSERLOGs.FirstOrDefault(t => t.Token == TId.Trim());
            try
            {
                if (userStatus != null)
                {
                    Result = oFCUBSACCServiceLayer.QueryAccSummary(CustNo,AcNo);
                    Result.STATUS = true;
                    Result.REQMSG = Result.ACLIST.ToList().Count.ToString();
                }
                else
                {
                    Result.STATUS = false;
                    Result.REQMSG = SystemMessage.MSG_INVALID_TOKEN;
                }

            }
            catch (Exception ex)
            {
                Result.STATUS = false;
                Result.REQMSG = (ex.InnerException != null) ? ex.Message : ex.InnerException.Message;

            }
            return Result;
        }

        [Route("GetFCUBSAcTranList/{TId}/{AcNo}")]
        [HttpGet]
        public TRANSACTIONREQUEST GetFCUBSAcTranList(string TId,string AcNo)
        {
            var Result = new TRANSACTIONREQUEST();
            var userStatus = dbContext.APPUSERLOGs.FirstOrDefault(t => t.Token == TId.Trim());
            try
            {
                if (userStatus != null)
                {
                    Result.TRANLIST = oOracleDBAccessServiceLayer.QueryDaysTransactionList(AcNo, TranQueryDays);
                    Result.STATUS = true;
                    Result.REQMSG = Result.TRANLIST.ToList().Count.ToString();
                }
                else
                {
                    Result.STATUS = false;
                    Result.REQMSG = SystemMessage.MSG_INVALID_TOKEN;
                }
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