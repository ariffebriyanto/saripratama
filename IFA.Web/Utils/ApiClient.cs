using ERP.Domain.Base;
using ERP.Domain.Objects;
using MultipartDataMediaFormatter;
using MultipartDataMediaFormatter.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ERP.Web.Utils
{
    public class ApiClient
    {
        private readonly HttpClient _httpClient;

        private SolutionConfig Config { get; set; }

        public ApiClient(SolutionConfig config)
        {

            _httpClient = new HttpClient();
            this.Config = config;
        }

        public async Task<Response> CallAPIGet(string url)
        {
            var cookieContainer = new CookieContainer();

            var handler = new HttpClientHandler();
            handler.AllowAutoRedirect = true;
            handler.UseCookies = true;
            var cookieList = new List<Cookie>();

            cookieContainer.Add(new Uri(Config.SIF_API_Server + url), new Cookie("passhttpclient", "passhttpclient"));

            handler.CookieContainer = cookieContainer;

            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };

            HttpClient httpClient = new HttpClient(handler);

            Uri requestUrl = new Uri(Config.SIF_API_Server + url);
            Response response = new Response();

            HttpResponseMessage httpResponse;
            try
            {

                httpResponse = await httpClient.GetAsync(requestUrl, HttpCompletionOption.ResponseHeadersRead);
                httpResponse.EnsureSuccessStatusCode();
                var data = await httpResponse.Content.ReadAsStringAsync();
                response = JsonConvert.DeserializeObject<Response>(data);
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = "fail";
            }
            return response;
        }

        public async Task<Response> CallAPIPost<T>(string url, T content)
        {
            var cookieContainer = new CookieContainer();

            var handler = new HttpClientHandler();
            handler.AllowAutoRedirect = true;
            handler.UseCookies = true;
            var cookieList = new List<Cookie>();

            cookieContainer.Add(new Uri(Config.SIF_API_Server + url), new Cookie("passhttpclient", "passhttpclient"));

            handler.CookieContainer = cookieContainer;

            //solved https issue
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };

            HttpClient httpClient = new HttpClient(handler);

            Uri requestUrl = new Uri(Config.SIF_API_Server + url);

            Response response = new Response();
            HttpResponseMessage httpresponse;

            var mediaTypeFormatter = new FormMultipartEncodedMediaTypeFormatter(new MultipartFormatterSettings()
            {
                SerializeByteArrayAsHttpFile = true,
                CultureInfo = CultureInfo.CurrentCulture,
                ValidateNonNullableMissedProperty = true
            });

            try
            {

                httpresponse = await httpClient.PostAsync(requestUrl.ToString(), content, mediaTypeFormatter);
                httpresponse.EnsureSuccessStatusCode();
                var data = await httpresponse.Content.ReadAsStringAsync();
                response = JsonConvert.DeserializeObject<Response>(data);
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = "fail";
            }
            return response;
        }


        public async Task<T> CallAPIGet_Old<T>(string url)
        {
            var cookieContainer = new CookieContainer();

            var handler = new HttpClientHandler();
            handler.AllowAutoRedirect = true;
            handler.UseCookies = true;
            var cookieList = new List<Cookie>();

            //foreach (Cookie cookie in cookieContainer.GetCookies(new Uri(Config.MAA_API_Server + url)))
            //{
            //    cookieList.Add(cookie);
            //}

            cookieContainer.Add(new Uri(Config.SIF_API_Server + url), new Cookie("passhttpclient", "passhttpclient"));

            handler.CookieContainer = cookieContainer;

            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };

            HttpClient httpClient = new HttpClient(handler);

            Uri requestUrl = new Uri(Config.SIF_API_Server + url);
            string data = string.Empty;
            HttpResponseMessage response;
            try
            {
                //_httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                //_httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));

                response = await httpClient.GetAsync(requestUrl, HttpCompletionOption.ResponseHeadersRead);
                response.EnsureSuccessStatusCode();
                data = await response.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {

            }
            return JsonConvert.DeserializeObject<T>(data);
        }

        public async Task<bool> CallAPIPost_Old<T, T2>(string url, T2 content)
        {
            var cookieContainer = new CookieContainer();

            var handler = new HttpClientHandler();
            handler.AllowAutoRedirect = true;
            handler.UseCookies = true;
            var cookieList = new List<Cookie>();

            //foreach (Cookie cookie in cookieContainer.GetCookies(new Uri(Config.MAA_API_Server + url)))
            //{
            //    cookieList.Add(cookie);
            //}

            cookieContainer.Add(new Uri(Config.SIF_API_Server + url), new Cookie("passhttpclient", "passhttpclient"));

            handler.CookieContainer = cookieContainer;

            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };

            HttpClient httpClient = new HttpClient(handler);

            Uri requestUrl = new Uri(Config.SIF_API_Server + url);
            string data = string.Empty;
            HttpResponseMessage response;
            bool isSuccess = true;

            var mediaTypeFormatter = new FormMultipartEncodedMediaTypeFormatter(new MultipartFormatterSettings()
            {
                SerializeByteArrayAsHttpFile = true,
                CultureInfo = CultureInfo.CurrentCulture,
                ValidateNonNullableMissedProperty = true
            });

            //var json = JsonConvert.SerializeObject(content);
            //var content2 = new StringContent(json, Encoding.UTF8, "application/json");
            //content2.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            try
            {
                response = await httpClient.PostAsync(requestUrl.ToString(), content, mediaTypeFormatter);
                response.EnsureSuccessStatusCode();
                data = await response.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                isSuccess = false;
            }
            return isSuccess;
        }

    }
}
