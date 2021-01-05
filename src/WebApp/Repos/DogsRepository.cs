using WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApp
{

    public interface IDogRepository
    {
        List<Dog> GetAllDogs();
    }


    public class DogsRepository : IDogRepository
    {
        PGTestContext _context;
        public DogsRepository(PGTestContext context)
        {
            _context = context;
        }
        public List<Dog> GetAllDogs()
        {
            var res = _context.Dogs.ToList();
            return res;
        }

    }
}