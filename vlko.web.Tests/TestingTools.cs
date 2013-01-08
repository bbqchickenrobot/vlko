﻿using System.Web.Mvc;
using vlko.BlogModule.NH.Commands;
using vlko.core.Authentication;
using vlko.core.Base;
using vlko.core.Commands;
using vlko.core.InversionOfControl;
using vlko.core.Roots;
using vlko.web.Tests.Controllers.Admin;

namespace vlko.web.Tests
{
	public static class TestingTools
	{
        private static CommentCrud referenceToBlogNHImplementation = new CommentCrud();
        private static System.Data.SQLite.SQLiteJournalModeEnum referenceToSQLite = System.Data.SQLite.SQLiteJournalModeEnum.Default;
		/// <summary>
		/// Binds the model.
		/// </summary>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		/// <param name="controller">The controller.</param>
		/// <param name="valueProvider">The value provider.</param>
		/// <returns>Model instance</returns>
		public static TModel BindModel<TModel>(this Controller controller, IValueProvider valueProvider) where TModel : class
		{
			IModelBinder binder = ModelBinders.Binders.GetBinder(typeof(TModel));
			ModelBindingContext bindingContext = new ModelBindingContext()
			{
				FallbackToEmptyPrefix = true,
				ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, typeof(TModel)),
				ModelName = "NotUsedButNotNull",
				ModelState = controller.ModelState,
				PropertyFilter = (name => { return true; }),
				ValueProvider = valueProvider
			};

			return (TModel)binder.BindModel(controller.ControllerContext, bindingContext);
		}

		/// <summary>
		/// Mocks the user.
		/// </summary>
		/// <param name="controller">The controller.</param>
		/// <param name="userName">Name of the user.</param>
		public static void MockUser(this BaseController controller, string userName)
		{
			IoC.AddRerouting<IUserAuthenticationService>(() => new StaticPageControllerTest.UserAuthenticationServiceMock());
			var user = IoC.Resolve<IUserCommands>().GetByName(userName);
			controller.HttpContext.User = new UserPrincipal(user);
		}

	}
}
