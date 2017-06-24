using Android.App;
using Android.Widget;
using Android.OS;
using SALLab11;

namespace Lab11
{
    [Activity(Label = "Lab11", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        Complex Data;
        int Counter = 0;
        string Status;
        string Fullname;
        string Token;
        
        protected override void OnCreate(Bundle bundle)
        {
            Android.Util.Log.Debug("Lab11Log", "Activity A - OnCreate");

            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);
            FindViewById<Button>(Resource.Id.StartActivity).Click += (sender, e) =>
            {
                var ActivityIntent = new Android.Content.Intent(this, typeof(SecondActivity));
                StartActivity(ActivityIntent);
            };

            // Utilizar FragmentManager para recuperar el Fragmento
            Data = (Complex)this.FragmentManager.FindFragmentByTag("Data");

            if (Data==null)
            {
                // No ha sido almacenado, agregar el fragmento a la Activity
                Data = new Complex();                
                var FragmentTransaction = this.FragmentManager.BeginTransaction();
                FragmentTransaction.Add(Data, "Data");
                FragmentTransaction.Commit();
            }

            if (bundle!=null)
            {
                Counter = bundle.GetInt("CounterValue", 0);

                Status = bundle.GetString("StatusValue", Status);
                Fullname = bundle.GetString("FullnameValue", Fullname);
                Token = bundle.GetString("TokenValue", Token);

                var ValidateMessage = FindViewById<TextView>(Resource.Id.ValidateMessageTextView);
                ValidateMessage.SetPadding(40, 20, 0, 0);
                ValidateMessage.Text = $"{Status}\n{Fullname}\n{Token}";

                Android.Util.Log.Debug("Lab11Log", "Activity A - Recovered Instance State");
            }
            else
            {
                Validate();
            }

            var ClickCounter = FindViewById<Button>(Resource.Id.ClicksCounter);
            ClickCounter.Text = Resources.GetString(Resource.String.ClicksCounter_Text, Counter);

            ClickCounter.Text += $"\n{Data.ToString()}";

            ClickCounter.Click += (sender, e) =>
            {
                Counter++;
                ClickCounter.Text = Resources.GetString(Resource.String.ClicksCounter_Text, Counter);

                // Modificar con cualquier valor solo para verificar la persistencia.
                Data.Real++;
                Data.Imaginary++;
                // Mostrar el valor de los miembros
                ClickCounter.Text += $"\n{Data.ToString()}";
            };
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            outState.PutInt("CounterValue", Counter);

            outState.PutString("StatusValue", Status);
            outState.PutString("FullnameValue", Fullname);
            outState.PutString("TokenValue", Token);

            Android.Util.Log.Debug("Lab11Log", "Activity A - OnSaveInstanceState");
            base.OnSaveInstanceState(outState);
        }

        protected override void OnStart()
        {
            Android.Util.Log.Debug("Lab11Log", "Activity A - OnStart");
            base.OnStart();
        }

        protected override void OnResume()
        {
            Android.Util.Log.Debug("Lab11Log", "Activity A - OnResume");
            base.OnPause();
        }

        protected override void OnPause()
        {
            Android.Util.Log.Debug("Lab11Log", "Activity A - OnPause");
            base.OnPause();
        }

        protected override void OnStop()
        {
            Android.Util.Log.Debug("Lab11Log", "Activity A - OnStop");
            base.OnPause();
        }

        protected override void OnDestroy()
        {
            Android.Util.Log.Debug("Lab11Log", "Activity A - OnDestroy");
            base.OnPause();
        }

        protected override void OnRestart()
        {
            Android.Util.Log.Debug("Lab11Log", "Activity A - OnRestart");
            base.OnPause();
        }

        private async void Validate()
        {
            string myDevice = Android.Provider.Settings.Secure.GetString(ContentResolver, Android.Provider.Settings.Secure.AndroidId);

            var ServiceClient = new SALLab11.ServiceClient();
            var Result = await ServiceClient.ValidateAsync("email@email.com", "password", myDevice);
            Status = Result.Status.ToString();
            Fullname = Result.Fullname;
            Token = Result.Token;

            var ValidateMessage = FindViewById<TextView>(Resource.Id.ValidateMessageTextView);
            ValidateMessage.SetPadding(40, 20, 0, 0);
            ValidateMessage.Text = $"{Status}\n{Fullname}\n{Token}";
        }
    }
}

