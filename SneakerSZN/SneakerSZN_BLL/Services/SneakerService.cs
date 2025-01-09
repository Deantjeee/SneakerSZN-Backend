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
    public class SneakerService : ISneakerService
    {
        private readonly ISneakerRepository _repository;

        public SneakerService(ISneakerRepository repository)
        {
            _repository = repository;
        }

        public List<Sneaker> GetAll()
        {
            return _repository.GetAll();
        }

        public Sneaker? GetById(int id)
        {
            return _repository.GetById(id);
        }

        public bool Create(Sneaker sneaker)
        {
            return _repository.Create(sneaker);
        }

        public bool Update(Sneaker sneaker)
        {
            return _repository.Update(sneaker);
        }

        public bool Delete(int id)
        {
            return _repository.Delete(id);

        }
    }
}
