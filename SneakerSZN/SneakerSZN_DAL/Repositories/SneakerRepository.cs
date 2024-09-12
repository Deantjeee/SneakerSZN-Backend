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
    public class SneakerRepository : ISneakerRepository
    {
        private readonly ApplicationDbContext _dbContext; //TEST

        public SneakerRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Sneaker> GetAll()
        {
            return _dbContext.Sneakers.ToList();
        }

        public Sneaker? GetById(int id)
        {
            return _dbContext.Sneakers.Find(id);
        }

        public bool Create(Sneaker sneaker)
        {
            _dbContext.Sneakers.Add(sneaker);
            return _dbContext.SaveChanges() > 0;
        }

        public bool Update(Sneaker sneaker)
        {
            _dbContext.Sneakers.Update(sneaker);
            return _dbContext.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            Sneaker? sneaker = _dbContext.Sneakers.Find(id);
            if(sneaker == null)
            {
                return false;
            }

            _dbContext.Sneakers.Remove(sneaker);
            return _dbContext.SaveChanges() > 0;
        }
    }
}
