using Commons.Converters;
using ImageViewer.ViewModels;
using ImageViewerDomain.Models;

namespace ImageViewer.ViewModelsConverters;

public class GalleryItemViewModelConverter : AbstractResourceConverter<IGallery, GalleryItemViewModel>
{
    public override GalleryItemViewModel toDTO(IGallery entity)
    {
        return new GalleryItemViewModel()
        {
            ImageUrl = entity.GalleryThumbUrl,
            Title = entity.Title,
            GalleryItem = entity
        };
    }

    public override IGallery toEntity(GalleryItemViewModel resource)
    {
        throw new NotImplementedException();
    }
}