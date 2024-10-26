using AutoMapper;
using Store.Data.Entity;
using Store.Repository.Interfaces;
using Store.Repository.UnitOfWork;
using Store.Service.Services.Products.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<BrandtypeDetailsDto>> GetAllBrandsAsync()
        {
            var brands = await _unitOfWork.Repository<ProductBrand, int> ().GetAllAsync();

            var MappedBrands = _mapper.Map<IReadOnlyList<BrandtypeDetailsDto>>(brands);

            return MappedBrands;
        }

        public async Task<IReadOnlyList<ProductDto>> GetAllProductsAsync()
        {
            var products = await _unitOfWork.Repository<Product,int>().GetAllAsync();

            var MappedProducts = _mapper.Map<IReadOnlyList<ProductDto>>(products);
            return MappedProducts;
        }

        public async Task<IReadOnlyList<BrandtypeDetailsDto>> GetAllTypesAsync()
        {
            var types = await _unitOfWork.Repository<ProductType, int>().GetAllAsync();

            var MappedTypes = _mapper.Map < IReadOnlyList < BrandtypeDetailsDto >>(types);
            return MappedTypes;
        }

        public async Task<ProductDto> GetProductByIdAsync(int? id)
        {
            if(id is null)
            {
                throw new Exception("Id IS NULL");
            }
            var product = await _unitOfWork.Repository<Product, int>().GetByIdAsync(id.Value);

            if (product is null)
                throw new Exception("Product Not Found");
            var MappedProduct = _mapper.Map<ProductDto>(product);
            return MappedProduct;

        }
    }
}
