using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ImageEffectEditor.Commands;
using ImageProcessorToolkit;
using ToolDevExam2.Helpers;

namespace ImageEffectEditor.ViewModel;

public partial class MainViewModel : ObservableObject
{
    private const string ImagePath = "Resources/crate.jpg";
    
    [ObservableProperty]
    private ImageSource _originalImage;

    [ObservableProperty]
    private ImageSource _processedImage;
    
    // Grayscale (boolean, default = false)
    // Hue Shift (int [0-360], default = 0)
    // Saturation (double [-10 – 10], default = 1)
    // Overlay Color (string [Red, Blue, Green])
    // Overlay Alpha (byte [0-255], default = 0)
    [ObservableProperty]
    private ImageProcessorSettings _imageSettings = new ImageProcessorSettings();

    private readonly ImageProcessor _imageProcessor = new ImageProcessor();
    private readonly GenericFileService _genericFileService = new GenericFileService();

    private static CommandHistory CommandHistory { get; } = new CommandHistory();
    
    private bool    _isUpdatingFromCommand = false;
    
    public MainViewModel()
    {
        _ = InitImages();
        
        ImageSettings.OnSettingChanged += OnImageSettingsChanged;
        
        CommandHistory.StacksChanged += (sender, args) =>
        {
            UndoCommand.NotifyCanExecuteChanged();
            RedoCommand.NotifyCanExecuteChanged();
        };
    }

    private void OnImageSettingsChanged(object sender, string propertyname, object? oldvalue, object? newvalue)
    {
        if (_isUpdatingFromCommand) return;

        ICommand? command = propertyname switch
        {
            nameof(ImageProcessorSettings.Grayscale) => new SetPropertyCommand<bool>(SetGrayscale, (bool)oldvalue!, (bool)newvalue!),
            nameof(ImageProcessorSettings.HueShift) => new SetPropertyCommand<int>(SetHueShift, (int)oldvalue!, (int)newvalue!),
            nameof(ImageProcessorSettings.Saturation) => new SetPropertyCommand<double>(SetSaturation, (double)oldvalue!, (double)newvalue!),
            nameof(ImageProcessorSettings.OverlayColor) => new SetPropertyCommand<string>(SetOverlayColor, (string)oldvalue!, (string)newvalue!),
            nameof(ImageProcessorSettings.OverlayAlpha) => new SetPropertyCommand<byte>(SetOverlayAlpha, (byte)oldvalue!, (byte)newvalue!),
            _ => null
        };
        
        if (command != null)
        {
            CommandHistory.PushCommand(command);
        }

        _ = UpdateProcessedImage(ImageSettings);
    }

    private void SetOverlayAlpha(byte value)
    {
        _isUpdatingFromCommand = true;
        ImageSettings.OverlayAlpha = value;
        _isUpdatingFromCommand = false;
    }

    private void SetOverlayColor(string value)
    {
        _isUpdatingFromCommand = true;
        ImageSettings.OverlayColor = value;
        _isUpdatingFromCommand = false;
    }

    private void SetSaturation(double value)
    {
        _isUpdatingFromCommand = true;
        ImageSettings.Saturation = value;
        _isUpdatingFromCommand = false;
    }

    private void SetHueShift(int value)
    {
        _isUpdatingFromCommand = true;
        ImageSettings.HueShift = value;
        _isUpdatingFromCommand = false;
    }

    private void SetGrayscale(bool value)
    {
        _isUpdatingFromCommand = true;
        ImageSettings.Grayscale = value;
        _isUpdatingFromCommand = false;
    }

    private async Task InitImages()
    {
        OriginalImage = await _imageProcessor.ApplyAsync(ImageLoader.LoadFromFile(ImagePath), ImageSettings) ?? throw new InvalidOperationException();
        ProcessedImage = await _imageProcessor.ApplyAsync(OriginalImage, ImageSettings) ?? throw new InvalidOperationException();
    }
    
    public async Task UpdateProcessedImage(ImageProcessorSettings newImageSettings)
    {
        ProcessedImage = await _imageProcessor.ApplyAsync(OriginalImage, newImageSettings) ?? throw new InvalidOperationException();
    }

    [RelayCommand]
    private void LoadTexture()
    {
        var imageSource = ImageLoader.LoadFromFile();
        if (imageSource == null) return;

        OriginalImage = imageSource;
        ProcessedImage = imageSource;
        ImageSettings.Reset();
        CommandHistory.Reset();
    }

    [RelayCommand]
    private async Task LoadImageSettings()
    {
        string? path = DialogService.OpenFile();
        if (path == null) return;

        var setting = await _genericFileService.LoadJsonAsync<ImageProcessorSettings>(path);
        if (setting != null) ImageSettings.CopyFrom(setting);
    }

    [RelayCommand]
    private async Task SaveImageSettings()
    {
        string? path = DialogService.SaveFile();
        if (path == null) return;

        await _genericFileService.SaveJsonAsync(ImageSettings, path);
    }

    [RelayCommand]
    private void ResetImageSettings()
    {
        ImageSettings.Reset();
        CommandHistory.Reset();
    }

    public bool CanUndo => CommandHistory.CanUndo;

    [RelayCommand(CanExecute = nameof(CanUndo))]
    private void Undo()
    {
        CommandHistory.Undo();
        _ = UpdateProcessedImage(ImageSettings);
    }
    
    public bool CanRedo => CommandHistory.CanRedo;

    [RelayCommand(CanExecute = nameof(CanRedo))]
    private void Redo()
    {
        CommandHistory.Redo();
        _ = UpdateProcessedImage(ImageSettings);
    }

    [RelayCommand]
    private void LoadDefaultTheme()
    {
        Application.Current.Resources.MergedDictionaries.Clear();
    }

    [RelayCommand]
    private void LoadDarkTheme()
    {
        var darkTheme = new ResourceDictionary
        {
            Source = new Uri($@"Theming/ButtonStyle.xaml", UriKind.Relative)
        };
        Application.Current.Resources.MergedDictionaries.Clear();
        Application.Current.Resources.MergedDictionaries.Add(darkTheme);
    }
}