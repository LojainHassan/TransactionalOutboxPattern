using TransactionalOutboxPattern.Models.OutBox;

namespace TransactionalOutboxPattern.Contract.OutBox;

public interface IEmailOutbox
{
    Task<EmailOutbox> Add(EmailOutbox emailOutbox);
    Task<EmailOutbox> Update(EmailOutbox emailOutbox);
    IEnumerable<EmailOutbox> GetAll();
}