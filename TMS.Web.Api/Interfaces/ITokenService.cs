﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Domain;

namespace TMS.Web.Api
{
    public interface ITokenService
    {
        string CreateToken(UserEntity user);
    }
}
