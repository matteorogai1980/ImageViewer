using ImageViewerDomain.Helpers;
using ImageViewerDomain.Models;

namespace ImageViewerDataLayer.Models;

public class Gallery : IGallery
{
    public EnumeServiceProvider Provider { get; set; }
    public string? GalleryId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? GalleryThumbUrl { get; set; }
    
    public object Clone()
    {
        return new Gallery();
    }
}