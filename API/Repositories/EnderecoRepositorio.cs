using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Context;

namespace API.Repositories;

    public class EnderecoRepositorio: Repositorio
    {
        public EnderecoRepositorio(FoodPlaceContext context) : base(context)
        {}
    }
