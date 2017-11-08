using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Locations;
using Android.Util;
using Android;

namespace Location
{
    [Activity(Label = "Location", MainLauncher = true)]

    //Implement ILocationListener interface to get location updates
    public class MainActivity : Activity, ILocationListener
    {
        LocationManager locMgr;
        string tag = "MainActivity";
        Button button;
        TextView latitude;
        TextView longitude;
        TextView provider;


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            Log.Debug(tag, "OnCreate called");

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            button = FindViewById<Button>(Resource.Id.myButton);
            latitude = FindViewById<TextView>(Resource.Id.latitude);
            longitude = FindViewById<TextView>(Resource.Id.longitude);
            provider = FindViewById<TextView>(Resource.Id.provider);
        }

        protected override void OnStart()
        {
            base.OnStart();

        }

        // OnResume gets called every time the activity starts, so we'll put our RequestLocationUpdates
        // code here, so that 
        protected override void OnResume()
        {
            base.OnResume();


            // initialize location manager
            locMgr = GetSystemService(Context.LocationService) as LocationManager;

            button.Click += delegate {
                button.Text = "Location Service Running";


                //GPS provider
                string Provider = LocationManager.GpsProvider;
                if (locMgr.IsProviderEnabled(Provider))
                {
                    locMgr.RequestLocationUpdates(Provider, 2000, 1, this);
                }
                else
                {
                    Log.Info(tag, Provider + " is not available. Does the device have location services enabled?");
                }
            };
        }

        protected override void OnPause()
        {
            base.OnPause();

            // stop sending location updates when the application goes into the background
            // to learn about updating location in the background, refer to the Backgrounding guide
            // http://docs.xamarin.com/guides/cross-platform/application_fundamentals/backgrounding/


            // RemoveUpdates takes a pending intent - here, we pass the current Activity
            locMgr.RemoveUpdates(this);
        }

        protected override void OnStop()
        {
            base.OnStop();

        }

        public void OnLocationChanged(Android.Locations.Location location)
        {

            latitude.Text = "Latitude: " + location.Latitude.ToString();
            longitude.Text = "Longitude: " + location.Longitude.ToString();
            provider.Text = "Provider: " + location.Provider.ToString();
        }
        public void OnProviderDisabled(string provider)
        {

        }
        public void OnProviderEnabled(string provider)
        {

        }
        public void OnStatusChanged(string provider, Availability status, Bundle extras)
        {

        }
    }
}


