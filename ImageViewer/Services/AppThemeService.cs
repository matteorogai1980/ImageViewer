using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Commons.Services;
using ImageViewer.Helpers;
using ImageViewer.Themes;
using Microsoft.Maui.Controls;

namespace ImageViewer.Services;

public class AppThemeService : IAppThemeService
{
	public readonly static string themeFile = Path.Combine(FileSystem.AppDataDirectory, "apptheme.txt");
	public readonly static string defaultTheme = Constants.BLUE;

    public AppThemeService()
	{
		if (!File.Exists(themeFile))
		{
			File.WriteAllText(themeFile, defaultTheme);
		}
	}

	public string ReadTheme()
	{
		return File.ReadAllText(themeFile);
	}

	public void WriteTheme(string appTheme)
	{
		File.WriteAllText(themeFile, appTheme);
	}

	public void InitTheme()
	{
		var theme = ReadTheme();
		LoadTheme(theme);
	}

	public void LoadTheme(string? theme)
	{
		if (string.IsNullOrEmpty(theme)) { return; }
		Collection<ResourceDictionary> mergedDictionaries = (Collection<ResourceDictionary>)Application.Current.Resources.MergedDictionaries;
		if (mergedDictionaries != null)
		{
			mergedDictionaries.Clear();
			mergedDictionaries.Add(new CommonStyle());

			switch (theme)
			{
				case Constants.BLUE:
					mergedDictionaries.Add(new BlueTheme());
					break;
				case Constants.PINK:
					mergedDictionaries.Add(new PinkTheme());
					break;
				default:
					mergedDictionaries.Add(new BlueTheme());
					break;
			}
			WriteTheme(theme);
			/*Shell.Current.Resources.TryGetValue(Constants.hdrResourceKey, out object hdrBgColor); Color statusBarColor = (Color)hdrBgColor;
			Shell.Current.Resources.TryGetValue(Constants.ftrResourceKey, out object ftrBgColor); Color navigationBarColor = (Color)ftrBgColor;
			double brightnessStatusBarColor = statusBarColor.Red * .3 + statusBarColor.Green * .59 + statusBarColor.Blue * .11;
			double brightnessNavigationBarColor = navigationBarColor.Red * .3 + navigationBarColor.Green * .59 + navigationBarColor.Blue * .11;
			PlatformsService.SetStatusNavigationBarsColor(statusBarColor, brightnessStatusBarColor > 0.5, navigationBarColor, brightnessNavigationBarColor > 0.5);*/
		}
	}
}
