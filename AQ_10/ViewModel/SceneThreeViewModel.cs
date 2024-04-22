using System;
using System.Windows.Input;
using AQ_10.Services;

namespace AQ_10.ViewModel
{
    /// <summary>
    /// ViewModel for Scene Three, handling audio controls, user answer selection, and navigation between scenes.
    /// </summary>
    public class SceneThreeViewModel : BaseViewModel
    {
        private bool _isAudioOn = true;
        private string _audioIcon = "🔊"; // Default icon for audio on
        private int _selectedAnswer;
        private int _questionNumber = 3;

        /// <summary>
        /// Gets or sets the current question number.
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
        /// Gets or sets the selected answer for the current question.
        /// Updates the user's answer score in <see cref="AnswersService"/>.
        /// </summary>
        public int SelectedAnswer
        {
            get => _selectedAnswer;
            set
            {
                if (SetProperty(ref _selectedAnswer, value))
                {
                    // Calculate and update the score for question 3 based on the selected answer
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
        /// Initializes a new instance of the SceneThreeViewModel class.
        /// </summary>
        public SceneThreeViewModel()
        {
            ToggleAudioCommand = new Command(() => IsAudioOn = !IsAudioOn);
            NavigateToPreviousCommand = new Command(async () => await Shell.Current.GoToAsync("//SceneTwo"));
            NavigateToNextCommand = new Command(async () => await Shell.Current.GoToAsync("//SceneFour"));
            SelectedAnswer = AnswersService.Instance.GetAnswer(_questionNumber);
        }

        /// <summary>
        /// Calculates the score based on the selected answer for question 3.
        /// Adjust this logic as per the scoring criteria for each question.
        /// </summary>
        /// <param name="questionNumber">The question number. Should be 3 for this ViewModel.</param>
        /// <param name="selectedAnswer">The selected answer by the user.</param>
        /// <returns>The calculated score based on the selected answer.</returns>
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
