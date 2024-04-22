using System;
using System.Windows.Input;
using AQ_10.Services;
using System.Diagnostics;

namespace AQ_10.ViewModel
{
    /// <summary>
    /// ViewModel for Scene One, managing audio state, question navigation, and scoring for answers.
    /// </summary>
    public class SceneOneViewModel : BaseViewModel
    {

        private bool _isAudioOn = true;
        private string _audioIcon = "🔊"; // Default icon for audio on
        private int _selectedAnswer;
        private int _questionNumber = 1; // SceneOne is always question 1


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
        /// Gets or sets the selected answer for the current question.
        /// </summary>
        public int SelectedAnswer
        {
            get => _selectedAnswer;
            set
            {
                if (SetProperty(ref _selectedAnswer, value))
                {
                    Debug.WriteLine($"Question {_questionNumber}, Selected Answer Updated: {value}");
                    // Update the answer with the correct score based on the selected answer
                    int score = CalculateScoreBasedOnQuestionAndAnswer(1, value);
                    AnswersService.Instance.SetAnswer(1, score);
                    Debug.WriteLine($"Question {_questionNumber}, Selected Answer Updated: {value}");

                }
            }
        }

        // Commands
        public ICommand ToggleAudioCommand { get; }
        public ICommand NavigateToPreviousCommand { get; }
        public ICommand NavigateToNextCommand { get; }

        /// <summary>
        /// Initializes a new instance of the SceneOneViewModel class.
        /// </summary>
        public SceneOneViewModel()
        {
            ToggleAudioCommand = new Command(() => IsAudioOn = !IsAudioOn);
            NavigateToPreviousCommand = new Command(async () => await Shell.Current.GoToAsync("//MainPage"));
            NavigateToNextCommand = new Command(async () => await Shell.Current.GoToAsync("//SceneTwo"));
            SelectedAnswer = AnswersService.Instance.GetAnswer(1); // Load any previously selected answer
        }

        /// <summary>
        /// Calculates the score based on the question number and the selected answer.
        /// </summary>
        /// <param name="questionNumber">The question number.</param>
        /// <param name="selectedAnswer">The selected answer.</param>
        /// <returns>The score for the given answer to the question.</returns>
        private int CalculateScoreBasedOnQuestionAndAnswer(int questionNumber, int selectedAnswer)
        {
            // Apply the scoring logic for question 1
            // Adjust the logic based on your specific scoring rules for each question
            switch (selectedAnswer)
            {
                case 1: // Definitely Agree
                case 2: // Slightly Agree
                    return 1; // For question 1, these answers score 1 point
                case 4: // Slightly Disagree
                case 5: // Definitely Disagree
                    // For other questions, you may need to reverse the scoring logic
                    return 0; // For question 1, these answers do not score
                default:
                    return 0; // Not Sure, or any other case does not score
            }
        }
    }
}
