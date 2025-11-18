using SchoolERP.Application.DTOs;
using SchoolERP.Application.DTOs.Utilities;
using SchoolERP.Application.Interfaces;
using SchoolERP.Application.Interfaces.Utilities;
using SchoolERP.Domain.Entities;
using SchoolERP.Domain.Entities.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolERP.Application.Services.Utilities
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;

        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _repo.GetAll();
        }

        public async Task<User?> GetById(int id)
        {
            return await _repo.GetById(id);
        }

        public async Task<User> Create(UserDto dto)
        {
            var user = new User
            {
                UserName = dto.UserName,
                Password = dto.Password,
                Email = dto.Email,
                FullName = dto.FullName,
                MobileNo = dto.MobileNo,
                UserType = dto.UserType,
                Category = dto.Category,

                Status = "Active",
                AuthAdd = "",
                AuthDel = "",
                AuthLstEdt = ""
            };

            return await _repo.Add(user);
        }

        public async Task<User?> Update(int id, UserDto dto)
        {
            var existing = await _repo.GetById(id);
            if (existing == null)
                return null;

            existing.UserName = dto.UserName;
            existing.Email = dto.Email;
            existing.FullName = dto.FullName;
            existing.MobileNo = dto.MobileNo;

            return await _repo.Update(existing);
        }

        public async Task<bool> Delete(int id)
        {
            return await _repo.Delete(id);
        }
    }
}
