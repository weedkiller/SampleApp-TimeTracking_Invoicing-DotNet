using System;
using System.Collections.Generic;

using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeTracking.Models;
using TimeTracking.Models.Repository;
using TimeTracking.Models.Service;

namespace TimeTracking.Controllers
{
    public class HomeController : BaseController
    {
        Multiplemodels multiplemodels = null;
        SyncRepository syncRepo = new SyncRepository();
        public ActionResult Index()
        {
            multiplemodels = new Multiplemodels();
            multiplemodels.SyncObjectsModel = new Syncdto();
            multiplemodels.OAuthorizationModel = new OAuthorizationdto();
            var oAuthModel = new OAuthService(multiplemodels.OAuthorizationModel).IsTokenAvailable(this);
            if (oAuthModel.IsConnected)
            {
                multiplemodels.OAuthorizationModel = oAuthModel;
                var syncService = new SyncService(oAuthModel);
                multiplemodels.SyncObjectsModel.OauthToken = oAuthModel;
                multiplemodels.SyncObjectsModel = syncService.IsEmpSync(multiplemodels.SyncObjectsModel, syncService);
                multiplemodels.SyncObjectsModel = syncService.IsCustSync(multiplemodels.SyncObjectsModel, syncService);
                multiplemodels.SyncObjectsModel = syncService.IsServiceItemSync(multiplemodels.SyncObjectsModel, syncService);
                multiplemodels.SyncObjectsModel.CompanyId = oAuthModel.Realmid;
                multiplemodels.SyncObjectsModel = syncRepo.Save(this, multiplemodels.SyncObjectsModel);
                return View(multiplemodels);
            }
            else
            {
                return View(multiplemodels);
            }
        }

        public ActionResult Time()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Invoices()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Close()
        {
            return View();
        }
        public ActionResult Sync(int id, bool isConnected)
        {
            multiplemodels = new Multiplemodels();
            if (multiplemodels != null)
            {
                multiplemodels.SyncObjectsModel = new SyncRepository().Get(this, id);
                multiplemodels.OAuthorizationModel = new OAuthorizationdto();
                multiplemodels.OAuthorizationModel.IsConnected = isConnected;
                return View("Index", multiplemodels);
            }
            else
            {
                return View("Index");
            }
        }
    }
}