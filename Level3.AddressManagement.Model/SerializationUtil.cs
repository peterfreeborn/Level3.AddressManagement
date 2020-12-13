using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Level3.AddressManagement.Model
{
    public static class SerializationUtil
    {

        public static string SerializeToXmlString(object obj)
        {
            XmlSerializer xs = new XmlSerializer(obj.GetType());
            using (MemoryStream buffer = new MemoryStream())
            {
                xs.Serialize(buffer, obj);
                return Encoding.UTF8.GetString(buffer.ToArray());
            }
        }


        public static T Deserialize<T>(string input)
        where T : class
        {
            System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(typeof(T));

            using (StringReader sr = new StringReader(input))
                return (T)ser.Deserialize(sr);
        }




        public static string SerializeToJsonString(object obj)
        {
            //return JsonConvert.SerializeObject(obj);

            return JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }



        public static T DeserializeFromJson<T>(string input)
        where T : class
        {
            System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(typeof(T));

            return JsonConvert.DeserializeObject<T>(input);
        }

    }
}
