using ImageViewerDomain.Helpers;
using ImageViewerDomain.Models;

namespace ImageViewerDataLayer.Models;

public class Photo : IPhoto
{
    public EnumeServiceProvider Provider { get; set; }
    public string? Id { get; set; }
    public string? Secret { get; set; }
    public string? Owner { get; set; }
    public string? ThumbUrl { get; set; }
    public string? PhotoUrl { get; set; }
    public string? Title { get; set; }
    public IPhotoDetails? Details { get; set; }
    public IGallery? Gallery { get; set; }
    
    public object Clone()
    {
        return new Photo();
    }
}