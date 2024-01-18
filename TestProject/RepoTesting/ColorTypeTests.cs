using infrastructure.DatabaseManager;
using infrastructure.Entities;
using infrastructure.Repositories;
using NUnit.Framework;

namespace TestProject.RepoTesting;

public class ColorTypeTests
{
    [TestFixture]
    public class UserTest
    {
        private DBConnection _dbConnection;
        private ColorTypeRepository _repository;

        [SetUp]
        public void Setup()
        {
            Environment.SetEnvironmentVariable("IsTestMode", "true");
            _dbConnection = new DBConnection();
            _dbConnection.TriggerRebuild();
            _repository = new ColorTypeRepository(_dbConnection);
        }

        [Test]
        public void CreateColorType()
        {
            var colorType = new ColorType
            {
                Color = "Grey"
            };

            var createdColor = _repository.Create(colorType);
            
            Assert.That(createdColor.Color, Is.EqualTo(colorType.Color));
        }
        [TearDown]
        public void CleanUp()
        {
            Environment.SetEnvironmentVariable("IsTestMode", "false");
        }
    }
}