using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Maya.EntityFramework.Domain
{
    public interface IEntity
    {
        int Id { get; set; }
    }

    public class Entity : IEntity
    {
        [Key, Required]
        [DisplayName("Id"), Display(Name = "Id")]
        public int Id { get; set; }
    }
}