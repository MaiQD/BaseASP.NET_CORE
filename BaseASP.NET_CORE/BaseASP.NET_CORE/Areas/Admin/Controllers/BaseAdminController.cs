﻿using BaseASP.NET_CORE.Filters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net;

namespace BaseASP.NET_CORE.Areas.Admin.Controllers
{
	[Area("Admin")]
	[AuthorizeAppWork]
	public abstract partial class BaseAdminController : Controller
	{
		/// <summary>
		/// Save selected tab name
		/// </summary>
		/// <param name="tabName">Tab name to save; empty to automatically detect it</param>
		/// <param name="persistForTheNextRequest">A value indicating whether a message should be persisted for the next request. Pass null to ignore</param>
		protected virtual void SaveSelectedTabName(string tabName = "", bool persistForTheNextRequest = true)
		{
			//default root tab
			SaveSelectedTabName(tabName, "selected-tab-name", null, persistForTheNextRequest);
			//child tabs (usually used for localization)
			//Form is available for POST only
			if (!Request.Method.Equals(WebRequestMethods.Http.Post, StringComparison.InvariantCultureIgnoreCase))
				return;

			foreach (var key in Request.Form.Keys)
				if (key.StartsWith("selected-tab-name-", StringComparison.InvariantCultureIgnoreCase))
					SaveSelectedTabName(null, key, key.Substring("selected-tab-name-".Length), persistForTheNextRequest);
		}

		/// <summary>
		/// Save selected tab name
		/// </summary>
		/// <param name="tabName">Tab name to save; empty to automatically detect it</param>
		/// <param name="persistForTheNextRequest">A value indicating whether a message should be persisted for the next request. Pass null to ignore</param>
		/// <param name="formKey">Form key where selected tab name is stored</param>
		/// <param name="dataKeyPrefix">A prefix for child tab to process</param>
		protected virtual void SaveSelectedTabName(string tabName, string formKey, string dataKeyPrefix, bool persistForTheNextRequest)
		{
			//keep this method synchronized with
			//"GetSelectedTabName" method of \GS.Web.Framework\Extensions\HtmlExtensions.cs
			if (string.IsNullOrEmpty(tabName))
			{
				tabName = Request.Form[formKey];
			}

			if (string.IsNullOrEmpty(tabName))
				return;

			var dataKey = "nop.selected-tab-name";
			if (!string.IsNullOrEmpty(dataKeyPrefix))
				dataKey += $"-{dataKeyPrefix}";

			if (persistForTheNextRequest)
			{
				TempData[dataKey] = tabName;
			}
			else
			{
				ViewData[dataKey] = tabName;
			}
		}

		/// <summary>
		/// Creates an object that serializes the specified object to JSON.
		/// </summary>
		/// <param name="data">The object to serialize.</param>
		/// <returns>The created object that serializes the specified data to JSON format for the response.</returns>
		public override JsonResult Json(object data)
		{
			//use IsoDateFormat on writing JSON text to fix issue with dates in KendoUI grid

			var serializerSettings = new JsonSerializerSettings();

			serializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
			serializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Unspecified;
			return base.Json(data, serializerSettings);
		}
	}
}