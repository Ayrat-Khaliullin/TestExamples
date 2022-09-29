using RestSharp;
using System;
using APITests.Utilities;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;

namespace AlphaBI.APIProjects.AnalyticWorkspace.Requests
{
    internal class BaseRequests
    {
        public static IRestRequest BaseRequest(string path, Method method, Dictionary<string, string> parameters = null)
        {
            var request = new RestRequest($"/{path}", method);
            if (parameters != null)
            {
                foreach (KeyValuePair<string, string> keyValue in parameters)
                {
                    request.AddParameter($"{keyValue.Key}", $"{keyValue.Value}");
                }
            }
            return request;
        }
        public static IRestResponse BaseResponse(string path, Method method, Dictionary<string, string> parameters = null)
        {
            var request = BaseRequest(path, method, parameters);
            return Common.Client.Execute(request);
        }
        /// <summary>
        /// Creates http request and convert response to <see cref="JObject"/>.
        /// </summary>
        /// <param name="path">request path</param>
        /// <param name="method">request type</param>
        /// <param name="parameters">request parameters</param>
        /// <returns> Server response as <see cref="JObject"/></returns>
        public static JObject BaseResponse_JObject(string path, Method method, JToken parameters)
        {
            var request = new RestRequest($"/{path}", method);
            request.AddParameter("application/json", parameters, ParameterType.RequestBody);
            var response = Common.Client.Execute(request);
            return ParseJSON.Parse_JObject(response.Content);
        }
        public static JObject BaseResponse_JObject(string path, Method method, Dictionary<string, string> parameters = null)
        {
            return ParseJSON.Parse_JObject(BaseResponse(path, method, parameters).Content);
        }
        public static JObject BaseResponse_Upload(string path, Method method, Dictionary<string, string> fileParameters, Dictionary<string, string> parameters = null)
        {
            var request = BaseRequest(path, method, parameters);
            var filePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileParameters["path"]));
            request.AddHeader("Content-Type", "multipart/form-data");
            request.AddFile(fileParameters["name"], filePath, fileParameters["contentType"]);
            var response = Common.Client.Execute(request);
            return ParseJSON.Parse_JObject(response.Content);
        }
        public static JObject BaseResponse_FormData (string path, Method method, Dictionary<string, string> parameters = null)
        {
            var request = BaseRequest(path, method, parameters);
            request.AlwaysMultipartFormData = true;
            var response = Common.Client.Execute(request);
            return ParseJSON.Parse_JObject(response.Content);
        }
    }
}
 