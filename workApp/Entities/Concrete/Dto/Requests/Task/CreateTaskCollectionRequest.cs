using Core.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.Dto.Requests.Task
{
    public class CreateTaskCollectionRequest : IDto
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public DateTime Creation_Date { get; set; } = DateTime.Now;
    }
}
