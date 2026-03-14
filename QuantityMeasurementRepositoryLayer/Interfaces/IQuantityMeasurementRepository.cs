using QuantityMeasurementModelLayer.Entities;
using System.Collections.Generic;

namespace QuantityMeasurementRepositoryLayer.Interfaces
{
    public interface IQuantityMeasurementRepository
    {
        void Save(QuantityMeasurementEntity entity);

        List<QuantityMeasurementEntity> GetAllMeasurements();
    }
}