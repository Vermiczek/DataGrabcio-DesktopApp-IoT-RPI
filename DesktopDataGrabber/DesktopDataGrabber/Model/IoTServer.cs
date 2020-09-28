using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DesktopDataGrabcio.Model
{
    public class IoTServer
    {
        private string ip;

        public IoTServer(string _ip)
        {
            ip = _ip;
        }

        /**
         * @brief obtaining the address of the data file from IoT server IP.
         */
        private string GetFileUrl()
        {
            return "http://" + ip + "/chartsdata.json";
        }

       
       
          // HTTP GET request using HttpClient
          
        public async Task<string> GETwithClient()
        {
            string responseText = null;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    responseText = await client.GetStringAsync(GetFileUrl());
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("NETWORK ERROR");
                Debug.WriteLine(e);
            }

            return responseText;
        }

       //Sends LED information using GET request
        public async Task<string> ClientSendLed(string x, string y, string r, string g, string b)
        {
            string responseText = null;
            string url = "http://" + ip + "/led_displayCs.php?x=" + x + "&y=" + y + "&r=" + r + "&g=" + g + "&b=" + b;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    responseText = await client.GetStringAsync(url);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("NETWORK ERROR");
                Debug.WriteLine(e);
            }

            return responseText;
        }

        //Activates the script that clears the sense hat led matrix almost instantly
        public async Task<string> ClientClearLed()
        {
            
            string responseText = null;
            string url = "http://" + ip + "/led_displayClearCs.php";
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    responseText = await client.GetStringAsync(url);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("NETWORK ERROR");
                Debug.WriteLine(e);
            }

            return responseText;
        }

       
        
        //Requests data neccessary for list view
        public async Task<string> GETwithClientTableview()
        {
            string responseText = null;
            string url = "http://" + ip + "/tableview.json";
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    responseText = await client.GetStringAsync(url);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("NETWORK ERROR");
                Debug.WriteLine(e);
            }

            return responseText;
        }
    }
}
