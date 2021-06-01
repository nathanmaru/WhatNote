using Android.App;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;
using Android.Widget;
using Android.Support.V7.Widget;
using System.Collections.Generic;
using WhatNote.Data_Models;
using WhatNote.Fragments;
using Android.Content;
using WhatNote.Activities;
using WhatNote.EventListeners;
using System.Linq;
using WhatNote.Adapter;

namespace WhatNote
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        ImageView searchButton;
        EditText searchText;
        ImageView addButton;
        RecyclerView myRecyclerView;
        TextView whatNote;
        List<Note> NoteList;

        NoteAdapter adapter;

        NoteListener noteListener;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            myRecyclerView = (RecyclerView)FindViewById(Resource.Id.MyRecyclerView);
            addButton = (ImageView)FindViewById(Resource.Id.addButton);
            searchButton = (ImageView)FindViewById(Resource.Id.searchButton);
            searchText = (EditText)FindViewById(Resource.Id.searchText);
            whatNote = (TextView)FindViewById(Resource.Id.whatNote);

            searchButton.Click += SearchButton_Click;
            searchText.TextChanged += SearchText_TextChanged;
            addButton.Click += AddButton_Click;
            whatNote.Click += WhatNote_Click;

            RetriveData();
        }

        private void SearchText_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            List<Note> SearchResult =
                (from note in NoteList
                 where note.NoteTitle.ToLower().Contains(searchText.Text.ToLower()) ||
                 note.NoteContent.ToLower().Contains(searchText.Text.ToLower())select note).ToList();
            adapter = new NoteAdapter(SearchResult);
            myRecyclerView.SetAdapter(adapter);
        }

        private void WhatNote_Click(object sender, System.EventArgs e)
        {
            var intent = new Intent(this, typeof(LoginActivity));
            StartActivity(intent);
        }

        public void RetriveData()
        {
            noteListener = new NoteListener();
            noteListener.Create();
            noteListener.NoteRetrived += NoteListener_NoteRetrived;
        }

        private void NoteListener_NoteRetrived(object sender, NoteListener.NoteDataEventArgs e)
        {
            NoteList = e.Note;
            SetupRecyclerView();
        }

        private void AddButton_Click(object sender, System.EventArgs e)
        {
            var intent = new Intent(this, typeof(Add_Note));
            StartActivity(intent);
            /*addNoteFragment = new AddNoteFragment();
            var trans = SupportFragmentManager.BeginTransaction();
            addNoteFragment.Show(trans, "new note");*/

        }
        private void SetupRecyclerView()
        {
            myRecyclerView.SetLayoutManager(new Android.Support.V7.Widget.LinearLayoutManager(myRecyclerView.Context));
            adapter = new NoteAdapter(NoteList);
            adapter.DeleteItemClick += Adapter_DeleteItemClick;
           
            adapter.ItemClick += Adapter_ItemClick;// this after setting up adapter
            myRecyclerView.SetAdapter(adapter);
        }

       
        ///When a recycle viewItem is click
        private void Adapter_ItemClick(object sender, NoteAdapterClickEventArgs e)
        {
            string key = NoteList[e.Position].ID;
            string noteTitle = NoteList[e.Position].NoteTitle;
            string noteContent = NoteList[e.Position].NoteContent;
            string noteDate = NoteList[e.Position].NoteDate;
            var intent = new Intent(this, typeof(ViewNoteContentActivity));
            intent.PutExtra("key", key);
            intent.PutExtra("noteTitle", noteTitle);
            intent.PutExtra("noteContent", noteContent);
            intent.PutExtra("noteDate", noteDate);

            StartActivity(intent);
        }

        private void Adapter_DeleteItemClick(object sender, NoteAdapterClickEventArgs e)
        {
            string key = NoteList[e.Position].ID;
            Android.Support.V7.App.AlertDialog.Builder deleteNote = new Android.Support.V7.App.AlertDialog.Builder(this);
            deleteNote.SetTitle("Delete Note");
            deleteNote.SetMessage("Are you sure you want to delete this note?");
            deleteNote.SetPositiveButton("Continue", (deleteAlert, args) =>
            {
                noteListener.DeleteNote(key);
                Toast.MakeText(this, "Note Deleted!", ToastLength.Short).Show();
            });
            deleteNote.SetNegativeButton("Cancel", (deleteAlert, args) =>
            {
                deleteNote.Dispose();
            });
            deleteNote.Show();

        }

       

        private void SearchButton_Click(object sender, System.EventArgs e)
        {
            if(searchText.Visibility == Android.Views.ViewStates.Gone)
            {
                searchText.Visibility = Android.Views.ViewStates.Visible;
            }
            else
            {
                searchText.ClearFocus();
                searchText.Visibility = Android.Views.ViewStates.Gone;
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}