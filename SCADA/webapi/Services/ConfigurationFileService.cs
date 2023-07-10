using System.Security.Cryptography.X509Certificates;
using System.Xml;
using System.Xml.Linq;
using webapi.Enum;
using webapi.model;
using webapi.Repositories;

public interface IConfigurationFileService
{
    void AddTag(TagBase tag);
}

public class ConfigurationFileService : IConfigurationFileService
{
    XmlWriterSettings XmlWriterSettings { get; set; }
    string xmlFilePath { get; }
    public ConfigurationFileService() 
    {
        XmlWriterSettings = new XmlWriterSettings
        {
            Indent = true,  // Enable indentation for readability
            OmitXmlDeclaration = false  // Include the XML declaration at the beginning
        };
        xmlFilePath = "Configuration/dataConfig.xml";
    }
    public void AddTag(TagBase tag)
    {
        string xmlTag="";
        if (tag is AnalogInput analogInput)
            xmlTag = "analogInput";
        else if (tag is AnalogOutput analogOutput)
            xmlTag = "analogOutput";
        else if (tag is DigitalInput digitalInput)
            xmlTag = "digitalInput";
        else if (tag is DigitalOutput digitalOutput)
            xmlTag = "digitalOutput";

        XDocument xDoc;
        if (File.Exists(xmlFilePath))
            xDoc = XDocument.Load(xmlFilePath);
        else
        {
            xDoc = new XDocument();
            xDoc.Add(new XElement("Scada"));
            xDoc.Save(xmlFilePath);

        }

        XElement elementToDelete = xDoc.Descendants(xmlTag).FirstOrDefault(e => (int)e.Element("Id") == tag.Id);

        if (elementToDelete != null)
            elementToDelete.Remove();

        XElement newElement = new XElement(xmlTag,
            new XElement("Id", tag.Id),
            new XElement("Description", tag.Description),
            new XElement("Value", tag.Values.Count==0? "/" : tag.Values[tag.Values.Count-1].Value),
            new XElement("Timestamp", tag.Values.Count == 0 ? "/" : tag.Values[tag.Values.Count - 1].Date.ToString("HH:mm:ss:fff dd.MM.yyyy"))
        );
        xDoc.Root?.Add(newElement);

        xDoc.Save(xmlFilePath);
    }
}
