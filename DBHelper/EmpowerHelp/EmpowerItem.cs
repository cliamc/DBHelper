using System;
using System.Net;
using System.Web;

namespace DBHelper.EmpowerHelp
{
    public class EmpowerItem
    {
        const string TestURL = "http://amc-plm-app.a-m-c.com/Omnify7Test/Apps/Toolkit/EmpowerConsumer.ashx?op=get-item&";
        const string ProdURL = "http://amc-plm-app.a-m-c.com/Omnify7/Apps/Toolkit/EmpowerConsumer.ashx?op=get-item&";

        const string TestURLbom = "http://amc-plm-app.a-m-c.com/Omnify7Test/Apps/Toolkit/EmpowerConsumer.ashx?op=get-bom&";
        const string ProdURLbom = "http://amc-plm-app.a-m-c.com/Omnify7/Apps/Toolkit/EmpowerConsumer.ashx?op=get-bom&";

        // Request parameter sample: pn=100A25M&rev=0.01
        // DR100RE20A8BDCC  0.04

        private string PartNumber = "";
        private string Revision = "";

        WebClient client = new WebClient();                         // May use HttpClient.GetStringAsync(uri)
        private string Result = "";

        public EmpowerItem(string PN, string Rev)
        {
            this.PartNumber = PN;
            this.Revision = Rev;

            client.Headers.Add("User-Agent", "Read a Web Page");
        }

        public void SendRequest()
        {
            string url = "";
#if DEBUG
            url = TestURL;
#else
            url = ProdURL;
#endif
            url = url + "pn=" + this.PartNumber + "&rev=" + this.Revision;

            //WebClient client = new WebClient();
            //client.Headers.Add("User-Agent", "Read a Web Page");

            this.Result = client.DownloadString(url);
        }

        public void SendRequestBOM()
        {
            string url = "";
#if DEBUG
            url = TestURLbom;
#else
            url = ProdURLbom;
#endif
            url = url + "pn=" + this.PartNumber + "&rev=" + this.Revision;
            this.Result = client.DownloadString(url);
        }

        public void SendRequest(string handler)                                     /// e.g. "http://amc-plm-app.a-m-c.com/Omnify7/Apps/Toolkit/EmpowerConsumer.ashx?op=get-item&"
        {
            string url = handler + "pn=" + this.PartNumber + "&rev=" + this.Revision;

            //WebClient client = new WebClient();
            //client.Headers.Add("User-Agent", "Read a Web Page");

            this.Result = client.DownloadString(url);
        }

        public void SendItemRequestAsync(string handler)                                     /// e.g. "http://amc-plm-app.a-m-c.com/Omnify7/Apps/Toolkit/EmpowerConsumer.ashx?op=get-item&"
        {
            // Need to do URL-encoding to handle '+' in front of some part numbers. cli, 5/21/2020
            string pnEnc = HttpUtility.UrlEncode(this.PartNumber);
            string url = handler + "pn=" + pnEnc + "&rev=" + this.Revision;
            Uri iUri = new Uri(url);

            //WebClient client = new WebClient();
            //client.Headers.Add("User-Agent", "Read a Web Page");

            client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(GotDataEventHandler);
            client.DownloadStringAsync(iUri);
        }

        public void SendBOMRequestAsync(string handler)                                     // e.g. "http://amc-plm-app.a-m-c.com/Omnify7/Apps/Toolkit/EmpowerConsumer.ashx?op=get-bom&"
        {                                                                                   //       http://amc-plm-app.a-m-c.com/Omnify7Test/Apps/Toolkit/EmpowerConsumer.ashx?op=get-bom&pn=100A25M&rev=0.01
            // Need to do URL-encoding to handle '+' in front of some part numbers. cli, 5/21/2020
            string pnEnc = HttpUtility.UrlEncode(this.PartNumber);
            string url = handler + "pn=" + pnEnc + "&rev=" + this.Revision;
            Uri iUri = new Uri(url);

            //WebClient client = new WebClient();
            //client.Headers.Add("User-Agent", "Read a Web Page");

            client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(GotDataEventHandler);
            client.DownloadStringAsync(iUri);
        }

        private void GotDataEventHandler(object sender, DownloadStringCompletedEventArgs e)
        {
            if (!e.Cancelled && e.Error == null)
            {
                this.Result = (string)e.Result;
            }            
        }

        public bool IsClientBusy()
        {
            bool ret = false;
            if (client.IsBusy)
                ret = true;

            return ret;
        }

        public void CancelSendRequestAsync()
        {
            client.CancelAsync();
        }

        public string GetResult()
        {
            return this.Result;
        }

    } // class
}
