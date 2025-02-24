using ImageViewerDomain.Helpers;

namespace ImageViewerDomain.Models;

public interface IPhoto : ICloneable
{
    public EnumeServiceProvider Provider { get; set; }
    public string? Owner { get; set; }
    
    public string? ThumbUrl { get; set; }
    public string? Title { get; set; }
    public string? Id { get; set; }
    public string? Secret { get; set; }
    public IPhotoDetails? Details { get; set; }
    public IGallery? Gallery { get; set; }
}