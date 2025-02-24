using System.Reflection.Metadata;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ImageViewer.Helpers;

namespace ImageViewer.ViewModels;

public partial class BaseViewModel : ObservableObject
{
    
    [ObservableProperty]
    string busyMessage;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    bool isBusy;

    public bool IsNotBusy => !IsBusy;

    [RelayCommand]
    public void ChangeTheme()
    {
        MyApplication.INSTANCE.ThemeService!.LoadTheme(MyApplication.INSTANCE.ThemeService!.ReadTheme() == Constants.BLUE
            ? Constants.PINK
            : Constants.BLUE);
    }
}