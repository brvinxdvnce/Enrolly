namespace DictionaryWorker.DTOs;

public class KreoProgramResponceDto
{
    public List<ProgramDto> Programs { get; set; }
    public Pagination Pagination { get; set; }
}

public class Pagination
{
    int Size { get; set; }
    int Count { get; set; }
    int Current { get; set; }
}