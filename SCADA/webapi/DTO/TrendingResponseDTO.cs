using webapi.Enum;
using webapi.model;

public class TrendingResponseDTO
{
    public int id { get; set; }
    public string description { get; set; }
    public int address { get; set; }
    public string value { get; set; }
    public string limit { get; set; }
    public string unit { get; set; }
    public double scanTime { get; set; }
    public string alarmPriority { get; set; }

    public TrendingResponseDTO(int id, string description, int address, string value, string limit, string unit)
    {
        this.id = id;
        this.description = description;
        this.address = address;
        this.value = value;
        this.limit = limit;
        this.unit = unit;
    }

    public TrendingResponseDTO(AnalogInput analogInput, string priority)
    {
        this.id = analogInput.Id;
        this.scanTime = analogInput.ScanTime;
        this.description = analogInput.Description;
        this.address = analogInput.Address.Id;
        if (analogInput.Values.Count == 0)
            this.value = "/";
        else if (analogInput.Values[analogInput.Values.Count - 1].Type == "double")
            this.value = Math.Round(Double.Parse(analogInput.Values[analogInput.Values.Count - 1].Value), 4).ToString();
        else
            this.value = analogInput.Values[analogInput.Values.Count - 1].Value;
        this.limit = analogInput.LowLimit.ToString() + " - " + analogInput.HighLimit.ToString();
        this.unit = analogInput.Unit;
        this.alarmPriority = priority;
    }

    public TrendingResponseDTO(DigitalInput digitalInput)
    {
        this.id = digitalInput.Id;
        this.scanTime = digitalInput.ScanTime;
        this.description = digitalInput.Description;
        this.address = digitalInput.Address.Id;
        if (digitalInput.Values.Count == 0)
            this.value = "/";
        else
            this.value = digitalInput.Values[digitalInput.Values.Count - 1].Value;
        this.limit = "/";
        this.unit = "/";
    }
}