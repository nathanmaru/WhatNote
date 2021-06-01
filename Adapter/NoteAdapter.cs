using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using WhatNote;
using WhatNote.Data_Models;

namespace WhatNote.Adapter
{
    class NoteAdapter : RecyclerView.Adapter
    {
        public event EventHandler<NoteAdapterClickEventArgs> ItemClick;
        public event EventHandler<NoteAdapterClickEventArgs> ItemLongClick;
        public event EventHandler<NoteAdapterClickEventArgs> DeleteItemClick;
        List<Note> Items;

        public NoteAdapter(List<Note>Data)
        {
            Items = Data;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            //Setup your layout here
            View itemView = null;
            var id = Resource.Layout.NoteRow;
            itemView = LayoutInflater.From(parent.Context).Inflate(id, parent, false);

            var vh = new NoteAdapterViewHolder(itemView, OnClick, OnLongClick, OnDeleteClick);
            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var holder = viewHolder as NoteAdapterViewHolder;
            //holder.TextView.Text = items[position];
            holder.noteTitle.Text = Items[position].NoteTitle;
            holder.noteDate.Text = Items[position].NoteDate;
            

        }

        public override int ItemCount => Items.Count;

        void OnClick(NoteAdapterClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnLongClick(NoteAdapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);
        void OnDeleteClick(NoteAdapterClickEventArgs args) => DeleteItemClick(this, args);

    }

    public class NoteAdapterViewHolder : RecyclerView.ViewHolder
    {
        //public TextView TextView { get; set; }
        public TextView noteTitle { get; set; }
        public TextView noteContent { get; set; }
        public TextView noteDate { get; set; }
        
        public ImageView deleteButton { get; set; }
        public NoteAdapterViewHolder(View itemView, Action<NoteAdapterClickEventArgs> clickListener,
                            Action<NoteAdapterClickEventArgs> longClickListener, Action<NoteAdapterClickEventArgs> deleteClickListener) : base(itemView)
        {
            //TextView = v;
            noteTitle = (TextView)itemView.FindViewById(Resource.Id.noteTitle);
            noteDate = (TextView)itemView.FindViewById(Resource.Id.noteDate);
            deleteButton = (ImageView)itemView.FindViewById(Resource.Id.deleteButton);
            itemView.Click += (sender, e) => clickListener(new NoteAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new NoteAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
            deleteButton.Click += (sender, e) => deleteClickListener(new NoteAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
        }
    }

    public class NoteAdapterClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
    }
}