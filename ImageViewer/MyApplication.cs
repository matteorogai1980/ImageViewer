using System;
using Commons.Services;
using ImageViewer.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace ImageViewer;

public class MyApplication
{
    public static MyApplication INSTANCE { get; private set; }

    public IAppThemeService? ThemeService { get; private set; }

    public IServiceProvider ServiceProvider { get; private set; }

    public MyApplication(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
        INSTANCE = this;
        ThemeService = serviceProvider.GetService<IAppThemeService>();
        ThemeService!.InitTheme();
    }
    
}