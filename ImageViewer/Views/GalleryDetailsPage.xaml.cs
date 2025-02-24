using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageViewer.ViewModels;

namespace ImageViewer.Views;

public partial class GalleryDetailsPage : ContentPage
{
    public GalleryDetailsPage(GalleryDetailsViewModel model)
    {
        InitializeComponent();
        BindingContext = model;
    }
}