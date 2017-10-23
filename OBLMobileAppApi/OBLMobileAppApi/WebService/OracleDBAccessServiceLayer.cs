using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OBLMobileAppApi.OracleDBService;
using System.Data;
using OBLMobileAppApi.Models;
using System.Globalization;



namespace OBLMobileAppApi.WebService
{


    public class OracleDBAccessServiceLayer
    {
        //Account
        private string TAG_ACNO = "AC_NO";
        private string TAG_ACCURRENCY = "AC_CCY";
        private string TAG_TRANDATE = "TRN_DT";
        private string TAG_TRANDATETIME = "TRA";
        private string TAG_TRANREFNO = "TRN_REF_NO";
        private string TAG_TRANDES = "TRN_DESC";
        private string TAG_TRANACBAL = "BALANCE";
        private string TAG_ACENTRYSERIAL = "AC_ENTRY_SR_NO";
        private string TAG_CR = "CR";
        private string TAG_DR = "DR";

        //Card
        private string TAG_CLIENTID = "CLIENTID";
        private string TAG_CARDNO = "CARDNO";
        private string TAG_CARDHOLDERNAME = "CARDHOLDERNAME";
        private string TAG_DATEOFBIRTH = "DATEOFBIRTH";

        //Exchange Rate
        private string TAG_CURRENCY = "CCY1";
        private string TAG_RATE_TYPE = "RATE_TYPE";
        private string TAG_RATE_DATE = "RATE_DATE";
        private string TAG_BUY_RATE = "BUY_RATE";
        private string TAG_SALE_RATE = "SALE_RATE";

        public static string[] INIT_CURRENCY_LIST = new string[] { "USD", "GBP", "EUR", "JPY" };

        


        ACTRANSACTION oACTRANSACTION = null;

        Oradbaccess oOradbaccess = null;

        public OracleDBAccessServiceLayer()
        {
            oOradbaccess = new Oradbaccess();
        }

        public List<ACTRANSACTION> QueryDaysTransactionList(string AccountNo, int Days)
        {
            var TranReqList = new List<ACTRANSACTION>();
            DateTime dtToday = DateTime.Today;
            DateTime dtToQueryDay = dtToday.AddDays(-Days);

            var result = oOradbaccess.GetCustAccTransactions(AccountNo, dtToQueryDay, dtToday);
            if (result != null && result.Rows.Count > 0)
            {
                ACTRANSACTION trnRequest = null;
                foreach (DataRow drTran in result.Rows)
                {
                    trnRequest = new ACTRANSACTION();
                    trnRequest.ACNO = drTran[TAG_ACNO].ToString().Trim();
                    if (!string.IsNullOrEmpty(drTran[TAG_CR].ToString()) && Convert.ToDecimal(drTran[TAG_CR].ToString()) != 0)
                    {
                        trnRequest.TRANAMOUNT = Convert.ToDecimal(drTran[TAG_CR].ToString().Trim());
                        trnRequest.TRANTYPE = "Cr";
                    }
                    else if (!string.IsNullOrEmpty(drTran[TAG_DR].ToString()) && Convert.ToDecimal(drTran[TAG_DR].ToString()) != 0)
                    {
                        trnRequest.TRANAMOUNT = Convert.ToDecimal(drTran[TAG_DR].ToString().Trim());
                        trnRequest.TRANTYPE = "Dr";
                    }
                    trnRequest.TRANACBAL = Convert.ToDecimal(drTran[TAG_TRANACBAL].ToString().Trim());
                    trnRequest.TRANDATE =DateTime.Parse(drTran[TAG_TRANDATE].ToString().Trim(),CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
                    trnRequest.TRANDATETIME = DateTime.Parse(drTran[TAG_TRANDATETIME].ToString().Trim(),CultureInfo.InvariantCulture);
                    trnRequest.TRANREFNO = drTran[TAG_TRANREFNO].ToString().Trim();
                    trnRequest.TRANDES = drTran[TAG_TRANDES].ToString().Trim();
                    trnRequest.ACENTRYSERIAL = Convert.ToInt32(drTran[TAG_ACENTRYSERIAL].ToString().Trim());
                    TranReqList.Add(trnRequest);
                }
            }
            return TranReqList.OrderByDescending(t => t.TRANDATETIME).ToList();
        }

        public List<EXCHANGERATEINFO> QueryExchangeRateList()
        {
            var xchangeRateList = new EXCHANGERATELIST();
            var rateInfoList = new List<EXCHANGERATEINFO>();
            try
            {
                foreach (string CurrencyName in INIT_CURRENCY_LIST)
                {
                    var result = oOradbaccess.GetRate(CurrencyName);
                    if (result != null && result.Rows.Count > 0)
                    {
                        EXCHANGERATEINFO xchngRate = null;
                        foreach (DataRow drTran in result.Rows)
                        {
                            xchngRate = new EXCHANGERATEINFO();
                            xchngRate.CURRENCY = drTran[TAG_CURRENCY].ToString();
                            xchngRate.BUYRATE = Convert.ToDecimal(drTran[TAG_BUY_RATE].ToString().Trim());
                            xchngRate.RATEDATE = DateTime.Parse(drTran[TAG_RATE_DATE].ToString().Trim(), CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
                            xchngRate.RATETYPE = drTran[TAG_RATE_TYPE].ToString();
                            xchngRate.SALERATE = Convert.ToDecimal(drTran[TAG_SALE_RATE].ToString().Trim());
                            rateInfoList.Add(xchngRate);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                
            }
            return rateInfoList;
        }




    }
}