namespace Enrolly.AdminClient.Models.Models;

public record ImportSummary (
    Guid Id,
    string CollectionName,
    DateTime StartedAt ,
    DateTime? CompletedAt, 
    int Received,
    int Added,
    int Removed, 
    int Updated
);