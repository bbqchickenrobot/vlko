﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using vlko.core.Components;
using vlko.core.Models.Action.ActionModel;
using vlko.core.Models.Action.ViewModel;

namespace vlko.web.ViewModel.Page
{
	public enum CommentViewTypeEnum
	{
		Flat,
		FlatDesc,
		Tree
	}

	public class PageViewModel
	{
		/// <summary>
		/// Gets or sets the static text.
		/// </summary>
		/// <value>The static text.</value>
		public StaticTextWithFullTextViewModel StaticText { get; set; }

		/// <summary>
		/// Gets or sets the type of the comment view.
		/// </summary>
		/// <value>The type of the comment view.</value>
		public CommentViewTypeEnum CommentViewType { get; set; }

		/// <summary>
		/// Gets or sets the new comment.
		/// </summary>
		/// <value>The new comment.</value>
		public CommentActionModel NewComment { get; set; }

		/// <summary>
		/// Gets or sets the flat comments.
		/// </summary>
		/// <value>The flat comments.</value>
		public PagedModel<CommentViewModel> FlatComments { get; set; }

		/// <summary>
		/// Gets or sets the tree comments.
		/// </summary>
		/// <value>The tree comments.</value>
		public IEnumerable<CommentTreeViewModel> TreeComments { get; set; }
	}
}