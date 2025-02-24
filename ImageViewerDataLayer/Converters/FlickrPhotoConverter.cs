using Commons.Converters;
using ImageViewerDomain.DTO;
using ImageViewerDomain.Helpers;
using ImageViewerDomain.Models;

namespace ImageViewerDataLayer.Converters;

public class FlickrPhotoConverter : AbstractResourceConverter<IPhoto, PhotoDTO>
{
    private IPhoto item;

    public FlickrPhotoConverter(IPhoto photo)
    {
        item = photo;
    }
    public override PhotoDTO toDTO(IPhoto entity)
    {
        throw new NotImplementedException();
    }

    public override IPhoto toEntity(PhotoDTO resource)
    {
        IPhoto clonedItem = (IPhoto)item.Clone();
        clonedItem.Provider = EnumeServiceProvider.FLICKR;
        clonedItem.Owner = resource.Owner;
        clonedItem.Title = resource.Title;
        clonedItem.Id = resource.Id;
        clonedItem.Secret = resource.Secret;
        clonedItem.ThumbUrl = "https://live.staticflickr.com/" + resource.Server + "/" + resource.Id + "_" +
                              resource.Secret + "_q.jpg";
        return clonedItem;
    }
}