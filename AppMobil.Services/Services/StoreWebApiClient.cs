﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using AppMobil.Services.Helpers;
using Newtonsoft.Json;
using AppMobil.Models;
using Plugin.Connectivity;

namespace AppMobil.Services.Services
{
    public class StoreWebApiClient : HttpClient
    {
        private static  StoreWebApiClient instance = new StoreWebApiClient();
        static StoreWebApiClient() { }

        private StoreWebApiClient() : base()
        {
            Timeout = TimeSpan.FromMilliseconds(15000);
            MaxResponseContentBufferSize = 256000;
            BaseAddress = new Uri(Constants.StoreWebApiURL);
            DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static StoreWebApiClient Instance
        {
            get
            {
                return instance;
            }
        }
        public async Task<Response> CheckConnection()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Please turn on your internet settings.",
                };
            }

            var isReachable = await CrossConnectivity.Current.IsRemoteReachable(
                "google.com");
            if (!isReachable)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Check you internet connection.",
                };
            }

            return new Response
            {
                IsSuccess = true,
                Message = "Ok",
            };
        }

        public async Task<Response> GetItems<T>(string service)
        {
            try
            {
                var response = await GetAsync(service);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }

                var list = JsonConvert.DeserializeObject<List<T>>(result);
                return new Response
                {
                    IsSuccess = true,
                    Message = "Ok",
                    Result = list,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }
        
        public async Task<List<T>> GetItems<T>(string service, string method)
        {
            var response = await GetAsync($"{service}/{method}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<T>>(content);
            }
            //throw new Exception(response.ReasonPhrase);
            return default(List<T>);
        }

        public async Task<T> GetItem<T>(string service, int id)
        {
            var response = await GetAsync($"{service}/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(content);
            }
            //throw new Exception(response.ReasonPhrase);
            return default(T);
        }

        public async Task<T> GetItem<T>(string service, string method)
        {
            var response = await GetAsync($"{service}/{method}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(content);
            }
            //throw new Exception(response.ReasonPhrase);
            return default(T);
        }

        public async Task<Response> PostItem<T>(string service, T item)
        {
            try
            {
                var body = JsonConvert.SerializeObject(item);
                var content = new StringContent(body, Encoding.UTF8, "application/json");
                var response = await PostAsync(service, content);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }

                var list = JsonConvert.DeserializeObject<T>(result);
                return new Response
                {
                    IsSuccess = true,
                    Message = "Ok",
                    Result = list,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<bool> PutItem<T>(string service, T item, int id)
        {
            var body = JsonConvert.SerializeObject(item);
            var content = new StringContent(body, Encoding.UTF8, "application/json");
            var response = await PutAsync($"{service}/{id}", content);

            if (response.IsSuccessStatusCode)
                return true;
            //throw new Exception(response.ReasonPhrase);
            return false;
        }

        public async Task<bool> DeleteItem<T>(string service, int id)
        {
            var response = await DeleteAsync($"{service}/{id}");
            if (response.IsSuccessStatusCode)
                return true;
            //throw new Exception(response.ReasonPhrase);
            return false;
        }



        public async Task<Usuario> LoginCustomer(string email, string password)
        {
            var credentials = new { email = email, password = password }; //MD5Security.ToMD5Hash(password) };

            var body = JsonConvert.SerializeObject(credentials);
            var content = new StringContent(body, Encoding.UTF8, "application/json");
            var response = await PostAsync("Usuarios/Login", content);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Usuario>(json);
            }
            //throw new Exception(response.ReasonPhrase);
            return default(Usuario);
        }
      
    }
}
