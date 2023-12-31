﻿using Newtonsoft.Json;

namespace Infrastructure.Tests.Data.Helpers;

public static class JsonHelper
{
    public static JsonSerializerSettings GetJsonSettings()
    {
        return new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };
    }

    public static string Serialize<T>(T obj)
    {
        return JsonConvert.SerializeObject(obj, GetJsonSettings());
    }
}
