using Xunit;
using AQ_10.Services;
using AQ_10.ViewModel;
using FluentAssertions;

[Collection("AnswersService Tests")]
public class EndPageViewModelTests
{
    public EndPageViewModelTests()
    {
        // Reset the AnswersService before each test to ensure a clean state
        AnswersService.Instance.ResetScore();
    }

    [Fact]
    public void ScoreMessage_ReflectsCurrentScore_UsingDirectSetup()
    {
        // Arrange
        AnswersService.Instance.SetAnswer(1, 1); // Setup the answers to control the score
        AnswersService.Instance.SetAnswer(2, 1); // Total score will be 2

        var viewModel = new EndPageViewModel(); // Use the actual service

        // Act
        var message = viewModel.ScoreMessage;

        // Assert
        message.Should().Contain("Your score is: 2/10", because: "the score message should reflect the calculated score based on the actual service.");
    }
}
