using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity.Data
{
    public abstract class Entity
    {
        public Entity () 
        {
            Id = Guid.NewGuid().ToString();
            CreatedDate = DateTime.Now; 
            UpdatedDate = DateTime.Now;
        }
        [Key]
        public string Id { get; set; } 
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
