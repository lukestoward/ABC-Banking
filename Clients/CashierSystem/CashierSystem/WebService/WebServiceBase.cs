using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace CashierSystem.WebService
{
    internal abstract class WebServiceBase
    {
        protected HttpCustomResult MakeHttpRequest(string url, object data)
        {
            try
            {
                // Create a request using a URL that can receive a post. 
                WebRequest request = WebRequest.Create(url);
                // Set the Method property of the request to POST.
                request.Method = "POST";

                //Seralise data to json string
                var json = JsonConvert.SerializeObject(data);

                // convert it to a byte array.
                byte[] byteArray = Encoding.UTF8.GetBytes(json);

                // Set the ContentType property of the WebRequest.
                request.ContentType = "application/json";

                // Set the ContentLength property of the WebRequest.
                request.ContentLength = byteArray.Length;

                // Get the request stream.
                Stream dataStream = request.GetRequestStream();

                // Write the data to the request stream.
                dataStream.Write(byteArray, 0, byteArray.Length);

                // Close the Stream object.
                dataStream.Close();

                // Get the response.
                HttpCustomResult result = GetHttpResponse(request.GetResponse());
                
                //Return
                return result;
            }
            catch (WebException e)
            {
                //Still retrieve the response
                using (WebResponse response = e.Response)
                {
                    return GetHttpResponse(response);
                }
            }
        }

        private HttpCustomResult GetHttpResponse(WebResponse response)
        {
            HttpWebResponse httpResponse = (HttpWebResponse)response;
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);

            HttpCustomResult result = new HttpCustomResult
            {
                MessageBody = reader.ReadToEnd(),
                StatusCode = httpResponse.StatusCode
            };

            httpResponse.Close();
            dataStream?.Close();
            reader.Close();

            return result;
        }
    }

    internal class HttpCustomResult
    {
        public HttpStatusCode StatusCode { get; set; }

        public string MessageBody { get; set; }
    }
}