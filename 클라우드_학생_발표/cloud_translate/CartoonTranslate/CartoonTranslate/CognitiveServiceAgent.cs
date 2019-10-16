
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CartoonTranslate
{
    public class CognitiveServiceAgent
    {
        //const string OcrEndPointV1 = "https://koreacentral.api.cognitive.microsoft.com/vision/v2.0/ocr?detectOrientation=true&language=";
        //const string OcrEndPointV2 = "https://koreacentral.api.cognitive.microsoft.com/vision/v2.0/recognizeText?mode=Printed";
        //const string VisionKey1 = "e168dc55371f4fefa417e1dd695d592c";
        //const string VisionKey2 = "28b20da03fb94384a003a178f1ad9353";
        //const string UrlContentTemplate = "{{\"url\":\"{0}\"}}";

        //const string TranslateEndPoint = "https://api.cognitive.microsoft.com/sts/v1.0/issuetoken/translate?api-version=3.0&from={0}&to={1}";
        //const string TKey1 = "7f2c25257b764b4bb4bcfacfc54ec439";
        //const string TKey2 = "07c717f46cb841749e87a0557aa754cc";
        const string OcrEndPointV1 = "https://japaneast.api.cognitive.microsoft.com/vision/v2.0/ocr?detectOrientation=true&language=";
        const string OcrEndPointV2 = "https://japaneast.api.cognitive.microsoft.com/vision/v2.0/recognizeText?mode=Printed";
        const string VisionKey1 = "108316beb0a5496fa4f0cf54b4b1a2d7";
        const string VisionKey2 = "8d4492fb6f39402288f5bc7d3e348fa5";
        const string UrlContentTemplate = "{{\"url\":\"{0}\"}}";
        //  https://api.cognitive.microsofttranslator.com";
        //string route = "/translate?api-version=3.0&to=de&to=it
        const string TranslateEndPoint = "https://api.cognitive.microsofttranslator.com/translate?api-version=3.0&to={0}";
        const string TKey1 = "b370f9ebb5f44ea2baf49a76ed1dd41d";
        const string TKey2 = "393fdda05502439aa0b87a6fa5f187e3";

        public static async Task<List<string>> DoTranslate(List<string> text, string fromLanguage, string toLanguage)
        {
            try
            {
                using (HttpClient hc = new HttpClient())
                {
                    hc.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", TKey1);
                    string jsonBody = CreateJsonBodyElement(text);
                    StringContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                    string uri = string.Format(TranslateEndPoint, toLanguage);
                    HttpResponseMessage resp = await hc.PostAsync(uri, content);
                    string json = await resp.Content.ReadAsStringAsync();
                    var ro = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TranslateResult.Class1>>(json);
                    List<string> list = new List<string>();
                    foreach (TranslateResult.Class1 c in ro)
                    {
                        list.Add(c.translations[0].text);
                    }
                    return list;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        private static string CreateJsonBodyElement(List<string> text)
        {
            var a = text.Select(t => new { Text = t }).ToList();
            var b = JsonConvert.SerializeObject(a);
            return b;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="imageUrl"></param>
        /// <param name="language">en, ja, zh</param>
        /// <returns></returns>
        public static async Task<OcrResult.Rootobject> DoOCR(string imageUrl, string language)
        {
            try
            {
                using (HttpClient hc = new HttpClient())
                {
                    ByteArrayContent content = CreateHeader(hc, imageUrl);
                    var uri = OcrEndPointV1 + language;
                    HttpResponseMessage resp = await hc.PostAsync(uri, content);
                    string result = string.Empty;
                    if (resp.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string json = await resp.Content.ReadAsStringAsync();
                        Debug.WriteLine(json);
                        OcrResult.Rootobject ro = Newtonsoft.Json.JsonConvert.DeserializeObject<OcrResult.Rootobject>(json);
                        return ro;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
                return null;
            }
        }

        private static ByteArrayContent CreateHeader(HttpClient hc, string imageUrl)
        {
            hc.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", VisionKey1);
            string body = string.Format(UrlContentTemplate, imageUrl);
            byte[] byteData = Encoding.UTF8.GetBytes(body);
            var content = new ByteArrayContent(byteData);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return content;
        }
    }
}