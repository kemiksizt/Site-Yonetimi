using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemiksiz.Service.Jwt
{
    public interface IJwtService
    {
        public string Generate(int id);
    }
}
