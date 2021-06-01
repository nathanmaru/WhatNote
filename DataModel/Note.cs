using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WhatNote.Data_Models
{
    public class Note
    {
        public string ID { get; set; }
        public string NoteTitle { get; set; }
        public string NoteContent { get; set; }
        public string NoteDate { get; set; }
    }
}