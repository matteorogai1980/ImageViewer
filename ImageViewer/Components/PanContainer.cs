namespace ImageViewer.Components;

using System;
using System.Windows.Input;

public class PanContainer : ContentView
{
    private const double MIN_SCALE = 1;
    private const double MAX_SCALE = 4;
    private double startScale;
    private double currentScale;
    private double xOffset, yOffset;
    private double x, y;


    public ICommand PinchCompletedCommand
    {
        get { return (ICommand)GetValue(PinchCompletedCommandProperty); }
        set { SetValue(PinchCompletedCommandProperty, value); }
    }
    public static readonly BindableProperty PinchCompletedCommandProperty = BindableProperty.Create(
                nameof(PinchCompletedCommand),
                typeof(ICommand),
                typeof(PanContainer),
                null);
    public ICommand PinchStartedCommand
    {
        get { return (ICommand)GetValue(PinchStartedCommandProperty); }
        set { SetValue(PinchStartedCommandProperty, value); }
    }
    public static readonly BindableProperty PinchStartedCommandProperty = BindableProperty.Create(
                nameof(PinchStartedCommand),
                typeof(ICommand),
                typeof(PanContainer),
                null);

    public static readonly BindableProperty IsPanEnabledProperty = BindableProperty.Create(nameof(IsPanEnabled), typeof(bool), typeof(PanContainer), false, BindingMode.TwoWay, propertyChanged: PanEnabledChanged);

    public bool IsPanEnabled
    {
        get { return (bool)GetValue(IsPanEnabledProperty); }
        set { SetValue(IsPanEnabledProperty, value); }
    }

