﻿using Android.App;
using Android.OS;

namespace CheckResolver.Droid
{
    [Activity(Label = "CheckBait.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var busines = new Model();

            var ret = busines.InvokePlatformFunction();

            var i = 0;
            i++;
        }
    }
}