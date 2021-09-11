using ERP.Api;
using IFA.Domain.Models;
using IFA.Domain.Utils;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IFA.Api.Utils
{
    public class PushNotification
    {
        public PushNotification(pushnotif obj)
        {
            var path = Path.Combine(Startup.contentRoot, "appsettings.json");
            try
            {
                var configurationBuilder = new ConfigurationBuilder();
                configurationBuilder.AddJsonFile(path, false);

                var root = configurationBuilder.Build();

                var appSetting = root.GetSection("ApplicationSettings");

                var applicationID = appSetting["applicationid"];

                string deviceId = "euxqdp------ioIdL87abVL";

                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");

                tRequest.Method = "post";

                tRequest.ContentType = "application/json";

                var data = new

                {
                    to = obj.to,
                    priority = "high",
                    restricted_package_name = "",
                    notification = new
                    {
                        body = obj.body,
                        title = obj.title,
                        icon = "fcm_push_icon",
                        click_action = "FCM_PLUGIN_ACTIVITY",
                        sound = "default"
                    },
                    data = new
                    {
                        landing_page = obj.landing_page
                    }
                };

                var json = JsonConvert.SerializeObject(data);

                Byte[] byteArray = Encoding.UTF8.GetBytes(json);

                tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));

                  tRequest.Headers.Add(string.Format("Sender: id={0}", "1096742735083"));

                tRequest.ContentLength = byteArray.Length;


                using (Stream dataStream = tRequest.GetRequestStream())
                {

                    dataStream.Write(byteArray, 0, byteArray.Length);


                    using (WebResponse tResponse = tRequest.GetResponse())
                    {

                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {

                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {

                                String sResponseFromServer = tReader.ReadToEnd();

                                string str = sResponseFromServer;

                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {

                StackTrace st = new StackTrace(ex, true);
                StackFrame frame = st.GetFrame(st.FrameCount - 1);
                string fileName = frame.GetFileName();
                string methodName = frame.GetMethod().Name;
                int line = frame.GetFileLineNumber();

                if (Startup.application != "development")
                {
                    string emailbody = EmailErrorLog.createHtml(ex.Message, fileName, line, methodName, path);
                    EmailErrorLog.SendEmail(emailbody, path);
                }
            }

        }
    }
}
