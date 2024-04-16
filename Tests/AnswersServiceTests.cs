using Xunit;
using AQ_10.Services;
using FluentAssertions;

public class AnswersServiceTests
{
    public AnswersServiceTests()
    {
        // Ensure each test starts with a clean state
        AnswersService.Instance.ResetScore();
    }

    [Theory]
    [InlineData(1, 5)]
    [InlineData(2, 3)]
    public void SetAndGetAnswer_WhenCalled_SetsAndGetsCorrectValue(int questionNumber, int answerValue)
    {
        // Act
        AnswersService.Instance.SetAnswer(questionNumber, answerValue);
        var result = AnswersService.Instance.GetAnswer(questionNumber);

        // Assert
        result.Should().Be(answerValue, because: "the set answer should be retrievable via GetAnswer.");
    }

    [Fact]
    public void GetAnswer_ForNonExistingQuestion_ReturnsZero()
    {
        // Act
        var result = AnswersService.Instance.GetAnswer(999); // Using a question number unlikely to be set

        // Assert
        result.Should().Be(0, because: "the default value for an unset question should be 0.");
    }

    [Fact]
    public void CalculateScore_WithMultipleAnswers_CalculatesCorrectTotalScore()
    {
        // Arrange
        AnswersService.Instance.SetAnswer(1, 1);
        AnswersService.Instance.SetAnswer(2, 1);
        AnswersService.Instance.SetAnswer(3, 0); // Assuming an answer of 0 does not contribute to the score

        // Act
        var score = AnswersService.Instance.CalculateScore();

        // Assert
        score.Should().Be(2, because: "the total score should be the sum of all answer values.");
    }

    [Fact]
    public void CalculateScore_WhenNoAnswersSet_ReturnsZero()
    {
        // Arrange & Act 

        var score = AnswersService.Instance.CalculateScore();

        // Assert
        score.Should().Be(0, because: "the total score should be 0 when no answers are set.");
    }
}
