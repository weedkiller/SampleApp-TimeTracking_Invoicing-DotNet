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
       
    }
}