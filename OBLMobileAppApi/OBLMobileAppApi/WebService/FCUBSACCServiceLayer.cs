using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using OBLMobileAppApi.FCUBSAccService;
using OBLMobileAppApi.FCUBSAccService;
using OBLMobileAppApi.Models;

namespace OBLMobileAppApi.WebService
{

    public class FCUBSACCServiceLayer
    {
        FCUBSAccServiceSEIClient oFCUBSAccServiceSEIClient = null;
        QUERYACCBAL_IOFS_REQ oQUERYACCBAL_IOFS_REQ = new QUERYACCBAL_IOFS_REQ();
        QUERYACCBAL_IOFS_RES oQUERYACCBAL_IOFS_RES = new QUERYACCBAL_IOFS_RES();

        QUERYACCSUMM_IOFS_REQ oQUERYACCSUMM_IOFS_REQ = new QUERYACCSUMM_IOFS_REQ();
        QUERYACCSUMM_IOFS_RES oQUERYACCSUMM_IOFS_RES = null;

        


        public FCUBSACCServiceLayer()
        {
            
        }

        public void FCUBSACCServiceConnectionOpen()
        {
            try
            {
                oFCUBSAccServiceSEIClient = new FCUBSAccServiceSEIClient();
                //oFCUBSAccServiceSEIClient.ClientCredentials.Windows.ClientCredential.UserName = "Admin";
                //oFCUBSAccServiceSEIClient.ClientCredentials.Windows.ClientCredential.Password = "TestServer2";
                //oFCUBSAccServiceSEIClient.ClientCredentials.UserName.UserName = "Admin";
                //oFCUBSAccServiceSEIClient.ClientCredentials.UserName.Password = "TestServer2";

                oFCUBSAccServiceSEIClient.ClientCredentials.Windows.ClientCredential.UserName = "software";
                oFCUBSAccServiceSEIClient.ClientCredentials.Windows.ClientCredential.Password = "Soft@123";
                oFCUBSAccServiceSEIClient.ClientCredentials.UserName.UserName = "software";
                oFCUBSAccServiceSEIClient.ClientCredentials.UserName.Password = "Soft@123";


                oFCUBSAccServiceSEIClient.Open();
            }
            catch(Exception ex)
            {

            }
        }

        private string GetAccountTypeName(string shortTypeName)
        {
            string fullAcTypeName = string.Empty;
            switch(shortTypeName)
            {
                case "N":
                    fullAcTypeName = "Nostro";
                    break;
                case "S":
                    fullAcTypeName = "Savings";
                    break;
                case "U":
                    fullAcTypeName = "OD";
                    break;
                case "Y":
                    fullAcTypeName = "TD/FD";
                    break;
                default:
                    fullAcTypeName = shortTypeName;
                    break;
            }
            return fullAcTypeName;
        }

        public void FCUBSACCServiceConnectionClose()
        {
            try
            {
                if (oFCUBSAccServiceSEIClient != null && oFCUBSAccServiceSEIClient.State == System.ServiceModel.CommunicationState.Opened)
                    oFCUBSAccServiceSEIClient.Close();
            }
            catch (Exception ex)
            {

            }
        }
        public QUERYACCBAL_IOFS_RES QueryAccBalIO(string CustomerAcNo)
        {
            QUERYACCBAL_IOFS_RES oQUERYACCBAL_IOFS_RES=null;
            FCUBSACCServiceConnectionOpen();

            oQUERYACCBAL_IOFS_REQ.FCUBS_HEADER = new FCUBS_HEADERType();
            oQUERYACCBAL_IOFS_REQ.FCUBS_HEADER.SOURCE = "RTGS";
            oQUERYACCBAL_IOFS_REQ.FCUBS_HEADER.UBSCOMP = UBSCOMPType.FCUBS;
            oQUERYACCBAL_IOFS_REQ.FCUBS_HEADER.USERID = "RTGS";
            oQUERYACCBAL_IOFS_REQ.FCUBS_HEADER.BRANCH = "012";
            oQUERYACCBAL_IOFS_REQ.FCUBS_HEADER.MODULEID = "ST";
            oQUERYACCBAL_IOFS_REQ.FCUBS_HEADER.SERVICE = "FCUBSAccService";
            oQUERYACCBAL_IOFS_REQ.FCUBS_HEADER.OPERATION = "QueryAccBal";
            oQUERYACCBAL_IOFS_REQ.FCUBS_HEADER.SOURCE_OPERATION = "QueryAccBal";
            oQUERYACCBAL_IOFS_REQ.FCUBS_HEADER.MSGSTAT = MsgStatType.SUCCESS;


            oQUERYACCBAL_IOFS_REQ.FCUBS_BODY = new QUERYACCBAL_IOFS_REQFCUBS_BODY();
            oQUERYACCBAL_IOFS_REQ.FCUBS_BODY.ACCBalance = new AccBalReqtype();
            oQUERYACCBAL_IOFS_REQ.FCUBS_BODY.ACCBalance.ACC_BAL = new AccBalReqtypeACC_BAL();
            oQUERYACCBAL_IOFS_REQ.FCUBS_BODY.ACCBalance.ACC_BAL.BRANCH_CODE = CustomerAcNo.Substring(0,3);
            oQUERYACCBAL_IOFS_REQ.FCUBS_BODY.ACCBalance.ACC_BAL.CUST_AC_NO = CustomerAcNo;

            QueryAccBalIORequest oQueryAccBalIORequest = new QueryAccBalIORequest(oQUERYACCBAL_IOFS_REQ);
            try
            {
                oQUERYACCBAL_IOFS_RES = oFCUBSAccServiceSEIClient.QueryAccBalIO(oQUERYACCBAL_IOFS_REQ);
                FCUBSACCServiceConnectionClose();
                //ObjectToXml(objResponse, @"E:\WorkStation\FCUBSWebService\QueryAccBalIO_" + DateTime.Now.ToString("hhmmss") + ".xml");

            }
            catch (Exception exp)
            {
                FCUBSACCServiceConnectionClose();
            }

            return oQUERYACCBAL_IOFS_RES;
        }

