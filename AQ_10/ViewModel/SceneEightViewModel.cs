using System;
using System.Windows.Input;
using AQ_10.Services;

namespace AQ_10.ViewModel
{
    /// <summary>
    /// ViewModel for Scene Eight, managing audio states, navigation, and user responses for the eighth question.
    /// </summary>
    public class SceneEightViewModel : BaseViewModel
    {
        private bool _isAudioOn = true;
        private string _audioIcon = "🔊"; // Indicates the audio is on by default
        private int _selectedAnswer;
        private int _questionNumber = 8;

        /// <summary>
        /// Gets or sets the current question number, which is 8 for this scene.
        /// </summary>
        public int QuestionNumber
        {
            get => _questionNumber;
            set => SetProperty(ref _questionNumber, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the audio is enabled.
        /// </summary>
        public bool IsAudioOn
        {
            get => _isAudioOn;
            set
            {
                if (SetProperty(ref _isAudioOn, value))
                {
                    // Update the icon to reflect the current state of the audio
                    AudioIcon = _isAudioOn ? "🔊" : "🔇";
                }
            }
        }

        /// <summary>
        /// Gets or sets the icon that indicates the audio state.
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
                    // Calculate and update the score for question 8 based on the selected answer
                    int score = CalculateScoreBasedOnQuestionAndAnswer(_questionNumber, value);
                    AnswersService.Instance.SetAnswer(_questionNumber, score);
                }
            }
        }

        /// <summary>
        /// Command to toggle the audio state between on and off.
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
        /// Initializes a new instance of the SceneEightViewModel class, setting up navigation commands and loading the previously selected answer for question 8.
        /// </summary>
        public SceneEightViewModel()
        {
            ToggleAudioCommand = new Command(() => IsAudioOn = !IsAudioOn);
            NavigateToPreviousCommand = new Command(async () => await Shell.Current.GoToAsync("//SceneSeven"));
            NavigateToNextCommand = new Command(async () => await Shell.Current.GoToAsync("//SceneNine"));
            SelectedAnswer = AnswersService.Instance.GetAnswer(_questionNumber);
        }

        /// <summary>
        /// Calculates the score for question 8 based on the selected answer.
        /// </summary>
        /// <param name="questionNumber">The question number, expected to be 8 in this context.</param>
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
