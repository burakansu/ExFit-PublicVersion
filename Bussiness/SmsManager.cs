using Data.Entities.ExFit;
using System.Net;
using System.Text;
using Task = System.Threading.Tasks.Task;

namespace Business
{
    public class SmsManager : ManagerBase
    {
        public async Task<string> XmlPostSms(string PostAdress, string XmlData)
        {
            try
            {
                var res = "";
                byte[] bytes = Encoding.UTF8.GetBytes(XmlData);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(PostAdress);
                request.Method = "POST";
                request.ContentLength = bytes.Length;
                request.ContentType = "text/xml";
                request.Timeout = 30000000;
                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(bytes, 0, bytes.Length);
                }
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        string message = string.Format(
                            "Post failed. Received HTTP {0}",
                            response.StatusCode);
                        throw new Exception(message);
                    }
                    Stream responseStream = response.GetResponseStream();
                    using (StreamReader reader = new StreamReader(responseStream))
                    {
                        res = reader.ReadToEnd();
                    }
                    return res;
                }
            }
            catch (Exception)
            {
                return "Sms Gönderilirken Bir Hata Oluştu.";
            }
        }
        public async Task SmsSender(string CompanyName, string SmsText, List<Member> Members = null, List<User> Users = null)
        {
            String Xml = "<request>";
            Xml += "<authentication>";
            Xml += "<username> **** </username>";
            Xml += "<password> **** </password>";
            Xml += "</authentication>";
            Xml += "<order>";
            Xml += $"<sender> {CompanyName} </sender>";
            Xml += "<message>";
            Xml += $"<text>< {SmsText} ></text>";
            Xml += "<receipts>";
            if (Members != null)
            {
                foreach (var item in Members)
                {
                    if (item.Phone.Substring(0, 1) == "0")
                        item.Phone = item.Phone.Substring(1, item.Phone.Length - 1);
                    Xml += $"<number> {item.Phone} </number>";
                }
            }
            if (Users != null)
            {
                foreach (var item in Users)
                {
                    if (item.Phone.Substring(0, 1) == "0")
                        item.Phone = item.Phone.Substring(1, item.Phone.Length - 1);
                    Xml += $"<number> {item.Phone} </number>";
                }
            }
            Xml += "</receipents>";
            Xml += "</message>";
            Xml += "</order>";
            Xml += "</request>";
            this.XmlPostSms("https://api.iletimerkezi.com/v1/send-sms", Xml);
        }
    }
}
