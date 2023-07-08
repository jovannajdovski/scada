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

        public void SimulateWaterFillingAsync()
        {
            Task.Run(async () =>
            {
                double time = 0;
                const double MaxLevel = 1000;
                const double MinLevel = 100;
                const double frequency = 0.05;

                while (true)
                {
                    double value = MinLevel + Math.Abs(Math.Sin(2 * Math.PI * frequency * time)) * (MaxLevel - MinLevel);

                    IOAdress adress = new IOAdress(1, "double", value.ToString());

                    lock (lockObj)
                    {
                        
                        var existingEntity = dbContext.Adresses.FirstOrDefault(a => a.Id == adress.Id);
                        
                        if (existingEntity != null)
                        {
                            Console.WriteLine(existingEntity.Id);
                            Console.WriteLine(existingEntity.Value);
                            existingEntity.Type = adress.Type;
                            double sin = Math.Sin(2 * Math.PI * new Random().NextDouble()*frequency * time);
                            if (sin < 0)
                                existingEntity.Value = (double.Parse(existingEntity.Value) + (double.Parse(existingEntity.Value) - MinLevel) * sin).ToString();
                            else
                                existingEntity.Value = (double.Parse(existingEntity.Value) + (MaxLevel - double.Parse(existingEntity.Value)) * sin).ToString();
                            dbContext.Update(existingEntity);
                        }
                        else
                        {
                            dbContext.Adresses.Add(adress);
                        }

                        dbContext.SaveChanges();
                        Console.WriteLine(existingEntity.Value);
                    }

                    time += 1;

                    await Task.Delay(1000);
                    
                }
            });
            
        }
        public void SimulateGasFillingAsync()
        {
            Task.Run(async () =>
            {
                double time = 0;
                const double MaxLevel = 1000;
                const double MinLevel = 100;
                const double frequency = 0.1;

                while (true)
                {
                    double value = MinLevel + Math.Abs(Math.Sin(2 * Math.PI * frequency * time)) * (MaxLevel - MinLevel);
                    IOAdress adress = new IOAdress(2, "double", value.ToString());
                    lock (lockObj)
                    {

                        var existingEntity = dbContext.Adresses.FirstOrDefault(a => a.Id == adress.Id);

                        if (existingEntity != null)
                        {
                            Console.WriteLine(existingEntity.Id);
                            Console.WriteLine(existingEntity.Value);
                            existingEntity.Type = adress.Type;
                            double sin = Math.Sin(2 * Math.PI * new Random().NextDouble() * frequency * time);
                            if (sin < 0)
                                existingEntity.Value = (double.Parse(existingEntity.Value) + (double.Parse(existingEntity.Value) - MinLevel) * sin).ToString();
                            else
                                existingEntity.Value = (double.Parse(existingEntity.Value) + (MaxLevel - double.Parse(existingEntity.Value)) * sin).ToString();
                            dbContext.Update(existingEntity);
                        }
                        else
                        {
                            dbContext.Adresses.Add(adress);
                        }

                        dbContext.SaveChanges();
                        Console.WriteLine(existingEntity.Value);
                    }

                    time += 1;

                    await Task.Delay(1000);
                }
            });
        }
        public void SimulateCoalFillingAsync()
        {
            Task.Run(async () =>
            {
                double time = 0;
                const double MaxLevel = 100;
                const double MinLevel = 0;
                const double frequency = 0.02;

                while (true)
                {
                    double value = MinLevel + Math.Abs(Math.Sin(2 * Math.PI * frequency * time)) * (MaxLevel - MinLevel);
                    IOAdress adress = new IOAdress(3, "double", value.ToString());
                    lock (lockObj)
                    {

                        var existingEntity = dbContext.Adresses.FirstOrDefault(a => a.Id == adress.Id);

                        if (existingEntity != null)
                        {
                            Console.WriteLine(existingEntity.Id);
                            Console.WriteLine(existingEntity.Value);
                            existingEntity.Type = adress.Type;
                            double sin = Math.Sin(2 * Math.PI * new Random().NextDouble() * frequency * time);
                            if (sin < 0)
                                existingEntity.Value = (double.Parse(existingEntity.Value) + (double.Parse(existingEntity.Value) - MinLevel) * sin).ToString();
                            else
                                existingEntity.Value = (double.Parse(existingEntity.Value) + (MaxLevel - double.Parse(existingEntity.Value)) * sin).ToString();
                            dbContext.Update(existingEntity);
                        }
                        else
                        {
                            dbContext.Adresses.Add(adress);
                        }

                        dbContext.SaveChanges();
                        Console.WriteLine(existingEntity.Value);
                    }


                    time += 1;

                    await Task.Delay(1000);
                }
            });
        }
        public async Task SimulatePoolValveAsync()
        {
            double time = 0;

            while (true)
            {
                bool value = Math.Abs(Math.Sin(2 * Math.PI * time)) > 0.5 ? true:false ;
                IOAdress adress = new IOAdress(4,"boolean", value.ToString());
                lock (lockObj)
                {
                    var existingEntity = dbContext.Adresses.FirstOrDefault(a => a.Id == adress.Id);
                    if (existingEntity != null)
                    {
                        existingEntity.Type = adress.Type;
                        existingEntity.Value = adress.Value;
                    }
                    else
                    {
                        dbContext.Adresses.Add(adress);
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
    }
}