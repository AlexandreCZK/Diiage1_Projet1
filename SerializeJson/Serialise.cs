using Diiage2020.CsharpGame.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SerializeJson
{
    public class Serialise
    {
        public static string ToJson<T>(T t)
        {
            string result = "";
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            try
            {
                result = JsonConvert.SerializeObject(t, settings);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public static T FromJson<T>(string json)
        {
            T t = default(T);
            try
            {
                t = JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return t;
        }

    }
}
