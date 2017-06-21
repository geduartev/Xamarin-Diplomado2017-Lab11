using System;
using Android.App;
using Android.OS;
using Android.Runtime;

namespace Lab11
{
    [Activity(Label = "SecondActivity", MainLauncher = true, Icon = "@drawable/icon")]
    public class SecondActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            Android.Util.Log.Debug("Lab11Log", "Activity B - OnCreate");
            base.OnCreate(savedInstanceState);
        }

        protected override void OnStart()
        {
            Android.Util.Log.Debug("Lab11Log", "Activity B - OnStart");
            base.OnStart();
        }

        protected override void OnResume()
        {
            Android.Util.Log.Debug("Lab11Log", "Activity B - OnResume");
            base.OnPause();
        }

        protected override void OnPause()
        {
            Android.Util.Log.Debug("Lab11Log", "Activity B - OnPause");
            base.OnPause();
        }

        protected override void OnStop()
        {
            Android.Util.Log.Debug("Lab11Log", "Activity B - OnStop");
            base.OnPause();
        }

        protected override void OnRestart()
        {
            Android.Util.Log.Debug("Lab11Log", "Activity B - OnRestart");
            base.OnPause();
        }
    }
}