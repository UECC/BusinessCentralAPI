using BCAPI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace HttpClientSample
{
   
    class Program
    {
        static HttpClient client = new HttpClient();
        static Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

        static string m_exePath = string.Empty;

        internal class Token
        {
            [JsonProperty("access_token")]
            public string AccessToken { get; set; }

            [JsonProperty("token_type")]
            public string TokenType { get; set; }

            [JsonProperty("expires_in")]
            public int ExpiresIn { get; set; }

            [JsonProperty("refresh_token")]
            public string RefreshToken { get; set; }

            [JsonProperty("scope")]
            public string Scope { get; set; }
        }

        private static async Task<Token> GetElibilityToken(HttpClient client)
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls | (SecurityProtocolType)3072;
            string baseAddress = @"https://login.microsoftonline.com/5e340b8a-764b-443a-bb18-318ef9c6a1f0/oauth2/v2.0/token";

            string grant_type = "client_credentials";
            string client_id = "{4d9267c6-92da-4fd9-9978-c99f009857dd}";
            string client_secret = "QVN7Q~OuxQJkCZta9X5zoLOKsM.e6-bqpSlZe";
            string scope  = "https://api.businesscentral.dynamics.com/.default";

            var form = new Dictionary<string, string>
                {
                    {"grant_type", grant_type},
                    {"client_id", client_id},
                    {"client_secret", client_secret},
                    {"scope", scope},
                };

            HttpResponseMessage tokenResponse = await client.PostAsync(baseAddress, new FormUrlEncodedContent(form));
            var jsonContent = await tokenResponse.Content.ReadAsStringAsync();
            Token tok = JsonConvert.DeserializeObject<Token>(jsonContent);
            return tok;
        }

        public static async Task<string> GetAuthorizeToken()
        {
            // Initialization.  
            string responseObj = string.Empty;            
            // Posting.  
            using (var client = new HttpClient())
            {
                // Setting Base address.  
                client.BaseAddress = new Uri("https://login.microsoftonline.com/5e340b8a-764b-443a-bb18-318ef9c6a1f0/oauth2/v2.0/token");

                // Setting content type.  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));                
                // Initialization.  
                HttpResponseMessage response = new HttpResponseMessage();
                List<KeyValuePair<string, string>> allIputParams = new List<KeyValuePair<string, string>>();

                // Convert Request Params to Key Value Pair.                  
                // URL Request parameters.  
                HttpContent requestParams = new FormUrlEncodedContent(allIputParams);

                // HTTP POST  
                response = await client.PostAsync("Token", requestParams).ConfigureAwait(false);

                // Verification  
                if (response.IsSuccessStatusCode)
                {
                    // Reading Response.  
                    
                }
            }

            return responseObj;
        }

        static async Task<bool> CreatePurchaseOrderAsync(POS purchaseOrder)
        {
            bool status = true;
            HttpResponseMessage response = null;
            String path;
            foreach (PO po in purchaseOrder.PO)
            {

                path = config.AppSettings.Settings["APIPath"].Value + "purchaseOrders?$filter=number eq '" + po.number + "'";                
                HttpResponseMessage responseGet = await client.GetAsync(path);                  

                if (responseGet.Content.ReadAsStringAsync().Result.Split(',').Length < 2) //More than 2 characters contains data, so order exists
                {
                    response = await client.PostAsJsonAsync("purchaseOrders?$expand=purchaseOrderLines", po);                    
                    //if (!response.IsSuccessStatusCode && response.StatusCode.ToString() != "NotFound")
                    if (!response.IsSuccessStatusCode)
                    {
                        status = false;                        
                            Log("PO:" + po.number + "=>" + response.Content.ReadAsStringAsync().Result.Split('"')[9].ToString());
                    }
                }
                
            }
            return status;
        }
      
        static async Task<bool> DeletePurchaseOrderAsync(string purchaseOrder)
        {
            bool status = true;
            HttpResponseMessage response = null;            
            //foreach (PO po in purchaseOrder.PO)
            //{ 
                response = await client.PostAsync(config.AppSettings.Settings["APIPOCancelEndPoint"].Value.Replace("{PONumber}", "'" + purchaseOrder + "'"),null);                
                if (!response.IsSuccessStatusCode)
                {
                    status = false;                    
                     Log("POCancel:" + purchaseOrder + "=>" + response.ReasonPhrase);                    
                }
            //}
            return status;
        }

        static async Task<bool> CreateSalesJournalAsync(JOURNALS journals)
        {
            bool status = true;
            HttpResponseMessage response = null;
            String path = "";

            foreach (JOURNAL jou in journals.JOURNAL)
            {
                path = config.AppSettings.Settings["APIPath"].Value + "ournals?$expand=journalLines&$filter=templateDisplayName eq 'SALES' and code eq '" + jou.code + "'";
                HttpResponseMessage responseGet = await client.GetAsync(path);

                if (responseGet.Content.ReadAsStringAsync().Result.Split(',').Length <= 2) //More than 2 characters contains data, so order exists
                {
                    response = await client.PostAsJsonAsync("journals?$expand=journalLines", jou);                    
                    if (!response.IsSuccessStatusCode)// && response.StatusCode.ToString() != "NotFound")
                    {
                        status = false;
                            Log("Journal:" + jou.code + "=>" + response.Content.ReadAsStringAsync().Result.Split('"')[9].ToString());
                    }
                }
            }
            return status;
        }

        
        //static void Log(string logMessage, TextWriter txtWriter)
        //{
        //    try
        //    {
        //        txtWriter.WriteLine("{0} {1}  :{2}", DateTime.Now.ToShortDateString(), DateTime.Now.ToLongTimeString(), logMessage);
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}

        static void Log(string logMessage)
        {
            m_exePath = config.AppSettings.Settings["LogFile"].Value;
            try
            {                
                    using (StreamWriter txtWriter = File.AppendText(m_exePath))
                    {
                    txtWriter.WriteLine("{0} {1}  :{2}", DateTime.Now.ToShortDateString(), DateTime.Now.ToLongTimeString(), logMessage);
                    }
            }
            catch (Exception ex)
            {
            }
        }

        static void Main()
        {
            RunAsync().GetAwaiter().GetResult();
        }

        static async Task RunAsync()
        {
            // Generate Authorize Access Token to authenticate REST Web API.              
            //Reference: https://stackoverflow.com/questions/38494279/how-do-i-get-an-oauth-2-0-authentication-token-in-c-sharp
            client = new HttpClient();
            Token token = Program.GetElibilityToken(client).Result;
            // Process response access token info.  
            // Call REST Web API method with authorize access token.  
            // Process Result
            var authValue = new AuthenticationHeaderValue("Bearer", token.AccessToken.ToString());
            client = new HttpClient()
            {
                DefaultRequestHeaders = { Authorization = authValue },
                BaseAddress = new Uri(config.AppSettings.Settings["ApiPath"].Value)
            };

            bool status;
            client = new HttpClient()
            {
                DefaultRequestHeaders = { Authorization = authValue },
                BaseAddress = new Uri(config.AppSettings.Settings["ApiPath"].Value)
            };


            try
            {
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls | (SecurityProtocolType)3072;                
                String xmlDocumentText;

                //Purchase Order
                string POArchiveFolder = config.AppSettings.Settings["POArchiveFolder"].Value;
                string POErrorFolder = config.AppSettings.Settings["POErrorFolder"].Value;
                
                foreach (string file in Directory.EnumerateFiles(config.AppSettings.Settings["POFolder"].Value, "*.xml"))
                {
                    xmlDocumentText = File.ReadAllText(file);
                    XmlSerializer serializer = new XmlSerializer(typeof(POS));

                    StringReader reader = new StringReader(xmlDocumentText);

                    POS pos = (POS)(serializer.Deserialize(reader));

                    status = await CreatePurchaseOrderAsync(pos);                   
                    

                    if (status)
                    {
                        //Move to Archive
                        System.IO.File.Move(file, POArchiveFolder + Path.GetFileName(file));
                    }
                    else
                    {
                        //Move to Error
                        System.IO.File.Move(file, POErrorFolder + Path.GetFileName(file));
                    }
                }


                //Purchase (Supplier) Invoice
                string PIArchiveFolder = config.AppSettings.Settings["PIArchiveFolder"].Value;
                string PIErrorFolder = config.AppSettings.Settings["PIErrorFolder"].Value;

                foreach (string file in Directory.EnumerateFiles(config.AppSettings.Settings["PIFolder"].Value, "*.xml"))
                {
                    xmlDocumentText = File.ReadAllText(file);
                    XmlSerializer serializer = new XmlSerializer(typeof(POS));

                    StringReader reader = new StringReader(xmlDocumentText);

                    POS pos = (POS)(serializer.Deserialize(reader));

                    status = await CreatePurchaseOrderAsync(pos);


                    if (status)
                    {
                        //Move to Archive
                        System.IO.File.Move(file, PIArchiveFolder + Path.GetFileName(file));
                    }
                    else
                    {
                        //Move to Error
                        System.IO.File.Move(file, PIErrorFolder + Path.GetFileName(file));
                    }
                }

                //Purchase Order Cancel
                string POCancelArchiveFolder = config.AppSettings.Settings["POCancelArchiveFolder"].Value;
                string POCancelErrorFolder = config.AppSettings.Settings["POCancelErrorFolder"].Value;
                foreach (string file in Directory.EnumerateFiles(config.AppSettings.Settings["POCancelFolder"].Value, "*.xml"))
                {
                    xmlDocumentText = File.ReadAllText(file);
                    XmlSerializer serializer = new XmlSerializer(typeof(PO));

                    StringReader reader = new StringReader(xmlDocumentText);

                    PO pos = (PO)(serializer.Deserialize(reader));
                    status = await DeletePurchaseOrderAsync(pos.number);

                    if (status)
                    {
                        //Move to Archive
                        System.IO.File.Move(file, POCancelArchiveFolder + Path.GetFileName(file));
                    }
                    else
                    {
                        //Move to Error
                        System.IO.File.Move(file, POCancelErrorFolder + Path.GetFileName(file));

                    }

                }
               

                //Sales Journal
                string JournalArchiveFolder = config.AppSettings.Settings["JournalArchiveFolder"].Value;
                string JournalErrorFolder = config.AppSettings.Settings["JournalErrorFolder"].Value;
                foreach (string file in Directory.EnumerateFiles(config.AppSettings.Settings["JournalFolder"].Value, "*.xml"))
                {
                    xmlDocumentText = File.ReadAllText(file);
                    XmlSerializer serializer = new XmlSerializer(typeof(JOURNALS));

                    StringReader reader = new StringReader(xmlDocumentText);

                    JOURNALS journal = (JOURNALS)(serializer.Deserialize(reader));
                    status  = await CreateSalesJournalAsync(journal);

                    if (status)
                    {
                        //Move to Archive
                        System.IO.File.Move(file, JournalArchiveFolder + Path.GetFileName(file));
                    }
                    else
                    {
                        //Move to Error
                        if(!System.IO.File.Exists(JournalErrorFolder + Path.GetFileName(file)))
                        {
                            System.IO.File.Move(file, JournalErrorFolder + Path.GetFileName(file));
                        }
                        
                    }
                }
            }
            catch (Exception e)
            {   
                status = false;
                Log(e.Message);
                
            }            
        }
    }
}