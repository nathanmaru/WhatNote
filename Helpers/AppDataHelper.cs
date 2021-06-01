using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WhatNote.Helpers
{
    public static class AppDataHelper
    {
        public static FirebaseDatabase GetDatabase()
        {
            var app = FirebaseApp.InitializeApp(Application.Context);
            FirebaseDatabase database;

            if(app == null)
            {
                var option = new FirebaseOptions.Builder()
                    .SetApplicationId("whatnote-96aa9")
                    .SetApiKey("AIzaSyB3-hsoDiW45phjm8I7ANhwRuEsEub8SC8")
                    .SetDatabaseUrl("https://whatnote-96aa9-default-rtdb.firebaseio.com/")
                    .SetStorageBucket("whatnote-96aa9.appspot.com")
                    .Build();
                app = FirebaseApp.InitializeApp(Application.Context, option);
                database = FirebaseDatabase.GetInstance(app);

            }
            else
            {
                database = FirebaseDatabase.GetInstance(app);
            }
            return database;
        }
    }
}