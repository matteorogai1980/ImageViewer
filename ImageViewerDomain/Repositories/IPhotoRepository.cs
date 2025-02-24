using ImageViewerDomain.Models;

namespace ImageViewerDomain.Repositories;

public interface IPhotoRepository
{
    Task<List<IPhoto>> SearchByText(string text, int page);
    Task<List<IGallery>> SearchGalleries(IPhoto user, int page);
    Task<List<IPhoto>> SearchPhotoOfGallery(IGallery gallery, int page);
    Task<IPhotoDetails> GetPhotoDetails(IPhoto photo);
}