using AQ_10.ViewModel;

namespace AQ_10;

public partial class SceneEight : ContentPage
{
    public SceneEight()
    {
        InitializeComponent();
        this.BindingContext = new SceneEightViewModel();

    }
}
