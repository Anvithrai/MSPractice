using AutoMapper;
using Mango.Services.ShoppingCartAPI.Data;
using Mango.Services.ShoppingCartAPI.Models;
using Mango.Services.ShoppingCartAPI.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ShoppingCartAPI.Controllers
{
    [Route("api/[cart]")]
    [ApiController]
    public class CartAPIController : ControllerBase
    {
        public ResponseDto _response;
        public IMapper _mapper;
        public readonly AppDbContext _appDbContext;
        
        public CartAPIController(AppDbContext appDbContext,IMapper mapper1)
        {
            _appDbContext = appDbContext;
            _mapper = mapper1;
            this._response = new ResponseDto();
        }

        [HttpPost("CartUpsert")]
        public async Task<ResponseDto> CartUpsert(CartDto cartdto)
        {
            try
            {
                var cartHeaderfromdb = await _appDbContext.CartHeaders.FirstOrDefaultAsync(u => u.UserId == cartdto.CartHeader.UserId);
                if (cartHeaderfromdb == null)

                {
                    //create header and details
                    CartHeader cartHeader = _mapper.Map<CartHeader>(cartdto.CartHeader);
                    _appDbContext.CartHeaders.Add(cartHeader);
                    await _appDbContext.SaveChangesAsync();
                    cartdto.CartDetails.First().CartHeaderId = cartHeader.CartHeaderId;
                    _appDbContext.CartDetails.Add(_mapper.Map<CartDetails>(cartdto.CartDetails.First()));
                    await _appDbContext.SaveChangesAsync();

                }
                else
                {
                    //if header is not null
                    //check if details has some product
                    var cartDetailsFromDb=await _appDbContext.CartDetails.FirstOrDefaultAsync(
                        u=>u.ProductId==cartdto.CartDetails.First().ProductId &&
                        u.CartHeaderId==cartHeaderfromdb.CartHeaderId);
                    if (cartDetailsFromDb == null)
                    {
                        //create cartdetails
                        cartdto.CartDetails.First().CartHeaderId=cartHeaderfromdb.CartHeaderId;
                        _appDbContext.CartDetails.Add(_mapper.Map<CartDetails>(cartdto.CartDetails.First()));
                        await _appDbContext.SaveChangesAsync();
                    }
                    else
                    {
                        //update count in cart details
                        cartdto.CartDetails.First().Count += cartDetailsFromDb.Count;
                        cartdto.CartDetails.First().CartHeaderId += cartDetailsFromDb.CartHeaderId;
                        cartdto.CartDetails.First().CartDetailsId += cartDetailsFromDb.CartDetailsId;

                        _appDbContext.CartDetails.Update(_mapper.Map<CartDetails>(cartdto.CartDetails.First() ));
                        await _appDbContext.SaveChangesAsync();

                    }
                }
                _response.Result=cartdto;
            } 
                catch (Exception ex)
            {
                _response.Message = ex.Message.ToString();
                _response.IsSuccess=false;
            }
            return _response;
           
        }
    }
}
