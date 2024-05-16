using Core.Utilities.Results.Abstract;
using Entities.Concrete.Dto.Requests.Task;
using Entities.Concrete.Dto.Responses.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ITaskService : ICrudService<Task>
    {
        public IDataResult<TaskCollectionResponse> Create(CreateTaskCollectionRequest request);
        public IDataResult<TaskCollectionResponse> Update(UpdateTaskCollectionRequest request);
        public IDataResult<TaskCollectionResponse> Delete(Guid taskGuid);
        public IDataResult<TaskCollectionResponse> Get(Guid taskGuid);
        public IDataResult<TaskCollectionResponse> GetAllWithUser(int userId);
    }
}
