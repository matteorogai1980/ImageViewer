using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ImageViewer.ViewModelsConverters;
using ImageViewerDomain.Repositories;

namespace ImageViewer.ViewModels;

public partial class SearchViewModel(IPhotoRepository photoRepository, PhotoItemViewModelConverter converter) : BaseViewModel
{
    [ObservableProperty]  
    string searchText;
    [ObservableProperty]
    public ObservableCollection<PhotoItemViewModel> photos = [];
    
    
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
        var photos = await photoRepository.SearchByText(SearchText, 1);
        foreach (var photo in photos)
        {
            Photos.Add(converter.toDTO(photo));
        }
        IsBusy = false;
    }
}