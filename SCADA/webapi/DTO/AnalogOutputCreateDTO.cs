namespace webapi.DTO
{
    public class AnalogOutputCreateDTO
    {
        public string Description { get; set; }
       // public string ValueType { get; set; }
        public double InitialValue { get; set; }
        public double LowLimit { get; set; }
        public double HighLimit { get; set; }
        public string Unit { get; set; }
    }
}
