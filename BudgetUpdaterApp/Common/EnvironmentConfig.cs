﻿using AspNetCore.Yandex.ObjectStorage;
using AspNetCore.Yandex.ObjectStorage.Configuration;
using TinkoffBusiness.Api.Client.Common;

namespace Sankirtana.Web.Common;

public class EnvironmentConfig
{
    public TinkoffApiSettings TinkoffApiSettings { get; private set; }
    
    public EnvironmentConfig()
    {
        var yaSecretKey = Environment.GetEnvironmentVariable("YA_OBJECT_STORAGE_SECRET_KEY")!;
        if (string.IsNullOrEmpty(yaSecretKey))
        {
            throw new InvalidOperationException("There is no environment variable YA_OBJECT_STORAGE_SECRET_KEY");
        };
        
        var oss = new YandexStorageService(new YandexStorageOptions()
        {
            AccessKey = "YCAJEWT3HNKiSCnrfiXOf_Bsm",
            SecretKey = yaSecretKey,
            BucketName = "secrets"
        });
        var result = oss.ObjectService.GetAsync("tinkoff_api_settings.json").Result;
        
        var stream = result.ReadAsStreamAsync().Result.Value;
        var reader = new StreamReader(stream);
        var text = reader.ReadToEnd();
        
        var config = Newtonsoft.Json.JsonConvert.DeserializeObject<TinkoffApiSettings>(text);
        if (config != null)
        {
            TinkoffApiSettings = config;
        }
        else
        {
            throw new InvalidOperationException("Cannot read config file from Yandex Object Storage");
        }
    }
}