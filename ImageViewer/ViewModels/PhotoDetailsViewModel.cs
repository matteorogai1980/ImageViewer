using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ImageViewer.Localizations;
using ImageViewer.ViewModelsConverters;
using ImageViewer.Views;
using ImageViewerDomain.Models;
using ImageViewerDomain.Repositories;

namespace ImageViewer.ViewModels;

public partial class PhotoDetailsViewModel(IPhotoRepository photoRepository, GalleryItemViewModelConverter converter) : BaseViewModel
{
    public IPhoto PhotoItem { get; set; }
    
    [ObservableProperty]
    string imageUrl;
    [ObservableProperty]
    string title;
    [ObservableProperty]
    string description;
    [ObservableProperty]
    string takenDate;
    [ObservableProperty]
    string userName;
    [ObservableProperty]
    string realName;
    [ObservableProperty] 
    bool hasGalleries;

    public bool ButtonGalleryVisible
    {
        get
        {
            return HasGalleries && PhotoItem.Gallery == null;
        }
    }

    [RelayCommand]
    public async Task ShowGalleries()
    {
        IsBusy = true;
        var galleries = await  photoRepository.SearchGalleries(PhotoItem, 1, 50);
        IsBusy = false;
        if (galleries.Count > 0)
        {   
            var viewModel = new GalleriesViewModel(photoRepository, new GalleryItemViewModelConverter(), new PhotoItemViewModelConverter(), new PhotoDetailsViewModelConverter(photoRepository))
            {
                Galleries = new ObservableCollection<GalleryItemViewModel>(converter.toDTOs(galleries)),
                PhotoItem = PhotoItem,
                UserName = PhotoItem.Owner
            };
            await Shell.Current.Navigation.PushAsync(new GalleriesPage(viewModel));
        }
        else
        {
            await Shell.Current.DisplayAlert(AppLanguage.AlertTitle, AppLanguage.NoGalleries, AppLanguage.OkButton);
        }
    }
}