using QuantityMeasurementRepositoryLayer.Repositories;
using QuantityMeasurementBusinessLayer.Services;
using QuantityMeasurementConsole.Controllers;
using QuantityMeasurementConsole.UI;
using QuantityMeasurementConsole.Interfaces;

namespace QuantityMeasurementConsole
{
    class Program
    {
        static void Main()
        {
            var repository = QuantityMeasurementCacheRepository.GetInstance();

            var service = new QuantityMeasurementServiceImpl(repository);

            var controller = new QuantityMeasurementController(service);

            IMenu menu = new Menu(controller);

            menu.Start();
        }
    }
}