    static void PanEnabledChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var contentView = (PanContainer)bindable;
        contentView.IsPanEnabled = (bool)newValue;
        if (contentView.IsPanEnabled)
        {
            contentView.AddPanGesture();
        }
        else
        {
            contentView.RemovePanGesture();
        }
    }

    public static readonly BindableProperty IsZoomEnabledProperty = BindableProperty.Create(nameof(IsZoomEnabled), typeof(bool), typeof(PanContainer), false, BindingMode.TwoWay, propertyChanged: ZoomEnabledChanged);

    public bool IsZoomEnabled
    {
        get { return (bool)GetValue(IsZoomEnabledProperty); }
        set { SetValue(IsZoomEnabledProperty, value); }
    }

    static void ZoomEnabledChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var contentView = (PanContainer)bindable;
        contentView.IsZoomEnabled = (bool)newValue;
        if (contentView.IsZoomEnabled)
        {
            contentView.AddZoomGesture();
        }
        else
        {
            contentView.RemoveZoomGesture();
        }
    }
    PanGestureRecognizer panGesture;
    IGestureRecognizer zoomGesture;

    public void RemovePanGesture()
    {
        GestureRecognizers.Remove(panGesture);
    }

    public void AddPanGesture()
    {
        if (!GestureRecognizers.Contains(panGesture)) GestureRecognizers.Add(panGesture);
    }

    public void RemoveZoomGesture()
    {
        GestureRecognizers.Remove(zoomGesture);
    }

    public void AddZoomGesture()
    {
        if (!GestureRecognizers.Contains(zoomGesture)) GestureRecognizers.Add(zoomGesture);
    }

    public PanContainer()
    {
#if ANDROID || IOS
        zoomGesture = new PinchGestureRecognizer();
        ((PinchGestureRecognizer)zoomGesture).PinchUpdated += OnPinchUpdated;
        GestureRecognizers.Add(zoomGesture);
#else
        zoomGesture = new TapGestureRecognizer { NumberOfTapsRequired = 2 };
        ((TapGestureRecognizer)zoomGesture).Tapped += OnTapped;
        GestureRecognizers.Add(zoomGesture);
#endif
        panGesture = new PanGestureRecognizer();
        panGesture.PanUpdated += OnPanUpdated;
        GestureRecognizers.Add(panGesture);

        Scale = MIN_SCALE;
        startScale = Scale;
        currentScale = startScale;
        TranslationX = TranslationY = 0;
        AnchorX = AnchorY = 0;
    }

    
    private void OnTapped(object sender, TappedEventArgs e)
    {
        if (currentScale > MIN_SCALE)
        {
            currentScale = MIN_SCALE;
            this.ScaleTo(currentScale, 250, Easing.CubicInOut);
            x = 0;
            y = 0;
            Content.TranslationX = 0;
            Content.TranslationY = 0;
        }
        else
        {
            Point? relativeToContainerPosition = e.GetPosition((View)sender);
            AnchorX = relativeToContainerPosition.Value.X / Width;
            AnchorY = relativeToContainerPosition.Value.Y / Height;//TODO tapped position
            x = AnchorX;
            y = AnchorY;
            currentScale = MAX_SCALE;
            this.ScaleTo(currentScale, 250, Easing.CubicInOut);
        }
    }

    private void OnPanUpdated(object sender, PanUpdatedEventArgs e)
    {
        Console.WriteLine("ContentWidth: " + Content.Width * currentScale);
        Console.WriteLine("ScreenWidth: " + Width);
        Console.WriteLine("ContentHeight: " + Content.Height * currentScale);
        Console.WriteLine("ScreenHeight: " + Height);

        switch (e.StatusType)
        {
            case GestureStatus.Running:

                // Translate and ensure we don't pan beyond the wrapped user interface element bounds.
#if ANDROID||IOS
                Content.TranslationX =
                Math.Max(Math.Min(0, x + e.TotalX), -Math.Abs((Content.Width * currentScale) - Width));
                Content.TranslationY =
                Math.Max(Math.Min(0, y + e.TotalY), -Math.Abs((Content.Height * currentScale) - Height));
#else
                Content.TranslationX =
                Math.Max(Math.Min(Math.Abs((Content.Width * currentScale) - Width) / 2, x + e.TotalX), -Math.Abs((Content.Width * currentScale) - Width) / 2);
                Content.TranslationY =
                Math.Max(Math.Min(Math.Abs((Content.Height * currentScale) - Height) / 2, y + e.TotalY), -Math.Abs((Content.Height * currentScale) - Height) / 2);
#endif
                break;

            case GestureStatus.Completed:
                // Store the translation applied during the pan
                xOffset = Content.TranslationX;
                yOffset = Content.TranslationY;
                x = Content.TranslationX;
                y = Content.TranslationY;
                break;
        }

        Console.WriteLine("Content.TranslationX: " + Content.TranslationX);
        Console.WriteLine("Content.TranslationY: " + Content.TranslationY);

    }


    private void OnPinchUpdated(object sender, PinchGestureUpdatedEventArgs e)
    {
        Console.WriteLine("eventScale: " + e.Scale);
        Console.WriteLine("Scale: " + Scale);
        if (e.Status == GestureStatus.Started)
        {
            // Store the current scale factor applied to the wrapped user interface element,
            // and zero the components for the center point of the translate transform.
            startScale = Content.Scale;
            Content.AnchorX = 0;
            Content.AnchorY = 0;
        }
        if (e.Status == GestureStatus.Running)
        {
            // Calculate the scale factor to be applied.
            currentScale += (e.Scale - 1) * startScale;
            currentScale = Math.Max(1, currentScale);

            // The ScaleOrigin is in relative coordinates to the wrapped user interface element,
            // so get the X pixel coordinate.
            double renderedX = Content.X + xOffset;
            double deltaX = renderedX / Width;
            double deltaWidth = Width / (Content.Width * startScale);
            double originX = (e.ScaleOrigin.X - deltaX) * deltaWidth;

            // The ScaleOrigin is in relative coordinates to the wrapped user interface element,
            // so get the Y pixel coordinate.
            double renderedY = Content.Y + yOffset;
            double deltaY = renderedY / Height;
            double deltaHeight = Height / (Content.Height * startScale);
            double originY = (e.ScaleOrigin.Y - deltaY) * deltaHeight;

            // Calculate the transformed element pixel coordinates.
            double targetX = xOffset - (originX * Content.Width) * (currentScale - startScale);
            double targetY = yOffset - (originY * Content.Height) * (currentScale - startScale);

            // Apply translation based on the change in origin.
            Content.TranslationX = Clamp(targetX, -Content.Width * (currentScale - 1), 0);
            Content.TranslationY = Clamp(targetY, -Content.Height * (currentScale - 1), 0);

            // Apply scale factor.
            Content.Scale = currentScale;
        }
        if (e.Status == GestureStatus.Completed)
        {
            // Store the translation delta's of the wrapped user interface element.
            xOffset = Content.TranslationX;
            yOffset = Content.TranslationY;
            x = Content.TranslationX;
            y = Content.TranslationY;
        }
    }

    private T Clamp<T>(T value, T minimum, T maximum) where T : IComparable
    {
        if (value.CompareTo(minimum) < 0)
            return minimum;
        else if (value.CompareTo(maximum) > 0)
            return maximum;
        else
            return value;
    }
}
