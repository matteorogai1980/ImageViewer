using ImageViewer.Localizations;
using ImageViewerDataLayer.Models;
using ImageViewerDomain.Helpers;
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
        else
        {
            await Shell.Current.DisplayAlert(AppLanguage.AlertTitle, response.Metadata.ErrorMessage,AppLanguage.OkButton);
        }
        //Here we can add more services to download images for example instagram or other api integration
        //TODO...
        
        return photos;
    }

    public async Task<List<IGallery>> SearchGalleries(IPhoto photo, int page)
    {
        if (photo.Provider == EnumeServiceProvider.FLICKR)
        {
            var response = await flickrApiServices.SearchGalleriesByUsername(photo.Owner, page, 50);
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

    public async Task<List<IPhoto>> SearchPhotoOfGallery(IGallery gallery, int page)
    {
        if (gallery.Provider == EnumeServiceProvider.FLICKR)
        {
            var response = await flickrApiServices.SearchPhotosOfGallery(gallery.GalleryId, page, 50);
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