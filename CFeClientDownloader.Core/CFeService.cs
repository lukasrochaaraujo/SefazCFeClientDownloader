using HTTPRequest;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CFeClientDownloader.Core
{
    public class CFeService
    {
        private readonly HttpRequest HttpRequest;
        private readonly string AuthToken;
        private readonly string MFeSerialNumber;
        private readonly string PathToSaveCFeXmls;
        private readonly Dictionary<string, string> CFeDictionary;
        private int MinutesBetweenQuery = 5;

        public CFeService(string authTaxid, string authToken, string mfeSerieNumber, string pathToSaveCFeXML)
        {
            CFeDictionary = new Dictionary<string, string>();
            MFeSerialNumber = mfeSerieNumber;
            PathToSaveCFeXmls = pathToSaveCFeXML;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            AuthToken = authToken;
            HttpRequest = new HttpRequest();
            HttpRequest.AppendHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.169 Safari/537.36");
            HttpRequest.AppendHeader("X-AUTHENTICATION-TAXID", authTaxid);
            HttpRequest.AppendHeader("X-AUTHENTICATION-TOKEN", AuthToken);
            HttpRequest.AppendHeader("Origin", "http://cfe.sefaz.ce.gov.br");
            HttpRequest.AppendHeader("Referer", "http://cfe.sefaz.ce.gov.br/mfe/portal");
            HttpRequest.AppendHeader("DNT", "1");
        }

        public async Task DownloadBetweenDateAsync(DateTime startDate, DateTime endDate, CFeOperationStatus cfeStatus = CFeOperationStatus.ALL)
        {
            string startDateString,
                   endDateString,
                   cfeOperationStatus = cfeStatus == CFeOperationStatus.ALL ? "" : cfeStatus.ToString();
            do
            {
                startDateString = startDate.ToString("yyyy-MM-dd+HH:mm:ss");
                startDate = startDate.AddMinutes(MinutesBetweenQuery);
                endDateString = startDate.ToString("yyyy-MM-dd+HH:mm:ss");
                var reponse = await HttpRequest.GETAsync<IEnumerable<SefazAPIResponse>>($"https://cfe.sefaz.ce.gov.br:8443/portalcfews/mfe/fiscal-coupons/extract?count=100&endDate={endDateString}&page=1&serialNumber={MFeSerialNumber}&startDate={startDateString}&type={cfeOperationStatus}");
                var cfeArray = reponse.SelectMany(r => r.CFeCollection).ToArray();
                foreach (CFeDto cFeDto in cfeArray)
                    CFeDictionary.TryAdd(cFeDto.CupomId, cFeDto.AccessKey);
            }
            while (startDate < endDate);

            foreach (var cfe in CFeDictionary)
            {
                var cfeContentXml = await HttpRequest.GETAsync<string>($"https://cfe.sefaz.ce.gov.br:8443/portalcfews/mfe/fiscal-coupons/xml/{cfe.Value}?apiKey={AuthToken}");
                File.WriteAllText($@"{PathToSaveCFeXmls}\{cfe.Value}.xml", cfeContentXml);
            }
        }
    }
}
