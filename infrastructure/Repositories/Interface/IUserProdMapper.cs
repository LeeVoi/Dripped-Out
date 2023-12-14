using infrastructure.Entities;

namespace infrastructure.Repositories.Interface
{
    public interface IUserProdMapper
    {
        List<Products> GetUserLikes(int UserId);
        
        List<Products> GetUserCart(int UserId);

        void AddProductToUserLikes(int userId, int productId);
        void AddProductToUserCart(int userId, int productId);
    }
}