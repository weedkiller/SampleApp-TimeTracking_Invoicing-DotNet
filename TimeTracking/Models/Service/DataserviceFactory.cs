using Intuit.Ipp.Core;
using Intuit.Ipp.DataService;
using Intuit.Ipp.Security;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace TimeTracking.Models
{
    /// <summary>
    /// /
    /// </summary>
    public class DataserviceFactory
    {
        private OAuthRequestValidator oAuthRequestValidator = null;
        private DataService dataService = null;
        IntuitServicesType intuitServicesType = new IntuitServicesType();
        private ServiceContext serviceContext = null;
        public ServiceContext getServiceContext { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oAuthorization"></param>
        public DataserviceFactory(OAuthorizationdto oAuthorization)
        {
            try
            {
                oAuthRequestValidator = new OAuthRequestValidator(
                oAuthorization.AccessToken, 
                oAuthorization.AccessTokenSecret, 
                oAuthorization.ConsumerKey, 
                oAuthorization.ConsumerSecret);
            intuitServicesType = oAuthorization.DataSource == "QBO" ? IntuitServicesType.QBO : IntuitServicesType.None;
            serviceContext = new ServiceContext(oAuthorization.Realmid.ToString(), intuitServicesType, oAuthRequestValidator);
            serviceContext.IppConfiguration.BaseUrl.Qbo = ConfigurationManager.AppSettings["ServiceContext.BaseUrl.Qbo"];
            serviceContext.IppConfiguration.Logger.RequestLog.EnableRequestResponseLogging = true;
            serviceContext.IppConfiguration.Logger.RequestLog.ServiceRequestLoggingLocation = ConfigurationManager.AppSettings["ServiceRequestLoggingLocation"];
            getServiceContext = serviceContext;
            dataService = new DataService(serviceContext);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataService getDataService()
        {
            return dataService;
        }
    }
}