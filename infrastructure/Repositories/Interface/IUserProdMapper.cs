using System.Collections.Generic;
using infrastructure.Entities;

namespace infrastructure.Repositories.Interface
{
    public interface IUserProdMapper
    {
        List<Products> GetUserLikes(int UserId);
        
        List<Products> GetUserCart(int UserId);
    }
}