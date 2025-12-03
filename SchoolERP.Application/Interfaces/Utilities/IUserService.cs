using SchoolERP.Application.DTOs.Utilities;
using SchoolERP.Domain.Entities.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolERP.Application.Interfaces.Utilities
{
    public interface IUserService
    {

        Task<IEnumerable<User>> GetAll();
        Task<User?> GetById(int id);
        Task<User> Create(UserDto dto);
        Task<User?> Update(int id, UserDto dto);
        Task<bool> Delete(int id);
        Task<LoginResponseDto> AuthenticateAsync(LoginRequestDto loginDto);

    }

}
