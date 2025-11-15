using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Application.Dtos;

namespace WebAPI.Application.Interfaces
{
    public interface ICareerService
    {
        /// <summary>
        /// Generates a ranked list of career recommendations for a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A list of CareerRecommendationDto objects.</returns>
        Task<List<CareerRecommendationDto>> GetRecommendationsAsync(int userId);
    }
}
