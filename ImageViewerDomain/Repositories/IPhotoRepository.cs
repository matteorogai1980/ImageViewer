using ImageViewerDomain.Models;

namespace ImageViewerDomain.Repositories;

public interface IPhotoRepository
{
    Task<List<IPhoto>> SearchByText(string text, int page, int pageSize);
    Task<List<IGallery>> SearchGalleries(IPhoto user, int page, int pageSize);
    Task<List<IPhoto>> SearchPhotosOfGallery(IGallery gallery, int page, int pageSize);
    Task<IPhotoDetails> GetPhotoDetails(IPhoto photo);
}