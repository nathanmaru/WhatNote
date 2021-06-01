using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using WhatNote.Data_Models;

namespace WhatNote.Adapter
{
    class ViewContentAdapter : RecyclerView.Adapter
    {
        public event EventHandler<ViewContentAdapterClickEventArgs> ItemClick;
        public event EventHandler<ViewContentAdapterClickEventArgs> ItemLongClick;
        List<Note> Items;

        public ViewContentAdapter(List<Note> data)
        {
            Items = data;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            //Setup your layout here
            View itemView = null;
            var id = Resource.Layout.ViewNoteContent;
            itemView = LayoutInflater.From(parent.Context).Inflate(id, parent, false);

            var vh = new ViewContentAdapterViewHolder(itemView, OnClick, OnLongClick);
            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {

            // Replace the contents of the view with that element
            var holder = viewHolder as ViewContentAdapterViewHolder;
            //holder.TextView.Text = items[position];
            
            holder.noteContent.Text = Items[position].NoteContent;
            
            
        }

        public override int ItemCount => Items.Count;

        void OnClick(ViewContentAdapterClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnLongClick(ViewContentAdapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);

    }

    public class ViewContentAdapterViewHolder : RecyclerView.ViewHolder
    {
        //public TextView TextView { get; set; }
        
        public TextView noteContent { get; set; }
      


        public ViewContentAdapterViewHolder(View itemView, Action<ViewContentAdapterClickEventArgs> clickListener,
                            Action<ViewContentAdapterClickEventArgs> longClickListener) : base(itemView)
        {
            //TextView = v;
            noteContent = (TextView)itemView.FindViewById(Resource.Id.noteContentText);
            itemView.Click += (sender, e) => clickListener(new ViewContentAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new ViewContentAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
        }
    }

    public class ViewContentAdapterClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
    }
}