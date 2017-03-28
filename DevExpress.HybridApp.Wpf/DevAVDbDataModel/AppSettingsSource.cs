using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using DevExpress.DevAV.Common.DataModel.WebApi;
using IGS.Data.Model;
using static DevExpress.DevAV.Properties.Settings;

namespace DevExpress.DevAV.DevAVDbDataModel
{
    public class AppSettingsSource : WebApiSourceBase<AppSettings> 
    {
        public AppSettingsSource() : base(CreateHttpClientContext)
        {
        }

        protected static HttpClient CreateHttpClientContext()
        {
            var baseadd = (Uri)Default[Default.CurrentService] ?? Default.EnvironmentUri;
            var handler = new HttpClientHandler
            {
                CookieContainer = new CookieContainer(),
                PreAuthenticate = true,
                UseDefaultCredentials = true,
                UseCookies = true
            };

            return new HttpClient(handler) {BaseAddress = baseadd};
        }

        protected override Dictionary<System.Data.Entity.EntityState, string> GetRoutes()
        {
            //NOTE: Only supply the Unchanged route for readonly sources.
            return new Dictionary<System.Data.Entity.EntityState, string>
            {
                [System.Data.Entity.EntityState.Unchanged] = Default.Production.OriginalString 
            };
        }
    }
}