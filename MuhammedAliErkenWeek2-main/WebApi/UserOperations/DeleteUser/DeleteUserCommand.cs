using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.UserOperations.DeleteUser
{
    public class DeleteUserCommand
    {
        private readonly BookShopDB _dbContext;
        public int UserId { get; set; }

        public DeleteUserCommand(BookShopDB dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var user = _dbContext.Users.SingleOrDefault(u => u.Id == UserId);
            if (user is null)
                throw new InvalidOperationException("Kullanıcı bulunamadı");

            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();
        }
    }
}