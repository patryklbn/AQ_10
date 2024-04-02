using System;
using System.Diagnostics;
namespace AQ_10.Services
{
    /// <summary>
    /// A singleton service responsible for managing and calculating scores based on user answers.
    /// </summary>
    public class AnswersService
    {
        private static readonly AnswersService _instance = new AnswersService();

        /// <summary>
        /// Gets the singleton instance of the AnswersService.
        /// </summary>
        public static AnswersService Instance => _instance;

        private Dictionary<int, int> answers = new Dictionary<int, int>();

        private AnswersService() { }

        /// <summary>
        /// Sets or updates the answer value for a specific question number.
        /// </summary>
        /// <param name="questionNumber">The question number.</param>
        /// <param name="answerValue">The answer value to set.</param>
        public void SetAnswer(int questionNumber, int answerValue)
        {
            answers[questionNumber] = answerValue;
            Debug.WriteLine($"Answer Set: Question {questionNumber}, Answer {answerValue}");

        }

        /// <summary>
        /// Retrieves the answer value for a specific question number.
        /// </summary>
        /// <param name="questionNumber">The question number.</param>
        /// <returns>The answer value if found; otherwise, 0.</returns>
        public int GetAnswer(int questionNumber)
        {
            return answers.TryGetValue(questionNumber, out int value) ? value : 0;
        }

        /// <summary>
        /// Calculates the total score based on all set answers.
        /// </summary>
        /// <remarks>
        /// The score calculation assumes that each answer contributes directly to the total score.
        /// </remarks>
        /// <returns>The total score calculated from all answers.</returns>
        public int CalculateScore()
        {
            int score = 0;
            Debug.WriteLine("Starting score calculation...");

            foreach (var answer in answers)
            {
                score += answer.Value; // Assuming answer.Value is 1 for a scoring answer and 0 otherwise
                Debug.WriteLine($"Checking answer for question {answer.Key}: {answer.Value}, running total: {score}");
            }

            Debug.WriteLine($"Final calculated score: {score}");
            return score;
        }

        /// <summary>
        /// Method for tests purpose
        /// </summary>
        public void ResetForTests()
        {
            answers.Clear();
        }
    }
}

