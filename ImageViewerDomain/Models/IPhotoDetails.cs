namespace ImageViewerDomain.Models;

public interface IPhotoDetails : ICloneable
{
    public string? Username { get; set; }
    public string? Realname { get; set; }
    public string? PhotoUrl { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? TakenDate { get; set; }
    public bool HasGalleries { get; set; }
}