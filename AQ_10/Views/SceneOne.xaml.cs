using Plugin.Maui.Audio;
using AQ_10.ViewModel;
using System.Reflection;

namespace AQ_10;

public partial class SceneOne : ContentPage
{
    private readonly IAudioManager audioManager;
    private IAudioPlayer player;
    public SceneOne(IAudioManager audioManager)
    {
        InitializeComponent();
        var viewModel = new SceneOneViewModel();
        this.BindingContext = viewModel;
        this.audioManager = audioManager;
        InitializeAudio();
    }

    private async void InitializeAudio()
    {
        player = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("background.wav"));
        player.Loop = true;
        player.Play();
    }

    private void OnAudioButtonClicked(object sender, EventArgs e)
    {
        if (player.IsPlaying)
        {
            player.Pause();
        }
        else
        {
            player.Play();
        }
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        if (player != null)
        {
            player.Stop();
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (!player.IsPlaying)
        {
            player.Play();
        }
    }

    private void OnPrevNextButtonClicked(object sender, EventArgs e)
    {

    }

    private void OnRadioButtonCheckedChanged(object sender, CheckedChangedEventArgs e)
    {

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