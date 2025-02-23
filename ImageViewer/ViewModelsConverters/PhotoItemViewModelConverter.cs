using Commons.Converters;
using ImageViewer.ViewModels;
using ImageViewerDomain.Models;

namespace ImageViewer.ViewModelsConverters;

public class PhotoItemViewModelConverter : AbstractResourceConverter<IPhoto, PhotoItemViewModel>
{
    public override PhotoItemViewModel toDTO(IPhoto entity)
    {
        return new PhotoItemViewModel()
        {
            ImageUrl = entity.ThumbUrl,
            Title = entity.Title,
            Owner = entity.Owner,
        };
    }

    public override IPhoto toEntity(PhotoItemViewModel resource)
    {
        throw new NotImplementedException();
    }
}