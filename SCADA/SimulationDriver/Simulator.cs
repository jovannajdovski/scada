using Microsoft.EntityFrameworkCore;
using SimulationDriver.model;

namespace SimulationDriver
{
    public class Driver
    {
        public SimulatorDBContext dbContext { get; set; }
        public object lockObj { get;set;}
        public Driver(object lockObj)
        {
            this.lockObj = lockObj;
        }
    }
    public class SimulationDriver:Driver
    {
        public SimulationDriver(object lockObj) : base(lockObj)
        {
            dbContext = new SimulatorDBContext();
        }
        public void StartSimulation()
        {
            SimulateWaterFillingAsync();
            SimulateGasFillingAsync();
            SimulateCoalFillingAsync();
        }
        private void SimulateWaterFillingAsync()
        {
            Task.Run(async () =>
            {
                double time = 0;
                const double MaxLevel = 1000;
                const double MinLevel = 100;
                const double frequency = 0.05;

                while (true)
                {
                    lock (lockObj)
                    {
                        
                        var existingEntity = dbContext.Addresses.FirstOrDefault(a => a.Id == 1);
                        if (existingEntity != null)
                        {
                            if (existingEntity.Type != null && existingEntity.Value != null)
                            {
                                double sin = Math.Sin(2 * Math.PI * new Random().NextDouble() * frequency * time);
                                if (sin < 0)
                                    existingEntity.Value = (double.Parse(existingEntity.Value) + (double.Parse(existingEntity.Value) - MinLevel) * sin).ToString();
                                else
                                    existingEntity.Value = (double.Parse(existingEntity.Value) + (MaxLevel - double.Parse(existingEntity.Value)) * sin).ToString();
                                
                            }
                            else
                            {
                                existingEntity.Type = "double";
                                existingEntity.Value = (MinLevel + Math.Abs(Math.Sin(2 * Math.PI * frequency * time)) * (MaxLevel - MinLevel)).ToString();

                            }
                            dbContext.Update(existingEntity);
                        }
                        

                        dbContext.SaveChanges();
                    }

                    time += 1;

                    await Task.Delay(1000);
                    
                }
            });
            
        }
        private void SimulateGasFillingAsync()
        {
            Task.Run(async () =>
            {
                double time = 0;
                const double MaxLevel = 1000;
                const double MinLevel = 100;
                const double frequency = 0.1;

                while (true)
                {
                    lock (lockObj)
                    {

                        var existingEntity = dbContext.Addresses.FirstOrDefault(a => a.Id == 2);
                        if (existingEntity != null)
                        {
                            if (existingEntity.Type != null && existingEntity.Value != null)
                            {
                                double sin = Math.Sin(2 * Math.PI * new Random().NextDouble() * frequency * time);
                                if (sin < 0)
                                    existingEntity.Value = (double.Parse(existingEntity.Value) + (double.Parse(existingEntity.Value) - MinLevel) * sin).ToString();
                                else
                                    existingEntity.Value = (double.Parse(existingEntity.Value) + (MaxLevel - double.Parse(existingEntity.Value)) * sin).ToString();

                            }
                            else
                            {
                                existingEntity.Type = "double";
                                existingEntity.Value = (MinLevel + Math.Abs(Math.Sin(2 * Math.PI * frequency * time)) * (MaxLevel - MinLevel)).ToString();

                            }
                            dbContext.Update(existingEntity);
                        }


                        dbContext.SaveChanges();
                    }

                    time += 1;

                    await Task.Delay(1000);
                }
            });
        }
        private void SimulateCoalFillingAsync()
        {
            Task.Run(async () =>
            {
                double time = 0;
                const double MaxLevel = 100;
                const double MinLevel = 0;
                const double frequency = 0.02;

                while (true)
                {
                    
                    lock (lockObj)
                    {

                        var existingEntity = dbContext.Addresses.FirstOrDefault(a => a.Id == 3);
                        if (existingEntity != null)
                        {
                            if (existingEntity.Type != null && existingEntity.Value != null)
                            {
                                double sin = Math.Sin(2 * Math.PI * new Random().NextDouble() * frequency * time);
                                if (sin < 0)
                                    existingEntity.Value = (double.Parse(existingEntity.Value) + (double.Parse(existingEntity.Value) - MinLevel) * sin).ToString();
                                else
                                    existingEntity.Value = (double.Parse(existingEntity.Value) + (MaxLevel - double.Parse(existingEntity.Value)) * sin).ToString();

                            }
                            else
                            {
                                existingEntity.Type = "double";
                                existingEntity.Value = (MinLevel + Math.Abs(Math.Sin(2 * Math.PI * frequency * time)) * (MaxLevel - MinLevel)).ToString();

                            }
                            dbContext.Update(existingEntity);
                        }


                        dbContext.SaveChanges();
                    }


                    time += 1;

                    await Task.Delay(1000);
                }
            });
        }
        private async Task SimulatePoolValveAsync()
        {
            double time = 0;

            while (true)
            {
                bool value = Math.Abs(Math.Sin(2 * Math.PI * time)) > 0.5 ? true:false ;
                IOAddress address = new IOAddress(4,"boolean", value.ToString());
                lock (lockObj)
                {
                    var existingEntity = dbContext.Addresses.FirstOrDefault(a => a.Id == address.Id);
                    if (existingEntity != null)
                    {
                        existingEntity.Type = address.Type;
                        existingEntity.Value = address.Value;
                    }
                    else
                    {
                        dbContext.Addresses.Add(address);
                    }

                    dbContext.SaveChanges();
                }

                time += 1;

                await Task.Delay(1000);
            }
        }
    }
    public class RealTimeDriver : Driver
    {
        public RealTimeDriver(object lockObj) : base(lockObj)
        {
            dbContext = new SimulatorDBContext();
        }
        public void StartSimulation()
        {
            Console.WriteLine("rtu sim");
            Task.Run(async () =>
            {
                List<RealTimeUnit> realTimeUnits;
                double time = 0;
                Random r = new Random();
                while (true)
                {
                    
                    realTimeUnits = dbContext.RealTimeUnits.Include(rt => rt.Address).ToList();
                    foreach (var realTimeUnit in realTimeUnits)
                    {
                        lock (lockObj)
                        {
                            double min = realTimeUnit.LowLimit;
                            double max = realTimeUnit.HighLimit;
                            double rand = (r.NextDouble() * 2 - 1) * (r.NextDouble() * (max - min) / 10);
                            IOAddress address = dbContext.Addresses.FirstOrDefault(a => a.Id == realTimeUnit.Address.Id);
                            if (address != null)
                            {
                                if (address.Type != null && address.Value != null)
                                {
                                    address.Value = (double.Parse(address.Value)+rand).ToString();
                                    if (double.Parse(address.Value) > max)
                                        address.Value = max.ToString();
                                    if (double.Parse(address.Value) < min)
                                        address.Value = min.ToString();
                                }
                                else
                                {
                                    address.Type = "double";
                                    address.Value = ((max+min)/2+rand).ToString();
                                }
                                dbContext.Update(address);
                            }


                            dbContext.SaveChanges();
                        }
                    }
                    time += 1;
                    await Task.Delay(1000);

                }
            });
        }
    }
}