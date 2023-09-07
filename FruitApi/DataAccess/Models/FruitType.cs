using System.Collections.Generic;

namespace FruitApi.DataAccess.Models
{
    public class FruitType : Entity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public List<Fruit> Fruits { get; set; }
        public FruitType(long id, string name, string description)
            : base()
        {
            SetID(id);
            Name = name;
            Description = description;
        }
        public FruitType(long id) => SetID(id);
    }
}
