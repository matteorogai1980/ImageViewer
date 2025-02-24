using CommunityToolkit.Mvvm.ComponentModel;
using ImageViewerDomain.Models;

namespace ImageViewer.ViewModels;

public partial class GalleryItemViewModel : BaseViewModel
{
    public IGallery GalleryItem { get; set; }
    
    [ObservableProperty]
    string imageUrl;
    [ObservableProperty]
    string title;
    [ObservableProperty]
    string owner;
}