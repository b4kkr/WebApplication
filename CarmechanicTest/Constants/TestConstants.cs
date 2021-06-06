using System;
using WebApplication.Constants;
using WebApplication.Entities;

namespace CarmechanicTest.Constants
{
    public class TestConstants
    {
        public static Car GetTestCar()
        {
           return new Car()
            {
                Type = GetTestCarType(),
                Vintage = new DateTime(year: 2011, month: 5, day:7),
                EngineSerial = "ASD",
                LicensePlate = "ABC123",
            };
        }

        public static CarType GetTestCarType() => new CarType()
        {
            Model = "Mondeo",
            TypeName = "Ford"
        };

        public static User GetTestUser() => new User()
        {
            Email = "test@mail.com",
                Name = "Test User",
                Password = PasswordGenerator.Instance.Generate(8),
                PhoneNumber = "0613456789"
        };

        public static Repair GetTestRepair() => new Repair()
        {
            Guid = Guid.NewGuid(),
            StatusEntity = new StatusEntity(){Status = Status.AddedForService},
            Paid = false,
            Price = 1000,
            Works = "Oil Change"
        };
        
        
    }
}