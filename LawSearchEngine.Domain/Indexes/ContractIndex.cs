using Elastic.Clients.Elasticsearch;
using LawSearchEngine.Domain.Common.ObjectTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawSearchEngine.Domain.Indexes
{
    public class ContractIndex : Entity<ulong>
    {
        public string SignerName { get; set; } = string.Empty;
        public string SignerSurname { get; set; } = string.Empty;
        public string GovernmentName { get; set; } = string.Empty;
        public string LevelOfGovernment { get; set; } = string.Empty;
        public string Contract { get; set; } = string.Empty;
        public GeoLocation Location { get; set; } = GeoLocation.Coordinates([0, 0]);
        public string ContractPath { get; set; } = string.Empty;
    }
}
