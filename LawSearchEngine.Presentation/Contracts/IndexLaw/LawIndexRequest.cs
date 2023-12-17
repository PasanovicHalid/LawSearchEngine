using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawSearchEngine.Presentation.Contracts.IndexLaw
{
    public class LawIndexRequest
    {
        public IFormFile Law { get; set; } = null!;
    }
}
