using FruitApi.DataAccess.Models;
using FruitApi.DataAccess.Repository;
using FruitApi.Entities;
using FruitApi.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FruitApi.BusinessLogic
{
    public class BLFruit : IBLFruit
    {
        private readonly IRepository<Fruit> _repository;
        private readonly IBLFruitType _bLFruitType;

        public BLFruit(IRepository<Fruit> repository, IBLFruitType bLFruitType)
        {
            _repository = repository;
            _bLFruitType = bLFruitType;
        }
        public async Task<long> CreateAsync(FruitDTO item)
        {
           var fruit = new Fruit(item.Name, item.Description);
           var fruitType = await _bLFruitType.GetByIdAync(item.Type);
           fruit.SetType(fruitType);
           var response = await _repository.CreateAync(fruit);
           return response.Id;
        }

        public async Task DeleteAync(long id)
        {
            var fruit = await _repository.GetByIdAsync(id);
            ValidateFruit(fruit);
            await _repository.DeleteAsync(fruit);
        }


        public async Task<IEnumerable<FruitVM>> FindAllAsync()
        {
            var types = await _repository.FindAllAsync(x => x.FruitType);
            return types.Select(x => new FruitVM(x));
        }

        public async Task<FruitVM> GetByIdAync(long id)
        {
            return new FruitVM(await _repository.GetByIdAsync(id, x => x.FruitType));
        }

        public async Task EditAync(long id, FruitDTO item)
        {
            var fruit = await _repository.GetByIdAsync(id);
            ValidateFruit(fruit);
            var fruitType = await _bLFruitType.GetByIdAync(item.Type);
            fruit.Edit(item.Name, item.Description, fruitType);
            await _repository.UpdateAsync(fruit);
        }
        private void ValidateFruit(Fruit fruit)
        {
            if (fruit == null)
                throw new NotFoundException("Fruit not found");
        }
    }
}
