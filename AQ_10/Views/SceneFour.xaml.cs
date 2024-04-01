﻿using Plugin.Maui.Audio;
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

    /// <summary>
    /// Initializes a new instance of the SceneTwo class, setting up audio management and bindings.
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
        backgroundAudio = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("background.wav"));
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
        }
        else
        {
            backgroundAudio.Play();
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
    /// Handles necessary audio management when the page becomes invisible.
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
    /// Ensures audio is correctly initialized or resumed when the page becomes visible.
    /// </summary>
    protected override void OnAppearing()
    {
        base.OnAppearing();

        InitializeAudio();

        // Ensure the BindingContext is of type SceneOneViewModel
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