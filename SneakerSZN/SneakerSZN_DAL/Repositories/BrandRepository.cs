using SneakerSZN_BLL.Interfaces.Repositories;
using SneakerSZN_BLL.Models;
using SneakerSZN_DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneakerSZN_DAL.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public BrandRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Brand> GetAll()
        {
            return _dbContext.Brands.ToList();
        }

        public Brand? GetById(int id)
        {
            return _dbContext.Brands.Find(id);
        }

        public bool Create(Brand brand)
        {
            _dbContext.Brands.Add(brand);
            return _dbContext.SaveChanges() > 0;
        }

        public bool Update(Brand brand)
        {
            _dbContext.Brands.Update(brand);
            return _dbContext.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            Brand? brand = _dbContext.Brands.Find(id);

            if (brand == null)
            {
                return false;
            }

            _dbContext.Brands.Remove(brand);
            return _dbContext.SaveChanges() > 0;
        }
    }
}
