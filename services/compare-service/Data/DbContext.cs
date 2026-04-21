using Microsoft.EntityFrameworkCore;
using CompareService.Models;

namespace CompareService.Data
{
    public class CompareDbContext : DbContext
    {
        public CompareDbContext(DbContextOptions<CompareDbContext> options) : base(options) { }

        public DbSet<QuantityMeasurementEntity> QuantityMeasurements { get; set; }
    }

    public interface IMeasurementRepository
    {
        void Save(QuantityMeasurementEntity entity);
        List<QuantityMeasurementEntity> GetAll();
        List<QuantityMeasurementEntity> GetByOperation(string operation);
        int GetOperationCount(string operation);
    }

    public class MeasurementRepository : IMeasurementRepository
    {
        private readonly CompareDbContext _context;

        public MeasurementRepository(CompareDbContext context)
        {
            _context = context;
        }

        public void Save(QuantityMeasurementEntity entity)
        {
            _context.QuantityMeasurements.Add(entity);
            _context.SaveChanges();
        }

        public List<QuantityMeasurementEntity> GetAll()
        {
            return _context.QuantityMeasurements
                .Where(x => x.OperationType.ToLower() == "compare")
                .OrderByDescending(x => x.CreatedAt)
                .ToList();
        }

        public List<QuantityMeasurementEntity> GetByOperation(string operation)
        {
            return _context.QuantityMeasurements
                .Where(x => x.OperationType.ToLower() == operation.ToLower())
                .OrderByDescending(x => x.CreatedAt)
                .ToList();
        }

        public int GetOperationCount(string operation)
        {
            return _context.QuantityMeasurements
                .Count(x => x.OperationType.ToLower() == operation.ToLower());
        }
    }
}
