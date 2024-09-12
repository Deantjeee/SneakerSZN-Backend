using SneakerSZN_BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneakerSZN_BLL.Interfaces.Services
{
    public interface ISneakerService
    {
        public List<Sneaker> GetAll();

        public Sneaker? GetById(int id);

        public bool Create(Sneaker sneaker);

        public bool Update(Sneaker sneaker);

        public bool Delete(int id);
    }
}
