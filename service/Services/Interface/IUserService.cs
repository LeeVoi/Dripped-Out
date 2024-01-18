using infrastructure.Entities;

namespace service.Services.Interface;

public interface IUserService
{
    Users getUserById(int userId);
    void createUser(Users users);
    void updateUser(Users users);
    void deleteUser(int userId);

}