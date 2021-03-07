using BaseASP.NET_CORE.Entities.HeThong;
using BaseASP.NET_CORE.Extensions;
using BaseASP.NET_CORE.Services.Interface;
using Microsoft.AspNetCore.Http;

namespace BaseASP.NET_CORE.Services.Implement
{
	public partial class WebWorkContext : IWorkContext
	{
		#region Fields

		private readonly IHttpContextAccessor _httpContextAccessor;
		private NguoiDung _cachedCustomer;

		#endregion Fields

		#region Ctor

		public WebWorkContext(
			IHttpContextAccessor httpContextAccessor
			)
		{
			this._httpContextAccessor = httpContextAccessor;
		}

		#endregion Ctor

		#region Properties

		/// <summary>
		/// Gets or sets the current customer
		/// </summary>
		public virtual NguoiDung CurrentCustomer
		{
			get
			{
				//whether there is a cached value
				if (_cachedCustomer != null)
					return _cachedCustomer;

				var customer = _httpContextAccessor.HttpContext.Session.GetObject<NguoiDung>("CurrentCustomer");
				_cachedCustomer = customer;

				return _cachedCustomer;
				//return _httpContextAccessor.HttpContext.Session.GetObject<NguoiDung>("CurrentCustomer");
			}
			set
			{
				_cachedCustomer = value;
			}
		}

		/// <summary>
		/// Gets or sets value indicating whether we're in admin area
		/// </summary>
		public virtual bool IsAdmin { get; set; }

		#endregion Properties
	}
}