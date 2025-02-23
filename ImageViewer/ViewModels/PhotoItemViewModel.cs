using CommunityToolkit.Mvvm.ComponentModel;

namespace ImageViewer.ViewModels;

public partial class PhotoItemViewModel : BaseViewModel
{
    [ObservableProperty]
    string imageUrl;
    [ObservableProperty]
    string title;
    [ObservableProperty]
    string owner;
}