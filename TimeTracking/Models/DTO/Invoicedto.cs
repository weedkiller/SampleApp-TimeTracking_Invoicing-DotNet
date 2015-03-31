using Intuit.Ipp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeTracking.Models.DTO
{
    public class Invoicedto
    {

        public OAuthorizationdto oAuthTokens { get; set; }
        public string CustomerId { get; set; }
        public DateTime TxnDate { get; set; }
        public int CompanyId { get; set; }
        public Invoice Invoice { get; set; }
        public List<Invoice> InvoiceList { get; set; }
        public long Id { get; set; }
        public Int64 QboId { get; set; }
        public string AlertMessage { get; set; }
        public string Description { get; set; }
        public string ConnectionString { get; set; }

        public List<Customer> CustomerList { get; set; }
        public string InvoiceStatus { get; set; }
        public string invoiceListLength { get; set; }
        public List<TimeActivityFill> InvoicePendingList { get; set; }
        public TimeActivityFill TimeActivityFill { get; set; }
        public List<InvoiceCreated> InvoiceCreated { get; set; }
        public string InvoicePending { get; set; }
        public int InvoicePendingLength { get; set; }

        public TimeActivitydto TimeActivityDto { get; set; }

        public string InvoiceQboId { get; set; }
    }
    public class InvoiceCreated
    {
        public string Employee { get; set; }
        public string Customer { get; set; }
        public string Item { get; set; }
        public string Date { get; set; }
        public string Hours { get; set; }
        public string QboId { get; set; }
    }
}