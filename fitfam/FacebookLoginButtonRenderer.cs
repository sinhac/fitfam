using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using Xamarin.Facebook;
using Xamarin.Facebook.Login.Widget;

namespace fitfam
{
    public class FacebookLoginButtonRenderer : ViewRenderer<Button, LoginButton>
    {
        protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            var activity = Context as MainActivity;

            var formsButton = e.NewElement as FacebookLoginButton;

            var loginButton = new LoginButton(Context);

            loginButton.SetReadPermissions("user_friends");

            var facebookCallback = new FacebookCallback<LoginResult>()
            {
                HandleSuccess = (LoginResult loginResult) =>
                {
                    formsButton.LoginSuccess(loginResult.AccessToken.Token);
                },

                HandleCancel = () =>
                {
                    // App code
                },

                HandleError = (FacebookException exception) =>
                {
                    // App code
                }
            };



            loginButton.RegisterCallback(activity.CallbackManager, facebookCallback);
            SetNativeControl(loginButton);
        }
    }
}