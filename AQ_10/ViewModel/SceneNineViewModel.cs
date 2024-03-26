using System;
using System.Windows.Input;
using AQ_10.Services;

namespace AQ_10.ViewModel
{
    /// <summary>
    /// ViewModel for Scene Nine, responsible for managing audio playback, user responses, and navigation between scenes.
    /// </summary>
    public class SceneNineViewModel : BaseViewModel
    {
        private bool _isAudioOn = true;
        private string _audioIcon = "🔊"; // Represents that audio is on by default
        private int _selectedAnswer;
        private int _questionNumber = 9;

        /// <summary>
        /// Gets or sets the number of the current question being displayed, which is 9 in this scene.
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
                    // Updates the audio icon to reflect the current state of audio playback
                    AudioIcon = _isAudioOn ? "🔊" : "🔇";
                }
            }
        }

        /// <summary>
        /// Gets or sets the icon representing the current state of audio playback.
        /// </summary>
        public string AudioIcon
        {
            get => _audioIcon;
            set => SetProperty(ref _audioIcon, value);
        }

        /// <summary>
        /// Gets or sets the user's selected answer for the current question and updates the score accordingly.
        /// </summary>
        public int SelectedAnswer
        {
            get => _selectedAnswer;
            set
            {
                if (SetProperty(ref _selectedAnswer, value))
                {
                    // Calculates and updates the score for question 9 based on the selected answer
                    int score = CalculateScoreBasedOnQuestionAndAnswer(_questionNumber, value);
                    AnswersService.Instance.SetAnswer(_questionNumber, score);
                }
            }
        }

        /// <summary>
        /// Command to toggle the state of audio playback.
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
        /// Initializes a new instance of the SceneNineViewModel class. This setup includes initializing navigation commands and loading the previously selected answer for question 9.
        /// </summary>
        public SceneNineViewModel()
        {
            ToggleAudioCommand = new Command(() => IsAudioOn = !IsAudioOn);
            NavigateToPreviousCommand = new Command(async () => await Shell.Current.GoToAsync("//SceneEight"));
            NavigateToNextCommand = new Command(async () => await Shell.Current.GoToAsync("//SceneTen"));
            SelectedAnswer = AnswersService.Instance.GetAnswer(_questionNumber);
        }

        /// <summary>
        /// Calculates the score for question 9 based on the selected answer. This method needs to be adjusted to reflect the scoring criteria specific to question 9.
        /// </summary>
        /// <param name="questionNumber">The question number, expected to be 9 in this context.</param>
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
