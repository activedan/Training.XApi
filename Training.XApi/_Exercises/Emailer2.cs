using System;

namespace CodeExamples.S2
{
    public class EmailerSecond
    {
        private readonly UserRepository repo;
        private readonly IMailSender sender;

        public EmailerSecond(UserRepository repo, IMailSender sender)
        {
            this.repo = repo;
            this.sender = sender;
        }

        public void Send(Email email, UserId toUser)
        {
            var userDetails = repo.UserDetails(toUser);
            sender.SendMessage(userDetails.Email, email);
        }
    }

    public interface IMailSender
    {
        void SendMessage(string address, Email message);
    }

    public class UserRepository
    {
        public UserDetails UserDetails(UserId toUser)
        {
            // Pretend this works
            throw new NotImplementedException();
        }
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
    }
}