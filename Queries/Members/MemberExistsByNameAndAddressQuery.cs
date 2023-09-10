using Domain;

using LinqKit;

using Microsoft.EntityFrameworkCore;

using Persistence;

namespace Queries.Members;

public record MemberExistsByNameAndAddressQuery(
    string FirstName,
    string LastName,
    string Street,
    string HouseNumber,
    string City,
    string ZipCode,
    string Country,
    Guid? ExcludeId = null) : IRequest<bool>;

public class MemberExistsByNameAndAddress : IRequestHandler<MemberExistsByNameAndAddressQuery, bool>
{
    private readonly HaSpManContext _dbContext;

    public MemberExistsByNameAndAddress(IDbContextFactory<HaSpManContext> dbContextFactory)
    {
        _dbContext = dbContextFactory.CreateDbContext();
    }

    public Task<bool> Handle(MemberExistsByNameAndAddressQuery request, CancellationToken cancellationToken)
    {
        var queryPredicate = PredicateBuilder.New<Member>(m =>
            m.FirstName == request.FirstName
            && m.LastName == request.LastName
            && m.Address.Street == request.Street
            && m.Address.HouseNumber == request.HouseNumber
            && m.Address.City == request.City
            && m.Address.ZipCode == request.ZipCode
            && m.Address.ZipCode == request.ZipCode
            && m.Address.Country == request.Country);

        if (request.ExcludeId is not null)
        {
            queryPredicate.And(m => m.Id != request.ExcludeId);
        }

        return _dbContext.Members
            .AsNoTracking()
            .AnyAsync(queryPredicate, cancellationToken);
    }
}
