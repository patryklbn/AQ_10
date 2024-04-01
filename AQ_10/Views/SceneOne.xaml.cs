using Plugin.Maui.Audio;
using AQ_10.ViewModel;
using System.Reflection;
using Microsoft.Maui.Controls;

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
        backgroundAudio.Play();
    }

    /// <summary>
    /// Toggles playback of the background audio based on its current state.
    /// </summary>
    private void OnAudioButtonClicked(object sender, EventArgs e)
    {
        if (backgroundAudio.IsPlaying)
        {
            backgroundAudio.Pause();
        }
        else
        {
            backgroundAudio.Play();
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
            narrator.Stop();
            DisposeAudioPlayer(backgroundAudio);
            DisposeAudioPlayer(narrator); 
            DisposeAudioPlayer(radButton);
        }
    }

    /// <summary>
    /// Ensures that audio is properly initialized when the page appears.
    /// </summary>
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await Task.Delay(100);
        backgroundSceneOne.IsAnimationPlaying = true;
       
        InitializeAudio();

        
        if (BindingContext is SceneOneViewModel viewModel)
        {

            // Play audio if it's not already playing
            if (viewModel.IsAudioOn == true)
            {
                backgroundAudio.Play();
            }
            else
            {
                backgroundAudio.Pause();
            }
        }
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
    private void OnResponseButtonClicked(object sender, EventArgs e)
    {
        radButton.Play();

        Button clickedButton = (Button)sender;

        // Access the container of your buttons. For example, if they are in a StackLayout named 'buttonsContainer'
        var buttonsContainer = this.FindByName<StackLayout>("ButtonsContainer");

        if (buttonsContainer != null && buttonsContainer.Children.FirstOrDefault() is StackLayout innerContainer)
        {
            // Iterate through each element in the container
            foreach (var child in innerContainer.Children)
            {
                // Check if the child is a Button
                if (child is Button button)
                {
                    // Check if this is the clicked button
                    if (button == clickedButton)
                    {
                        // Set the border for the clicked button
                        button.BorderWidth = 5;
                    }
                    else
                    {
                        // Reset the border for all other buttons
                        button.BorderWidth = 0;
                    }
                }
            }
        }

        string response = clickedButton.CommandParameter.ToString();

        var viewModel = this.BindingContext as SceneOneViewModel;
        if (viewModel == null) return;

        switch (response)
        {
            case "Strongly Agree":
            case "Agree":
                viewModel.SelectedAnswer = viewModel.QuestionNumber switch
                {
                    1 or 7 or 8 or 10 => 1,
                    _ => 0,
                };
                break;
            case "Disagree":
            case "Strongly Disagree":
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