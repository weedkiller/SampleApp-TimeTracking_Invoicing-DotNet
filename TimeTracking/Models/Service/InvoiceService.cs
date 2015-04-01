using Intuit.Ipp.Data;
using Intuit.Ipp.DataService;
using Intuit.Ipp.QueryFilter;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TimeTracking.Models.DTO;
using TimeTracking.Models.Repository;

namespace TimeTracking.Models.Service
{
    /// <summary>
    /// 
    /// </summary>
    public class InvoiceService
    {
         DataserviceFactory dataserviceFactory = null;
        DataService dataService = null;
        private InvoiceRepository invoiceRepository = null;
        public InvoiceService(Invoicedto invoicedto)
        {
            dataserviceFactory = new DataserviceFactory(invoicedto.oAuthTokens);
            dataService = dataserviceFactory.getDataService();
            invoiceRepository = new InvoiceRepository();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="invoicedto"></param>
        /// <returns></returns>
        internal Invoicedto GenerateInvoice(Invoicedto invoicedto)
        {
            TimeActivity timeActivity = GetCustomerId(invoicedto);
            Invoice invoice = PopulateInvoiceParam(invoicedto, timeActivity);
            invoice = dataService.Add<Invoice>(invoice);
            invoicedto.Invoice = invoice;
            invoicedto.InvoiceQboId = Convert.ToInt64(invoice.Id);
            invoicedto.AlertMessage = string.Format("Invoice successfully created and pushed to QBO (QBO ID = {0})", invoice.Id);
            return invoicedto;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="invoicedto"></param>
        /// <param name="timeActivity"></param>
        /// <returns></returns>
        private Invoice PopulateInvoiceParam(Invoicedto invoicedto, TimeActivity timeActivity)
        {
            Invoice invoice = new Invoice();

            invoice.TxnDate = timeActivity.TxnDate;
            invoice.Line = new Line[]
            {
                new Line{AmountSpecified=true,Description=timeActivity.Description,Amount=25,DetailType=LineDetailTypeEnum.SalesItemLineDetail,DetailTypeSpecified=true,AnyIntuitObject = new SalesItemLineDetail{Qty=Convert.ToDecimal(3.5),TaxCodeRef=new ReferenceType{Value="NON"},ItemRef=new ReferenceType{Value=timeActivity.ItemRef.Value}}},
                new Line{Amount=25,AmountSpecified=true,DetailType=LineDetailTypeEnum.SubTotalLineDetail,DetailTypeSpecified=true,AnyIntuitObject = new SubTotalLineDetail{}}
            };
            invoice.TxnTaxDetail = new TxnTaxDetail { TotalTax = 0 };
            invoice.CustomerRef = new ReferenceType { Value = timeActivity.CustomerRef.Value };
            //invoice.SalesTermRef = new ReferenceType { Value = timeActivity.ItemRef.Value };
            invoice.DueDate = DateTime.Now;
            invoice.TotalAmt = 25;
            invoice.ApplyTaxAfterDiscount = false;
            invoice.PrintStatus = PrintStatusEnum.NeedToPrint;
            invoice.PrintStatusSpecified = true;
            invoice.EmailStatus = EmailStatusEnum.NotSet;
            invoice.EmailStatusSpecified = true;
            invoice.Balance = 25;
            invoice.BalanceSpecified = true;
            invoice.Deposit = 0;
            invoice.DepositSpecified = true;
            invoice.AllowIPNPayment = false;
            invoice.AllowIPNPaymentSpecified = true;
            invoice.AllowOnlinePayment = false;
            invoice.AllowOnlinePaymentSpecified = true;
            invoice.AllowOnlineCreditCardPayment = false;
            invoice.AllowOnlineCreditCardPaymentSpecified = true;
            invoice.AllowOnlineACHPayment = false;
            invoice.AllowOnlineACHPaymentSpecified = true;
            return invoice;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="invoicedto"></param>
        /// <returns></returns>
        private TimeActivity GetCustomerId(Invoicedto invoicedto)
        {
            string EXISTING_TimeActive_QUERY = string.Format("select * from timeactivity where Id = '{0}'", invoicedto.timeQboId);
            QueryService<TimeActivity> queryService = new QueryService<TimeActivity>(dataserviceFactory.getServiceContext);
            TimeActivity resultFound = queryService.ExecuteIdsQuery(EXISTING_TimeActive_QUERY).FirstOrDefault<TimeActivity>();
            return resultFound;
        }
       /// <summary>
       /// 
       /// </summary>
       /// <param name="type"></param>
       /// <param name="dueDays"></param>
       /// <returns></returns>
        private string findTerm(string type, int dueDays)
        {
            ReadOnlyCollection<Term> terms = dataService.FindAll<Term>(new Term());
            return terms.Where(x => x.Type == type && x.ItemsElementName.Contains(ItemsChoiceType.DueDays) && x.AnyIntuitObjects.Contains(dueDays)).First().Id;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="invoicedto"></param>
        /// <returns></returns>
        internal Invoicedto LoadPending(Invoicedto invoicedto)
        {
            List<TimeActivityFill> timeActivityFillList = new List<TimeActivityFill>();
            using (SqlConnection sqlConnection = new SqlConnection(invoicedto.ConnectionString))
            {
                string oString = string.Format("select * from TimeActivity where RealmId='{0}' and Invoice_QboId is null", invoicedto.CompanyId);
                SqlCommand oCmd = new SqlCommand(oString, sqlConnection);
                sqlConnection.Open();
                using (SqlDataReader oReader = oCmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        TimeActivityFill timeActivityFill = new TimeActivityFill();
                        timeActivityFill.Employee = oReader["Employee"].ToString();
                        timeActivityFill.Customer = oReader["Customer"].ToString();
                        timeActivityFill.Item = oReader["Item"].ToString();
                        timeActivityFill.Date = Convert.ToDateTime(oReader["Date"].ToString()).ToShortDateString();
                        timeActivityFill.Hours = oReader["Hours"].ToString();
                        timeActivityFill.QboId = oReader["QboId"].ToString();
                        timeActivityFillList.Add(timeActivityFill);
                    }
                    sqlConnection.Close();
                }
            }
            if (invoicedto.InvoiceCreated==null)
            {
                invoicedto.InvoiceCreated = new List<InvoiceCreated>();
            }
            invoicedto.InvoicePending = "Pending";
            invoicedto.InvoicePendingLength = timeActivityFillList.Count;
            invoicedto.InvoicePendingList = timeActivityFillList;
            return invoicedto;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="invoicedto"></param>
        /// <returns></returns>
        internal Invoicedto UpdateDatabase(Invoicedto invoicedto)
        {
          
            var qboId = invoicedto.timeQboId;
            var invoiceQboId = invoicedto.InvoiceQboId;
            string query = "UPDATE TimeActivity SET Invoice_QboId=@Invoice_QboId WHERE QboId=@QboId";
            using (SqlCommand myCommand = new SqlCommand(query, new SqlConnection(invoicedto.ConnectionString)))
            {
                myCommand.Connection.Open();
                myCommand.Parameters.AddWithValue("@Invoice_QboId", invoiceQboId);
                myCommand.Parameters.AddWithValue("@QboId", qboId);
                myCommand.ExecuteNonQuery();
                myCommand.Connection.Close();
            }
            invoicedto.UpdatePending = true;
            return invoicedto;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="invoicedto"></param>
        /// <returns></returns>
        internal Invoicedto FillCreatedInvoice(Invoicedto invoicedto)
        {
            List<InvoiceCreated> invoiceCreatedList = new List<InvoiceCreated>();
            using (SqlConnection sqlConnection = new SqlConnection(invoicedto.ConnectionString))
            {
                string oString = string.Format("select * from TimeActivity where RealmId='{0}' and Invoice_QboId='{1}'", invoicedto.CompanyId, invoicedto.InvoiceQboId);
                SqlCommand oCmd = new SqlCommand(oString, sqlConnection);
                sqlConnection.Open();
                using (SqlDataReader oReader = oCmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        InvoiceCreated nvoiceCreated = new InvoiceCreated();
                        nvoiceCreated.Employee = oReader["Employee"].ToString();
                        nvoiceCreated.Customer = oReader["Customer"].ToString();
                        nvoiceCreated.Item = oReader["Item"].ToString();
                        nvoiceCreated.Date = Convert.ToDateTime(oReader["Date"].ToString()).ToShortDateString();
                        nvoiceCreated.Hours = oReader["Hours"].ToString();
                        nvoiceCreated.QboId = oReader["QboId"].ToString();
                        invoiceCreatedList.Add(nvoiceCreated);
                    }
                    sqlConnection.Close();
                }
            }
            invoicedto.InvoiceCreated = invoiceCreatedList;
            invoicedto.InvoiceStatus = "Invoiced";
            invoicedto.InvoiceListLength = invoiceCreatedList.Count;
            return invoicedto;
        }
    }
}