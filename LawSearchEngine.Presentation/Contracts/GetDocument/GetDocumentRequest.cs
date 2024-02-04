using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawSearchEngine.Presentation.Contracts.GetDocument
{
    public class GetDocumentRequest
    {
        public string DocumentPath { get; set; } = string.Empty;
    }
}
