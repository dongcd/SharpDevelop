﻿// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace ICSharpCode.AvalonEdit.Search
{
	/// <summary>
	/// A button that opens a drop-down menu when clicked.
	/// </summary>
	public class DropDownButton : ButtonBase
	{
		/// <summary>
		/// Identifies the <see cref="DropDownContent" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty DropDownContentProperty
			= DependencyProperty.Register("DropDownContent", typeof(Popup),
			                              typeof(DropDownButton), new FrameworkPropertyMetadata(null));
		
		/// <summary>
		/// The key that identifies the <see cref="IsDropDownContentOpen" /> dependency property.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
		protected static readonly DependencyPropertyKey IsDropDownContentOpenPropertyKey
			= DependencyProperty.RegisterReadOnly("IsDropDownContentOpen", typeof(bool),
			                                      typeof(DropDownButton), new FrameworkPropertyMetadata(false));
		
		/// <summary>
		/// Identifies the <see cref="IsDropDownContentOpen" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty IsDropDownContentOpenProperty = IsDropDownContentOpenPropertyKey.DependencyProperty;
		
		static DropDownButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(DropDownButton), new FrameworkPropertyMetadata(typeof(DropDownButton)));
		}
		
		/// <summary>
		/// Gets/Sets the popup that is used as drop-down content.
		/// </summary>
		public Popup DropDownContent {
			get { return (Popup)GetValue(DropDownContentProperty); }
			set { SetValue(DropDownContentProperty, value); }
		}
		
		/// <summary>
		/// Gets whether the drop-down is opened.
		/// </summary>
		public bool IsDropDownContentOpen {
			get { return (bool)GetValue(IsDropDownContentOpenProperty); }
			protected set { SetValue(IsDropDownContentOpenPropertyKey, value); }
		}
		
		/// <inheritdoc/>
		protected override void OnClick()
		{
			if (DropDownContent != null && !IsDropDownContentOpen) {
				DropDownContent.Placement = PlacementMode.Bottom;
				DropDownContent.PlacementTarget = this;
				DropDownContent.IsOpen = true;
				DropDownContent.Closed += DropDownContent_Closed;
				this.IsDropDownContentOpen = true;
			}
		}
		
		void DropDownContent_Closed(object sender, EventArgs e)
		{
			((Popup)sender).Closed -= DropDownContent_Closed;
			this.IsDropDownContentOpen = false;
		}
	}
}
