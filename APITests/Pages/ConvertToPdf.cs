using AlphaBI.APIProjects.AnalyticWorkspace.Requests;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client.Payloads;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System.Collections.Generic;
using System.Xml.Linq;


namespace APITests.Pages
{
    public class ConvertToPdf
    {
       public ConvertToPdf()
        {
           // taskToken = Utilities.Common.RandomString(180);
        }
        internal bool stopTest=false;
        internal string serverFilename { get; set; }
        internal string taskToken { get; set; } = "vz3l9frct5nqpwvpnsqn55nv5g5pbAmz8vlsAwz4hcnpdpsp7ggdjpczsbmkdybn8f2wmx824ym8h13s1m438nbcm5m161rs7ww02bhlvAr7n4jvbdxtkb2j24wbfz9qb18xxmwmf7md4dt1p10Apxqx3xyAkrbw3rx68gkgs3kzjsf0j4p1";
       public JObject UploadDocument()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>
            {
                { "name", "123.docx" },
                { "chunk", "0" },
                { "chunks", "1" },
                { "task", taskToken},
                { "preview", "1" },
                { "pdfinfo", "0" },
                { "pdfforms", "0" },
                { "pdfresetforms", "0" },
                { "v", "web.0" }
            };
            Dictionary<string, string> fileParameters = new Dictionary<string, string>
            {
                { "name", "file" },
                { "filename", "test"},
                {"path", "Resources/123.docx" },
                {"contentType","" }
            };
            return BaseRequests.BaseResponse_Upload("v1/upload", Method.POST, fileParameters, parameters); 
        }

        public JObject ConvertToPdfProcess()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>
            {
                { "output_filename", "{filename}" },
                { "packaged_filename", "ilovepdf_converted" },
                { "task", taskToken },
                { "tool", "officepdf" },
                { "files[0][server_filename]", serverFilename },
                { "files[0][filename]", "123.docx" }
            };
           
            return BaseRequests.BaseResponse_FormData("v1/process", Method.POST, parameters);
        }

        public IRestResponse DownloadPdf()
        {
            return BaseRequests.BaseResponse($"v1/download/{taskToken}", Method.GET);
        }
    }
}