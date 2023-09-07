using System;

namespace FruitApi.DataAccess.Models
{
    public class Fruit : Entity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public long FruitTypeId { get; private set; }
        public FruitType FruitType { get; private set; }
        public Fruit(string name, string description)
            : base()
        {
            SetName(name);
            SetDescription(description);
        }

        private void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name can't be null or empty!");
            Name = name;
        }
        private void SetDescription(string description)
        {
            if (string.IsNullOrEmpty(description))
                throw new ArgumentException("Description can't be null or empty!");
            Description = description;
        }

        public void Edit(string name, string description, FruitType type)
        {
            SetName(name);
            SetDescription(description);
            SetType(type);
        }

        public void SetType(FruitType type)
        {
            if (type == null)
                throw new ArgumentException("Type Not Found");
            FruitType = type;
            FruitTypeId = type.Id;
        }
    }
}
