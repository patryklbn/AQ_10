using AQ_10.ViewModel;

namespace AQ_10;

public partial class SceneFour : ContentPage
{
	public SceneFour()
	{
		InitializeComponent();
        this.BindingContext = new SceneFourViewModel();

    }
}