        public ACREQUEST QueryCustomerSummary(string CustomerNo)
        {
            var result_acInfo = new ACREQUEST();
            FCUBSACCServiceConnectionOpen();

            oQUERYACCSUMM_IOFS_REQ.FCUBS_HEADER = new FCUBS_HEADERType();
            oQUERYACCSUMM_IOFS_REQ.FCUBS_HEADER.SOURCE = "RTGS";
            oQUERYACCSUMM_IOFS_REQ.FCUBS_HEADER.UBSCOMP = UBSCOMPType.FCUBS;
            oQUERYACCSUMM_IOFS_REQ.FCUBS_HEADER.USERID = "RTGS";
            oQUERYACCSUMM_IOFS_REQ.FCUBS_HEADER.BRANCH = "000";
            oQUERYACCSUMM_IOFS_REQ.FCUBS_HEADER.MODULEID = "ST";
            oQUERYACCSUMM_IOFS_REQ.FCUBS_HEADER.SERVICE = "FCUBSAccService";
            oQUERYACCSUMM_IOFS_REQ.FCUBS_HEADER.OPERATION = "QueryAccSumm";
            oQUERYACCSUMM_IOFS_REQ.FCUBS_HEADER.SOURCE_OPERATION = "QueryAccSumm";
            //oQUERYACCSUMM_IOFS_REQ.FCUBS_HEADER.MSGSTAT = MsgStatType.SUCCESS;


            oQUERYACCSUMM_IOFS_REQ.FCUBS_BODY = new QUERYACCSUMM_IOFS_REQFCUBS_BODY();
            oQUERYACCSUMM_IOFS_REQ.FCUBS_BODY.StvwAccountSumaryIO = new QueryAccSummQueryIOType();
            oQUERYACCSUMM_IOFS_REQ.FCUBS_BODY.StvwAccountSumaryIO.CUST_NO = CustomerNo;

            QueryAccSummIORequest oQueryAccSummIORequest = new QueryAccSummIORequest(oQUERYACCSUMM_IOFS_REQ);
            try
            {
                oQUERYACCSUMM_IOFS_RES = new QUERYACCSUMM_IOFS_RES();
                //oQUERYACCSUMM_IOFS_RES.FCUBS_BODY = new QUERYACCSUMM_IOFS_RESFCUBS_BODY();
                //oQUERYACCSUMM_IOFS_RES.FCUBS_BODY.FCUBS_ERROR_RESP=new ERRORDETAILSType[0][];

                oQUERYACCSUMM_IOFS_RES = oFCUBSAccServiceSEIClient.QueryAccSummIO(oQUERYACCSUMM_IOFS_REQ);
                FCUBSACCServiceConnectionClose();

                QueryAccSummFullTypeStvwAccountSumaryA oQueryAccSummFullTypeStvwAccountSumaryA = new QueryAccSummFullTypeStvwAccountSumaryA();
                result_acInfo.ACLIST = new List<USERACINFO>();
                foreach(QueryAccSummFullTypeStvwAccountSumaryA response in oQUERYACCSUMM_IOFS_RES.FCUBS_BODY.StvwAccountSumaryFull.StvwAccountSumaryA.ToList())
                {
                    var oAc = new USERACINFO();
                    var oCustACBal = QueryAccBalIO(response.CUSTACNO);
                    if(oCustACBal != null)
                    {
                        oAc.ACAVAILBAL = oCustACBal.FCUBS_BODY.ACCBalance.ElementAt(0).AVLBAL;
                        oAc.ACCURRENTBAL = oCustACBal.FCUBS_BODY.ACCBalance.ElementAt(0).CURBAL;
                       
                    }
                    
                    oAc.ACNAME = response.CUSTOMER_NAME;
                    oAc.CUSTNO = CustomerNo;
                    oAc.ACNO = response.CUSTACNO;
                    oAc.ACTYPE =GetAccountTypeName(response.ACCOUNT_TYPE);
                    result_acInfo.ACLIST.Add(oAc);
                }
                result_acInfo.CUSTOMERNO = CustomerNo;

            }
            catch (Exception exp)
            {
                FCUBSACCServiceConnectionClose();
                
            }
            return result_acInfo;

        }

