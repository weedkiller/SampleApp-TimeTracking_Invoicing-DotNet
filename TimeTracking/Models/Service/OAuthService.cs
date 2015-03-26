using DevDefined.OAuth.Framework;
using DevDefined.OAuth.Consumer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimeTracking.Models;
using TimeTracking.Models.Repository;
namespace TimeTracking.Models.Service
{
    public class OAuthService
    {
        private OAuthorizationdto oAuthorizationdto = null;
        private OAuthRepository oAuthRepository = null;
        public OAuthService(OAuthorizationdto oAuthDto)
        {
           
            oAuthorizationdto = oAuthDto;
            oAuthRepository = new OAuthRepository();
        }
        public string GrantUrl(object oauthController)
        {
            oAuthorizationdto.Token = oAuthorizationdto.OAuthSession.GetRequestToken();
            oAuthorizationdto.ResponseLink = string.Format(oAuthorizationdto.ResponseFormat,
                oAuthorizationdto.AuthorizeUrl,
                oAuthorizationdto.Token.Token,
                UriUtility.UrlEncode(oAuthorizationdto.CallBackUrl));
            oAuthRepository.Save(oauthController, oAuthorizationdto);
            return oAuthorizationdto.ResponseLink;
        }
        public OAuthorizationdto IsTokenAvailable(object oauthController)
        {
            var oAuthDetails = new OAuthorizationdto();
            using (var db = new OAuthdataContext("DBContext"))
            {
                foreach (var currentIndex in db.Tokens)
                {
                    oAuthDetails.Realmid = currentIndex.realmid;
                    string testAccesToken = Utility.Decrypt(currentIndex.access_token, oAuthDetails.SecurityKey);
                    string testAccesTokenSecret = Utility.Decrypt(currentIndex.access_secret, oAuthDetails.SecurityKey);
                    oAuthDetails.AccessToken = testAccesToken;
                    oAuthDetails.AccessTokenSecret = testAccesTokenSecret;
                    oAuthDetails.IsConnected = true;
                    oAuthDetails.DataSource = currentIndex.datasource;
                    oAuthRepository.Save(oauthController, oAuthDetails);
                }
            }
            return oAuthDetails;
        }
        internal OAuthorizationdto GetRequestToken(object oauthController)
        {
            return oAuthRepository.Get(oauthController);
        }
        internal OAuthorizationdto GetAccessToken(object oauthController)
        {
            return oAuthRepository.Get(oauthController);
        }
        internal OAuthorizationdto GetAccessTokenFromServer(object oauthController,OAuthorizationdto oAuthorizationdto)
        {
            try
            {
                IToken accessToken = oAuthorizationdto.OAuthSession.ExchangeRequestTokenForAccessToken(oAuthorizationdto.Token, oAuthorizationdto.OauthVerifyer);
                oAuthorizationdto.AccessToken = accessToken.Token;
                oAuthorizationdto.AccessTokenSecret = accessToken.TokenSecret;
                oAuthorizationdto.IsConnected = true;
                oAuthRepository.Save(oauthController, oAuthorizationdto);
                return oAuthorizationdto;
            }
            catch (Exception ex)
            {
                //Handle Exception if token is rejected or exchange of Request Token for Access Token failed.
                throw ex;
            }
        }
    }
}