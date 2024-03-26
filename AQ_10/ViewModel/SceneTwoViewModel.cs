using System;
using System.Windows.Input;
using AQ_10.Services;
using System.Diagnostics;

namespace AQ_10.ViewModel
{
    public class SceneTwoViewModel : BaseViewModel
    {
        private bool _isAudioOn = true;
        private string _audioIcon = "🔊"; // Default icon for audio on
        private int _selectedAnswer;
        private int _questionNumber = 2; 

        public int QuestionNumber
        {
            get => _questionNumber;
            set => SetProperty(ref _questionNumber, value);
        }

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
        public int SelectedAnswer
        {
            get => _selectedAnswer;
            set
            {
                if (SetProperty(ref _selectedAnswer, value))
                {

                    int score = CalculateScoreBasedOnQuestionAndAnswer(1, value);
                    AnswersService.Instance.SetAnswer(2, score);

                }
            }
        }

        public ICommand ToggleAudioCommand { get; }
        public ICommand NavigateToPreviousCommand { get; }
        public ICommand NavigateToNextCommand { get; }

        public SceneTwoViewModel()
        {
            ToggleAudioCommand = new Command(() => IsAudioOn = !IsAudioOn);
            NavigateToPreviousCommand = new Command(async () => await Shell.Current.GoToAsync("//SceneOne"));
            NavigateToNextCommand = new Command(async () => await Shell.Current.GoToAsync("//SceneThree"));
            // Load any previously selected answer for question 2
            SelectedAnswer = AnswersService.Instance.GetAnswer(2);
        }
        private int CalculateScoreBasedOnQuestionAndAnswer(int questionNumber, int selectedAnswer)
        {
            // Adjust this logic based on the scoring criteria for question 2
            switch (selectedAnswer)
            {
                case 1: // Definitely Agree
                case 2: // Slightly Agree
                    return 1;
                case 4: // Slightly Disagree
                case 5: // Definitely Disagree
                    return 0;
                default:
                    return 0; // Not Sure, or any other case does not score
            }
        }

    }

}

