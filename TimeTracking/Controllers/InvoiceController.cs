using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeTracking.Models;
using TimeTracking.Models.DTO;
using TimeTracking.Models.Repository;
using TimeTracking.Models.Service;

namespace TimeTracking.Controllers
{
    public class InvoiceController : BaseController
    {
        InvoiceService invoiceService = null;
        Invoicedto invoicedto = new Invoicedto();
        InvoiceRepository invoiceRepository = null;
        Multiplemodels multiplemodels = null;
        // GET: Invoice
        [HttpGet]
        public ActionResult Load(Int64 id)
        {
            TimeActivitydto timeActivitydto = id > 0 ? new TimeActivityRepository().Get(this, id) : new TimeActivitydto();
            invoicedto.oAuthTokens = timeActivitydto.oAuthTokens;
            invoicedto.CustomerList = timeActivitydto.CustomerList;
            invoicedto.CompanyId = invoicedto.oAuthTokens.Realmid;
            invoicedto.ConnectionString = timeActivitydto.Syncdto.ConnectionString;
            invoicedto.TimeActivityDto = timeActivitydto;
            invoiceService = new InvoiceService(invoicedto);
            invoicedto = invoiceService.LoadPending(invoicedto);
            invoiceRepository = new InvoiceRepository();
            invoicedto = invoiceRepository.Save(this, invoicedto);
            multiplemodels = new Multiplemodels();
            multiplemodels.TimeActivityModel = timeActivitydto;
            multiplemodels.InvoiceModel = invoicedto;
            multiplemodels.SyncObjectsModel = invoicedto.TimeActivityDto.Syncdto;
            return View("Invoices", multiplemodels);
        }
        [HttpPost]
        public JsonResult Save(Int64 id, Int64 qboId)
        {
            Invoicedto invoicedto = id > 0 ? new InvoiceRepository().Get(this, id) : new Invoicedto();
            invoicedto.QboId = qboId;
            invoiceService = new InvoiceService(invoicedto);
            invoicedto = invoiceService.GenerateInvoice(invoicedto);
            invoicedto = invoiceService.UpdateDatabase(invoicedto);
            invoicedto = invoiceService.FillCreatedInvoice(invoicedto);
            invoiceRepository = new InvoiceRepository();
            invoicedto = invoiceRepository.Save(this, invoicedto);
            object data = new
            {
                Id = invoicedto.Id
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult View(Int64 id)
        {
            Invoicedto invoicedto = id > 0 ? new InvoiceRepository().Get(this, id) : new Invoicedto();
            invoiceService = new InvoiceService(invoicedto);
            invoicedto = invoiceService.LoadPending(invoicedto);
            multiplemodels = new Multiplemodels();
            multiplemodels.TimeActivityModel = invoicedto.TimeActivityDto;
            multiplemodels.InvoiceModel = invoicedto;
            multiplemodels.SyncObjectsModel = invoicedto.TimeActivityDto.Syncdto;
            return View("Invoices", multiplemodels);
        }
    }
}