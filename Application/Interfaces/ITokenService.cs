using System;
using WebAPI.Core.Entities;

namespace WebAPI.Application.Interfaces;

public interface ITokenService
{
    string CreateToken(User user);
}
