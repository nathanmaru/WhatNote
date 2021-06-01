using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Firebase.Database;
using FR.Ganfra.Materialspinner;
using Java.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhatNote.Adapter;
using WhatNote.Data_Models;
using WhatNote.EventListeners;
using WhatNote.Helpers;
using SupportV7 = Android.Support.V7.App;

namespace WhatNote.Activities
{
    [Activity(Label = "ViewNoteContentActivity")]
    public class ViewNoteContentActivity : Activity
    {
        EditText noteTitleText;
        EditText noteContentText;    
        Button updateButton;

        string id;
        
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.ViewNoteContent);
            string key = Intent.GetStringExtra("key");
            string title = Intent.GetStringExtra("noteTitle");
            string content = Intent.GetStringExtra("noteContent");
            string notedate = Intent.GetStringExtra("noteDate");
            

            // Create your application here
            noteTitleText = (EditText)FindViewById(Resource.Id.noteTitleText);
            noteContentText = (EditText)FindViewById(Resource.Id.noteContentText);
            updateButton = (Button)FindViewById(Resource.Id.submitButton);


            noteTitleText.Text = title;
            noteContentText.Text = content;
            id = key;

            updateButton.Click += UpdateButton_Click;
            

            //SetupStatusSPinner();
            //RetriveData();
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            string title = noteTitleText.Text;
            string content = noteContentText.Text;
            var date = DateTime.Now;
            string noteDate = date.ToString("dd/MM/yyyy, hh:mm");

            

            SupportV7.AlertDialog.Builder saveDataAlert = new SupportV7.AlertDialog.Builder(updateButton.Context);
            saveDataAlert.SetTitle("Note Update");
            saveDataAlert.SetMessage("Do you want to update this Note?");
            saveDataAlert.SetPositiveButton("OK", (senderAlert, args) =>
            {
                AppDataHelper.GetDatabase().GetReference("note/" + id + "/noteTitle").SetValue(title);
                AppDataHelper.GetDatabase().GetReference("note/" + id + "/noteContent").SetValue(content);
                AppDataHelper.GetDatabase().GetReference("note/" + id + "/noteDate").SetValue(noteDate);
                Toast.MakeText(updateButton.Context, "Note Updated!", ToastLength.Short).Show();
                var intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
            });
            saveDataAlert.SetNegativeButton("Cancel", (senderAlert, args) =>
            {
                saveDataAlert.Dispose();
            });
            saveDataAlert.Show();

        }
    }
}