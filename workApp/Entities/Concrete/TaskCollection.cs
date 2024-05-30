using Core.Entity.Abstract;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class TaskCollection : BaseEntity, IEntity
    {
        public int UserId {  get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<TaskItem> Tasks { get; set; }  = new List<TaskItem>();
    }
}
