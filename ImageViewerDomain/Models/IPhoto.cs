namespace ImageViewerDomain.Models;

public interface IPhoto : ICloneable
{
    public string? Owner { get; set; }
    
    public string? ThumbUrl { get; set; }
    public string? Title { get; set; }
    
    public IPhotoDetails? Details { get; set; }
    public IGallery? Gallery { get; set; }
}