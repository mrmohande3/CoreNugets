using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HashSharpCore.DataLayer.Models;
using HashSharpCore.Models;

namespace HashSharpCore.Services.Contracts
{
    public interface IJwtUtility
    {
        Task<AccessToken> Generate(User user);
    }
}
