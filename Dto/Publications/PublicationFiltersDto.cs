using System.ComponentModel.DataAnnotations;

namespace SmartsearchApi.Dto.Publications;

public class PublicationFiltersDto
{
    public string? Title { get; set; } = string.Empty;
    
    public string? Summary { get; set; } = string.Empty;
    
    [DataType(DataType.Date)]
    public DateTime? DateFrom { get; set; }
    
    [DataType(DataType.Date)]
    public DateTime? DateTo { get; set; }
    
    public long? ProjectId { get; set; }
}