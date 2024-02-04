using FluentResults;
using LawSearchEngine.Application.Common.Connectors.Contracts.LocationIQ.GetLocation;

namespace LawSearchEngine.Application.Common.Connectors
{
    public interface ILocationIQConnector
    {
        public Task<Result<LocationIQResponse>> GetLocation(string location);
    }
}
