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
    public class HomeController : BaseController
    {
        Multiplemodels multiplemodels = null;
        SyncRepository syncRepo = new SyncRepository();
        public ActionResult Index()
        {
            multiplemodels = new Multiplemodels();
            multiplemodels.SyncObjectsModel = new Syncdto();
            multiplemodels.OAuthorizationModel = new OAuthorizationdto();
            multiplemodels.TimeActivityModel = new TimeActivitydto();
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

      

        public ActionResult Invoices()
        {
            multiplemodels = new Multiplemodels();
            multiplemodels.OAuthorizationModel = new OAuthorizationdto();
            multiplemodels.SyncObjectsModel = new Syncdto();
            multiplemodels.TimeActivityModel = new TimeActivitydto();
            return View(multiplemodels);
        }
        /// <summary>
        /// Changes made
        /// </summary>
        /// <returns></returns>
        public ActionResult Close()
        {
            multiplemodels = new Multiplemodels();
            multiplemodels.OAuthorizationModel = new OAuthorizationdto();
            multiplemodels.SyncObjectsModel = new Syncdto();
            multiplemodels.TimeActivityModel = new TimeActivitydto();
            return View(multiplemodels);
        }
        public ActionResult Sync(int id, bool isConnected)
        {
            multiplemodels = new Multiplemodels();
            if (multiplemodels != null)
            {
                multiplemodels.SyncObjectsModel = new SyncRepository().Get(this, id);
                multiplemodels.OAuthorizationModel = new OAuthorizationdto();
                multiplemodels.OAuthorizationModel.IsConnected = isConnected;
                multiplemodels.TimeActivityModel = new TimeActivitydto();
                return View("Index", multiplemodels);
            }
            else
            {
                return View("Index");
            }
        }
    }
}