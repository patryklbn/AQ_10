﻿using Android.App;
using Android.Content.PM;
using Android.OS;

namespace AQ_10;

[Activity(
    Theme = "@style/Maui.SplashTheme",
    MainLauncher = true,
    ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density,
    ScreenOrientation = ScreenOrientation.Landscape)] // Set the screen orientation to landscape
public class MainActivity : MauiAppCompatActivity
{
}
