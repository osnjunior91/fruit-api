using FruitApi.DataAccess.Models;
using FruitApi.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FruitApi.BusinessLogic
{
    public interface IBLFruitType
    {
        Task<IEnumerable<FruitTypeVM>> FindAllFruitTypesAsync();
        Task<FruitType> GetByIdAync(long id);
    }
}
