using System.Security.Cryptography.X509Certificates;
using System.Xml;
using System.Xml.Linq;
using webapi.Enum;
using webapi.model;
using webapi.Model;
using webapi.Repositories;

public interface IConfigurationFileService
{
    void AddTag(TagBase tag);
    void AddAlarm(Alarm alarm, DateTime? timestamp);
}

public class ConfigurationFileService : IConfigurationFileService
{
    XmlWriterSettings XmlWriterSettings { get; set; }
    string xmlTagsFilePath { get; }
    string xmlAlarmsFilePath { get; }
    string logsFilePath { get; }
    public ConfigurationFileService() 
    {
        XmlWriterSettings = new XmlWriterSettings
        {
            Indent = true,  // Enable indentation for readability
            OmitXmlDeclaration = false  // Include the XML declaration at the beginning
        };
        xmlTagsFilePath = "Configuration_Logs/dataConfig.xml";
        xmlAlarmsFilePath = "Configuration_Logs/alarmsConfig.xml";
        logsFilePath = "Configuration_Logs/alarmsLog.txt";
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
        if (File.Exists(xmlTagsFilePath))
            xDoc = XDocument.Load(xmlTagsFilePath);
        else
        {
            xDoc = new XDocument();
            xDoc.Add(new XElement("Scada"));
            xDoc.Save(xmlTagsFilePath);

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

        xDoc.Save(xmlTagsFilePath);
    }
    public void AddAlarm(Alarm alarm, DateTime? timestamp)
    {
        string xmlTag = "alarm";
        
        XDocument xDoc;
        if (File.Exists(xmlAlarmsFilePath))
            xDoc = XDocument.Load(xmlAlarmsFilePath);
        else
        {
            xDoc = new XDocument();
            xDoc.Add(new XElement("Alarms"));
            xDoc.Save(xmlAlarmsFilePath);

        }

        XElement elementToDelete = xDoc.Descendants(xmlTag).FirstOrDefault(e => (int)e.Element("Id") == alarm.Id);

        if (elementToDelete != null)
            elementToDelete.Remove();

        XElement newElement = new XElement(xmlTag,
            new XElement("Id", alarm.Id),
            new XElement("AnalogInput", new XElement("Id", alarm.AnalogInput.Id),
                                        new XElement("Description", alarm.AnalogInput.Description)),
            new XElement("Limit", new XElement("Value", alarm.Limit),
                                  new XElement("Type", alarm.Type.ToString())),
            new XElement("Priority", alarm.Priority.ToString()),

            new XElement("LastTrigger", timestamp==null ? "/" :timestamp.Value.ToString("HH:mm:ss:fff dd.MM.yyyy"))
        );
        xDoc.Root?.Add(newElement);

        xDoc.Save(xmlAlarmsFilePath);
        LogAlarmTrigger(alarm, timestamp);

    }

    private void LogAlarmTrigger(Alarm alarm, DateTime? timestamp)
    {

        using (StreamWriter writer = new StreamWriter(logsFilePath, true))
        {
            string alarmData = $"{alarm.Id} {alarm.AnalogInput.Description} {alarm.Type.ToString()} {alarm.Priority.ToString()} {alarm.Limit} {timestamp}";

            writer.WriteLine(alarmData);
        }
    }
}
