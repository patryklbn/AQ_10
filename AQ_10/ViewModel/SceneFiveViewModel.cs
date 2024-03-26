using System;
using System.Windows.Input;
using AQ_10.Services;

namespace AQ_10.ViewModel
{
    /// <summary>
    /// ViewModel for Scene Five, handling audio control, navigation, and managing responses for the fifth question.
    /// </summary>
    public class SceneFiveViewModel : BaseViewModel
    {
        private bool _isAudioOn = true;
        private string _audioIcon = "🔊"; // Indicates the audio is on by default
        private int _selectedAnswer;
        private int _questionNumber = 5;

        /// <summary>
        /// Gets or sets the current question number, which is set to 5 for Scene Five.
        /// </summary>
        public int QuestionNumber
        {
            get => _questionNumber;
            set => SetProperty(ref _questionNumber, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the audio is on or off.
        /// </summary>
        public bool IsAudioOn
        {
            get => _isAudioOn;
            set
            {
                if (SetProperty(ref _isAudioOn, value))
                {
                    // Update the audio icon to reflect the current audio state
                    AudioIcon = _isAudioOn ? "🔊" : "🔇";
                }
            }
        }

        /// <summary>
        /// Gets or sets the audio icon, changing depending on the audio state.
        /// </summary>
        public string AudioIcon
        {
            get => _audioIcon;
            set => SetProperty(ref _audioIcon, value);
        }

        /// <summary>
        /// Gets or sets the selected answer for the current question and updates the score accordingly.
        /// </summary>
        public int SelectedAnswer
        {
            get => _selectedAnswer;
            set
            {
                if (SetProperty(ref _selectedAnswer, value))
                {
                    // Calculate and update the score for question 5 based on the selected answer
                    int score = CalculateScoreBasedOnQuestionAndAnswer(_questionNumber, value);
                    AnswersService.Instance.SetAnswer(_questionNumber, score);
                }
            }
        }

        /// <summary>
        /// Command to toggle the audio state.
        /// </summary>
        public ICommand ToggleAudioCommand { get; }

        /// <summary>
        /// Command to navigate to the previous scene.
        /// </summary>
        public ICommand NavigateToPreviousCommand { get; }

        /// <summary>
        /// Command to navigate to the next scene.
        /// </summary>
        public ICommand NavigateToNextCommand { get; }

        /// <summary>
        /// Initializes a new instance of the SceneFiveViewModel class.
        /// </summary>
        public SceneFiveViewModel()
        {
            ToggleAudioCommand = new Command(() => IsAudioOn = !IsAudioOn);
            NavigateToPreviousCommand = new Command(async () => await Shell.Current.GoToAsync("//SceneFour"));
            NavigateToNextCommand = new Command(async () => await Shell.Current.GoToAsync("//SceneSix"));
            // Load the previously selected answer for question 5, if any
            SelectedAnswer = AnswersService.Instance.GetAnswer(_questionNumber);
        }

        /// <summary>
        /// Calculates the score for question 5 based on the selected answer.
        /// </summary>
        /// <param name="questionNumber">The question number, expected to be 5 in this context.</param>
        /// <param name="selectedAnswer">The answer selected by the user.</param>
        /// <returns>The score calculated based on the selected answer.</returns>
        private int CalculateScoreBasedOnQuestionAndAnswer(int questionNumber, int selectedAnswer)
        {
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
