using Commons.Converters;
using ImageViewer.ViewModels;
using ImageViewerDomain.Models;
using ImageViewerDomain.Repositories;

namespace ImageViewer.ViewModelsConverters;

public class PhotoDetailsViewModelConverter(IPhotoRepository photoRepository) : AbstractResourceConverter<IPhoto, PhotoDetailsViewModel>
{
    public override PhotoDetailsViewModel toDTO(IPhoto entity)
    {
        return new PhotoDetailsViewModel(photoRepository,new GalleryItemViewModelConverter())
        {
            ImageUrl = entity.Details.PhotoUrl,
            Title = entity.Details.Title,
            Description = entity.Details.Description,
            UserName = entity.Details.Username,
            TakenDate = entity.Details.TakenDate,
            RealName = entity.Details.Realname,
            HasGalleries = entity.Details.HasGalleries,
            PhotoItem = entity
        };
    }

    public override IPhoto toEntity(PhotoDetailsViewModel resource)
    {
        throw new NotImplementedException();
    }
}