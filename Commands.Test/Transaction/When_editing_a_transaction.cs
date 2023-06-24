using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Commands.Handlers.Transaction.EditTransaction;

using Domain;

using FluentAssertions;

using MediatR;

using Moq;

using Persistence.Repositories;

using Xunit;

using AttachmentFile = Commands.Handlers.AttachmentFile;

namespace Commands.Test.Transaction;
public class When_editing_a_transaction
{
    private readonly Mock<ITransactionRepository> _transactionRepositoryMock;

    public When_editing_a_transaction()
    {
        _transactionRepositoryMock = new Mock<ITransactionRepository>();
        var mediatorMock = new Mock<IMediator>();
        SUT = new EditTransactionHandler(_transactionRepositoryMock.Object, mediatorMock.Object);
    }

    public EditTransactionHandler SUT { get; set; }

    [Fact]
    public async Task It_should_throw_an_exception_when_locked()
    {
        var bankAccountId = Guid.NewGuid();
        var transaction = new CreditTransaction("Random counter party", bankAccountId, 20m, DateTimeOffset.Now,
            "A description", new List<TransactionAttachment>(), null, new List<TransactionTypeAmount>());
        transaction.Lock();
        _transactionRepositoryMock
            .Setup(x => x.GetByIdAsync(transaction.Id, CancellationToken.None))
            .ReturnsAsync(transaction);

        var exception = await Assert.ThrowsAsync<InvalidOperationException>(async () => await SUT.Handle(
            new EditTransactionCommand(transaction.Id, "Random counter party name", null, bankAccountId,
                DateTimeOffset.Now, "Another description", new List<TransactionTypeAmount>(),
                new List<AttachmentFile>()), CancellationToken.None));

        exception.Message.Should().Be("Transaction is locked");
    }
}
