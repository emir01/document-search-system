using System;
using System.ComponentModel.DataAnnotations;

namespace DSS.Data.Model.Entities
{
    public class BaseEntitiy
    {
        /// <summary>
        /// The primary key of the entity.
        /// </summary>
        [Key, Required]
        public Guid Id { get; set; }

        /// <summary>
        /// Constructs a Base Entitiy
        /// </summary>
        public BaseEntitiy()
        {
            // Generate a new guid
            Id = Guid.NewGuid();
        }
    }
}
