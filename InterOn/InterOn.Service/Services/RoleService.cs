using System;
using System.Collections.Generic;
using System.Text;
using InterOn.Data.DbModels;
using InterOn.Repo.Interfaces;
using InterOn.Service.Interfaces;

namespace InterOn.Service.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRepository<Role> _roleRepository;

        public RoleService(IRepository<Role> roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public IEnumerable<Role> GetAll()
        {
            return _roleRepository.GetAll();
        }

        public Role GetRole(long id)
        {
            return _roleRepository.Get(id);
        }

        public void InsertRole(Role role)
        {
            _roleRepository.Insert(role);
        }

        public void UpdateRole(Role role)
        {
            _roleRepository.Update(role);
        }

        public void DeleteRole(long id)
        {
            var role = GetRole(id);
            _roleRepository.Remove(role);
            _roleRepository.Save();
        }
    }
}
