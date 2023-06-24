namespace Domain.Interfaces;

public interface IMemberRepository
{
    Task<Member?> GetById(Guid id);
    Task<Member?> GetByEmail(string email);
    Task<IEnumerable<Member>> GetAllAsync();
    void Add(Member member);
    void Remove(Member member);
    Task Save(CancellationToken cancellationToken);
}
