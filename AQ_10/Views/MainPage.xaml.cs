using AQ_10.ViewModel;

namespace AQ_10;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        this.BindingContext = new MainPageViewModel();
    }

}



