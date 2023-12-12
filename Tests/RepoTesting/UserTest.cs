using infrastructure.DatabaseManager;
using infrastructure.Entities;
using infrastructure.Repositories;
using NUnit.Framework;

namespace Tests.RepoTesting
{
    [TestFixture]
    public class UserTest
    {
        private DBConnection dbConnection;
        private UserRepository userRepository;
        
        [SetUp]
        public void Setup()
        {
            dbConnection = new DBConnection();
            userRepository = new UserRepository(dbConnection);
        }

        [Test]
        public void TestDeleteUser()
        {
            var user = new Users
            {
                UserId = 1
            };
            userRepository.Delete(user.UserId);
        }

        [Test]
        public void TestUpdateUser()
        {
            var user = new Users
            {
                UserId = 2,
                Email = "Updated@Email.com",
                IsAdmin = true
            };
            userRepository.Update(user);
        }

        [Test]
        public void TestReadUser()
        {
            const int userId = 2;

            var readUser = userRepository.Read(userId);

            Console.WriteLine($"User ID: {readUser.UserId}");
            Console.WriteLine($"User Email: {readUser.Email}");
            Console.WriteLine($"User Type: {readUser.IsAdmin}");
            
        }

        [Test]
        public void TestCreateUser()
        {
            var user = new Users
            {
                Email = "test@email.com",
                IsAdmin = true
            };
        
            userRepository.Create(user);
        
            Console.WriteLine("User has been created!!");
        }
    }
}