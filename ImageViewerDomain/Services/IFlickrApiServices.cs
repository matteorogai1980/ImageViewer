using Commons.Models;
using ImageViewerDomain.Models;

namespace ImageViewerDomain.Services;

public interface IFlickrApiServices
{
    public Task<ResponseModel<List<IPhoto>>> SearchPhotosByText(string text,int pageIndex, int itemsPerPage);
    public Task<ResponseModel<IPhotoDetails>> GetPhotoInfo(string photoId, string photoSecret);
    public Task<ResponseModel<List<IGallery>>> SearchGalleriesByUsername(string username, int pageIndex, int itemsPerPage);
    public Task<ResponseModel<List<IPhoto>>> SearchPhotosOfGallery(string galleryId,int pageIndex, int itemsPerPage);
}