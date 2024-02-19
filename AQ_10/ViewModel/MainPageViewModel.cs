using System;
using System.Windows.Input;

namespace AQ_10.ViewModel
{
	public class MainPageViewModel : BaseViewModel
	{
        private bool _isAudioOn = true;
        private string _audioIcon = "🔊"; // Default icon for audio on

        public bool IsAudioOn
        {
            get => _isAudioOn;
            set
            {
                if (SetProperty(ref _isAudioOn, value))
                {
                    // Update the icon based on the audio state
                    AudioIcon = _isAudioOn ? "🔊" : "🔇";
                }
            }
        }

        public string AudioIcon
        {
            get => _audioIcon;
            set => SetProperty(ref _audioIcon, value);
        }

        public ICommand ToggleAudioCommand { get; }
        public ICommand NavigateToSceneOneCommand { get; }

        public MainPageViewModel()
		{
            ToggleAudioCommand = new Command(() => IsAudioOn = !IsAudioOn);
            NavigateToSceneOneCommand = new Command(async () => await Shell.Current.GoToAsync("//SceneOne"));

        }
    }
}

