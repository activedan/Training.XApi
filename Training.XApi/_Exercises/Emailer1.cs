using System;
using System.Net.Mail;

namespace CodeExamples.S1
{
    public class EmailerFirst
    {
        private readonly IDbAccess repo;
        private SmtpClient client;

        public EmailerFirst(IDbAccess repo)
        {
            this.repo = repo;
            client = new SmtpClient { Host = "mail.carsales.com.au" };
        }

        public void Send(Email email, UserId toUser)
        {
            var userDetails = repo.Load<UserDetails>("select * from user_details where id = {0}", toUser);

            client.Send("people@carsales.com.au", userDetails.Email,
                email.Subject(userDetails),
                email.Body(userDetails));
        }
    }

    public interface IDbAccess
    {
        T Load<T>(string selectFromUserDetailsWhereId, UserId toUser);
    }

    public class UserDetails
    {
        public string Email { get; set; }
    }

    public class UserId
    {
    }

    public class Email
    {
        public string Subject(UserDetails userDetails)
        {
            throw new NotImplementedException();
        }

        public string Body(UserDetails userDetails)
        {
            throw new NotImplementedException();
        }
    }
}