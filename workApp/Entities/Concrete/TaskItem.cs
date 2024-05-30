using Core.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class TaskItem : BaseEntity, IEntity
    {
        public int CollectionId { get; set; }
        public string Name { get; set; }
        public bool IsCompleted { get; set; }
    }
}
