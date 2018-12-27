﻿using System;
using System.Collections.Generic;

namespace Xamarin.Forms
{
	public class SelectionChangedEventArgs : EventArgs
	{
		public IReadOnlyList<object> PreviousSelection { get; }
		public IReadOnlyList<object> CurrentSelection { get; }

		static readonly IReadOnlyList<object> s_empty = new List<object>(0);

		internal SelectionChangedEventArgs(object previousSelection, object currentSelection)
		{
			PreviousSelection = previousSelection != null ? new List<object>(1) { previousSelection } : s_empty;
			CurrentSelection = currentSelection != null ? new List<object>(1) { currentSelection } : s_empty;
		}

		internal SelectionChangedEventArgs(List<object> previousSelection, List<object> currentSelection)
		{
			PreviousSelection = previousSelection ?? throw new ArgumentNullException(nameof(previousSelection));
			CurrentSelection = currentSelection ?? throw new ArgumentNullException(nameof(currentSelection));
		}
	}
}