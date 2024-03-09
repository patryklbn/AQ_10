using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using AQ_10.ViewModel;
using Plugin.SimpleAudioPlayer;
using System.Reflection;
using System.ComponentModel;

namespace AQ_10;

public partial class SceneOne : ContentPage
{
    private ISimpleAudioPlayer _audioPlayer;
    public SceneOne()
    {
        InitializeComponent();
        var viewModel = new SceneOneViewModel();
        this.BindingContext = viewModel;

        // Subscribe to PropertyChanged event
        viewModel.PropertyChanged += ViewModel_PropertyChanged;

        this.Padding = new Thickness(0);

        // Initialize the audio player
        _audioPlayer = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
        LoadAudioFile("Resources/Raw/background.wav"); // Make sure to replace "audio_file_name.mp3" with your actual audio file name
    }

    private void LoadAudioFile(string fileName)
    {
        var assembly = GetType().GetTypeInfo().Assembly;
        var stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{fileName}");
        if (stream != null)
        {
            _audioPlayer.Load(stream);
        }
    }

    private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(SceneOneViewModel.IsAudioOn))
        {
            // Check the IsAudioOn property and play or pause the audio accordingly
            if (((SceneOneViewModel)sender).IsAudioOn)
            {
                _audioPlayer.Play();
            }
            else
            {
                _audioPlayer.Pause();
            }
        }
    }

    private void OnRadioButtonCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (sender is RadioButton radioButton && e.Value)
        {
            var viewModel = this.BindingContext as SceneOneViewModel;
            if (viewModel == null) return;

            // Directly mapping radio button content to scoring logic
            switch (radioButton.Content.ToString())
            {
                case "Definitely Agree":
                case "Slightly Agree":
                    // For questions 1, 7, 8, and 10, "Agree" scores a point
                    viewModel.SelectedAnswer = viewModel.QuestionNumber switch
                    {
                        1 or 7 or 8 or 10 => 1,
                        _ => 0,
                    };
                    break;
                case "Slightly Disagree":
                case "Definitely Disagree":
                    // For questions 2, 3, 4, 5, 6, and 9, "Disagree" scores a point
                    viewModel.SelectedAnswer = viewModel.QuestionNumber switch
                    {
                        2 or 3 or 4 or 5 or 6 or 9 => 1,
                        _ => 0,
                    };
                    break;
                case "Not Sure":
                default:
                    viewModel.SelectedAnswer = 0; // "Not Sure" or any other case does not score
                    break;
            }
        }
    }

}

