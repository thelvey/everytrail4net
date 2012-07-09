using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;
using System.Net;

namespace EveryTrailNET.Core
{
    public interface IRequestHandler
    {
        EveryTrailResponse MakeRequest(string endPoint, List<EveryTrailRequestParameter> requestParams);
    }
    public class EveryTrailRequestParameter
    {
        public string Name { get; set; }
        public object Value { get; set; }
    }
    public class EveryTrailResponse
    {
        public Stream ResponseStream { get; set; }
        public bool SuccessfulConnection { get; set; }
    }
    public static class EveryTrailRequest
    {
        private static IRequestHandler _requestHandler = new DefaultEveryTrailRequestHandler();

        public static void SetImplementation(IRequestHandler handler)
        {
            _requestHandler = handler;
        }

        public static EveryTrailResponse MakeRequest(string endPoint, List<EveryTrailRequestParameter> requestParams)
        {
            return _requestHandler.MakeRequest(endPoint, requestParams);
        }
    }
    public class DefaultEveryTrailRequestHandler : IRequestHandler
    {
        private static readonly string _apiBaseUrl = "http://www.everytrail.com";

        public EveryTrailResponse MakeRequest(string endPoint, List<EveryTrailRequestParameter> requestParams)
        {
            string url = _apiBaseUrl + endPoint + "?";
            foreach (EveryTrailRequestParameter p in requestParams)
            {
                url += p.Name + "=" + p.Value.ToString() + "&";
            }

            string key = String.Empty;
            string secret = String.Empty;

            if (String.IsNullOrEmpty(ConfigurationManager.AppSettings["Key"]))
            {
                throw new Exception("Must provide key in configuration");
            }
            else
            {
                key = ConfigurationManager.AppSettings["Key"];
            }
            if (String.IsNullOrEmpty(ConfigurationManager.AppSettings["Secret"]))
            {
                throw new Exception("Must provide secret in configuration");
            }
            else
            {
                secret = ConfigurationManager.AppSettings["Secret"];
            }

            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["Version"]))
            {
                url += "&version=" + ConfigurationManager.AppSettings["Version"];
            }

            HttpWebRequest r = (HttpWebRequest)WebRequest.Create(url);
            NetworkCredential nc = new NetworkCredential(key, secret);
            r.Credentials = nc;

            EveryTrailResponse etResponse = new EveryTrailResponse();

            try
            {
                WebResponse response = r.GetResponse();
                Stream s = response.GetResponseStream();
                etResponse.ResponseStream = s;
                etResponse.SuccessfulConnection = true;
            }
            catch (Exception exc)
            {
                etResponse.SuccessfulConnection = false;
            }
            return etResponse;
        }
    }

}
