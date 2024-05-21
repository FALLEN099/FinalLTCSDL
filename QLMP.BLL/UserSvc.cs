using QLMP.Common.Rsp;
using QLMP.DAL;
using QLMP.DAL.Models;
using QLMP.Common.Req;
using QLMP.Common.BLL;

namespace QLMP.BLL
{
    public class UserSvc : GenericSvc<UserRep, User>
    {
        private UserRep userRep;

        public UserSvc()
        {
            userRep = new UserRep();
        }

        public SingleRsp CreateUser(UserReq userReq)
        {
            var res = new SingleRsp();
            var user = new User
            {
                UserName = userReq.UserName,
                PassWord = userReq.PassWord,
                FullName = userReq.FullName,
                Email = userReq.Email,
                Phone = userReq.Phone,
                Address = userReq.Address,
                Role = "customer" // Assigning default role as customer
            };

            return res = userRep.CreateUser(user);
        }

        public SingleRsp AuthenticateUser(LoginReq loginReq)
        {
            var res = new SingleRsp();
            var user = userRep.Read(loginReq.UserName);

            if (user == null || user.PassWord != loginReq.Password)
            {
                res.SetError("Invalid username or password.");
            }
            else
            {
                res.Data = user;
            }

            return res;
        }
    }
}
