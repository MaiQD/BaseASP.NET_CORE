using System;

namespace BaseASP.NET_CORE.Entities.HeThong
{
	public enum NguoiDungStatusID
	{
		/// <summary>
		/// Bình thường
		/// </summary>
		ok = 1,

		/// <summary>
		/// Khóa
		/// </summary>
		Lock = 2,

		/// <summary>
		/// Xóa
		/// </summary>
		Delete = 3
	}

	public enum NguoiDungKetQuaDangNhap
	{
		/// <summary>
		/// Login successful
		/// </summary>
		Successful = 1,

		/// <summary>
		/// Customer does not exist (email or username)
		/// </summary>
		CustomerNotExist = 2,

		/// <summary>
		/// Wrong password
		/// </summary>
		WrongPassword = 3,

		/// <summary>
		/// Account have not been activated
		/// </summary>
		NotActive = 4,

		/// <summary>
		/// Customer has been deleted
		/// </summary>
		Deleted = 5,

		/// <summary>
		/// Customer not registered
		/// </summary>
		NotRegistered = 6,

		/// <summary>
		/// Locked out
		/// </summary>
		LockedOut = 7
	}

	public partial class NguoiDung : BaseEntity
	{
		public NguoiDung()
		{
			this.GUID = new Guid();
		}

		public int ID { get; set; }
		public Guid GUID { get; set; }
		public String TEN_DANG_NHAP { get; set; }
		public String MAT_KHAU { get; set; }
		public String TEN_DAY_DU { get; set; }
		public String EMAIL { get; set; }
		public DateTime NGAY_TAO { get; set; }
		public int TRANG_THAI_ID { get; set; }

		public NguoiDungStatusID nguoiDungStatusID
		{
			get => (NguoiDungStatusID)TRANG_THAI_ID;
			set => TRANG_THAI_ID = (int)value;
		}

		public DateTime? NGAY_TRUY_CAP { get; set; }
		public String IP_TRUY_CAP { get; set; }
		public String GHI_CHU { get; set; }
		public bool IS_QUAN_TRI { get; set; }
		public String MOBILE { get; set; }
		public String MAT_KHAU_KEY { get; set; }
	}
}