using AQ_10.ViewModel;

namespace AQ_10;

public partial class SceneThree : ContentPage
{
	public SceneThree()
	{
		InitializeComponent();
        this.BindingContext = new SceneThreeViewModel();

    }
}
