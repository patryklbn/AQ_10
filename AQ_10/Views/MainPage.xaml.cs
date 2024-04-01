using AQ_10.ViewModel;

namespace AQ_10;

/// <summary>
/// The main page of the application that serves as the entry point for users. 
/// It initializes the MainPageViewModel as its data context to handle user interactions.
/// </summary>
public partial class MainPage : ContentPage
{
    /// <summary>
    /// Initializes a new instance of the MainPage class.
    /// Sets up the page and its binding context to interact with the MainPageViewModel.
    /// </summary>
    public MainPage()
    {
        InitializeComponent();
        this.BindingContext = new MainPageViewModel();
    }
}
