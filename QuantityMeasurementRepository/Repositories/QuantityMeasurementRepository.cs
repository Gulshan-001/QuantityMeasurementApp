using QuantityMeasurementModel.Entities;
using QuantityMeasurementRepository.Interfaces;
using QuantityMeasurementRepository.Context;
using System.Collections.Generic;
using System.Linq;

namespace QuantityMeasurementRepository.Repositories
{
    public class QuantityMeasurementRepository : IQuantityMeasurementRepository
    {
        private readonly ApplicationDbContext _context;

        public QuantityMeasurementRepository(ApplicationDbContext context)
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
            return _context.QuantityMeasurements.ToList();
        }

        public List<QuantityMeasurementEntity> GetByOperation(string operation)
        {
            return _context.QuantityMeasurements
                .Where(x => x.OperationType.ToLower() == operation.ToLower())
                .ToList();
        }

        public int GetOperationCount(string operation)
        {
            return _context.QuantityMeasurements
                .Count(x => x.OperationType.ToLower() == operation.ToLower());
        }
    }
}