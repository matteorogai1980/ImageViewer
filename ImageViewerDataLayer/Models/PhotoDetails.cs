using ImageViewerDomain.Models;

namespace ImageViewerDataLayer.Models;

public class PhotoDetails : IPhotoDetails
{
    public string? Username { get; set; }
    public string? Realname { get; set; }
    public string? PhotoUrl { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? TakenDate { get; set; }
    public object Clone()
    {
        return new PhotoDetails();
    }
}