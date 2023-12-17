using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawSearchEngine.Application.Common.Connectors
{
    public interface IElasticSearchConnector
    {
        public Task<bool> IndexContractAsync(string signerName, string signerSurname, string governmentName, string levelOfGovernment, string contract, double latitude, double longitude);
        public Task<bool> IndexLawAsync(string law);


    }
}
