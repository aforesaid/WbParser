using System;
using System.ComponentModel.DataAnnotations;

namespace WbParser.Domain.Entities
{
    public class Entity
    {
        [Key]
        public Guid Id { get; private set; } = new();
        public DateTime Created { get; private set; } = DateTime.Now;
        public DateTime Updated { get; private set; } = DateTime.Now;

        protected void SetUpdated()
        {
            Updated = DateTime.Now;
        }
    }
}