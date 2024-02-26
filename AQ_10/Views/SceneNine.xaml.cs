using AQ_10.ViewModel;

namespace AQ_10;

public partial class SceneNine : ContentPage
{
    public SceneNine()
    {
        InitializeComponent();
        this.BindingContext = new SceneNineViewModel();

    }
}
