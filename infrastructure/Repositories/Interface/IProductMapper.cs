using infrastructure.Entities;

namespace infrastructure.Repositories.Interface
{
    public interface IProductMapper
    {
        List<Products> getProductbyType(int TypeId);

        List<Products> getProductbyGenderType(int TypeId, string Gender);

        List<Products> getProductbyGender(string Gender);
    }
}