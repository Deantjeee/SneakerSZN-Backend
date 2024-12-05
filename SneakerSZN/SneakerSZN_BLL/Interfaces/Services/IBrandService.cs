using SneakerSZN_BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneakerSZN_BLL.Interfaces.Services
{
    public interface IBrandService
    {
        public List<Brand> GetAll();

        public Brand? GetById(int? id);

        public bool Create(Brand brand);

        public bool Update(Brand brand);

        public bool Delete(int id);
    }
}
