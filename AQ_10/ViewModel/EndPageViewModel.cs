using System;
using System.Windows.Input;
using AQ_10.Services; 


namespace AQ_10.ViewModel
{
    public class EndPageViewModel : BaseViewModel
    {
        /// <summary>
        /// ViewModel for the end page of the application, handling the display of the final score and navigation commands.
        /// </summary>
        private bool _isAudioOn = true;
        private string _audioIcon = "🔊"; // Default icon for audio on

        /// <summary>
        /// Generates a message based on the calculated score, offering suggestions based on the score's value.
        /// </summary>
        public string ScoreMessage
        {
            get
            {
                int score = AnswersService.Instance.CalculateScore();
                if (score >= 6)
                {
                    return $"Your score is: {score}/10.\n\nYou may want to consider taking the longer 50 question test, or getting in touch with a specialist for a diagnostic assessment.";
                }
                else
                {
                    return $"Your score is: {score}/10.\n\nThe AQ-10 does not offer a lot of insight, but your score is not indicative of autism or a significant number of autistic traits. You could, however, try the 50-question version of the test.";
                }
            }
        }


        /// <summary>
        /// Indicates whether the audio is currently enabled.
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
        /// Represents the icon to be displayed depending on the audio state.
        /// </summary>
        public string AudioIcon
        {
            get => _audioIcon;
            set => SetProperty(ref _audioIcon, value);
        }

        // Commands
        public ICommand ToggleAudioCommand { get; }
        public ICommand NavigateToPreviousCommand { get; }
        public ICommand NavigateToNextCommand { get; }


        /// <summary>
        /// Initializes a new instance of the EndPageViewModel class.
        /// </summary>
        public EndPageViewModel()
        {
            ToggleAudioCommand = new Command(() => IsAudioOn = !IsAudioOn);
            NavigateToPreviousCommand = new Command(async () => await Shell.Current.GoToAsync("//SceneTen"));
            NavigateToNextCommand = new Command(async () => await Shell.Current.GoToAsync("//MainPage"));

        }

        /// <summary>
        /// Refreshes the ScoreMessage property to update its value in the view.
        /// </summary>
        public void RefreshScoreMessage()
        {
            OnPropertyChanged(nameof(ScoreMessage));
        }
    }

}

