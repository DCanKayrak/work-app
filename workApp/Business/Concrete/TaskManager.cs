using Business.Abstract;
using Core.Utilities.Results.Abstract;
using Entities.Concrete.Dto.Requests.Task;
using Entities.Concrete.Dto.Responses.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class TaskManager : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskManager(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        [SecuredOperation("USER")]
        public IDataResult<List<TaskItem>> GetAll(Expression<Func<TaskItem, bool>> filter)
        {
            List<TaskItem> tasks = _taskRepository.GetAll(filter);
            if (tasks != null)
            {
                return new SuccessDataResult<List<TaskItem>>(tasks, "Tasklar başarıyla getirildi");
            }

            return new ErrorDataResult<List<TaskItem>>("Tasklar getirilemedi");
        }
        [SecuredOperation("USER")]
        public IDataResult<TaskItem> Get(int id)
        {
            TaskItem? task = _taskRepository.Get(t => t.Id == id);
            if (task != null)
            {
                return new SuccessDataResult<TaskItem>(task, "Task başarıyla getirildi");
            }
            return new ErrorDataResult<TaskItem>("Task getirilemedi");
        }
        [SecuredOperation("USER")]
        public IDataResult<TaskItem> Create(TaskItem entity)
        {
            TaskItem? task = _taskRepository.Create(entity);
            if (task != null)
            {
                return new SuccessDataResult<TaskItem>(task, "Task başarıyla oluşturuldu");
            }
            return new ErrorDataResult<TaskItem>("Task oluşturulurken bir hatayla karşılaşıldı");
        }
        [SecuredOperation("USER")]
        public IResult Update(TaskItem entity)
        {
            throw new NotImplementedException();
        }
        [SecuredOperation("USER")]
        public IResult Delete(int id)
        {
            _taskRepository.Delete(this.Get(id).Data);
            return new SuccessResult("Task başarıyla getirildi");
        }
        [SecuredOperation("USER")]
        public IResult ChangeTaskStatus(int id)
        {
            TaskItem tempTask = _taskRepository.Get(t => t.Id == id);
            tempTask.IsCompleted = !tempTask.IsCompleted;
            tempTask.Updated_At = DateTime.UtcNow;
            _taskRepository.Update(tempTask);
            return new SuccessResult("Task güncellendi");
        }
    }
}
