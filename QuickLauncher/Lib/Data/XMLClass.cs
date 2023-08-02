using System.IO;
using System.Xml.Serialization;

namespace QuickLauncher.Lib.Data
{
    public class XMLClass
    {
        public static void SaveData<T>(T data, string fileName)
        {
            string xml = SaveToString(data);

            File.WriteAllText(fileName, xml);
        }

        public static T? LoadData<T>(string fileName) => LoadFromString<T>(File.ReadAllText(fileName));

        public static string SaveToString<T>(T control)
        {
            var writer = new StringWriter();
            var serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(writer, control);
            
            return writer.ToString();
        }

        public static T? LoadFromString<T>(string xml)
        {
            var serializer = new XmlSerializer(typeof(T));
            return (T?)serializer.Deserialize(new StringReader(xml));
        }
    }
}
