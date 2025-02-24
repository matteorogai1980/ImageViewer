using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ImageViewer.ViewModelsConverters;
using ImageViewer.Views;
using ImageViewerDomain.Models;
using ImageViewerDomain.Repositories;

namespace ImageViewer.ViewModels;

public partial class GalleriesViewModel(IPhotoRepository photoRepository, GalleryItemViewModelConverter galleryConverter, PhotoItemViewModelConverter photoConverter, PhotoDetailsViewModelConverter detailsConverter) : BaseViewModel
{
    public static int PAGE_SIZE = 50;

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
        var moreGalleries = await photoRepository.SearchGalleries(PhotoItem, pageIndex++,PAGE_SIZE);
        foreach (var gallery in moreGalleries)
        {
            Galleries.Add(galleryConverter.toDTO(gallery));
        }
        IsBusy = false;
    }

    [RelayCommand]
    public async void GalleryTapped(GalleryItemViewModel gallery)
    {
        IsBusy = true;
        var photosOfGallery = await photoRepository.SearchPhotosOfGallery(gallery.GalleryItem, 1, PAGE_SIZE);
        IsBusy = false;
        var viewModel = new GalleryDetailsViewModel(photoRepository, photoConverter, detailsConverter){
            Photos = new ObservableCollection<PhotoItemViewModel>(photoConverter.toDTOs(photosOfGallery)),
            GalleryItem = gallery.GalleryItem,
            Title = gallery.Title
        };
        await Shell.Current.Navigation.PushAsync(new GalleryDetailsPage(viewModel));
    }
}