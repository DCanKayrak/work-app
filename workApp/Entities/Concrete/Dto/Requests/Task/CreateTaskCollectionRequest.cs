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
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
