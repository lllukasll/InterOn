using System.Collections.Generic;
using InterOn.Data.DbModels;

namespace InterOn.Service.Interfaces
{
    public interface IRoleService
    {
        IEnumerable<Role> GetAll();
        Role GetRole(long id);
        void InsertRole(Role role);
        void UpdateRole(Role role);
        void DeleteRole(long id);
    }
}
