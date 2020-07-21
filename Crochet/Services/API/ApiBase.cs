using Crochet.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crochet.Services.API
{
    public class ApiBase
    {
        protected readonly IApi API;

        public ApiBase(IApi api)
        {
            API = api;
        }
    }
}
