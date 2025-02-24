using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ImageViewer.ViewModelsConverters;
using ImageViewer.Views;
using ImageViewerDomain.Models;
using ImageViewerDomain.Repositories;

namespace ImageViewer.ViewModels;

public partial class GalleryDetailsViewModel(IPhotoRepository photoRepository, PhotoItemViewModelConverter converter, PhotoDetailsViewModelConverter detailsConverter) : BaseViewModel
{
    public static int PAGE_SIZE = 50;
    
    public IGallery GalleryItem { get; set; }
    [ObservableProperty]  
    string title;
    [ObservableProperty]
    public ObservableCollection<PhotoItemViewModel> photos = [];
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
        var photos = await photoRepository.SearchPhotosOfGallery(GalleryItem, pageIndex++, PAGE_SIZE);
        foreach (var photo in photos)
        {
            Photos.Add(converter.toDTO(photo));
        }
        IsBusy = false;
    }

    [RelayCommand]
    public async void PhotoTapped(PhotoItemViewModel photo)
    {
        if (photo.PhotoItem.Details is null)
        {
            IsBusyDetails = true;
            var photoDetaile = await photoRepository.GetPhotoDetails(photo.PhotoItem);
            photo.PhotoItem.Details = photoDetaile;
            photo.PhotoItem.Gallery = GalleryItem;
            IsBusyDetails = false;
        }
        var detailsViewModel = detailsConverter.toDTO(photo.PhotoItem);
        await Shell.Current.Navigation.PushAsync(new PhotoDetailsPage(detailsViewModel));
    }
}