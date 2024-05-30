using System.ComponentModel.DataAnnotations;
using Core.Entity.Abstract;

namespace Entities.Concrete.Dto.Requests.Task;

public class CreateTaskRequest : IDto
{
    [Required]
    public int CollectionId { get; set; }
    [Required]
    public string Name { get; set; }
}