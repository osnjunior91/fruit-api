using FruitApi.DataAccess.Models;
using FruitApi.DataAccess.Repository;
using FruitApi.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FruitApi.BusinessLogic
{
    public class BLFruitType : IBLFruitType
    {
        private readonly IRepository<FruitType> _repository;

        public BLFruitType(IRepository<FruitType> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<FruitTypeVM>> FindAllFruitTypesAsync()
        {
            var types = await _repository.FindAllAsync();
            return types.Select(x => new FruitTypeVM(x));
        }

        public async Task<FruitType> GetByIdAync(long id) => await _repository.GetByIdAsync(id);
    }
}
