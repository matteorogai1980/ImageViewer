namespace ImageViewerDomain.DTO;

public class FlickrErrorResponseDTO
{
    public string Stat { get; set; } 
    public int Code { get; set; }
    public string Message { get; set; }
}