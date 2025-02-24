using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ImageViewer.ViewModelsConverters;
using ImageViewer.Views;
using ImageViewerDomain.Repositories;

namespace ImageViewer.ViewModels;

public partial class SearchViewModel(IPhotoRepository photoRepository, PhotoItemViewModelConverter converter, PhotoDetailsViewModelConverter detailsConverter) : BaseViewModel
{
    [ObservableProperty]  
    string searchText;
    [ObservableProperty]
    public ObservableCollection<PhotoItemViewModel> photos = [];
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusyDetails))]
    bool isBusyDetails;

    private int pageIndex = 1;
    public bool IsNotBusyDetails => !IsBusyDetails;
    [RelayCommand]
    public async Task Search()
    {
        Console.WriteLine(SearchText);
        IsBusy = true;
        var photos = await photoRepository.SearchByText(SearchText, 1);
        Photos = new ObservableCollection<PhotoItemViewModel>(converter.toDTOs(photos));
        IsBusy = false;
    }
    [RelayCommand]
    public async Task LoadMore()
    {
        Console.WriteLine("LoadMore");
        IsBusy = true;
        var photos = await photoRepository.SearchByText(SearchText, pageIndex++);
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
            IsBusyDetails = false;
        }
        var detailsViewModel = detailsConverter.toDTO(photo.PhotoItem);
        await Shell.Current.Navigation.PushAsync(new PhotoDetailsPage(detailsViewModel));
    }
}