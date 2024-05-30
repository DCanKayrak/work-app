using Core.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Pomodoro : BaseEntity
    {
        public int UserId { get; set; }
        public byte Duration { get; set; }
    }
}
