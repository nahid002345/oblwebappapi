using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using OBLMobileAppApi.FCUBSRTService;
using OBLMobileAppApi.FCUBSRTService;
using OBLMobileAppApi.Models;

namespace OBLMobileAppApi.WebService
{

    public class FCUBSRTServiceLayer
    {
        FCUBSRTServiceSEIClient oFCUBSRTServiceSEIClient = null;

        CREATETRANSACTION_FSFS_REQ oCREATETRANSACTION_FSFS_REQ = new CREATETRANSACTION_FSFS_REQ();
        CREATETRANSACTION_FSFS_RES oCREATETRANSACTION_FSFS_RES = null;
        public FCUBSRTServiceLayer()
        {
            
        }

        public void FCUBSRTServiceConnectionOpen()
        {
            try
            {
                oFCUBSRTServiceSEIClient = new FCUBSRTServiceSEIClient();
                oFCUBSRTServiceSEIClient.ClientCredentials.Windows.ClientCredential.UserName = "Admin";
                oFCUBSRTServiceSEIClient.ClientCredentials.Windows.ClientCredential.Password = "TestServer2";
                oFCUBSRTServiceSEIClient.ClientCredentials.UserName.UserName = "Admin";
                oFCUBSRTServiceSEIClient.ClientCredentials.UserName.Password = "TestServer2";

                oFCUBSRTServiceSEIClient.Open();
            }
            catch(Exception ex)
            {

            }
        }

        public void FCUBSRTServiceConnectionClose()
        {
            try
            {
                if (oFCUBSRTServiceSEIClient != null && oFCUBSRTServiceSEIClient.State == System.ServiceModel.CommunicationState.Opened)
                    oFCUBSRTServiceSEIClient.Close();
            }
            catch (Exception ex)
            {

            }
        }

        public CREATETRANSACTION_FSFS_RES CreateTransaction(string CustomerAcNo)
        {

            FCUBSRTServiceConnectionOpen();

            oCREATETRANSACTION_FSFS_REQ.FCUBS_HEADER = new FCUBS_HEADERType();
            oCREATETRANSACTION_FSFS_REQ.FCUBS_HEADER.SOURCE = "RTGS";
            oCREATETRANSACTION_FSFS_REQ.FCUBS_HEADER.UBSCOMP = UBSCOMPType.FCUBS;
            oCREATETRANSACTION_FSFS_REQ.FCUBS_HEADER.USERID = "RTGS";
            oCREATETRANSACTION_FSFS_REQ.FCUBS_HEADER.BRANCH = "000";
            oCREATETRANSACTION_FSFS_REQ.FCUBS_HEADER.MODULEID = "RT";
            oCREATETRANSACTION_FSFS_REQ.FCUBS_HEADER.SERVICE = "FCUBSRTService";
            oCREATETRANSACTION_FSFS_REQ.FCUBS_HEADER.OPERATION = "CreateTransaction";
            oCREATETRANSACTION_FSFS_REQ.FCUBS_HEADER.SOURCE_OPERATION = "CreateTransaction";


            oCREATETRANSACTION_FSFS_REQ.FCUBS_BODY = new CREATETRANSACTION_FSFS_REQFCUBS_BODY();
            oCREATETRANSACTION_FSFS_REQ.FCUBS_BODY.TransactionDetails = new RetailTellerTypeFull();
            oCREATETRANSACTION_FSFS_REQ.FCUBS_BODY.TransactionDetails.PRD = "FTPP";
            oCREATETRANSACTION_FSFS_REQ.FCUBS_BODY.TransactionDetails.BRN = "012";
            oCREATETRANSACTION_FSFS_REQ.FCUBS_BODY.TransactionDetails.MODULE = "RT";
            oCREATETRANSACTION_FSFS_REQ.FCUBS_BODY.TransactionDetails.TXNBRN = "012";
            oCREATETRANSACTION_FSFS_REQ.FCUBS_BODY.TransactionDetails.TXNACC = "0122010002275";
            oCREATETRANSACTION_FSFS_REQ.FCUBS_BODY.TransactionDetails.TXNCCY = "BDT";
            oCREATETRANSACTION_FSFS_REQ.FCUBS_BODY.TransactionDetails.TXNAMT = Convert.ToDecimal(1.5);
            oCREATETRANSACTION_FSFS_REQ.FCUBS_BODY.TransactionDetails.OFFSETBRN = "0122010002275";
            CreateTransactionFSRequest oCreateTransactionIORequest = new CreateTransactionFSRequest(oCREATETRANSACTION_FSFS_REQ);
            //try
            //{
            //    oCREATETRANSACTION_FSFS_RES = oFCUBSRTServiceSEIClient.CreateTransactionFS(oCREATETRANSACTION_FSFS_REQ);
            //    FCUBSRTServiceConnectionClose();
            //}
            //catch (Exception exp)
            //{
            //    FCUBSRTServiceConnectionClose();
            //}

            return oCREATETRANSACTION_FSFS_RES;
        }


        
    }


}