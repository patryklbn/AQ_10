﻿using AQ_10.ViewModel;

namespace AQ_10;

public partial class EndPage : ContentPage
{
    public EndPage()
    {
        InitializeComponent();
        this.BindingContext = new EndPageViewModel();

    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        (BindingContext as EndPageViewModel)?.RefreshScoreMessage();
    }


}
