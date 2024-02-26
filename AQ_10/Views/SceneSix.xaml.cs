using AQ_10.ViewModel;

namespace AQ_10;

public partial class SceneSix : ContentPage
{
    public SceneSix()
    {
        InitializeComponent();
        this.BindingContext = new SceneSixViewModel();

    }
}
