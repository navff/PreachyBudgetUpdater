using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using AspNetCore.Yandex.ObjectStorage;
using AspNetCore.Yandex.ObjectStorage.Configuration;
using Newtonsoft.Json;
using YaCloudCommon;

namespace AmoKeyUpdateFunc
{
    public class AmoKeyUpdateFunc
    {
        private string YA_ACCESS_KEY = "YCAJEWT3HNKiSCnrfiXOf_Bsm";
        private readonly string _ya_secret_key = string.Empty;
        private readonly string _amo_client_secret = string.Empty;
        private readonly YandexStorageService _oss;
        
        
        public AmoKeyUpdateFunc()
        {
            _ya_secret_key = Environment.GetEnvironmentVariable("YA_OBJECT_STORAGE_SECRET_KEY")!;
            if (string.IsNullOrEmpty(_ya_secret_key))
            {
                throw new InvalidOperationException("There is no environment variable YA_OBJECT_STORAGE_SECRET_KEY");
            }
            
            _amo_client_secret = Environment.GetEnvironmentVariable("AMO_CLIENT_SECRET")!;
            if (string.IsNullOrEmpty(_amo_client_secret))
            {
                throw new InvalidOperationException("There is no environment variable AMO_CLIENT_SECRET");
            }
            
            _oss = new YandexStorageService(new YandexStorageOptions()
            {
                AccessKey = YA_ACCESS_KEY,
                SecretKey = _ya_secret_key,
                BucketName = "secrets"
            });
        }

        public YaCloudFunctionResponseBase FunctionHandler(string? request = null)
        {
            // if (request == null) return new YaCloudFunctionErrorResponse
            // {
            //     ErrorMessage = "empty request",
            //     ErrorType = "RequestError"
            // };

            var oldAmoSecrets = GetAmoKeyFileFromYaObjectStorage();
            var newAmoSecrets = GetNewAmoSecretsFromAmo(new AmoSecretsRequest
            {
                ClientId = "894018a5-13f4-4559-b1ba-4ed161e315ea",
                ClientSecret = _amo_client_secret,
                GrantType = "refresh_token",
                RedirectUri = "https://functions.yandexcloud.net/d4e05h2vor3d7liq90tf",
                RefreshToken = oldAmoSecrets.RefreshToken
            });
            SaveNewSecretToYaCloud(newAmoSecrets);
            
            return new YaCloudFunctionResponse();
        }

        private AmoTokenConfig GetAmoKeyFileFromYaObjectStorage()
        {
            var amoSecretsResult = _oss.ObjectService.GetAsync("anyor_amocrm_token.json").Result;
    
            var stream = amoSecretsResult.ReadAsStreamAsync().Result.Value;
            var reader = new StreamReader(stream);
            var text = reader.ReadToEnd();

            var config = JsonConvert.DeserializeObject<AmoTokenConfig>(text);
            if (config != null)
            {
                return config;
            }
            
            throw new InvalidOperationException("Cannot read config file from Yandex Object Storage");
        }

        private AmoTokenConfig GetNewAmoSecretsFromAmo(AmoSecretsRequest amoSecretsRequest)
        {
            string url = "https://anyor.amocrm.ru/oauth2/access_token";
            
            var content = JsonConvert.SerializeObject(amoSecretsRequest);
            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using var httpClient = new HttpClient();
            using var response =  httpClient.PostAsync(url, byteContent).Result;
            if (!response.IsSuccessStatusCode) 
                throw new Exception("Error while getting new Amo token");
            
            var responseString = response.Content.ReadAsStringAsync().Result;
            var amoTokenConfig = JsonConvert.DeserializeObject<AmoTokenConfig>(responseString);
            if (amoTokenConfig == null)
                throw new Exception("Error while parsing new Amo token");
            
            return amoTokenConfig;
        }

        private bool SaveNewSecretToYaCloud(AmoTokenConfig tokenConfig)
        {
            var jsonString = JsonConvert.SerializeObject(tokenConfig, Formatting.Indented);
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            var putToYaCloudResult = _oss.ObjectService
                .PutAsync(stream, "anyor_amocrm_token.json")
                .Result;
            return putToYaCloudResult.IsSuccessStatusCode;
        }
    }
}

