using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace CashierSystem.WebService
{
    internal abstract class WebServiceBase
    {
        protected object MakeHttpRequest(string url, object data)
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
                WebResponse response = request.GetResponse();

                // Get the stream containing content returned by the server.
                dataStream = response.GetResponseStream();

                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);

                // Read the content.
                string responseFromServer = reader.ReadToEnd();

                //Create Custom Response object
                CustomHttpResponse httpResult = new CustomHttpResponse
                {
                    ResponseBody = responseFromServer,
                    StatusCode = ((HttpWebResponse)response).StatusCode
                };

                // Clean up the streams.
                reader.Close();
                dataStream.Close();
                response.Close();

                //Return
                return httpResult;
            }
            catch (WebException wex)
            {
                //Log error
                throw;
            }
        }
    }
}