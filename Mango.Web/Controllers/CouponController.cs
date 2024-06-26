﻿
using Mango.Web.Models;
using Mango.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Mango.Web.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;
        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        public async Task<IActionResult> CouponIndex()

        {
            List<CouponDTO>? list = new();

            ResponseDto? response = await _couponService.GetAllCouponsAsync();
            if(response != null && response.IsSuccess) 
            {
                list=JsonConvert.DeserializeObject<List<CouponDTO>>(Convert.ToString(response.Result));
            }
            return View(list);  

           
        }
        public async Task<IActionResult> CreateCoupon()
        {
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> CreateCoupon(CouponDTO model)
        {
            if(ModelState.IsValid)
            {
                ResponseDto response=await _couponService.CreateCouponsAsync(model);
                if(response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(CouponIndex));
                }
            }
            return View();

        }
        public async Task<IActionResult> CouponDelete(int couponId)
        {
            ResponseDto? response=await _couponService.GetCouponByIdAsync(couponId);
            if (response != null && response.IsSuccess)
            {
                CouponDTO? model = JsonConvert.DeserializeObject<CouponDTO>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();

        }
        [HttpPost]
		public async Task<IActionResult> CouponDelete(CouponDTO couponDTO)
		{
			ResponseDto? response = await _couponService.DeleteCouponsAsync(couponDTO.CouponId);
			if (response != null && response.IsSuccess)
			{
                return RedirectToAction(nameof(CouponIndex));
			
			}
			return View(couponDTO);

		}

	}
}
