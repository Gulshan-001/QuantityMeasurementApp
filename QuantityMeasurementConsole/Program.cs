using QuantityMeasurementRepositoryLayer.Repositories;
using QuantityMeasurementBusinessLayer.Services;
using QuantityMeasurementConsole.Controllers;
using QuantityMeasurementConsole.UI;
using QuantityMeasurementRepositoryLayer.Interfaces;

class Program
{
    static void Main(string[] args)
    {
        //  Choose repository (SQL or Cache)
        IQuantityMeasurementRepository repository = new SqlQuantityMeasurementRepository();
        // IQuantityMeasurementRepository repository = new QuantityMeasurementCacheRepository();

        //  Service
        var service = new QuantityMeasurementServiceImpl(repository);

        //  Controller
        var controller = new QuantityMeasurementController(service);

        //  Menu (IMPORTANT FIX)
        var menu = new Menu(controller, repository);

        menu.Start();
    }
}