namespace TransactionalOutboxPattern.Contract.Email;

public interface IMailService
{
    bool Send(string sender, string subject, string body, bool isBodyHTML);

}
