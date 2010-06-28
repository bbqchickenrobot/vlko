﻿using System;
using System.ComponentModel.DataAnnotations;
using vlko.core.ValidationAtribute;

namespace vlko.core.Models.Action.ActionModel
{
	public class CommentActionModel
	{

		/// <summary>
		/// Initializes a new instance of the <see cref="CommentActionModel"/> class.
		/// </summary>
		public CommentActionModel()
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CommentActionModel"/> class.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <param name="contentId">The content id.</param>
		/// <param name="name">The name.</param>
		/// <param name="text">The text.</param>
		/// <param name="changeDate">The change date.</param>
		/// <param name="parentId">The parent id.</param>
		/// <param name="changeUser">The change user.</param>
		/// <param name="anonymousName">Name of the anonymous.</param>
		/// <param name="clientIp">The client ip.</param>
		/// <param name="userAgent">The user agent.</param>
		public CommentActionModel(Guid id, Guid contentId, string name, string text, DateTime changeDate, Guid? parentId, User changeUser, string anonymousName, string clientIp, string userAgent)
		{
			Id = id;
			ContentId = contentId;
			Name = name;
			Text = text;
			ChangeDate = changeDate;
			ParentId = parentId;
			ChangeUser = changeUser;
			AnonymousName = anonymousName;
			ClientIp = clientIp;
			UserAgent = userAgent;
		}

		/// <summary>
		/// Gets or sets the Id.
		/// </summary>
		/// <value>The Id.</value>
		[Key]
		public Guid Id { get; set; }

		/// <summary>
		/// Gets or sets the content id.
		/// </summary>
		/// <value>The content id.</value>
		public Guid ContentId { get; set; }

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		[Required(ErrorMessageResourceType = typeof(ModelResources), ErrorMessageResourceName = "CommentNameRequireError")]
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the text.
		/// </summary>
		/// <value>The text.</value>
		[DataType(DataType.Html)]
		[AntiXssHtmlText]
		[Required(ErrorMessageResourceType = typeof(ModelResources), ErrorMessageResourceName = "CommentTextRequireError")]
		public string Text { get; set; }

		/// <summary>
		/// Gets or sets the change date.
		/// </summary>
		/// <value>The change date.</value>
		public DateTime ChangeDate { get; set; }

		/// <summary>
		/// Gets or sets the parent id.
		/// </summary>
		/// <value>The parent id.</value>
		public Guid? ParentId { get; set; }

		/// <summary>
		/// Gets or sets the change user.
		/// </summary>
		/// <value>The change user.</value>
		public User ChangeUser { get; set; }

		/// <summary>
		/// Gets or sets the name of the anonymous.
		/// </summary>
		/// <value>The name of the anonymous.</value>
		public string AnonymousName { get; set; }

		/// <summary>
		/// Gets or sets the client ip.
		/// </summary>
		/// <value>The client ip.</value>
		public string ClientIp { get; set; }

		/// <summary>
		/// Gets or sets the user agent.
		/// </summary>
		/// <value>The user agent.</value>
		public string UserAgent { get; set; }
	}
}
