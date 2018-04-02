using System;
using Android.App;
using Android.Content;
using Android.OS;

namespace GoogleDriveIntegration.Mobile.Droid
{
    [Activity(Label = "UrlSchemeInterceptorActivity",
             NoHistory = true,
              LaunchMode = Android.Content.PM.LaunchMode.SingleTop)]
    [IntentFilter(new[] { Intent.ActionView },
                  Categories = new[] { Intent.CategoryDefault},
                  DataSchemes = new[] { "com.googleusercontent.apps.884248401974-dhbmgcipcqpfn693r3o5p4t4hsjn2ilb" },
                  DataPath = "/oauth2redirect")]
    public class UrlSchemeInterceptorActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Convert Android.Net.Url to Uri
            var uri = new Uri(Intent.Data.ToString());

            // Load redirectUrl page
            Core.AuthenticationState.Authenticator.OnPageLoading(uri);

            Finish();
        }
    }
}
