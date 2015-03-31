using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeTracking.Models.DTO;

namespace TimeTracking.Models.Repository
{
    public class InvoiceRepository
    {
        Dictionary<Int64, Invoicedto> invoiceRepository = null;
        Controller invoiceController = null;
        public InvoiceRepository()
        {
            invoiceRepository = new Dictionary<Int64, Invoicedto>();
        }

        internal Invoicedto Save(object controller, Invoicedto invoicedto)
        {
            invoiceController = controller as Controller;
            Random random = new Random();
            invoicedto.Id = random.Next(1, 100);
            invoiceRepository.Add(invoicedto.Id, invoicedto);
            invoiceController.TempData["Invoice"] = invoiceRepository;
            return invoicedto;
        }
        internal Invoicedto Get(object controller, Int64 id)
        {
            invoiceController = controller as System.Web.Mvc.Controller;
            Dictionary<Int64, Invoicedto> invoiceRepo = invoiceController.TempData["Invoice"] as Dictionary<Int64, Invoicedto>;
            return invoiceRepo[id];
        }
        internal void SavetoDb(string conString, Invoicedto invoicedto)
        {
            //var employeeObj = ReturnListItem(invoicedto, "emp") as Employee;
            //var CustomerObj = ReturnListItem(invoicedto, "cust") as Customer;
            //var ItemObj = ReturnListItem(invoicedto, "item") as Item;
            //var dateObj = invoicedto.TxnDate;
            //var hoursObj = invoicedto.Hours;
            //var qboId = invoicedto.QboId;
            //string query = "INSERT INTO [TimeActivity] ([RealmId], [Employee], [Customer], [Item], [Date],  [Hours], [QboId])";
            //query += " VALUES (@RealmId, @Employee, @Customer, @Item, @Date, @Hours, @QboId)";
            //using (SqlCommand myCommand = new SqlCommand(query, new SqlConnection(conString)))
            //{
            //    myCommand.Connection.Open();
            //    myCommand.Parameters.AddWithValue("@RealmId", invoicedto.CompanyId);
            //    myCommand.Parameters.AddWithValue("@Employee", string.Format("{0} {1}", employeeObj.GivenName, employeeObj.FamilyName));
            //    myCommand.Parameters.AddWithValue("@Customer", string.Format("{0} {1}", CustomerObj.GivenName, CustomerObj.FamilyName));
            //    myCommand.Parameters.AddWithValue("@Item", ItemObj.Name);
            //    myCommand.Parameters.AddWithValue("@Date", dateObj);
            //    myCommand.Parameters.AddWithValue("@Hours", hoursObj);
            //    myCommand.Parameters.AddWithValue("@QboId", qboId);
            //    myCommand.ExecuteNonQuery();
            //    myCommand.Connection.Close();
            //}
        }
    }
}