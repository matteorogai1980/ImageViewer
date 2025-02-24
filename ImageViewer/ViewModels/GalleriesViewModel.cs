using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ImageViewer.ViewModelsConverters;
using ImageViewer.Views;
using ImageViewerDomain.Models;
using ImageViewerDomain.Repositories;

namespace ImageViewer.ViewModels;

public partial class GalleriesViewModel(IPhotoRepository photoRepository, GalleryItemViewModelConverter converter) : BaseViewModel
{
    public IPhoto PhotoItem { get; set; }

    [ObservableProperty]
    public string userName;
    [ObservableProperty]
    public ObservableCollection<GalleryItemViewModel> galleries = [];
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusyDetails))]
    bool isBusyDetails;

    private int pageIndex = 2;
    public bool IsNotBusyDetails => !IsBusyDetails;
    
    [RelayCommand]
    public async Task LoadMore()
    {
        Console.WriteLine("LoadMore");
        IsBusy = true;
        var moreGalleries = await photoRepository.SearchGalleries(PhotoItem, pageIndex++);
        foreach (var gallery in moreGalleries)
        {
            Galleries.Add(converter.toDTO(gallery));
        }
        IsBusy = false;
    }

    [RelayCommand]
    public async void Galleryapped(PhotoItemViewModel photo)
    {
        /*if (photo.PhotoItem.Details is null)
        {
            IsBusyDetails = true;
            var photoDetaile = await photoRepository.GetPhotoDetails(photo.PhotoItem);
            photo.PhotoItem.Details = photoDetaile;
            IsBusyDetails = false;
        }
        var detailsViewModel = detailsConverter.toDTO(photo.PhotoItem);
        Shell.Current.Navigation.PushAsync(new PhotoDetailsPage(detailsViewModel));*/
    }
}