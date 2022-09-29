using System.Xml;
using System.IO;


namespace UITests.Utilities
{
    public class ConfigReader
    {
        private XmlDocument document;

        public ConfigReader()
        {
            document = new XmlDocument();
            document.Load("_Default.config");
        }

        public string GetValue(string key)
        {
            XmlNode AppSettings = document.SelectSingleNode("appSettings");
            XmlNodeList nodes = AppSettings.ChildNodes;
            foreach (XmlElement node in nodes)
            {
                var k = node.GetAttribute("key");
                if (k == key) return node.GetAttribute("value");
            }
            
            return string.Empty;
        }
    }
}
