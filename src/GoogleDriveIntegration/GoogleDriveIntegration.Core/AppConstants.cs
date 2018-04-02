namespace GoogleDriveIntegration.Core
{
    public static class AppConstants
    {
        public const string AppName = "GoogleDriveIntegration";
        // OAuth
        // For Google login, configure at https://console.developers.google.com/
        public static string iOSClientId = "884248401974-2fjvnio8m901ug9mb75vnkf4og4pje55.apps.googleusercontent.com";
        public static string AndroidClientId = "884248401974-dhbmgcipcqpfn693r3o5p4t4hsjn2ilb.apps.googleusercontent.com";

        // These values do not need changing
        public static string Scope = "https://www.googleapis.com/auth/userinfo.email";
        public static string AuthorizeUrl = "https://accounts.google.com/o/oauth2/auth";
        public static string AccessTokenUrl = "https://www.googleapis.com/oauth2/v4/token";
        public static string UserInfoUrl = "https://www.googleapis.com/oauth2/v2/userinfo";

        // Set these to reversed iOS/Android client ids, with :/oauth2redirect appended
        public static string iOSRedirectUrl = "com.googleusercontent.apps.884248401974-2fjvnio8m901ug9mb75vnkf4og4pje55:/oauth2redirect";
        public static string AndroidRedirectUrl = "com.googleusercontent.apps.884248401974-dhbmgcipcqpfn693r3o5p4t4hsjn2ilb:/oauth2redirect";
    }
}
