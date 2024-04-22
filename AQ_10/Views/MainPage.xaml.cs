using Plugin.Maui.Audio;
using AQ_10.ViewModel;
using System.Reflection;
using Microsoft.Maui.Controls;

namespace AQ_10;

/// <summary>
/// Represents the first scene of the questionnaire, handling initialization of the view model, audio playback, 
/// and user interactions for the first question.
/// </summary>
public partial class MainPage : ContentPage
{
    private readonly IAudioManager audioManager;
    private IAudioPlayer backgroundAudio;
    bool audioOn = true;

    /// <summary>
    /// Initializes a new instance of the MainPage class, setting up audio management and bindings.
    /// </summary>
    /// <param name="audioManager">The audio manager to handle audio operations.</param>
    public MainPage(IAudioManager audioManager)
    {
        InitializeComponent();
        var viewModel = new MainPageViewModel();
        this.BindingContext = viewModel;
        this.audioManager = audioManager;
        InitializeAudio();
    }

    /// <summary>
    /// Initializes audio playback for background music, narrative, and UI sounds.
    /// </summary>
    private async void InitializeAudio()
    {
        // Load and play various audio elements, adjusting volumes and looping as necessary.
        backgroundAudio = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("Ambient.wav"));

        backgroundAudio.Loop = true;
        backgroundAudio.Volume = 0.3;
    }

    /// <summary>
    /// Toggles playback of the background audio based on its current state.
    /// </summary>
    private void OnAudioButtonClicked(object sender, EventArgs e)
    {
        if (backgroundAudio.IsPlaying)
        {
            backgroundAudio.Pause();
            audioOn = false;
        }
        else
        {
            backgroundAudio.Play();
            audioOn = true;
        }
    }

    /// <summary>
    /// Stops and disposes of an audio player when it is no longer needed.
    /// </summary>
    /// <param name="player">The audio player to dispose.</param>
    private void DisposeAudioPlayer(IAudioPlayer player)
    {
        if (player != null)
        {
            if (player.IsPlaying)
            {
                player.Stop();
            }
            player.Dispose();
        }
    }

    /// <summary>
    /// Cleans up audio resources when the page is no longer visible.
    /// </summary>
    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        if (backgroundAudio != null)
        {
            backgroundAudio.Stop();
            DisposeAudioPlayer(backgroundAudio);
        }
    }

    /// <summary>
    /// Ensures that audio is properly initialized when the page appears.
    /// </summary>
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await Task.Delay(100);

        InitializeAudio();

        if (audioOn == true)
        {
            backgroundAudio.Play();
        }
    }
}