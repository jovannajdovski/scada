using Microsoft.EntityFrameworkCore;
using webapi.model;

namespace webapi.Repositories
{
    public interface IDigitalInputRepository
    {
        List<DigitalInput> GetAll();
        DigitalInput GetById(int id);
        void Add(DigitalInput digitalInput);
        void Update(DigitalInput digitalInput);
        void Delete(DigitalInput digitalInput);
        List<DigitalInput> GetAllScanningDigitalInputs();
    }

    public class DigitalInputRepository : IDigitalInputRepository
    {
        private readonly ScadaDBContext _context;

        public DigitalInputRepository(ScadaDBContext context)
        {
            _context = context;
        }

        public List<DigitalInput> GetAll()
        {
            return _context.DigitalInputs.Include(di => di.Values).ToList();
        }

        public DigitalInput GetById(int id)
        {
            return _context.DigitalInputs.Include(ai => ai.Address).FirstOrDefault(ai => ai.Id == id);
        }

        public void Add(DigitalInput digitalInput)
        {
            _context.DigitalInputs.Add(digitalInput);
            _context.SaveChanges();
        }

        public void Update(DigitalInput digitalInput)
        {          
            _context.DigitalInputs.Update(digitalInput);
            _context.SaveChanges();
        }

        public void Delete(DigitalInput digitalInput)
        {
            _context.DigitalInputs.Remove(digitalInput);
            _context.SaveChanges();
        }
        public List<DigitalInput> GetAllScanningDigitalInputs()
        {
            return _context.DigitalInputs.Include(ai => ai.Values).Include(ai => ai.Address).Where(ai => ai.IsScanning).ToList();
        }
    }
}
