using AQ_10.ViewModel;

namespace AQ_10;

public partial class SceneTen : ContentPage
{
    public SceneTen()
    {
        InitializeComponent();
        this.BindingContext = new SceneTenViewModel();

    }
}
