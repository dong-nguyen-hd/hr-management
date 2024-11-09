using Business.Domain.Models.Base;
using Business.Extensions;

namespace Business.Domain.Models;

public class Technology : BaseModel
{
    public string Id { get; set; } = RelateText.GenId();
    public string Name { get; set; } = null!;
    public string CategoryId { get; set; }
    public Category Category { get; set; }
}