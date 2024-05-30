using Core.Utilities.Results.Abstract;
using Entities.Concrete.Dto.Requests.Task;
using Entities.Concrete.Dto.Responses.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ITaskService : ICrudService<TaskItem>
    {
        IResult ChangeTaskStatus(int id);
    }
}
