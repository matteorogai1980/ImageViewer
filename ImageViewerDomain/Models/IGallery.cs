using ImageViewerDomain.Helpers;

namespace ImageViewerDomain.Models;

public interface IGallery : ICloneable
{
    public EnumeServiceProvider Provider { get; set; }
    public string? GalleryId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? GalleryThumbUrl { get; set; }
}