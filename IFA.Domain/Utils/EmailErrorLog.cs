using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;


namespace IFA.Domain.Utils
{
    public class EmailErrorLog
    {
        public static string createHtml(string message, string file, int line, string method, string path)
        {
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile(path, false);
            string _connectionString = string.Empty;
            var root = configurationBuilder.Build();

            var appSetting = root.GetSection("ApplicationSettings");
            string appName = appSetting["ApplicationName"];
            string emailBody = "";
            emailBody = "<html><head></head><body style='background-color:#f3f3f3;'> <meta http-equiv='Content-Type' content='text/html; charset=utf-8'> <title>FPS Error Log</title> <style type='text/css'> body{font-size: 11px; margin-left: 0px; margin-top: 0px; margin-right: 0px; margin-bottom: 0px;}</style> </meta> <div style='background-color:#f3f3f3;'> <table style='background-color:#f3f3f3;' width='100%'> <tbody> <tr> <td align='center' style='color:#666666'> <table width='700' align='center' cellpadding='0' cellspacing='0' style='border:#d7d7d7 1px solid;'> <tbody> <tr> <td> <table width='100%' cellpadding='0' cellspacing='0'> <tbody> <tr> <td width='80%' bgcolor='#1c75bc' height='50' style='FONT-SIZE: 20.0pt; COLOR: white; FONT-FAMILY: Arial,Helvetica,sans-serif; padding:20px 20px 20px 35px;text-align:left;'>{applicationName}ERROR LOG </td><td width='20%' bgcolor='#50c3ea' height='50' style='padding:20px 20px 20px 35px;'> </td></tr></tbody> </table> </td></tr><tr> <td style='color:#000001'> <table width='700' border='0' align='center' cellpadding='0' cellspacing='0' style='background-color:#cccccc'> <tbody> <tr> <td height='20' valign='top' style='background-color:#ffffff'> </td></tr><tr> <td style='background-color:#ffffff'> <table width='650' border='0' align='center' bgcolor='#ffffff' cellpadding='0' cellspacing='0' style='color:#000001'> <tbody> <tr> <td colspan='2' align='left' valign='top' style='font-family:Arial,Helvetica,sans-serif; padding:5px 0 10px'> <p> <span style='font-size:18px; color:#666666;'> <font> <strong>Detail Error as below</strong> </font> </span> </p></td></tr></tbody> </table> <table width='650' border='0' align='center' style='FONT-SIZE: 11.0pt; COLOR: black; FONT-FAMILY: Arial,Helvetica,sans-serif;'> <tbody> <tr> <td> <table width='100%' border='0' style='FONT-SIZE: 11.0pt; COLOR: black; FONT-FAMILY: Arial,Helvetica,sans-serif;'> <tbody> <tr> <td width='25%' valign='top' style='text-align:left;'> <span style='font-size:13px'> <font color='#666666'>Date & Time</font> </span> </td><td valign='top'> <span style='font-size:13px'> <font color='#666666'>:</font> </span> </td><td valign='top' style='text-align:left;'> <span style='font-size:13px'> <font color='#666666'>{ErrorDate}</font> </span> </td></tr><tr> <td width='25%' valign='top' style='text-align:left;'> <span style='font-size:13px'> <font color='#666666'>File</font> </span> </td><td valign='top'> <span style='font-size:13px'> <font color='#666666'>:</font> </span> </td><td valign='top' style='text-align:left;'> <span style='font-size:13px'> <font color='#666666'>{File}</font> </span> </td></tr><tr> <td width='25%' valign='top' style='text-align:left;'> <span style='font-size:13px'> <font color='#666666'>Method</font> </span> </td><td valign='top'> <span style='font-size:13px'> <font color='#666666'>:</font> </span> </td><td valign='top' style='text-align:left;'> <span style='font-size:13px'> <font color='#666666'>{Method}</font> </span> </td></tr><tr> <td width='25%' valign='top' style='text-align:left;'> <span style='font-size:13px'> <font color='#666666'>Line</font> </span> </td><td valign='top'> <span style='font-size:13px'> <font color='#666666'>:</font> </span> </td><td valign='top' style='text-align:left;'> <span style='font-size:13px'> <font color='#666666'>{Line}</font> </span> </td></tr><tr> <td width='25%' valign='top' style='text-align:left;'> <span style='font-size:13px'> <font color='#666666'>Message</font> </span> </td><td valign='top'> <span style='font-size:13px'> <font color='#666666'>:</font> </span> </td><td valign='top' style='text-align:left;'> <span style='font-size:13px'> <font color='#666666'>{Message}</font> </span> </td></tr><tr> <td height='20' colspan='3' style='FONT-SIZE: 9.0pt; COLOR:#666666; FONT-FAMILY: Arial,Helvetica,sans-serif;text-align:left;'> <br/> <i>Please do not reply to this email. Emails sent to this address will not be answered.</i> </td></tr></tbody> </table> </td></tr></tbody> </table> <br/> </td></tr></tbody> </table> </td></tr></tbody> </table> <br/> </td></tr></tbody> </table> </div></body></html>";
            emailBody = emailBody.Replace("{Message}", message);
            emailBody = emailBody.Replace("{File}", file);
            emailBody = emailBody.Replace("{Method}", method);
            emailBody = emailBody.Replace("{Line}", line.ToString());
            emailBody = emailBody.Replace("{applicationName}", appName);
            emailBody = emailBody.Replace("{ErrorDate}", DateTime.Now.ToString("dd MMMM yyyy HH:mm:ss"));
            return emailBody;
        }

        public static void SendEmail(string htmlString, string path)
        {
            try
            {
                var configurationBuilder = new ConfigurationBuilder();
                configurationBuilder.AddJsonFile(path, false);
                string _connectionString = string.Empty;
                var root = configurationBuilder.Build();

                var appSetting = root.GetSection("ApplicationSettings");
                string FromMailAddress = appSetting["FromMailAddress"];
                string password = appSetting["password"];
                string ToMailAddress = appSetting["ToMailAddress"];
                string Subject = appSetting["Subject"];
                string Port = appSetting["Port"];
                string Host = appSetting["smtp"];
                string appName = appSetting["FromMailAddress"];

                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(FromMailAddress);
                foreach (var address in ToMailAddress.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    message.To.Add(address);
                }
                // message.To.Add(new MailAddress(ToMailAddress));
                message.Subject = Subject;
                message.IsBodyHtml = true;
                message.Body = htmlString;
                smtp.Port = Convert.ToInt32(Port);
                smtp.Host = Host;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(FromMailAddress, password);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception e)
            {
                //string x = e.Message;
                System.IO.File.WriteAllText(@"C:\88\log.txt", e.Message);

            }
        }
    }
}
