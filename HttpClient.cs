using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Collections.Specialized;

namespace InstaSharp
{
    public static class HttpClient
    {
        public static string GET(string uri)
        {
            try
            {
                var request = HttpWebRequest.Create(uri);
                request.Method = "GET";
                request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
                var response = request.GetResponse();
                var responseStream = UnzipResponse(response);
                return ReadResponse(responseStream);
            }
            catch (WebException ex)
            {
                //exceptions can also be zipped
                var response = ex.Response;
                var responseStream = UnzipResponse(response);
                return ReadResponse(responseStream);
            }
        }

        public static string POST(string url)
        {
            return POST(url, new Dictionary<string, string>());
        }

        public static string POST(string url, IDictionary<string, string> args)
        {
            try
            {
                return UploadValues(url, "POST", args);
            }
            catch (WebException ex)
            {
                //exceptions can also be zipped
                var response = ex.Response;
                var responseStream = UnzipResponse(response);
                return ReadResponse(responseStream);
            }
        }

        public static string DELETE(string uri)
        {
            return DELETE(uri, new Dictionary<string, string>());
        }

        public static string DELETE(string uri, IDictionary<string, string> args)
        {
            try
            {
                return UploadValues(uri, "DELETE", args);
            }
            catch (WebException ex)
            {
                //exceptions can also be zipped
                var response = ex.Response;
                var responseStream = UnzipResponse(response);
                return ReadResponse(responseStream);
            }
        }

        private static string UploadValues(string uri, string method, IDictionary<string, string> args)
        {
            NameValueCollection parameters = new NameValueCollection();
            foreach (var arg in args)
            {
                parameters.Add(arg.Key, arg.Value);
            }
            WebClient client = new WebClient();
            client.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
            //fetch
            var resultBytes = client.UploadValues(uri, method, parameters);
            //passing response
            Stream responseStream = new MemoryStream(resultBytes);
            try
            {
                if (client.ResponseHeaders[HttpResponseHeader.ContentEncoding].Equals("gzip") ||
                    client.ResponseHeaders[HttpResponseHeader.ContentEncoding].Equals("deflate"))
                {
                    responseStream = new GZipStream(responseStream, CompressionMode.Decompress);
                }
            }
            catch (Exception) {/*no zipped response*/ }
            StringBuilder result = new StringBuilder();
            using (var sr = new StreamReader(responseStream))
            {
                while (!sr.EndOfStream)
                {
                    result.AppendLine(sr.ReadLine()); //better way to hold up line breaks from response
                }
            }
            return result.ToString();
        }

        private static string ReadResponse(Stream response)
        {
            StreamReader reader = new StreamReader(response);
            string line;
            StringBuilder result = new StringBuilder();
            while ((line = reader.ReadLine()) != null)
            {
                result.AppendLine(line); //better way to hold up line breaks from response
            }
            response.Close();
            return result.ToString();
        }

        private static Stream UnzipResponse(WebResponse response)
        {
            Stream responseStream = response.GetResponseStream();
            try
            {
                if (response.Headers[HttpRequestHeader.AcceptEncoding].Equals("gzip") ||
                    response.Headers[HttpRequestHeader.AcceptEncoding].Equals("deflate"))
                {
                    responseStream = new GZipStream(responseStream, CompressionMode.Decompress);
                }
            }
            catch (Exception) { /*no zipped response*/ }
            return responseStream;
        }
    }
}
