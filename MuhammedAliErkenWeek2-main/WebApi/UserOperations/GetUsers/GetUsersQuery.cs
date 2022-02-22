using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.UserOperations.GetUsers
{
    public class GetUsersQuery
    {
        private readonly BookShopDB _dbContext;
        private readonly IMapper _mapper;

        public GetUsersQuery(BookShopDB dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<UserViewModel> Handle()
        {
            var userList = _dbContext.Users.OrderBy(u => u.Id).ToList<User>();
            List<UserViewModel> vm = _mapper.Map<List<UserViewModel>>(userList);
            
            
            return vm;
        }
    }

    public class UserViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string UserRole { get; set; }
    }
}