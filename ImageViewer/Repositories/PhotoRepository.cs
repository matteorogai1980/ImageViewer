using ImageViewerDomain.Models;
using ImageViewerDomain.Repositories;
using ImageViewerDomain.Services;

namespace ImageViewer.Repositories;

public class PhotoRepository(IFlickrApiServices flickrApiServices) : IPhotoRepository
{
    public async Task<List<IPhoto>> SearchByText(string text, int page)
    {
        List<IPhoto> photos = new List<IPhoto>();
        var response = await flickrApiServices.SearchPhotosByText(text, page, 50);
        if (response.Metadata.Result)
        {
            photos.AddRange(response.Payload);
        }
        //Here we can add more services to download images for example instagram or other api integration
        //TODO...
        
        return photos;
    }

    public Task<List<IGallery>> SearchGalleries(IPhoto user, int page)
    {
        throw new NotImplementedException();
    }

    public Task<List<IPhoto>> SearchPhotoOfGallery(IGallery gallery, int page)
    {
        throw new NotImplementedException();
    }

    public Task<List<IPhoto>> GetPhotoDetails(IPhoto gallery, int page)
    {
        throw new NotImplementedException();
    }
}