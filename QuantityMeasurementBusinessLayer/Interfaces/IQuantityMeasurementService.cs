using QuantityMeasurementModelLayer.DTO;
using QuantityMeasurementModelLayer.Entities;
using System.Collections.Generic;
namespace QuantityMeasurementBusinessLayer.Interfaces
{
    public interface IQuantityMeasurementService
    {
        bool CompareEquality(QuantityDTO q1, QuantityDTO q2);

        QuantityDTO Convert(QuantityDTO source, string targetUnit);

        QuantityDTO Add(QuantityDTO q1, QuantityDTO q2);

        QuantityDTO Subtract(QuantityDTO q1, QuantityDTO q2);

        double Divide(QuantityDTO q1, QuantityDTO q2);

        List<QuantityMeasurementEntity> GetAllHistory();

        List<QuantityMeasurementEntity> GetByOperation(string operation);

        int GetOperationCount(string operation);
    }
}