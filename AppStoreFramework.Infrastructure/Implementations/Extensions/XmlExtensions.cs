using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace AppStoreFramework.Infrastructure.Implementations.Extensions
{
   public static class XmlExtensions
    {
           public static string Serialize<T>(this T obj)
           {
               var sb = new StringBuilder();
               var outStream = new StringWriter(sb);
               var xmlSer = new XmlSerializer(typeof(T));
               xmlSer.Serialize(outStream, obj);
               return sb.ToString();
           }

           public static T DeSerialize<T>(this string xml) where T : new()
           {
               if (String.IsNullOrEmpty(xml))
               {
                   return new T();
               }
               var xmlSer = new XmlSerializer(typeof(T));
               using (var stream = new StringReader(xml))
                   return (T)xmlSer.Deserialize(stream);
           }
       
    }
}
