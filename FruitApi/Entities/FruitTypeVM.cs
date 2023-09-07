using FruitApi.DataAccess.Models;

namespace FruitApi.Entities
{
    public class FruitTypeVM
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        
        public FruitTypeVM(FruitType type)
        {
            Id = type.Id;
            Name = type.Name;
            Description = type.Description;
        }
    }
}
