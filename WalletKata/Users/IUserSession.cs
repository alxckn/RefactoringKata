using System;
namespace WalletKata.Users
{
    public interface IUserSession
    {
        User GetLoggedUser();
    }
}
