namespace Commons.Services;

public interface IAppThemeService
{
    public void InitTheme();

    public string ReadTheme();

    public void WriteTheme(string appTheme);

    public void LoadTheme(string? theme);
}