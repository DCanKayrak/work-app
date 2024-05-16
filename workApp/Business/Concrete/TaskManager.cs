using Business.Abstract;
using Core.Utilities.Results.Abstract;
using Entities.Concrete.Dto.Requests.Task;
using Entities.Concrete.Dto.Responses.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class TaskManager : ITaskService
    {
        public IDataResult<TaskCollectionResponse> Create(CreateTaskCollectionRequest request)
        {
            throw new NotImplementedException();
        }

        public IDataResult<Task> Create(Task entity)
        {
            throw new NotImplementedException();
        }

        public IDataResult<TaskCollectionResponse> Delete(Guid taskGuid)
        {
            throw new NotImplementedException();
        }

        public IResult Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<TaskCollectionResponse> Get(Guid taskGuid)
        {
            throw new NotImplementedException();
        }

        public IDataResult<Task> Get(int id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<Task>> GetAll()
        {
            throw new NotImplementedException();
        }

        public IDataResult<TaskCollectionResponse> GetAllWithUser(int userId)
        {
            throw new NotImplementedException();
        }

        public IDataResult<TaskCollectionResponse> Update(UpdateTaskCollectionRequest request)
        {
            throw new NotImplementedException();
        }

        public IResult Update(Task entity)
        {
            throw new NotImplementedException();
        }
    }
}
