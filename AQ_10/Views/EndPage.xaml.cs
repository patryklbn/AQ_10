using AQ_10.ViewModel;

namespace AQ_10;

/// <summary>
/// Represents the end page of the questionnaire, displaying the user's final score and offering additional guidance based on the score.
/// </summary>
public partial class EndPage : ContentPage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EndPage"/> class.
    /// Sets the BindingContext to a new instance of the <see cref="EndPageViewModel"/>.
    /// </summary>
    public EndPage()
    {
        InitializeComponent();
        this.BindingContext = new EndPageViewModel();
    }

    /// <summary>
    /// Called when the page becomes visible.
    /// Refreshes the score message to ensure it displays the most up-to-date information.
    /// </summary>
    protected override void OnAppearing()
    {
        base.OnAppearing();
        (BindingContext as EndPageViewModel)?.RefreshScoreMessage();
    }
}
