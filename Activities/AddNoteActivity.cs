using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Widget;
using Firebase.Database;
using FR.Ganfra.Materialspinner;
using Java.Util;
using System;
using System.Collections.Generic;
using WhatNote.Helpers;
using SupportV7 = Android.Support.V7.App;

namespace WhatNote.Activities
{
    [Activity(Label = "Add_Note")]
    public class Add_Note : Activity
    {
        TextInputLayout noteTitleText;
        TextInputLayout noteContentText;
        
        Button submitButton;

        
        ArrayAdapter<string> adapter;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.AddNote);

            // Create your application here
            noteTitleText = (TextInputLayout)FindViewById(Resource.Id.noteTitleText);
            noteContentText = (TextInputLayout)FindViewById(Resource.Id.noteContentText);
            submitButton = (Button)FindViewById(Resource.Id.submitButton);

            submitButton.Click += SubmitButton_Click;

        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            string noteTitle = noteTitleText.EditText.Text;
            string noteContent = noteContentText.EditText.Text;
            var date = DateTime.Now;
            string noteDate = date.ToString("dd/MM/yyyy, hh:mm");

            HashMap noteInfo = new HashMap();
            noteInfo.Put("noteTitle", noteTitle);
            noteInfo.Put("noteContent", noteContent);
            noteInfo.Put("noteDate", noteDate);

            SupportV7.AlertDialog.Builder saveDataAlert = new SupportV7.AlertDialog.Builder(submitButton.Context);
            saveDataAlert.SetTitle("Note Save");
            saveDataAlert.SetMessage("Do you want to save this Note?");
            saveDataAlert.SetPositiveButton("OK", (senderAlert, args) =>
            {
                DatabaseReference newNoteRef = AppDataHelper.GetDatabase().GetReference("note").Push();
                newNoteRef.SetValue(noteInfo);
                Toast.MakeText(submitButton.Context, "Note Saved!", ToastLength.Short).Show();
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