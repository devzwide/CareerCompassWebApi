using System;
using CareerCompassWebApi.Application.Dtos;

namespace CareerCompassWebApi.Application.Interfaces;

public interface ICareerService
{
    Task<List<CareerDto>> GetCareersAsync(int userId);
}
