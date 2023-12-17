using infrastructure.Entities;

namespace infrastructure.Repositories.Interface
{
    public interface ILoginMapper
    {
        Users GetUserByEmail(string email);
    }
}