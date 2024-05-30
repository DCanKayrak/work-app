using Core.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.Dto.Requests.Pomodoro
{
    public class CreatePomodoroRequest : IDto
    {
        public byte Duration { get; set; }
    }
}
