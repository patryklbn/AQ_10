using AQ_10.ViewModel;

namespace AQ_10;

public partial class SceneFive : ContentPage
{
    public SceneFive()
    {
        InitializeComponent();
        this.BindingContext = new SceneFiveViewModel();

    }
}
