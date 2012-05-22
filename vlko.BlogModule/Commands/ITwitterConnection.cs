﻿using System.ComponentModel.Composition;
using vlko.BlogModule.Commands.ComplexHelpers.Twitter;
using vlko.BlogModule.Roots;
using vlko.core.Repository;

namespace vlko.BlogModule.Commands
{
	[InheritedExport]
	public interface ITwitterConnection : ICommandGroup<TwitterStatus>
	{
		/// <summary>
		/// Gets the authorize URL.
		/// </summary>
		/// <param name="consumerAppIdent">The consumer app ident.</param>
		/// <param name="returnUrl">The return URL.</param>
		/// <returns>Authorize URL.</returns>
		string GetAuthorizeUrl(ConsumerAppIdent consumerAppIdent, string returnUrl);

		/// <summary>
		/// Gets the OAuth token.
		/// </summary>
		/// <param name="consumerAppIdent">The consumer app ident.</param>
		/// <param name="requestToken">The request token.</param>
		/// <param name="requestVerifier">The request verifier.</param>
		/// <returns>OAuth token.</returns>
		OAuthToken GetOAuthToken(ConsumerAppIdent consumerAppIdent, string requestToken, string requestVerifier);

		/// <summary>
		/// Determines whether [is token valid] [the specified consumer app ident].
		/// </summary>
		/// <param name="token">The token.</param>
		/// <returns>
		/// 	<c>true</c> if [is token valid] [the specified consumer app ident]; otherwise, <c>false</c>.
		/// </returns>
		bool IsTokenValid(OAuthToken token);

		/// <summary>
		/// Gets the statuses for user.
		/// </summary>
		/// <param name="token">The token.</param>
		/// <param name="userName">Name of the user.</param>
		/// <param name="page">The page.</param>
		/// <param name="pageCount">The page count.</param>
		/// <returns>
		/// List of twitter status for specified user.
		/// </returns>
		TwitterStatus[] GetStatusesForUser(OAuthToken token, string userName, int page = 0, int pageCount = 20);

        /// <summary>
        /// Gets the retweets for user.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageCount">The page count.</param>
        /// <returns>List of retweets of current user.</returns>
        TwitterStatus[] GetRetweetsForUser(OAuthToken token, string userName, int page = 0, int pageCount = 20);
	}
}