        public ACREQUEST QueryAccSummary(string CustomerNo,string AccountNo)
        {
            var result_acInfo = new ACREQUEST();
            FCUBSACCServiceConnectionOpen();

            oQUERYACCSUMM_IOFS_REQ.FCUBS_HEADER = new FCUBS_HEADERType();
            oQUERYACCSUMM_IOFS_REQ.FCUBS_HEADER.SOURCE = "RTGS";
            oQUERYACCSUMM_IOFS_REQ.FCUBS_HEADER.UBSCOMP = UBSCOMPType.FCUBS;
            oQUERYACCSUMM_IOFS_REQ.FCUBS_HEADER.USERID = "RTGS";
            oQUERYACCSUMM_IOFS_REQ.FCUBS_HEADER.BRANCH = "000";
            oQUERYACCSUMM_IOFS_REQ.FCUBS_HEADER.MODULEID = "ST";
            oQUERYACCSUMM_IOFS_REQ.FCUBS_HEADER.SERVICE = "FCUBSAccService";
            oQUERYACCSUMM_IOFS_REQ.FCUBS_HEADER.OPERATION = "QueryAccSumm";
            oQUERYACCSUMM_IOFS_REQ.FCUBS_HEADER.SOURCE_OPERATION = "QueryAccSumm";
            //oQUERYACCSUMM_IOFS_REQ.FCUBS_HEADER.MSGSTAT = MsgStatType.SUCCESS;


            oQUERYACCSUMM_IOFS_REQ.FCUBS_BODY = new QUERYACCSUMM_IOFS_REQFCUBS_BODY();
            oQUERYACCSUMM_IOFS_REQ.FCUBS_BODY.StvwAccountSumaryIO = new QueryAccSummQueryIOType();
            oQUERYACCSUMM_IOFS_REQ.FCUBS_BODY.StvwAccountSumaryIO.CUST_NO = CustomerNo;

            QueryAccSummIORequest oQueryAccSummIORequest = new QueryAccSummIORequest(oQUERYACCSUMM_IOFS_REQ);
            try
            {
                oQUERYACCSUMM_IOFS_RES = new QUERYACCSUMM_IOFS_RES();
                //oQUERYACCSUMM_IOFS_RES.FCUBS_BODY = new QUERYACCSUMM_IOFS_RESFCUBS_BODY();
                //oQUERYACCSUMM_IOFS_RES.FCUBS_BODY.FCUBS_ERROR_RESP=new ERRORDETAILSType[0][];

                oQUERYACCSUMM_IOFS_RES = oFCUBSAccServiceSEIClient.QueryAccSummIO(oQUERYACCSUMM_IOFS_REQ);
                FCUBSACCServiceConnectionClose();

                QueryAccSummFullTypeStvwAccountSumaryA oQueryAccSummFullTypeStvwAccountSumaryA = new QueryAccSummFullTypeStvwAccountSumaryA();
                result_acInfo.ACLIST = new List<USERACINFO>();
                foreach (QueryAccSummFullTypeStvwAccountSumaryA response in oQUERYACCSUMM_IOFS_RES.FCUBS_BODY.StvwAccountSumaryFull.StvwAccountSumaryA.Where(t=>t.CUSTACNO == AccountNo).ToList())
                {
                    var oAc = new USERACINFO();
                    var oCustACBal = QueryAccBalIO(response.CUSTACNO);
                    if (oCustACBal != null)
                    {
                        oAc.ACAVAILBAL = oCustACBal.FCUBS_BODY.ACCBalance.ElementAt(0).AVLBAL;
                        oAc.ACCURRENTBAL = oCustACBal.FCUBS_BODY.ACCBalance.ElementAt(0).CURBAL;
                    }
                    oAc.ACNAME = response.CUSTOMER_NAME;
                    oAc.CUSTNO = CustomerNo;
                    oAc.ACNO = response.CUSTACNO;
                    oAc.ACTYPE =GetAccountTypeName(response.ACCOUNT_TYPE);
                    result_acInfo.ACLIST.Add(oAc);
                }
                result_acInfo.CUSTOMERNO = CustomerNo;

            }
            catch (Exception exp)
            {
                FCUBSACCServiceConnectionClose();

            }
            return result_acInfo;

        }

        
    }


}