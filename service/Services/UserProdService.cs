using infrastructure.Entities;
using infrastructure.Entities.Helper;
using infrastructure.Repositories;
using infrastructure.Repositories.Factory;
using infrastructure.Repositories.Interface;

namespace service.Services;

public class UserProdService
{
    private readonly IUserProdMapper _UserCartRepository;


    public UserProdService(UserProdRepository userProdRepository)
    {
        _UserCartRepository = userProdRepository;
    }


    public List<Products> GetUserLikes(int userId)
    {
        return _UserCartRepository.GetUserLikes(userId);
    }

    public List<Products> GetUserCart(int userId)
    {
        return _UserCartRepository.GetUserCart(userId);
    }

    public void AddProductToUserCart(int userId, int productId)
    {
        _UserCartRepository.AddProductToUserCart(userId, productId);
    }

    public void AddProductToUserLikes(int userId, int productId)
    {
        _UserCartRepository.AddProductToUserLikes(userId, productId);
    }



}