using Core.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class TaskItem : IEntity
    {
        public Guid Id { get; set; }
        public Guid CollectionId { get; set; }
        public string Name { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime Creation_Date { get; set; }
    }
}
