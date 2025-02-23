using Commons.Converters;
using ImageViewerDomain.DTO;
using ImageViewerDomain.Models;

namespace ImageViewerDataLayer.Converters;

public class FlickrGalleryConverter : AbstractResourceConverter<IGallery, GalleryDTO>
{
    private IGallery item;

    public FlickrGalleryConverter(IGallery gallery)
    {
        item = gallery;
    }
    public override GalleryDTO toDTO(IGallery entity)
    {
        throw new NotImplementedException();
    }

    public override IGallery toEntity(GalleryDTO resource)
    {
        IGallery clonedItem = (IGallery)item.Clone();
        clonedItem.GalleryId = resource.GalleryId;
        clonedItem.Title = resource.title.Content;
        clonedItem.Description = resource.description.Content;
        clonedItem.GalleryThumbUrl = "https://live.staticflickr.com/" + resource.primary_photo_server + "/" + resource.primary_photo_id + "_" +
                              resource.primary_photo_secret + "_q.jpg";
        return clonedItem;
    }
}