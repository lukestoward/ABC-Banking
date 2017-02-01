using System;
using System.Collections.Generic;
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

        public WithdrawResponse HandleWithdrawRequest(WithdrawFunds model)
        {
            try
            {

                string badStaticStringUrl = "http://localhost:2975/api/Withdraw/ProcessWithdrawATM";
                //1. validate request values


                //2. make the request to the web service
                CustomHttpResponse response = MakeHttpPostRequest(badStaticStringUrl, model);

                WithdrawResponse verdict = new WithdrawResponse();

                //Check for errors
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    //Handle service error
                    verdict.Approved = false;
                    return verdict;
                }
                
                //3. Should have status code 200 (approved)
                verdict.Approved = true;

                return verdict;
            }
            catch (Exception ex)
            {
                //Log error
                throw;
            }
        }

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
        }

        public List<MiniStatementItem> HandleMiniStatmentRequest(MiniStatementRequestDTO model)
        {
            string badHardCodedStringUrl = "http://localhost:56698/api/Statement/GetMiniStatement";

            CustomHttpResponse response = MakeHttpPostRequest(badHardCodedStringUrl, model);

            //Check for errors
            if (response.StatusCode != HttpStatusCode.OK)
            {
                //Handle service error
            }

            //Deserialise response
                List<MiniStatementItem> responseObject =
                    JsonConvert.DeserializeObject<List<MiniStatementItem>>(response.ResponseBody);

            return responseObject;
        }

        private CustomHttpResponse MakeHttpPostRequest(string url, object data)
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
            catch (Exception ex)
            {
                //Log error
                throw;
            }
        }
    }
}