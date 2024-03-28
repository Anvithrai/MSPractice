using AutoMapper;

using Mango.Services.ProductAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Mango.Services.CouponAPI.Models.Dto;
using Mango.Services.ProductsAPI.Models;
using Mango.Services.ProductsAPI.Models.Dto;

namespace Mango.Services.ProductAPI.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly AppDbContext _db;
        private ResponseDto _responseDto;
        private IMapper _mapper;
        public ProductController(AppDbContext db,IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _responseDto = new ResponseDto();
        }

        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<Product> objList = _db.products.ToList();
                _responseDto.Result = _mapper.Map<IEnumerable<ProductDto>>(objList);  
              
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;  

            }
            return _responseDto;
        }
        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto Get(int id)
        {
            try
            {
                Product obj = _db.products.First(u => u.ProductId == id);
                _responseDto.Result=_mapper.Map<ProductDto>(obj);
                 
            }
            catch(Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;


            }
            return _responseDto;
        }
      
        [HttpPost]
        public ResponseDto Post([FromBody] ProductDto ProductDto)
        {
            try
            {
                Product obj = _mapper.Map<Product>(ProductDto);
                _db.products.Add(obj);
                _db.SaveChanges();
               
                _responseDto.Result = _mapper.Map<ProductDto>(obj);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;


            }
            return _responseDto;
        }
        [HttpPut]
        public ResponseDto Put([FromBody] ProductDto ProductDto)
        {
            try
            {
                Product obj = _mapper.Map<Product>(ProductDto);
                _db.products.Update(obj);
                _db.SaveChanges();

                _responseDto.Result = _mapper.Map<ProductDto>(obj);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;


            }
            return _responseDto;
        }
        [HttpDelete]
        [Route("{id:int}")]
        public ResponseDto Delete(int id)
        {
            try
            {
                Product obj = _db.products.First(u=>u.ProductId==id);
                _db.products.Remove(obj);
                _db.SaveChanges();

            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;


            }
            return _responseDto;
        }
    }
     
    }

