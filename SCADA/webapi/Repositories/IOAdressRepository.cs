

using webapi.model;

namespace webapi.Repositories
{

    public interface IIOAddressRepository
    {
        List<IOAddress> GetAll();
        IOAddress GetById(int id);
        void Add(IOAddress ioAddress);
        void Delete(IOAddress ioAddress);
    }

    public class IOAddressRepository : IIOAddressRepository
    {
        private readonly ScadaDBContext _context;

        public IOAddressRepository(ScadaDBContext context)
        {
            _context = context;
        }

        public List<IOAddress> GetAll()
        {
            return _context.Addresses.ToList();
        }

        public IOAddress GetById(int id)
        {
            return _context.Addresses.Find(id);
        }

        public void Add(IOAddress ioAddress)
        {
            _context.Addresses.Add(ioAddress);
            _context.SaveChanges();
        }

        public void Delete(IOAddress ioAddress)
        {
            _context.Addresses.Remove(ioAddress);
            _context.SaveChanges();
        }
    }
}
