using System;
using infrastructure.Repositories.Interface;

namespace infrastructure.Repositories.Factory
{
    public class CRUDFactory
    {
        public static ICrud<T> GetDao<T>(RepoType daoType) where T : class
        {
            switch (daoType)
            {
                case DAOType.CustomerDAO:
                    return new CustomerRepository() as ICrud<T>;
                case DAOType.UserDAO:
                    return new UserRepository() as ICrud<T>;
                case DAOType.DocumentDAO:
                    return new DocumentRepository() as ICrud<T>;
                case DAOType.CityDAO:
                    return new CityRepository() as ICrud<T>;
                case DAOType.ProjectDAO:
                    return new ProjectRepository() as ICrud<T>;
                case DAOType.ContentDAO:
                    return new ContentRepository() as ICrud<T>;
                default:
                    throw new ArgumentException("Invalid DAO type", nameof(daoType));
            }
        }
    }
}