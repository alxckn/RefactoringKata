using NUnit.Framework;
using System.Collections.Generic;
using WalletKata.Wallets;
using WalletKata.Users;
using WalletKata.Exceptions;
using Moq;

namespace WalletKata.Test
{
    public class WalletServiceTest
    {
        private IUserSession GetMockedSession(bool withUser)
        {
            User loggedUser = withUser ? new User() : null;
            var session = new Mock<IUserSession>();
            session.Setup(s => s.GetLoggedUser()).Returns(loggedUser);
            return session.Object;
        }

        [Test]
        public void GetWalletsByUser_LoggedUserNotFriend()
        {
            WalletService service = new WalletService(this.GetMockedSession(true));
            Assert.AreEqual(new List<Wallet>(), service.GetWalletsByUser(new User()));
        }

        [Test]
        public void GetWalletsByUser_NoLoggedUser()
        {
            WalletService service = new WalletService(this.GetMockedSession(false));
            Assert.Throws<UserNotLoggedInException>(() => service.GetWalletsByUser(new User()));
        }

        [Test]
        public void GetWalletsByUser_NullFriend()
        {
            WalletService service = new WalletService(this.GetMockedSession(true));
            service.GetWalletsByUser(null);
        }

        [Test]
        public void GetWalletsByUser_HasFriend()
        {
            IUserSession session = this.GetMockedSession(true);
            User loggedUser = session.GetLoggedUser();
            User friend = new User();
            friend.AddFriend(loggedUser);
            WalletService service = new WalletService(session);
            Assert.Throws<ThisIsAStubException>(() => service.GetWalletsByUser(friend));
        }
    }
}
