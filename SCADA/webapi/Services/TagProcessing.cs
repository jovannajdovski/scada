using Microsoft.EntityFrameworkCore;
using SimulationDriver;
using webapi.Controllers;
using webapi.model;
using webapi.Model;

namespace webapi.Services
{
    public interface ITagProcessingService
    {
        void Process();
        void CreateAnalogTimer(AnalogInput analogInput);
        void CreateDigitalTimer(DigitalInput digitalInput);
        public void QuitTimer(int tagId);

        public void QuitTimers();

    }
    public class TagProcessingService:ITagProcessingService
    {
        public ScadaDBContext dbContext { get; set; }
        public object analogInputValuesLockObj { get; set; }
        public object digitalInputValuesLockObj { get; set; }
        public Dictionary<int, Timer> timers { get; set; }

        public TagProcessingService(ScadaDBContext scadaDBContext)
        {
            this.analogInputValuesLockObj = new object();
            this.digitalInputValuesLockObj = new object();
            this.dbContext = scadaDBContext;
            this.timers = new Dictionary<int, Timer>();
        }
        public void Process()
        {
            List<AnalogInput> analogInputs = dbContext.AnalogInputs.Include(ai => ai.Values).Include(ai => ai.Address)
                .Where(ai => ai.IsScanning).ToList();
            List<DigitalInput> digitalInputs = dbContext.DigitalInputs.Include(ai => ai.Values).Include(ai => ai.Address)
                .Where(di => di.IsScanning).ToList();

            foreach (var analogInput in analogInputs)
                CreateAnalogTimer(analogInput);
            foreach (var digitalInput in digitalInputs)
                CreateDigitalTimer(digitalInput);

        }
        public void CreateAnalogTimer(AnalogInput analogInput)
        {
            Timer timer = new Timer(state =>
            {
                AnalogTimerCallback(analogInput.Address.Id, analogInput.Id);
            }, null, TimeSpan.Zero, TimeSpan.FromSeconds(analogInput.ScanTime));
            timers.Add(analogInput.Id, timer);
        }
        public void CreateDigitalTimer(DigitalInput digitalInput)
        {
            Timer timer = new Timer(state =>
            {
                DigitalTimerCallback(digitalInput.Address.Id, digitalInput.Id);
            }, null, TimeSpan.Zero, TimeSpan.FromSeconds(digitalInput.ScanTime));
            timers.Add(digitalInput.Id, timer);
        }
        private void AnalogTimerCallback(int addressId, int analogInputId)
        {
            lock (analogInputValuesLockObj)
            {
                dbContext = new ScadaDBContext();
 
                IOAddress address = dbContext.Addresses.FirstOrDefault(a => a.Id == addressId);
                AnalogInput analogInput = dbContext.AnalogInputs.Include(a => a.Values).FirstOrDefault(a=>a.Id == analogInputId);

                List<AnalogOutput> analogOutputs = dbContext.AnalogOutputs.Include(a => a.Values).Where(ao => ao.Address.Id == addressId).ToList();
                double sum = analogOutputs.Sum(output => output.Values.Sum(val => double.Parse(val.Value)));

                if (address != null && address.Value != null && analogInput != null)
                {
                    if (double.TryParse(address.Value, out double value))
                    {
                        TagValue inTagValue = new TagValue();
                        TagValue outTagValue;
                        double nextValue = double.Parse(address.Value) + analogInput.ScanTime * sum;
                        
                        if (nextValue < analogInput.LowLimit) nextValue = analogInput.LowLimit;
                        if(nextValue> analogInput.HighLimit) nextValue=analogInput.HighLimit;
                        
                        inTagValue.Value = nextValue.ToString();
                        inTagValue.Type = "double";
                        inTagValue.Date = DateTime.Now;
                        inTagValue.TagId = analogInputId;
                        if (analogInput.Values == null)
                            analogInput.Values = new List<TagValue>();
                        analogInput.Values.Add(inTagValue);
                        dbContext.TagValues.Add(inTagValue);
                        dbContext.AnalogInputs.Update(analogInput);

                        foreach(var analogOutput in analogOutputs)
                        {
                            if(analogOutput.Values.Count>0)
                            {
                                string lastElement = analogOutput.Values[analogOutput.Values.Count - 1].Value;
                                outTagValue = new TagValue();
                                outTagValue.Value = lastElement;
                                outTagValue.Type = "double";
                                outTagValue.Date = DateTime.Now;
                                outTagValue.TagId = analogOutput.Id;
                                analogOutput.Values.Add(outTagValue);
                                dbContext.TagValues.Add(outTagValue);
                                dbContext.AnalogOutputs.Update(analogOutput);
                            }
                            
                        }

                        dbContext.SaveChanges();
                    }
                    
                }
            }
        }
        private void DigitalTimerCallback(int addressId, int digitalInputId)
        {
            lock (digitalInputValuesLockObj)
            {
                IOAddress address = dbContext.Addresses.FirstOrDefault(a => a.Id == addressId);
                DigitalInput digitalInput = dbContext.DigitalInputs.Include(a => a.Values).FirstOrDefault(a => a.Id == digitalInputId);
                if (address != null && address.Value != null && digitalInput != null)
                {
                    if (double.TryParse(address.Value, out double value))
                    {
                        TagValue tagValue = new TagValue();
                        tagValue.Value = address.Value;
                        tagValue.Type = "double";
                        tagValue.TagId = digitalInputId;
                        tagValue.Date = DateTime.Now;
                        if (digitalInput.Values == null)
                            digitalInput.Values = new List<TagValue>();
                        digitalInput.Values.Add(tagValue);
                        dbContext.DigitalInputs.Update(digitalInput);
                        dbContext.SaveChanges();
                    }

                }
            }
        }
        public void QuitTimer(int tagId)
        {
            this.timers[tagId].Dispose();
            this.timers.Remove(tagId);
        }
        public void QuitTimers()
        {
            foreach (var timer in this.timers)
                timer.Value.Dispose();
            this.timers = new Dictionary<int, Timer>();
        }
    }
}