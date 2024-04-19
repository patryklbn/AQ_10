using Plugin.Maui.Audio;
using AQ_10.ViewModel;
using System.Reflection;
using Microsoft.Maui.Controls;
using System.Diagnostics;

namespace AQ_10;

/// <summary>
/// Represents the first scene of the questionnaire, handling initialization of the view model, audio playback, 
/// and user interactions for the first question.
/// </summary>
public partial class SceneOne : ContentPage
{
    private readonly IAudioManager audioManager;
    private IAudioPlayer backgroundAudio;
    private IAudioPlayer radButton;
    private IAudioPlayer prevButton;
    private IAudioPlayer nextButton;
    private IAudioPlayer narrator;
    bool audioOn = true;

    /// <summary>
    /// Initializes a new instance of the SceneOne class, setting up audio management and bindings.
    /// </summary>
    /// <param name="audioManager">The audio manager to handle audio operations.</param>
    public SceneOne(IAudioManager audioManager)
    {
        InitializeComponent();
        var viewModel = new SceneOneViewModel();
        this.BindingContext = viewModel;
        this.audioManager = audioManager;
        InitializeAudio();
        NarrativeButton.Clicked += OnNarrativeButtonClicked;
    }

    /// <summary>
    /// Initializes audio playback for background music, narrative, and UI sounds.
    /// </summary>
    private async void InitializeAudio()
    {
        // Load and play various audio elements, adjusting volumes and looping as necessary.
        backgroundAudio = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("background.wav"));
        radButton = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("radioButton.wav"));
        prevButton = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("prevButton.wav"));
        nextButton = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("nextButton.wav"));
        narrator = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("Question1.wav"));

        prevButton.Volume = 0.05;
        nextButton.Volume = 0.05;
        radButton.Volume = 0.05;

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
        backgroundSceneOne.IsAnimationPlaying = true;
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
    /// Handles the event when the narrative button is clicked, playing or replaying the narrative audio.
    /// </summary>
    private void OnNarrativeButtonClicked(object sender, EventArgs e)
    {
        // Play or restart the narrator audio when the NarrativeButton is clicked
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
    /// Plays feedback sound when the "next" button is clicked.
    /// </summary>
    private void OnNextButtonClicked(object sender, EventArgs e)
    {
        nextButton.Play();
    }

    /// <summary>
    /// Plays feedback sound when the "previous" button is clicked.
    /// </summary>
    private void OnPrevButtonClicked(object sender, EventArgs e)
    {
        prevButton.Play();
    }

    /// <summary>
    /// Handles changes in radio button selection, updating the selected answer in the view model.
    /// </summary>
    private void OnRadioButtonCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        radButton.Play();

        if (sender is RadioButton radioButton && e.Value)
        {
            var viewModel = this.BindingContext as SceneOneViewModel;
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