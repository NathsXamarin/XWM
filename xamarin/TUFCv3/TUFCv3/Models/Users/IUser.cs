using System.ComponentModel;

namespace TUFCv3.Models.Users
{
    public interface IUser
    {
        string City { get; set; }
        string Country { get; set; }
        string CreateDate { get; set; }
        string Email { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Number { get; set; }
        string Password { get; set; }
        string PhoneHome { get; set; }
        string PhoneMobile { get; set; }
        string PhoneWork { get; set; }
        string State { get; set; }
        string Street { get; set; }
        long UserId { get; set; }
        string Zip { get; set; }

        event PropertyChangedEventHandler PropertyChanged;
    }
}