using FruitApi.DataAccess.Models;
using FruitApi.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FruitApi.BusinessLogic
{
    public interface IBLFruit
    {
        Task<IEnumerable<FruitVM>> FindAllAsync();
        Task<long> CreateAsync(FruitDTO item);
        Task<FruitVM> GetByIdAync(long id);
        Task DeleteAync(long id);
        Task EditAync(long id, FruitDTO item);
    }
}
