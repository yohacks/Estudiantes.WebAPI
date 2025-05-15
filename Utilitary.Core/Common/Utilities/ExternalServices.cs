namespace Utilitary.Core.Common.Utilities
{
    using Newtonsoft.Json;
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using Utilitary.Domine.Common;

    public class ExternalServices
    {
        private Transaction transaction;
        private object JsonResult;

        public Transaction PUTExternalService(object registrationForm, string registrationUrl, string aplicationType = "application/json")
        {
            using (var client1 = new HttpClient())
            {
                var dataExternalService = JsonConvert.SerializeObject(registrationForm).ToString();
                var content = new StringContent(dataExternalService, Encoding.UTF8, aplicationType);

                var ResponseTask = client1.PutAsync(registrationUrl, content);
                string Result = ResponseTask.Result.Content.ReadAsStringAsync().Result;

                transaction = JsonConvert.DeserializeObject<Transaction>(Result, Converter.Settings);
            }
            return transaction;
        }

        public Transaction PUTExternalSyncService(object registrationForm, string registrationUrl, string aplicationType = "application/json")
        {
            using (var client1 = new HttpClient())
            {
                var dataExternalService = JsonConvert.SerializeObject(registrationForm).ToString();
                var content = new StringContent(dataExternalService, Encoding.UTF8, aplicationType);

                var ResponseTask = client1.PutAsync(registrationUrl, content);
                string Result = ResponseTask.Result.Content.ReadAsStringAsync().Result;

                transaction = JsonConvert.DeserializeObject<Transaction>(Result, Converter.Settings);
            }
            return transaction;
        }

        public async Task<object> POSTExternalServiceTokenAsync(object registrationForm, string registrationUrl, string tokenRef, string tokenHeader, string aplicationType = "application/json")
        {
            using (var client1 = new HttpClient())
            {
                client1.BaseAddress = new Uri(registrationUrl);
                client1.DefaultRequestHeaders.Accept.Clear();
                client1.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(aplicationType));
                client1.DefaultRequestHeaders.Add(tokenHeader, tokenRef);

                var content = new StringContent(registrationForm.ToString(), Encoding.UTF8, aplicationType);

                var ResponseTask = await client1.PostAsync(registrationUrl, content);
                JsonResult = ResponseTask.Content.ReadAsStringAsync().Result;
            }
            return JsonResult;
        }

        public object POSTExternalServiceToken(object registrationForm, string registrationUrl, string tokenRef, string tokenHeader, string aplicationType = "application/json")
        {
            using (var client1 = new HttpClient())
            {
                client1.BaseAddress = new Uri(registrationUrl);
                client1.DefaultRequestHeaders.Accept.Clear();
                client1.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(aplicationType));
                client1.DefaultRequestHeaders.Add(tokenHeader, tokenRef);

                var content = new StringContent(registrationForm.ToString(), Encoding.UTF8, aplicationType);

                var ResponseTask = client1.PostAsync(registrationUrl, content);
                JsonResult = ResponseTask.Result.Content.ReadAsStringAsync().Result;
            }
            return JsonResult;
        }

        public object POSTExternalService(object registrationForm, string registrationUrl, string aplicationType = "application/json")
        {
            using (var client1 = new HttpClient())
            {
                var dataExternalService = JsonConvert.SerializeObject(registrationForm).ToString();
                var content = new StringContent(dataExternalService, Encoding.UTF8, aplicationType);

                var ResponseTask = client1.PostAsync(registrationUrl, content);
                JsonResult = ResponseTask.Result.Content.ReadAsStringAsync().Result;                
            }
            return JsonResult;
        }

        public object GETExternalService(string registrationUrl)
        {
            using (var client1 = new HttpClient())
            {
                var ResponseTask = client1.GetAsync(registrationUrl);
                JsonResult = ResponseTask.Result.Content.ReadAsStringAsync().Result;
            }

            return JsonResult;

        }

        public async Task<object> GETExternalServiceTokenAsync(string registrationUrl, string tokenRef, string tokenHeader, string aplicationType = "application/json")
        {
            using (var client1 = new HttpClient())
            {
                client1.DefaultRequestHeaders.Accept.Clear();
                client1.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(aplicationType));
                client1.DefaultRequestHeaders.Add(tokenHeader, tokenRef);

                var ResponseTask = await client1.GetAsync(registrationUrl);
                JsonResult = ResponseTask.Content.ReadAsStringAsync().Result;
            }

            return JsonResult;

        }
    }
}
