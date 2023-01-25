using System;
using System.Buffers.Text;
using System.ComponentModel;
using System.Globalization;
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
            
            var base64EncodedBytes = Convert.FromBase64String(amoRequest.body);
            var decodedBody = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            var unescaped = Uri.UnescapeDataString(decodedBody);
            var hookConverted = Parse(unescaped, typeof(AmoLeadStatusHook));
            
            Console.WriteLine("DECODED BODY: " + unescaped);
            Console.WriteLine("BODY: " + amoRequest.body);
            
            return new YaCloudFunctionResponse();
        }
            
        public object Parse(string valueToConvert, Type dataType)
        {
            TypeConverter obj = TypeDescriptor.GetConverter(dataType);
            object value = obj.ConvertFromString(null, CultureInfo.InvariantCulture,  valueToConvert);
            return value;
        }
    }
}