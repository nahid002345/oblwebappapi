using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OBLMobileAppApi.Entity;
using System.Text;
using OBLMobileAppApi.Models;
using OBLMobileAppApi.Class;
using System.Collections.Specialized;
using Newtonsoft.Json;
using System.IO;
using System.Web;

namespace OBLMobileAppApi.Controllers
{
    [RoutePrefix("api/Login")]
    public class ValuesController : ApiController
    {

        OBLAPPEntities dbContext = new OBLAPPEntities();
        public static string strAppEmailSubject = "OBL Mobile App Token Request";
        // GET api/values
        private void SendSms(string mobileNo, string oTP)
        {
            CustomHttpRequest oRequest = new CustomHttpRequest();
            var url = "http://10.156.0.105/OBLSMS/api/sendsms";
            var paraCollection = new NameValueCollection();

            paraCollection.Add("SendTo", mobileNo);
            paraCollection.Add("Msg", "You have sent a request to Log In OBL Mobile App.Your OTP is : " + oTP);
            paraCollection.Add("ApiId", "$01$02#03");
            paraCollection.Add("Password", "0BL$App#0TP@1");

            oRequest.PostJSONRequest(url, paraCollection);
        }


        [Route("check/{DId}/{MNo}/{UId}/{Pwd}")]
        [HttpGet]
        public LOGINRESULT CheckLogIn(string DId, string MNo, string UId, string Pwd)
        {
            var Result = new LOGINRESULT();

            try
            {
                var oLogin = dbContext.APPUSERs.FirstOrDefault(t => t.USERID == UId && t.PASSWORD == Pwd && t.ISACTIVE == true);
                if (oLogin != null)
                {
                    Result.STATUS = true;
                    APPUSERLOG oAPPUSERLOG = new APPUSERLOG();
                    oAPPUSERLOG.DEVICEID = DId;
                    oAPPUSERLOG.MOBILENO = MNo;
                    oAPPUSERLOG.USERID = oLogin.ID;
                    oAPPUSERLOG.OTP = RandomString.Generate(5,5,true);
                    oAPPUSERLOG.Token = RandomString.Generate(5, 10);
                    oAPPUSERLOG.LOGTIME = DateTime.Now;
                    dbContext.APPUSERLOGs.Add(oAPPUSERLOG);
                    dbContext.SaveChanges();
                    if(!string.IsNullOrEmpty(oLogin.MOBILENO.Trim()))
                        SendSms(oLogin.MOBILENO, oAPPUSERLOG.OTP);
                    if(!string.IsNullOrEmpty(oLogin.EMAIL.Trim()))
                    {
                        
                        string body = string.Empty;
                        using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Content/TokenOTPEmailBody.html")))
                        {
                            body = reader.ReadToEnd();
                        }
                        body = body.Replace("{UserID}", UId);
                        body = body.Replace("{LogTime}", oAPPUSERLOG.LOGTIME.Value.ToString("dd-MM-yyyy hh:mm:ss tt"));
                        body = body.Replace("{OTP}", oAPPUSERLOG.OTP);
                        SendEmail.SendMail(strAppEmailSubject + " (" + oAPPUSERLOG.LOGTIME.Value.ToString("dd-MM-yyyy hh:mm:ss tt") + ")", oLogin.EMAIL, body);
                    }

                    Result.TOKENID = oAPPUSERLOG.Token;
                    Result.RES_MSG = "success";
                    Result.USERID = oLogin.ID.ToString();

                    Result.CUSTOMER_NAME = oLogin.USERID.ToString();
                    Result.CUSTOMER_MOBILE = oLogin.MOBILENO.ToString();

                }
                else
                {
                    Result.STATUS = false;
                    Result.TOKENID = string.Empty;
                    Result.RES_MSG = "unsuccess";
                }
            }
            catch (Exception ex)
            {
                Result.STATUS = false;
                Result.RES_MSG = (ex.InnerException != null) ? ex.Message : ex.InnerException.Message;
            }
            return Result;
        }

