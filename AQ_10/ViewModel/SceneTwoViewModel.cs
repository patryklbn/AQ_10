using System;
using System.Windows.Input;
using AQ_10.Services;
using System.Diagnostics;

namespace AQ_10.ViewModel
{
    /// <summary>
    /// ViewModel for Scene Two, managing audio state, navigation, and scoring for answers specific to question 2.
    /// </summary>
    public class SceneTwoViewModel : BaseViewModel
    {
        private bool _isAudioOn = true;
        private string _audioIcon = "🔊"; // Default icon for audio on
        private int _selectedAnswer;
        private int _questionNumber = 2;

        /// <summary>
        /// Gets or sets the current question number.
        /// </summary>
        public int QuestionNumber
        {
            get => _questionNumber;
            set => SetProperty(ref _questionNumber, value);
        }

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
        /// Gets or sets the selected answer for the current question, updating the score based on the answer.
        /// </summary>
        public int SelectedAnswer
        {
            get => _selectedAnswer;
            set
            {
                if (SetProperty(ref _selectedAnswer, value))
                {
                    // Calculate and update the score for question 2 based on the selected answer
                    int score = CalculateScoreBasedOnQuestionAndAnswer(_questionNumber, value);
                    AnswersService.Instance.SetAnswer(_questionNumber, score);
                }
            }
        }

        // Commands
        public ICommand ToggleAudioCommand { get; }
        public ICommand NavigateToPreviousCommand { get; }
        public ICommand NavigateToNextCommand { get; }

        /// <summary>
        /// Initializes a new instance of the SceneTwoViewModel class.
        /// </summary>
        public SceneTwoViewModel()
        {
            ToggleAudioCommand = new Command(() => IsAudioOn = !IsAudioOn);
            NavigateToPreviousCommand = new Command(async () => await Shell.Current.GoToAsync("//SceneOne"));
            NavigateToNextCommand = new Command(async () => await Shell.Current.GoToAsync("//SceneThree"));
            // Load any previously selected answer for question 2
            SelectedAnswer = AnswersService.Instance.GetAnswer(2);
        }

        /// <summary>
        /// Calculates the score for question 2 based on the selected answer.
        /// </summary>
        /// <param name="questionNumber">The question number, which should be 2 for this ViewModel.</param>
        /// <param name="selectedAnswer">The selected answer.</param>
        /// <returns>The score calculated based on the selected answer.</returns>
        private int CalculateScoreBasedOnQuestionAndAnswer(int questionNumber, int selectedAnswer)
        {
            // Logic specific to scoring for question 2
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
