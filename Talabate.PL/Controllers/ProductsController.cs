using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Specefication;
using Talabate.PL.Dtos;
using Talabate.PL.ErrorsHandle;
using Talabate.PL.Helper;

namespace Talabate.PL.Controllers
{

    public class ProductsController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ProductsController( IGenericRepository<Product> productRepo,IMapper mapper,IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        [ProducesResponseType(typeof(ProductToReturnDto), 200)]

        [Authorize]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>>  GetAllProducts([FromQuery]SpecParameter Specparameter)
        {
            var spec=new ProductWithBrandAndTypeSpecification(Specparameter);
            var products= await _unitOfWork.Repository<Product>().GetAllWithSpecAsync(spec);
            var mappedProduct=_mapper.Map<IReadOnlyList<ProductToReturnDto>>(products);
            var count = await _unitOfWork.Repository<Product>().GetCountWithSpecsAsync(new ProductWithCountFiltration(Specparameter));
            return Ok(new Pagination<ProductToReturnDto>(Specparameter.PageSize, Specparameter.PageIndex, mappedProduct,count));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductToReturnDto), 200)]
        [ProducesResponseType(typeof(ErrorApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProductById(int id)
        {
            var spec=new ProductWithBrandAndTypeSpecification(id);
            var product = await _unitOfWork.Repository<Product>().GetByIdWithSpecsAsync(spec);
            if (product == null)
                return NotFound(new ErrorApiResponse(404));
            var mappedProduct = _mapper.Map<ProductToReturnDto>(product);

            return Ok(mappedProduct); 
        }

    }
}
