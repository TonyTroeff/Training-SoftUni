using BlackFriday.Models.Contracts;
using BlackFriday.Utilities.Messages;

namespace BlackFriday.Models
{
    public abstract class User : IUser
    {
        private string userName;
        private string email;

        public string UserName
        {
            get => this.userName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(ExceptionMessages.UserNameRequired);

                this.userName = value;
            }
        }
        public bool HasDataAccess { get; }
        public string Email
        {
            get => this.email;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception(ExceptionMessages.EmailRequired);

                this.email = value;
            }
        }

        protected User(string userName, string email, bool hasDataAccess)
        {
            this.UserName = userName;
            this.Email = hasDataAccess ? "hidden" : email;
            this.HasDataAccess = hasDataAccess;
        }

        public override string ToString()
            => $"{this.UserName} - Status: {this.GetType().Name}, Contact Info: {this.Email}";
    }
}
