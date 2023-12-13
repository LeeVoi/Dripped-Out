using infrastructure.Entities;
using infrastructure.Entities.Helper;
using infrastructure.Repositories.Factory;
using infrastructure.Repositories.Interface;

namespace service.Services;

public class UserService
{
    private readonly ICrud<Users> _userRepository;

    public UserService(CRUDFactory crudFactory)
    {
        _userRepository = crudFactory.GetRepository<Users>(RepoType.UserRepo);
    }

    public Users getUserById(int userId)
    {
        return _userRepository.Read(userId);
    }

    public void createUser(Users users)
    { 
        _userRepository.Create(users);
    }

    public void updateUser(Users users)
    {
        _userRepository.Update(users);
    }

    public void deleteUser(int userId)
    {
        _userRepository.Delete(userId);
    }
}