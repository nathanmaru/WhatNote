using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using System;
using WhatNote;
using System.Collections.Generic;
using WhatNote.Data_Models;

namespace WhatNote.Adapter
{
    class Adapter1 : RecyclerView.Adapter
    {
        public event EventHandler<Adapter1ClickEventArgs> ItemClick;
        public event EventHandler<Adapter1ClickEventArgs> ItemLongClick;
        List<Note> Items;

        public Adapter1(List<Note> data)
        {
            Items = data;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            //Setup your layout here
            View itemView = null;
            var id = Resource.Layout.NoteRow;
            itemView = LayoutInflater.From(parent.Context).Inflate(id, parent, false);

            var vh = new Adapter1ViewHolder(itemView, OnClick, OnLongClick);
            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var item = Items[position];

            // Replace the contents of the view with that element
            var holder = viewHolder as Adapter1ViewHolder;
            //holder.TextView.Text = items[position];
            holder.noteTitle.Text = Items[position].NoteTitle;
            holder.noteDate.Text = Items[position].NoteDate;
            
        }

        public override int ItemCount => Items.Count;

        void OnClick(Adapter1ClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnLongClick(Adapter1ClickEventArgs args) => ItemLongClick?.Invoke(this, args);

    }

    public class Adapter1ViewHolder : RecyclerView.ViewHolder
    {
        //public TextView TextView { get; set; }
        public TextView noteTitle { get; set; }
        public TextView noteDate{ get; set; }
        public Button deleteButton { get; set; }
        public Adapter1ViewHolder(View itemView, Action<Adapter1ClickEventArgs> clickListener,
                            Action<Adapter1ClickEventArgs> longClickListener) : base(itemView)
        {
            //TextView = v;
            itemView.Click += (sender, e) => clickListener(new Adapter1ClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new Adapter1ClickEventArgs { View = itemView, Position = AdapterPosition });
        }
    }

    public class Adapter1ClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
    }
}