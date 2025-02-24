using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageViewer.ViewModels;

namespace ImageViewer.Views;

public partial class PhotoDetailsPage : ContentPage
{
    public PhotoDetailsPage(PhotoDetailsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}