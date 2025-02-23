using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageViewer.ViewModels;

namespace ImageViewer.Views;

public partial class SearchPage : ContentPage
{
    public SearchPage(SearchViewModel model)
    {
        InitializeComponent();
        BindingContext = model;
        model.ChangeThemeColor = (Color)MyApplication.INSTANCE.ThemeService.CurrentThemeDictionary()["Secondary"];
    }
}