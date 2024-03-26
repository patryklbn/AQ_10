using System;
using System.Diagnostics;
namespace AQ_10.Services
{
    public class AnswersService
    {
        private static readonly AnswersService _instance = new AnswersService();
        public static AnswersService Instance => _instance;

        private Dictionary<int, int> answers = new Dictionary<int, int>();

        private AnswersService() { }

        public void SetAnswer(int questionNumber, int answerValue)
        {
            answers[questionNumber] = answerValue;
            Debug.WriteLine($"Answer Set: Question {questionNumber}, Answer {answerValue}");

        }

        public int GetAnswer(int questionNumber)
        {
            return answers.TryGetValue(questionNumber, out int value) ? value : 0;
        }

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
    }

}

