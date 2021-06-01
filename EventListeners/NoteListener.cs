using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhatNote.Data_Models;
using WhatNote.Helpers;

namespace WhatNote.EventListeners
{
    public class NoteListener : Java.Lang.Object, IValueEventListener
    {
        List<Note> noteList = new List<Note>();
        public event EventHandler<NoteDataEventArgs> NoteRetrived;

        public class NoteDataEventArgs : EventArgs
        {
            public List<Note> Note { get; set; }
        }
        public void OnCancelled(DatabaseError error)
        {
            
        }

        public void OnDataChange(DataSnapshot snapshot)
        {
            if (snapshot.Value != null)
            {
                var child = snapshot.Children.ToEnumerable<DataSnapshot>();
                noteList.Clear();
                foreach (DataSnapshot noteData in child)
                {
                    Note note = new Note();
                    note.ID = noteData.Key;
                    note.NoteTitle = noteData.Child("noteTitle").Value.ToString();
                    note.NoteContent = noteData.Child("noteContent").Value.ToString();
                    note.NoteDate = noteData.Child("noteDate").Value.ToString();
                    noteList.Add(note);
                }
                NoteRetrived.Invoke(this, new NoteDataEventArgs { Note = noteList });
            }
        }
        public void Create()
        {
            DatabaseReference noteRef = AppDataHelper.GetDatabase().GetReference("note");
            noteRef.AddValueEventListener(this);
        }
        public void DeleteNote(string key)
        {
            DatabaseReference reference = AppDataHelper.GetDatabase().GetReference("note/" + key);
            reference.RemoveValue();
        }
    }
}