using Core.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.Dto.Requests
{
    public class CreatePomodoroRequest : IDto
    {
        public int UserId { get; set; }
        public byte Duration { get; set; }
    }
}
