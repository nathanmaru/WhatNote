package crc646e0a6183e82f2ae9;


public class NoteAdapterViewHolder
	extends androidx.recyclerview.widget.RecyclerView.ViewHolder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("WhatNote.Adapter.NoteAdapterViewHolder, WhatNote", NoteAdapterViewHolder.class, __md_methods);
	}


	public NoteAdapterViewHolder (android.view.View p0)
	{
		super (p0);
		if (getClass () == NoteAdapterViewHolder.class)
			mono.android.TypeManager.Activate ("WhatNote.Adapter.NoteAdapterViewHolder, WhatNote", "Android.Views.View, Mono.Android", this, new java.lang.Object[] { p0 });
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
