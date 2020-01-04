namespace SelfHostTest.API.Domain.Users
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string About { get; set; }

        protected bool Equals(User other)
        {
            return Id == other.Id && string.Equals(Username, other.Username) && string.Equals(Password, other.Password) && string.Equals(About, other.About);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((User) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id;
                hashCode = (hashCode * 397) ^ Username.GetHashCode();
                hashCode = (hashCode * 397) ^ Password.GetHashCode();
                hashCode = (hashCode * 397) ^ About.GetHashCode();
                return hashCode;
            }
        }
    }
}