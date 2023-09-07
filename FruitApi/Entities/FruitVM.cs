using FruitApi.DataAccess.Models;

namespace FruitApi.Entities
{
    public class FruitVM
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public FruitTypeVM Type { get; set; }

        public FruitVM(Fruit fruit) 
        {
            Id = fruit.Id;
            Name = fruit.Name;
            Description = fruit.Description;
            Type = new FruitTypeVM(fruit.FruitType);
        }
    }
}
