using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using FluentValidation;
using LawSearchEngine.Domain.Indexes.Common;

namespace LawSearchEngine.Application.Common.Contracts.DocumentSearch
{
    public abstract class DocumentSearchWithLocationRequest<T> : DocumentSearchRequest<T> where T : IndexWithLocation
    {
        public GeoLocation? Location { get; set; }
        public double? Radius { get; set; }

        public override QueryDescriptor<T> Query
        {
            get
            {
                QueryDescriptor<T> query = new QueryDescriptor<T>();

                query = query.Bool(booleanQuery =>
                {
                    GenerateBooleanQueries(booleanQuery);

                    if (Location == null || !Radius.HasValue)
                    {
                        return;
                    }

                    booleanQuery.Filter(filter =>
                    {
                        filter.GeoDistance(g =>
                        {
                            g.Field(f => f.Location)
                             .DistanceType(GeoDistanceType.Arc)
                             .Location(Location)
                             .Distance($"{Radius}km");
                        });
                    });
                });

                return query;
            }
        }
    }

    public class DocumentSearchWithLocationRequestValidator<T> : AbstractValidator<DocumentSearchWithLocationRequest<T>> where T : IndexWithLocation
    {
        public DocumentSearchWithLocationRequestValidator()
        {
            Include(new DocumentSearchRequestValidator<T>());

            RuleFor(x => x.Location).NotNull()
                .When(x => x.Radius.HasValue)
                .WithMessage("Location must be present");

            RuleFor(x => x.Radius).NotEmpty()
                .When(x => x.Location != null)
                    .WithMessage("Radius must be present")
                .GreaterThan(0)
                .When(x => x.Location != null)
                    .WithMessage("Radius must be greater than 0");
        }
    }
}
