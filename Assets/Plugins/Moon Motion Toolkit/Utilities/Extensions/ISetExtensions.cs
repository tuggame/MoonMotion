﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ISet Extensions: provides extension methods for handling ISets //
// #enumerables
public static class ISetExtensions
{
	#region including

	// method: (according to the given boolean:) include the given item in this given set, then return this given set //
	public static ISet<TItem> include<TItem>(this ISet<TItem> set, TItem item, bool boolean = true)
		=>	set.after(()=>
				set.Add(item),
				boolean);

	// method: (according to the given boolean:) include the given item in this given set, then return the given item //
	public static TItem includeGet<TItem>(this ISet<TItem> set, TItem item, bool boolean = true)
		=>	item.returnAnd(()=>
				set.include(item, boolean));

	// method: (according to the given boolean:) include this given item in the given set, then return the given set //
	public static ISet<TItem> includeInGet<TItem>(this TItem item, ISet<TItem> set, bool boolean = true)
		=> set.include(item, boolean);

	// method: (according to the given boolean:) include this given item in the given set, then return this given item //
	public static TItem includeIn<TItem>(this TItem item, ISet<TItem> set, bool boolean = true)
		=> set.includeGet(item, boolean);
	#endregion include


	#region acting upon all items

	// method: (according to the given boolean:) invoke the given action on each item in this given set, then return this given set //
	public static ISet<TItem> forEach<TItem>(this ISet<TItem> set, Action<TItem> action, bool boolean = true)
		=> set.forEach_EnumerableSpecializedViaCasting(action, boolean);
	#endregion acting upon all items
}