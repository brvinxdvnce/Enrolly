using Enrolly.Admissions.Domain.Enums;

namespace Enrolly.Admissions.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public Role Role { get; set; }
    public List<Document> Documents { get; set; }
}