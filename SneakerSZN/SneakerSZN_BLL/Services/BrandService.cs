using SneakerSZN_BLL.Interfaces.Repositories;
using SneakerSZN_BLL.Interfaces.Services;
using SneakerSZN_BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneakerSZN_BLL.Services
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _repository;

        public BrandService(IBrandRepository repository)
        {
            _repository = repository;
        }

        public List<Brand> GetAll()
        {
            return _repository.GetAll();
        }

        public Brand GetById(int id)
        {
            return _repository.GetById(id);
        }

        public bool Create(Brand brand)
        {
            if (string.IsNullOrWhiteSpace(brand.Name))
            {
                throw new ArgumentException("Brand name is required.");
            }

            if (brand.Name.Length > 20)
            {
                throw new ArgumentException("Brand name cannot exceed 20 characters.");
            }

            return _repository.Create(brand);
        }

        public bool Update(Brand brand)
        {
            return _repository.Update(brand);
        }

        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }
    }
}
