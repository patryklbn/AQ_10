using AQ_10.ViewModel;


namespace AQ_10;

public partial class SceneOne : ContentPage
{
	public SceneOne()
	{
		InitializeComponent();
        this.BindingContext = new SceneOneViewModel();
    }

}
