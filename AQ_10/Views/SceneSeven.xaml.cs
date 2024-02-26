using AQ_10.ViewModel;

namespace AQ_10;

public partial class SceneSeven : ContentPage
{
    public SceneSeven()
    {
        InitializeComponent();
        this.BindingContext = new SceneSevenViewModel();

    }
}
