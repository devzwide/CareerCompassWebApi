using System;
using CareerCompassWebApi.Core.Entities;

namespace CareerCompassWebApi.Application.Interfaces;

public interface ITokenService
{
    string CreateToken(User user);
}
