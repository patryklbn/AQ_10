using System;
using System.Windows.Input;

namespace AQ_10.ViewModel
{
	public class MainPageViewModel : BaseViewModel
	{
        /// <summary>
        /// ViewModel for the main page, handling audio state and navigation to the first scene.
        /// </summary>
        private bool _isAudioOn = true;
        private string _audioIcon = "🔊"; // Default icon for audio on

        /// <summary>
        /// Gets or sets a value indicating whether audio is enabled.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the icon displayed to represent the audio state.
        /// </summary>
        public string AudioIcon
        {
            get => _audioIcon;
            set => SetProperty(ref _audioIcon, value);
        }

        /// <summary>
        /// Command to toggle the audio state.
        /// </summary>
        public ICommand ToggleAudioCommand { get; }

        /// <summary>
        /// Command to navigate to the first scene.
        /// </summary>
        public ICommand NavigateToSceneOneCommand { get; }


        /// <summary>
        /// Initializes a new instance of the MainPageViewModel class.
        /// </summary>
        public MainPageViewModel()
		{
            ToggleAudioCommand = new Command(() => IsAudioOn = !IsAudioOn);
            NavigateToSceneOneCommand = new Command(async () => await Shell.Current.GoToAsync("//SceneOne"));

        }
    }
}

