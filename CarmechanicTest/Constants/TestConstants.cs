using System;
using WebApplication.Constants;
using WebApplication.Entities;

namespace CarmechanicTest.Constants
{
    public class TestConstants
    {
        public static Car GetTestCar(
            string engineSerial = "ASD",
            string licensePlate = "ABC123",
            string typeName = "Ford",
            string model = "Mondeo"
        )
        {
           return new Car()
            {
                Type = GetTestCarType(typeName: typeName, model: model),
                Vintage = new DateTime(year: 2011, month: 5, day:7),
                EngineSerial = engineSerial,
                LicensePlate = licensePlate,
            };
        }

        public static CarType GetTestCarType(
            string typeName = "Ford",
            string model = "Focus") => new CarType()
        {
            Model = model,
            TypeName = typeName
        };

        public static User GetTestUser(
            string name = "Test User",
            string email = "test@mail.com",
            string phoneNumber = "0613456789") => new User()
        {
            Email = email,
                Name = name,
                Password = PasswordGenerator.Instance.Generate(8),
                PhoneNumber = phoneNumber
        };

        public static Repair GetTestRepair(
            Status status = Status.AddedForService,
            bool paid = false,
            int price = 1000,
            string works = "Oil change") => new Repair()
        {
            Guid = Guid.NewGuid(),
            StatusEntity = new StatusEntity(){Status = status},
            Paid = paid,
            Price = price,
            Works = works
        };
        
        
    }
}