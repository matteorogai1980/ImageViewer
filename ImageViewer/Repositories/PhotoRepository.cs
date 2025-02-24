using ImageViewer.Localizations;
using ImageViewerDataLayer.Models;
using ImageViewerDomain.Helpers;
using ImageViewerDomain.Models;
using ImageViewerDomain.Repositories;
using ImageViewerDomain.Services;

namespace ImageViewer.Repositories;

public class PhotoRepository(IFlickrApiServices flickrApiServices) : IPhotoRepository
{
    public async Task<List<IPhoto>> SearchByText(string text, int page, int pageSize)
    {
        List<IPhoto> photos = new List<IPhoto>();
        var response = await flickrApiServices.SearchPhotosByText(text, page, pageSize);
        if (response.Metadata.Result)
        {
            photos.AddRange(response.Payload);
        }
        else
        {
            await Shell.Current.DisplayAlert(AppLanguage.AlertTitle, response.Metadata.ErrorMessage,AppLanguage.OkButton);
        }
        //Here we can add more services to download images for example instagram or other api integration
        //TODO...
        
        return photos;
    }

    public async Task<List<IGallery>> SearchGalleries(IPhoto photo, int page, int pageSize)
    {
        if (photo.Provider == EnumeServiceProvider.FLICKR)
        {
            var response = await flickrApiServices.SearchGalleriesByUsername(photo.Owner, page, pageSize);
            if (response.Metadata.Result)
            {
                return response.Payload;
            }
            else
            {
                await Shell.Current.DisplayAlert(AppLanguage.AlertTitle, response.Metadata.ErrorMessage,AppLanguage.OkButton);
            }
        }
        return [];
    }

    public async Task<List<IPhoto>> SearchPhotosOfGallery(IGallery gallery, int page, int pageSize)
    {
        if (gallery.Provider == EnumeServiceProvider.FLICKR)
        {
            var response = await flickrApiServices.SearchPhotosOfGallery(gallery.GalleryId, page, pageSize);
            if (response.Metadata.Result)
            {
                return response.Payload;
            }
            else
            {
                await Shell.Current.DisplayAlert(AppLanguage.AlertTitle, response.Metadata.ErrorMessage,AppLanguage.OkButton);
            }
        }
        return [];
    }

    public async Task<IPhotoDetails> GetPhotoDetails(IPhoto photo)
    {
        //Here we can check the provider and call the specific service
        if (photo.Provider == EnumeServiceProvider.FLICKR)
        {
            var response = await flickrApiServices.GetPhotoInfo(photo.Id, photo.Secret);
            if (response.Metadata.Result)
            {
                //Get if user has some galleries
                var hasGalleries = await flickrApiServices.SearchGalleriesByUsername(photo.Owner, 1, 1);
                if (hasGalleries.Metadata.Result && hasGalleries.Payload != null)
                    response.Payload.HasGalleries = hasGalleries.Payload.Count > 0;
                return response.Payload;
            }
            else
            {
                await Shell.Current.DisplayAlert(AppLanguage.AlertTitle, response.Metadata.ErrorMessage,AppLanguage.OkButton);
            }
        }
        return new PhotoDetails();
    }
}