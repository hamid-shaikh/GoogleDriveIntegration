using System;
using System.Linq;
//using GoogleDriveIntegration.Core.Models;
//using Newtonsoft.Json;
using Prism.Commands;
using Xamarin.Auth;
using Xamarin.Forms;

namespace GoogleDriveIntegration.Core.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        Account account;
        AccountStore accountStore;
        public HomeViewModel()
        {
            Title = "Home View";
            accountStore = AccountStore.Create();
            account = accountStore.FindAccountsForService(AppConstants.AppName).FirstOrDefault();
        }

        public DelegateCommand GoogleSigninCommand => new DelegateCommand(ExecuteGoogleSigninCommand);

        private void ExecuteGoogleSigninCommand()
        {
            string clientId = null;
            string redirectUri = null;

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    clientId = AppConstants.iOSClientId;
                    redirectUri = AppConstants.iOSRedirectUrl;
                    break;

                case Device.Android:
                    clientId = AppConstants.AndroidClientId;
                    redirectUri = AppConstants.AndroidRedirectUrl;
                    break;
            }

            var authenticator = new OAuth2Authenticator(
                clientId,
                null,
                AppConstants.Scope,
                new Uri(AppConstants.AuthorizeUrl),
                new Uri(redirectUri),
                new Uri(AppConstants.AccessTokenUrl),
                null,
                true);

            authenticator.Completed += OnAuthCompleted;
            authenticator.Error += OnAuthError;

            AuthenticationState.Authenticator = authenticator;

            var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
            presenter.Login(authenticator);
        }

        async void OnAuthCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {
            if (sender is OAuth2Authenticator authenticator)
            {
                authenticator.Completed -= OnAuthCompleted;
                authenticator.Error -= OnAuthError;
            }

            //User user = null;
            if (e.IsAuthenticated)
            {
                // If the user is authenticated, request their basic user data from Google
                // UserInfoUrl = https://www.googleapis.com/oauth2/v2/userinfo
                var request = new OAuth2Request("GET", new Uri(AppConstants.UserInfoUrl), null, e.Account);
                var response = await request.GetResponseAsync();
                if (response != null)
                {
                    // Deserialize the data and store it in the account store
                    // The users email address will be used to identify data in SimpleDB
                    string userJson = await response.GetResponseTextAsync();
                    //user = JsonConvert.DeserializeObject<User>(userJson);
                }

                if (account != null)
                {
                    accountStore.Delete(account, AppConstants.AppName);
                }

                await accountStore.SaveAsync(account = e.Account, AppConstants.AppName);
                //await DisplayAlert("Email address", user.Email, "OK");
            }
        }

        void OnAuthError(object sender, AuthenticatorErrorEventArgs e)
        {
            if (sender is OAuth2Authenticator authenticator)
            {
                authenticator.Completed -= OnAuthCompleted;
                authenticator.Error -= OnAuthError;
            }

            //Debug.WriteLine("Authentication error: " + e.Message);
        }
    }
}
