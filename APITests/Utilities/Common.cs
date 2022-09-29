using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using RestSharp;

namespace APITests.Utilities
{
    public static class Common
    {
        private static Random random = new Random();
        //URL
        public static string ApiUrl { get; } = "https://api24w.ilovepdf.com/";

        private static RestClient restClient;
        public static RestClient Client { get; } = GetClient();

        public static void SetClient()
        {
            restClient = new RestClient(ApiUrl)
            {
                FollowRedirects = true,
                CookieContainer = new CookieContainer(),
                Timeout = 300000//5min
            };
            restClient.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            restClient.AddDefaultHeader("authorization", "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiIiLCJhdWQiOiIiLCJpYXQiOjE1MjMzNjQ4MjQsIm5iZiI6MTUyMzM2NDgyNCwianRpIjoicHJvamVjdF9wdWJsaWNfYzkwNWRkMWMwMWU5ZmQ3NzY5ODNjYTQwZDBhOWQyZjNfT1Vzd2EwODA0MGI4ZDJjN2NhM2NjZGE2MGQ2MTBhMmRkY2U3NyJ9.qvHSXgCJgqpC4gd6-paUlDLFmg0o2DsOvb1EUYPYx_E");
        }

        private static RestClient GetClient()
        {
            if (restClient == null)
            {
                SetClient();
            }
            return restClient;
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}