
using Mango.Web.Models;

namespace Mango.Web.Service.IService
{
    public interface ICouponService
    {
        Task<ResponseDto?> GetCouponAsync(string couponcode);
        Task<ResponseDto?> GetAllCouponsAsync();
        Task<ResponseDto?> GetCouponByIdAsync(int id);
        Task<ResponseDto?> CreateCouponsAsync(CouponDTO couponDto);
        Task<ResponseDto?> UpdateCouponsAsync(CouponDTO couponDto);
        Task<ResponseDto?> DeleteCouponsAsync(int id);
		Task<ResponseDto> CreateCouponAsync(CouponDTO model);
	}
}
