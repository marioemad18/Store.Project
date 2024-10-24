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

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyList<BrandtypeDetailsDto>> GetAllBrandsAsync()
        {
            var brands = await _unitOfWork.Repository<ProductBrand, int> ().GetAllAsync();

            IReadOnlyList<BrandtypeDetailsDto> MappedBrands = brands.Select(x => new BrandtypeDetailsDto
            {
                Id = x.Id,
                CreatedAt = x.CreatedAt,
                Name = x.Name,
            }).ToList();

            return MappedBrands;
        }

        public async Task<IReadOnlyList<ProductDto>> GetAllProductsAsync()
        {
            var products = await _unitOfWork.Repository<Product,int>().GetAllAsync();

            var MappedProduct = products.Select(x => new ProductDto
            {
                Id = x.Id,
                Name = x.Name,
                BrandName = x.Brand.Name,
                TypeName = x.Type.Name,
                CreatedAt = x.CreatedAt,
                Description = x.Description,
                PictureUrl = x.ImageUrl
            }).ToList();
            return MappedProduct;
        }

        public async Task<IReadOnlyList<BrandtypeDetailsDto>> GetAllTypesAsync()
        {
            var types = await _unitOfWork.Repository<ProductType, int>().GetAllAsync();

            var MappedProduct = types.Select(x => new BrandtypeDetailsDto
            {
                Id = x.Id,
                Name = x.Name,
                CreatedAt = x.CreatedAt,
            }).ToList();
            return MappedProduct;
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
            var MappedProduct = new ProductDto {
                Id = product.Id,
                Name = product.Name,
                CreatedAt = product.CreatedAt,
                Description = product.Description,
                PictureUrl = product.ImageUrl,
                Price = product.Price,
                TypeName = product.Type.Name,
            };
            return MappedProduct;

        }
    }
}
