using Xunit;
using AQ_10.ViewModel;
using FluentAssertions;
using System.ComponentModel;

public class BaseViewModelTests
{
    private class TestViewModel : BaseViewModel
    {
        private string _testProperty;

        public string TestProperty
        {
            get => _testProperty;
            set => SetProperty(ref _testProperty, value);
        }
    }

    [Fact]
    public void OnPropertyChanged_RaisesPropertyChangedEvent()
    {
        // Arrange
        var viewModel = new TestViewModel();
        bool wasRaised = false;
        viewModel.PropertyChanged += (sender, args) =>
        {
            if (args.PropertyName == nameof(TestViewModel.TestProperty))
                wasRaised = true;
        };

        // Act
        viewModel.TestProperty = "new value";

        // Assert
        wasRaised.Should().BeTrue(because: "modifying a property should raise PropertyChanged event.");
    }

    [Fact]
    public void SetProperty_WhenValueDoesNotChange_DoesNotRaisePropertyChangedEvent()
    {
        // Arrange
        var viewModel = new TestViewModel() { TestProperty = "initial" };
        bool wasRaised = false;
        viewModel.PropertyChanged += (sender, args) => wasRaised = true;

        // Act
        viewModel.TestProperty = "initial"; // same value

        // Assert
        wasRaised.Should().BeFalse(because: "setting a property to its existing value should not raise PropertyChanged event.");
    }
}
