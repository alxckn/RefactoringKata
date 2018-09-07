using System.Collections.Generic;
using WalletKata.Users;
using WalletKata.Exceptions;

namespace WalletKata.Wallets
{
    public class WalletService
    {
		private readonly IUserSession userSession;

        public WalletService(IUserSession userSession)
		{
			this.userSession = userSession;
		}

        public List<Wallet> GetWalletsByUser(User user)
        {
            User loggedUser = this.userSession.GetLoggedUser();
			if (loggedUser == null) {
				throw new UserNotLoggedInException();
			}

			return user != null && user.IsFriendWith(loggedUser) ? 
				       WalletDAO.FindWalletsByUser(user) : 
				       new List<Wallet>();
        }         
    }
}