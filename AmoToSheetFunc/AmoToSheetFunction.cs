using System;
using System.Buffers.Text;
using System.ComponentModel;
using System.Globalization;
using System.Web;
using AmoToSheetFunction;
using Newtonsoft.Json;

namespace AmoToSheetFunc
{
    public class AmoToSheetFunction
    {
        public YaCloudFunctionResponseBase FunctionHandler(string? request = null)
        {
            if (request == null) return new YaCloudFunctionErrorResponse
            {
                ErrorMessage = "empty request",
                ErrorType = "RequestError"
            };
            
            Console.WriteLine("REQUEST: " + request);
            
            var amoRequest = JsonConvert.DeserializeObject<AmoHookRequest>(request);
            if (amoRequest == null) return new YaCloudFunctionErrorResponse
            {
                ErrorMessage = "Cannot deserialize webhook",
                ErrorType = "RequestError"
            };

            var hook = ParseHook(amoRequest.body);
            
            Console.WriteLine("LEAD_ID: " + hook.LeadId);
            Console.WriteLine("BODY: " + amoRequest.body);
            
            return new YaCloudFunctionResponse();
        }
            
        public AmoLeadStatusHook ParseHook(string valueToConvert)
        {
            
            var base64EncodedBytes = Convert.FromBase64String(valueToConvert);
            var decodedBody = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            var unescaped = Uri.UnescapeDataString(decodedBody);
            var valueCollection = HttpUtility.ParseQueryString(unescaped);

            return new AmoLeadStatusHook
            {
                LeadId = long.Parse(valueCollection["leads[status][0][id]"]),
                NewPipelineId = long.Parse(valueCollection["leads[status][0][pipeline_id]"]),
                NewStatusId = long.Parse(valueCollection["leads[status][0][status_id]"]),
                OldPipelineId = long.Parse(valueCollection["leads[status][0][old_pipeline_id]"]),
                OldStatusId = long.Parse(valueCollection["leads[status][0][old_status_id]"])
            };
        }
    }
}