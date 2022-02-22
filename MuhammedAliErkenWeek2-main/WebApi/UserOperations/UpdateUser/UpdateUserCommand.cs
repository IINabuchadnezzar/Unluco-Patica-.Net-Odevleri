using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.UserOperations.UpdateUser
{
    public class UpdateUserCommand
    {
        private readonly BookShopDB _dbContext;
        public int UserId { get; set; }
        public UpdateUserModel Model { get; set; }

        public UpdateUserCommand(BookShopDB dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var user = _dbContext.Users.SingleOrDefault(u => u.Id == UserId);
            if (user is null)
                throw new InvalidOperationException("kullanıcı bulunamadı");

            user.Email = Model.Email != default ? Model.Email : user.Email;
            user.FirstName = Model.FirstName != default ? Model.FirstName : user.FirstName;
            user.LastName = Model.LastName != default ? Model.LastName : user.LastName;

            _dbContext.SaveChanges();
        }
    }

    public class UpdateUserModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}