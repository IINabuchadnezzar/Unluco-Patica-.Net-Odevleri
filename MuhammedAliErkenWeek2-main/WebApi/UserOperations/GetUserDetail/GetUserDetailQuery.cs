using System;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.UserOperations.GetUserDetail
{
    public class GetUserDetailQuery
    {
        private readonly BookShopDB _dbContext;
        private readonly IMapper _mapper;
        public int UserId { get; set; }

        public GetUserDetailQuery(BookShopDB dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public UserDetailViewModel Handle()
        {
            var user = _dbContext.Users.Where(u => u.Id == UserId).SingleOrDefault();
            if (user is null)
                throw new InvalidOperationException("Kullanıcı bulunamadı");

            UserDetailViewModel vm = _mapper.Map<UserDetailViewModel>(user);

            return vm;
        }
    }

    public class UserDetailViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string UserRole { get; set; }
    }
}