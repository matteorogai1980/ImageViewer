using Commons.Converters;
using ImageViewerDomain.DTO;
using ImageViewerDomain.Models;

namespace ImageViewerDataLayer.Converters;

public class FlickrPhotoInfoConverter : AbstractResourceConverter<IPhotoDetails, PhotoInfoDTO>
{
    private IPhotoDetails item;

    public FlickrPhotoInfoConverter(IPhotoDetails photo)
    {
        item = photo;
    }
    public override PhotoInfoDTO toDTO(IPhotoDetails entity)
    {
        throw new NotImplementedException();
    }

    public override IPhotoDetails toEntity(PhotoInfoDTO resource)
    {
        IPhotoDetails clonedItem = (IPhotoDetails)item.Clone();
        clonedItem.Title = resource.title.Content;
        clonedItem.Description = resource.description.Content;
        clonedItem.TakenDate = resource.dates!=null ? resource.dates.taken : "";
        clonedItem.Username = resource.owner.username;
        clonedItem.Realname = resource.owner.realname;
        clonedItem.PhotoUrl = "https://live.staticflickr.com/" + resource.server + "/" + resource.id + "_" +
                              resource.secret + "_b.jpg";
        return clonedItem;
    }
}