        [Route("check/{DId}/{UId}/{Pwd}")]
        [HttpGet]
        public LOGINRESULT CheckLogIn(string DId, string UId, string Pwd)
        {
            var Result = new LOGINRESULT();

            try
            {
                var oLogin = dbContext.APPUSERs.FirstOrDefault(t => t.USERID == UId && t.PASSWORD == Pwd && t.ISACTIVE == true);
                if (oLogin != null)
                {
                    Result.STATUS = true;
                    APPUSERLOG oAPPUSERLOG = new APPUSERLOG();
                    oAPPUSERLOG.DEVICEID = DId;
                    oAPPUSERLOG.USERID = oLogin.ID;
                    oAPPUSERLOG.OTP = RandomString.Generate(5, 5, true);
                    oAPPUSERLOG.Token = RandomString.Generate(5, 10);
                    oAPPUSERLOG.LOGTIME = DateTime.Now;
                    dbContext.APPUSERLOGs.Add(oAPPUSERLOG);
                    dbContext.SaveChanges();

                    SendSms(oLogin.MOBILENO, oAPPUSERLOG.OTP);

                    Result.TOKENID = oAPPUSERLOG.Token;
                    Result.RES_MSG = "success";
                    Result.USERID = oLogin.ID.ToString();
                    Result.CUSTOMER_NAME = oLogin.USERID.ToString();
                    Result.CUSTOMER_MOBILE = oLogin.MOBILENO.ToString();

                }
                else
                {
                    Result.STATUS = false;
                    Result.TOKENID = string.Empty;
                    Result.RES_MSG = "unsuccess";
                }
            }
            catch (Exception ex)
            {
                Result.STATUS = false;
                Result.RES_MSG = (ex.InnerException != null) ? ex.Message : ex.InnerException.Message;
            }
            return Result;
        }

        [Route("ResendOTP/{TId}/{UId}")]
        [HttpGet]
        public LOGINRESULT CheckOTP(string TId, string UId)
        {
            var Result = new LOGINRESULT();
            var randomString = new RandomString();
            try
            {
                var oLoginOTP = dbContext.APPUSERLOGs.FirstOrDefault(t => t.Token == TId && t.USERID.ToString() == UId);
                if (oLoginOTP != null)
                {
                    Result.STATUS = true;
                    Result.RES_MSG = "success";
                    SendSms(oLoginOTP.APPUSER.MOBILENO, oLoginOTP.OTP);
                }
                else
                {
                    Result.STATUS = false;
                    Result.TOKENID = string.Empty;

                    Result.RES_MSG = "unsuccess";
                }
            }
            catch (Exception ex)
            {
                Result.STATUS = false;
                Result.RES_MSG = (ex.InnerException != null) ? ex.Message : ex.InnerException.Message;
            }
            return Result;
        }

        [Route("CheckOTP/{TId}/{UId}/{OTP}")]

        [HttpGet]
        public LOGINRESULT ResendOTP(string TId, string UId, string OTP)
        {
            var Result = new LOGINRESULT();
            var randomString = new RandomString();
            try
            {
                var oLoginOTP = dbContext.APPUSERLOGs.FirstOrDefault(t => t.Token == TId && t.OTP == OTP);
                if (oLoginOTP != null)
                {
                    Result.STATUS = true;
                    Result.RES_MSG = "success";
                    Result.USERID = oLoginOTP.USERID.ToString();
                    if (oLoginOTP.APPUSER.APPCUSTINFOes.ElementAt(0) != null)
                    {
                        Result.CUSTOMER_NO = oLoginOTP.APPUSER.APPCUSTINFOes.ElementAt(0).CUSTOMERNO;
                        Result.CUSTOMER_NAME = oLoginOTP.APPUSER.APPCUSTINFOes.ElementAt(0).APPUSER.USERID;
                        Result.CUSTOMER_MOBILE = oLoginOTP.APPUSER.APPCUSTINFOes.ElementAt(0).APPUSER.MOBILENO;
                    }
                }
                else
                {
                    Result.STATUS = false;
                    Result.TOKENID = string.Empty;

                    Result.RES_MSG = "unsuccess";
                }
            }
            catch (Exception ex)
            {
                Result.STATUS = false;
                Result.RES_MSG = (ex.InnerException != null) ? ex.Message : ex.InnerException.Message;
            }
            return Result;
        }

        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

    }
}