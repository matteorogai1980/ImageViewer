using Commons.Services;
using ImageViewer.ViewModelsConverters;
using ImageViewer.Helpers;
using ImageViewer.Repositories;
using ImageViewer.Services;
using ImageViewer.ViewModels;
using ImageViewerDataLayer.Converters;
using ImageViewerDataLayer.Models;
using ImageViewerDataLayer.Services;
using ImageViewerDomain.Helpers;
using ImageViewerDomain.Models;
using ImageViewerDomain.Repositories;
using ImageViewerDomain.Services;
using Microsoft.Extensions.Logging;

namespace ImageViewer;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("Frutiger.ttf", "Frutiger");
                fonts.AddFont("Frutiger-bold.ttf", "FrutigerBold");
                fonts.AddFont("materialdesignicons-webfont.ttf", "MaterialDesignIcons");
            });

#if DEBUG
        builder.Logging.AddDebug();
        builder.Services.AddSingleton<IApiHelper, DevApiHelper>();
#elif PRODUCTION
        builder.Services.AddSingleton<IApiHelper, ProdApiHelper>();
#endif
        builder.Services.AddSingleton<IAppThemeService, AppThemeService>();
        builder.Services.AddSingleton<HttpClient>();

        builder.Services.AddScoped<FlickrPhotoConverter>();
        builder.Services.AddScoped<FlickrGalleryConverter>();
        builder.Services.AddScoped<FlickrPhotoInfoConverter>();

        builder.Services.AddScoped<PhotoItemViewModelConverter>();
        builder.Services.AddScoped<PhotoDetailsViewModelConverter>();
        builder.Services.AddScoped<GalleryItemViewModelConverter>();

        builder.Services.AddScoped<IPhoto, Photo>();
        builder.Services.AddScoped<IGallery, Gallery>();
        builder.Services.AddScoped<IPhotoDetails, PhotoDetails>();
        
        builder.Services.AddScoped<IPhotoRepository, PhotoRepository>();
        builder.Services.AddScoped<IFlickrApiServices, FlickrApiServices>();

        builder.Services.AddScoped<SearchViewModel>();
        return builder.Build();
    }
}