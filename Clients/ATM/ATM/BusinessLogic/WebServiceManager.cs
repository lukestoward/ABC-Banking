using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ATM.Models;
using Newtonsoft.Json;

namespace ATM.BusinessLogic
{
    public class WebServiceManager
    {
        public WebServiceManager()
        {

        }

        //public async Task<object> HandleWithdrawRequest(WithdrawFunds request)
        //{
        //    try
        //    {
        //        string badStaticStringUrl = "https://localhost:3278/";

        //        //1. validate request values
        //        //2. make the request to the web service
        //        //3. handle the response
        //        //4. return something useful to the controller
        //    }
        //    catch (Exception ex)
        //    {
                
        //        throw;
        //    }
        //}

        public AccountBalanceDTO HandleGetBalanceRequest(string accNumber, string sortCode)
        {
            string badHardCodedStringUrl = "http://localhost:56698/api/Account/GetAccountBalance";

            BalanceRequestDTO data = new BalanceRequestDTO
            {
                AccountNumber = accNumber,
                SortCode = sortCode
            };

            CustomHttpResponse response = MakeHttpPostRequest(badHardCodedStringUrl, data);

            //Check for errors
            if (response.StatusCode != HttpStatusCode.OK)
            {
                //Handle service error
            }

            //Deserialise response
            try
            {
                AccountBalanceDTO responseObject =
                    JsonConvert.DeserializeObject<AccountBalanceDTO>(response.ResponseBody);

                return responseObject;
            }
            catch (Exception ex)
            {
                return null;
            }

            return null;
        }

        private CustomHttpResponse MakeHttpPostRequest(string url, object data)
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
    }
}