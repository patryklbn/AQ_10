using AQ_10.ViewModel;

namespace AQ_10;

public partial class SceneTen : ContentPage
{
    public SceneTen()
    {
        InitializeComponent();
        this.BindingContext = new SceneTenViewModel();
    }
    private void OnRadioButtonCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (sender is RadioButton radioButton && e.Value)
        {
            var viewModel = this.BindingContext as SceneTenViewModel;
            if (viewModel == null) return;

            // Directly mapping radio button content to scoring logic
            switch (radioButton.Content.ToString())
            {
                case "Definitely Agree":
                case "Slightly Agree":
                    // For questions 1, 7, 8, and 10, "Agree" scores a point
                    viewModel.SelectedAnswer = viewModel.QuestionNumber switch
                    {
                        1 or 7 or 8 or 10 => 1,
                        _ => 0,
                    };
                    break;
                case "Slightly Disagree":
                case "Definitely Disagree":
                    // For questions 2, 3, 4, 5, 6, and 9, "Disagree" scores a point
                    viewModel.SelectedAnswer = viewModel.QuestionNumber switch
                    {
                        2 or 3 or 4 or 5 or 6 or 9 => 1,
                        _ => 0,
                    };
                    break;
                case "Not Sure":
                default:
                    viewModel.SelectedAnswer = 0; // "Not Sure" or any other case does not score
                    break;
            }
        }
    }
}
