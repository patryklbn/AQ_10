using Plugin.Maui.Audio;
using AQ_10.ViewModel;
using System.Reflection;
using Microsoft.Maui.Controls;

namespace AQ_10;

/// <summary>
/// Represents the fourth scene of the application, handling initialization of the view model, audio playback,
/// and user interactions for the fourth question.
/// </summary>
public partial class SceneFour : ContentPage
{
    private readonly IAudioManager audioManager;
    private IAudioPlayer backgroundAudio;
    private IAudioPlayer radButton;
    private IAudioPlayer prevButton;
    private IAudioPlayer nextButton;
    private IAudioPlayer narrator;
    bool audioOn = true;

    /// <summary>
    /// Initializes a new instance of the SceneFour class, setting up audio management and bindings.
    /// </summary>
    /// <param name="audioManager">The audio manager to handle audio operations for the scene.</param>
    public SceneFour(IAudioManager audioManager)
    {
        InitializeComponent();
        var viewModel = new SceneFourViewModel();
        this.BindingContext = viewModel;
        this.audioManager = audioManager;
        InitializeAudio();
        NarrativeButton.Clicked += OnNarrativeButtonClicked;

    }

    /// <summary>
    /// Initializes audio playback for the scene, including background music, UI sounds, and narrative audio.
    /// </summary>
    private async void InitializeAudio()
    {
        backgroundAudio = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("Room Tone with Scissors.wav"));
        radButton = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("radioButton.wav"));
        prevButton = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("prevButton.wav"));
        nextButton = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("nextButton.wav"));
        narrator = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("Question4.wav"));

        prevButton.Volume = 0.05;
        nextButton.Volume = 0.05;
        radButton.Volume = 0.05;

        backgroundAudio.Loop = true;
        backgroundAudio.Volume = 0.3;
    }

    /// <summary>
    /// Toggles the playback of background audio based on its current state.
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
    /// Cleans up resources used by an audio player instance.
    /// </summary>
    /// <param name="player">The audio player to be disposed of.</param>
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
    /// Ensures that audio is properly initialized when the page appears.
    /// </summary>
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        InitializeAudio();
        ResetUIComponents();
        if (audioOn)
        {
            backgroundAudio.Play();
        }
        await Task.Delay(100);
        backgroundSceneFour.IsAnimationPlaying = true;
    }

    /// <summary>
    /// Cleans up audio resources when the page is no longer visible.
    /// </summary>
    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        DisposeAudioPlayers();

    }

    /// <summary>
    /// Cleans up all audio player resources within the scene
    /// ensuring that each player is stopped and disposed properly to release all associated resources.
    /// </summary>
    private void DisposeAudioPlayers()
    {
        DisposeAudioPlayer(backgroundAudio);
        DisposeAudioPlayer(radButton);
        DisposeAudioPlayer(narrator);
    }

    /// <summary>
    /// Resets the UI components to their default state.
    /// </summary>
    private void ResetUIComponents()
    {

        radioButton1.IsChecked = false;
        radioButton2.IsChecked = false;
        radioButton3.IsChecked = false;
        radioButton4.IsChecked = false;
        radioButton5.IsChecked = false;

    }

    /// <summary>
    /// Plays or restarts the narrative audio when the narrative button is clicked.
    /// </summary>
    private void OnNarrativeButtonClicked(object sender, EventArgs e)
    {
        if (!narrator.IsPlaying)
        {
            narrator.Play();
        }
        else
        {
            narrator.Stop();
            narrator.Play();
        }
    }

    /// <summary>
    /// Plays feedback sound when the "next" button is clicked, indicating progression to the next scene.
    /// </summary>
    private void OnNextButtonClicked(object sender, EventArgs e)
    {
        nextButton.Play();
    }

    /// <summary>
    /// Plays feedback sound when the "previous" button is clicked, indicating regression to the previous scene.
    /// </summary>
    private void OnPrevButtonClicked(object sender, EventArgs e)
    {
        prevButton.Play();
    }

    /// <summary>
    /// Updates the selected answer in the view model based on user interaction with radio buttons.
    /// </summary>
    private void OnRadioButtonCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        radButton.Play();

        if (sender is RadioButton radioButton && e.Value)
        {
            var viewModel = this.BindingContext as SceneFourViewModel;
            if (viewModel == null) return;

            switch (radioButton.Content.ToString())
            {
                case "Definitely Agree":
                case "Slightly Agree":
                    viewModel.SelectedAnswer = viewModel.QuestionNumber switch
                    {
                        1 or 7 or 8 or 10 => 1,
                        _ => 0,
                    };
                    break;
                case "Slightly Disagree":
                case "Definitely Disagree":
                    viewModel.SelectedAnswer = viewModel.QuestionNumber switch
                    {
                        2 or 3 or 4 or 5 or 6 or 9 => 1,
                        _ => 0,
                    };
                    break;
                case "Not Sure":
                default:
                    viewModel.SelectedAnswer = 0;
                    break;
            }
        }
    }

}