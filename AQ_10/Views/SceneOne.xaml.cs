using AQ_10.ViewModel;
using Plugin.SimpleAudioPlayer;
using System.Reflection;

namespace AQ_10;

public partial class SceneOne : ContentPage
{
    private ISimpleAudioPlayer _player;
    private bool _isMuted = false;
    public SceneOne()
    {
        InitializeComponent();
        var viewModel = new SceneOneViewModel();
        this.BindingContext = viewModel;

        _player = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
        PlayBackgroundMusic();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        _player.Stop();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _player.Play();
    }

    private void PlayBackgroundMusic()
    {
        if (!_player.IsPlaying)
        {
            var assembly = typeof(SceneOne).GetTypeInfo().Assembly;
            Stream audioStream = assembly.GetManifestResourceStream("AQ_10.Resources.Raw.background.mp3");
            if (audioStream != null)
            {
                _player.Load(audioStream);
                _player.Loop = true;
                _player.Play();
            }
        }
    }

    private void OnAudioButtonClicked(object sender, EventArgs e)
    {
        _isMuted = !_isMuted; // Toggle the mute state
        _player.Volume = _isMuted ? 0 : 1; // Set the volume to 0 if muted, else to 1

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
