using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FruitApi.DataAccess.Models
{
    public abstract class Entity : IEquatable<Entity>
    {
        public Entity()
        {
            CreatedAt = DateTime.Now;
        }

        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; private set; }
        public DateTime CreatedAt { get; private set; }
        protected void SetID(long id) => Id = id;

        public bool Equals(Entity other)
        {
            return Id == other.Id;
        }
    }
}
