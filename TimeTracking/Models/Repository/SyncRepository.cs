using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TimeTracking.Models.Repository
{
    public class SyncRepository
    {
        Dictionary<Int64, Syncdto> syncRepo = null;
        Controller syncController = null;
        public SyncRepository()
        {
            syncRepo = new Dictionary<Int64, Syncdto>();
        }
        internal Syncdto Get(object controller, Int64 id)
        {
            syncController = controller as System.Web.Mvc.Controller;
            Dictionary<Int64, Syncdto> syncRepo = syncController.TempData["Sync"] as Dictionary<Int64, Syncdto>;
            return syncRepo[id];

        }
        internal Syncdto Save(object controller, Syncdto syncObjects)
        {
            syncController = controller as Controller;
            Random random = new Random();
            syncObjects.Id = random.Next(1,100);
            syncRepo.Add(syncObjects.Id, syncObjects);
            syncController.TempData["Sync"] = syncRepo;
            return syncObjects;
        }
    }
}