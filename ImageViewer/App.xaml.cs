namespace ImageViewer;

public partial class App : Application
{
    public MyApplication ImageViewerApp;
    
    public App(IServiceProvider serviceProvider)
    {
        InitializeComponent();
        ImageViewerApp = new MyApplication(serviceProvider);
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new AppShell());
    }
}