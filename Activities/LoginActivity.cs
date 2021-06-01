using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WhatNote.Activities
{
    [Activity(Label = "LoginActivity")]
    public class LoginActivity : Activity
    {
        TextInputLayout username;
        TextInputLayout password;
        Button loginButton;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.LoginPage);

            // Create your application here
            username = (TextInputLayout)FindViewById(Resource.Id.usernameText);
            password = (TextInputLayout)FindViewById(Resource.Id.passwordText);
            loginButton = (Button)FindViewById(Resource.Id.loginButton);

            loginButton.Click += LoginButton_Click;
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            ///Checks the account
        }
    }
}