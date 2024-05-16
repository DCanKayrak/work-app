using Core.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Pomodoro : IEntity
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public byte Duration { get; set; }
        public DateTime Creation_Date { get; set; }
    }
}
