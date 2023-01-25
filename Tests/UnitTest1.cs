using AmoToSheetFunction = AmoToSheetFunc.AmoToSheetFunction;

namespace Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        var updater = new AmoToSheetFunc.AmoToSheetFunction();
        var s =
            "{\"httpMethod\":\"POST\",\"headers\":{\"Accept\":\"*/*\",\"Content-Length\":\"300\",\"Content-Type\":\"application/x-www-form-urlencoded\",\"Host\":\"functions.yandexcloud.net\",\"Uber-Trace-Id\":\"85ec914ea7c7c294:4a87c0711ff3f23d:85ec914ea7c7c294:1\",\"User-Agent\":\"amoCRM-Hook-Sender/1.0\",\"X-Forwarded-For\":\"23.111.99.28\",\"X-Real-Remote-Address\":\"[23.111.99.28]:35838\",\"X-Request-Id\":\"f5babbba-2533-4624-a269-0da3422ae8f9\",\"X-Trace-Id\":\"3d5aec65-eec4-4509-82b7-21ceccc93bf9\"},\"url\":\"\",\"params\":{},\"multiValueParams\":{},\"pathParams\":{},\"multiValueHeaders\":{\"Accept\":[\"*/*\"],\"Content-Length\":[\"300\"],\"Content-Type\":[\"application/x-www-form-urlencoded\"],\"Host\":[\"functions.yandexcloud.net\"],\"Uber-Trace-Id\":[\"85ec914ea7c7c294:4a87c0711ff3f23d:85ec914ea7c7c294:1\"],\"User-Agent\":[\"amoCRM-Hook-Sender/1.0\"],\"X-Forwarded-For\":[\"23.111.99.28\"],\"X-Real-Remote-Address\":[\"[23.111.99.28]:35838\"],\"X-Request-Id\":[\"f5babbba-2533-4624-a269-0da3422ae8f9\"],\"X-Trace-Id\":[\"3d5aec65-eec4-4509-82b7-21ceccc93bf9\"]},\"queryStringParameters\":{},\"multiValueQueryStringParameters\":{},\"requestContext\":{\"identity\":{\"sourceIp\":\"23.111.99.28\",\"userAgent\":\"amoCRM-Hook-Sender/1.0\"},\"httpMethod\":\"POST\",\"requestId\":\"f5babbba-2533-4624-a269-0da3422ae8f9\",\"requestTime\":\"25/Jan/2023:04:42:38 +0000\",\"requestTimeEpoch\":1674621758},\"body\":\"bGVhZHMlNUJzdGF0dXMlNUQlNUIwJTVEJTVCaWQlNUQ9OTk5ODkyOSZsZWFkcyU1QnN0YXR1cyU1RCU1QjAlNUQlNUJzdGF0dXNfaWQlNUQ9NTQ0MjM5MjImbGVhZHMlNUJzdGF0dXMlNUQlNUIwJTVEJTVCcGlwZWxpbmVfaWQlNUQ9NjM0NDI5MCZsZWFkcyU1QnN0YXR1cyU1RCU1QjAlNUQlNUJvbGRfc3RhdHVzX2lkJTVEPTU0NDM1MDE0JmxlYWRzJTVCc3RhdHVzJTVEJTVCMCU1RCU1Qm9sZF9waXBlbGluZV9pZCU1RD02MzQ0MjkwJmFjY291bnQlNUJpZCU1RD0zMDMwNzU1OCZhY2NvdW50JTVCc3ViZG9tYWluJTVEPWFueW9y\",\"isBase64Encoded\":true}";
        updater.FunctionHandler(s);
    }
}