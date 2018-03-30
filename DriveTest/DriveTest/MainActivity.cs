using Android.App;
using Android.Widget;
using Android.OS;
using Google.Apis.Drive.v2;
using Xamarin.Auth;

namespace DriveTest
{
    [Activity(Label = "Drive Test", MainLauncher = true)]
    [IntentFilter(actions: new[] { Android.Content.Intent.ActionView },
                  Categories = new[]{
        Android.Content.Intent.CategoryDefault,
        Android.Content.Intent.CategoryBrowsable
    },
                  DataSchemes =new[]{ "com.appointmentapp.appointmentapp"},
                  DataPaths = new[]{ "/oauth2redirect" })]
    public class MainActivity : Activity
    {
        Button AuthButton = null;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            AuthButton = FindViewById<Button>(Resource.Id.btnAuth);

            AuthButton.Click += delegate {
                //var auth = new RefreshOAuth2Authenticator(
                //"626477271686-dlo2o32kkp2dat3ijmaunjr4376794im.apps.googleusercontent.com",
                //"wzKofc_I9A_dmO-Tz4rhIykB", 
                //DriveService.Scope.DriveAppdata + " " + DriveService.Scope.DriveFile + " " +
                //DriveService.Scope.Drive + " " + "https://www.googleapis.com/auth/userinfo.email",
                //new System.Uri("https://accounts.google.com/o/oauth2/auth"),
                //new System.Uri("https://www.googleapis.com/plus/v1/people/me"),
                //new System.Uri("https://www.googleapis.com/oauth2/v4/token"));

                var auth = new OAuth2Authenticator(
                    "626477271686-9h38rrnbvq5fsmbjb66vse4fvto9j8n5.apps.googleusercontent.com",
                    string.Empty,
                    DriveService.Scope.DriveAppdata + " " + DriveService.Scope.DriveFile + " " +
                    DriveService.Scope.Drive + " " + "https://www.googleapis.com/auth/userinfo.email",
                    new System.Uri("https://accounts.google.com/o/oauth2/auth"),
                    new System.Uri("com.appointmentapp.appointmentapp:/oauth2redirect"),
                    new System.Uri("https://www.googleapis.com/oauth2/v4/token"),
                    isUsingNativeUI: true);
                auth.Completed += async (sender, eventArgs) => {
                    System.Diagnostics.Debug.WriteLine("Inside....." + eventArgs.IsAuthenticated);
                    if (eventArgs.IsAuthenticated)
                    {
                        
                    }

                    var request = new OAuth2Request("GET", new System.Uri("https://www.googleapis.com/oauth2/v2/userinfo"), null,
                        eventArgs.Account);
                    var response = await request.GetResponseAsync();

                    if (response != null)
                    {
                       var userJson = response.GetResponseText();
                    }
                    //StoringDataIntoCache(userJson, eventArgs.Account);
                };

                StartActivity(auth.GetUI(this));
            };
        }
    }
}

