using Mango.Web.Models;
using Mango.Web.Service.IService;
using Mango.Web.Utilities;

namespace Mango.Web.Service
{
    public class CouponService : ICouponService
    {
        private readonly IBaseService _baseService;
        public CouponService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<ResponseDto?> CreateCouponsAsync(CouponDTO couponDto)
        {
            return await _baseService.SendAsync(new Models.RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data=couponDto,
                Url = SD.CouponApiBase + "/api/coupon"
            });
        }

		public Task<ResponseDto?> CreateCouponAsync(CouponDTO couponDto)
		{
			throw new NotImplementedException();
		}

		public async Task<ResponseDto?> DeleteCouponsAsync(int id)
        {
            return await _baseService.SendAsync(new Models.RequestDto()
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.CouponApiBase + "/api/coupon/" + id
            });
        }

        public async Task<ResponseDto?> GetAllCouponsAsync()
        {
            return await _baseService.SendAsync(new Models.RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.CouponApiBase + "/api/coupon"
            });
        }

		public async Task<ResponseDto?> GetCouponAsync(string couponcode)
		{
			return await _baseService.SendAsync(new Models.RequestDto()
			{
				ApiType = SD.ApiType.GET,
				Url = SD.CouponApiBase + "/api/coupon/GetByCode/" + couponcode
			});
		}

		public async Task<ResponseDto?> GetCouponByIdAsync(int id)
		{
			return await _baseService.SendAsync(new Models.RequestDto()
			{
				ApiType = SD.ApiType.GET,
				Url = SD.CouponApiBase + "/api/coupon/" + id
			});
		}

        public async Task<ResponseDto?> UpdateCouponsAsync(CouponDTO couponDto)
        {
            return await _baseService.SendAsync(new Models.RequestDto()
            {
                ApiType = SD.ApiType.PUT,
                Data=couponDto,
                Url = SD.CouponApiBase + "/api/coupon/" 
            });
        }
	}
}
