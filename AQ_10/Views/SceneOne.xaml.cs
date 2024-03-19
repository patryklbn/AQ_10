using Plugin.Maui.Audio;
using AQ_10.ViewModel;
using System.Reflection;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Microsoft.Maui;

namespace AQ_10;

public partial class SceneOne : ContentPage
{
    private readonly IAudioManager audioManager;
    private IAudioPlayer backgroundAudio;
    private IAudioPlayer radButton;
    private IAudioPlayer prevButton;
    private IAudioPlayer nextButton;
    private IAudioPlayer narrator;
    public SceneOne(IAudioManager audioManager)
    {
        InitializeComponent();
        var viewModel = new SceneOneViewModel();
        this.BindingContext = viewModel;
        this.audioManager = audioManager;
        InitializeAudio();
        NarrativeButton.Clicked += OnNarrativeButtonClicked;
    }

    private async void InitializeAudio()
    {
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

    private void OnNextButtonClicked(object sender, EventArgs e)
    {
        nextButton.Play();
    }

    private void OnPrevButtonClicked(object sender, EventArgs e)
    {
        prevButton.Play();
    }

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