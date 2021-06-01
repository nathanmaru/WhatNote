using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using FR.Ganfra.Materialspinner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhatNote;

namespace WhatNote.Fragments
{
    public class AddNoteFragment : Android.Support.V4.App.DialogFragment
    {
        TextInputLayout noteTitleText;
        TextInputLayout noteContentText;
        Button submitButton;

        
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.AddNote, container, false);

            noteTitleText = (TextInputLayout)view.FindViewById(Resource.Id.noteTitleText);
            noteContentText = (TextInputLayout)view.FindViewById(Resource.Id.noteContentText);
            submitButton = (Button)view.FindViewById(Resource.Id.submitButton);
            return view;
        }

        

        
    }
}