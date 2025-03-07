using CommunityToolkit.Mvvm.ComponentModel;
using ImageViewerDomain.Models;

namespace ImageViewer.ViewModels;

public partial class PhotoItemViewModel : BaseViewModel
{
    public IPhoto PhotoItem { get; set; }
    
    [ObservableProperty]
    string imageUrl;
    [ObservableProperty]
    string title;
    [ObservableProperty]
    string owner;
}