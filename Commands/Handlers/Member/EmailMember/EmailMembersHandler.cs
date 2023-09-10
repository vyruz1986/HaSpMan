using Commands.Services;

using Microsoft.EntityFrameworkCore;

using Persistence;

namespace Commands.Handlers.Member.EmailMember;
internal class EmailMembersHandler : IRequestHandler<EmailMembersCommand, IEnumerable<SendError>>
{
    private readonly IMailingService _mailingService;
    private readonly HaSpManContext _dbContext;

    public EmailMembersHandler(IMailingService mailingService, HaSpManContext dbContext)
    {
        _mailingService = mailingService ?? throw new ArgumentNullException(nameof(mailingService));
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<IEnumerable<SendError>> Handle(EmailMembersCommand request, CancellationToken cancellationToken)
    {
        var members = await _dbContext.Members
            .AsNoTracking()
            .Where(m => request.MemberIds.Contains(m.Id) && !string.IsNullOrWhiteSpace(m.Email))
            .ToListAsync(cancellationToken: cancellationToken);

        var errors = await _mailingService.SendMailAsync(members, new(request.Subject, request.Message), cancellationToken);

        return errors;
    }
}
