using AQ_10.ViewModel;

namespace AQ_10;

public partial class SceneTwo : ContentPage
{
	public SceneTwo()
	{
		InitializeComponent();
        this.BindingContext = new SceneTwoViewModel();

    }
}
