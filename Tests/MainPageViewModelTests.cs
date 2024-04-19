using Xunit;
using AQ_10.ViewModel;
using FluentAssertions;
using System.ComponentModel;
using System;

public class MainPageViewModelTests
{
    [Fact]
    public void ToggleAudioCommand_Executes_ChangesAudioState()
    {
        // Arrange
        var viewModel = new MainPageViewModel();
        viewModel.IsAudioOn = true;  // initial state

        // Act
        viewModel.ToggleAudioCommand.Execute(null);

        // Assert
        viewModel.IsAudioOn.Should().BeFalse(because: "executing the toggle command should change the audio state.");
    }
}
