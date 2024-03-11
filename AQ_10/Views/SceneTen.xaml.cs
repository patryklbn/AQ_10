using Plugin.Maui.Audio;
using AQ_10.ViewModel;
using System.Reflection;
using Microsoft.Maui.Controls;

namespace AQ_10;

public partial class SceneTen : ContentPage
{
    private readonly IAudioManager audioManager;
    private IAudioPlayer backgroundAudio;
    private IAudioPlayer radButton;
    private IAudioPlayer prevButton;
    private IAudioPlayer nextButton;
    private IAudioPlayer narrator;
    public SceneTen(IAudioManager audioManager)
    {
        InitializeComponent();
        var viewModel = new SceneTenViewModel();
        this.BindingContext = viewModel;
        this.audioManager = audioManager;
        InitializeAudio();
    }

    private async void InitializeAudio()
    {
        backgroundAudio = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("background.wav"));
        radButton = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("radioButton.wav"));
        prevButton = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("prevButton.wav"));
        nextButton = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("nextButton.wav"));
        narrator = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("Question10.wav"));

        prevButton.Volume = 0.05;
        nextButton.Volume = 0.05;
        radButton.Volume = 0.05;

        backgroundAudio.Loop = true;
        backgroundAudio.Volume = 0.3;
        backgroundAudio.Play();
        narrator.Play();
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

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        if (backgroundAudio != null)
        {
            backgroundAudio.Stop();
            narrator.Stop();
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Ensure the BindingContext is of type SceneOneViewModel
        if (BindingContext is SceneOneViewModel viewModel)
        {

            // Play audio if it's not already playing
            if (viewModel.IsAudioOn == true)
            {
                backgroundAudio.Play();
                narrator.Play();
            }
            else
            {
                backgroundAudio.Pause();
            }
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

    private void OnRadioButtonCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        radButton.Play();

        if (sender is RadioButton radioButton && e.Value)
        {
            var viewModel = this.BindingContext as SceneTenViewModel;
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