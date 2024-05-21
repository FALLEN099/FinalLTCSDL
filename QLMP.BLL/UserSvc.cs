using QLMP.Common.Rsp;
using QLMP.DAL;
using QLMP.DAL.Models;
using QLMP.Common.Req;
using QLMP.Common.BLL;

namespace QLMP.BLL
{
    public class UserSvc : GenericSvc<UserRep, User>
    {
        private readonly UserRep userRep;

        public UserSvc()
        {
            userRep = new UserRep();
        }
        public User GetUserById(int id)
        {
            return userRep.GetById(id);
        }
        public User GetUserByUserName(string username)
        {
            return userRep.GetByUserName(username);
        }
        public List<User> GetAllUsersExceptAdmin()
        {
            return userRep.GetAllUsersExceptAdmin();
        }
        public SingleRsp CreateUser(UserReq userReq)
        {
            var res = new SingleRsp();
            if (string.IsNullOrWhiteSpace(userReq.UserName))
            {
                res.SetError("Username cannot be empty or whitespace.");
                return res;
            }

            var existingUser = userRep.GetByUserName(userReq.UserName);

            if (existingUser != null)
            {
                res.SetError("Username already exists.");
                return res;
            }

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


        public SingleRsp UpdateUser(int id, UserReq userReq)
        {
            var res = new SingleRsp();
            var existingUser = userRep.GetById(id);
            if (existingUser == null)
            {
                res.SetError("User not found.");
                return res;
            }
            if (!string.IsNullOrEmpty(userReq.UserName) && string.IsNullOrWhiteSpace(userReq.UserName))
            {
                res.SetError("Username cannot be empty or whitespace.");
                return res;
            }
            if (userRep.ExistsUserName(userReq.UserName, id))
            {
                res.SetError("Username already exists.");
                return res;
            }

            existingUser.UserName = userReq.UserName ?? existingUser.UserName;
            if (!string.IsNullOrEmpty(userReq.PassWord))
            {
                existingUser.PassWord = userReq.PassWord;
            }
            existingUser.FullName = userReq.FullName ?? existingUser.FullName;
            existingUser.Email = userReq.Email ?? existingUser.Email;
            existingUser.Phone = userReq.Phone ?? existingUser.Phone;
            existingUser.Address = userReq.Address ?? existingUser.Address;

            return userRep.UpdateUser(existingUser);
        }

        public SingleRsp UpdateUserByUserName(string username, UserReq userReq)
        {
            var res = new SingleRsp();
            var existingUser = userRep.GetByUserName(username);
            if (existingUser == null)
            {
                res.SetError("User not found.");
                return res;
            }
            if (!string.IsNullOrEmpty(userReq.UserName) && string.IsNullOrWhiteSpace(userReq.UserName))
            {
                res.SetError("Username cannot be empty or whitespace.");
                return res;
            }

            if (userRep.ExistsUserName(userReq.UserName, existingUser.Id))
            {
                res.SetError("Username already exists.");
                return res;
            }

            existingUser.UserName = userReq.UserName ?? existingUser.UserName;
            if (!string.IsNullOrEmpty(userReq.PassWord))
            {
                existingUser.PassWord = userReq.PassWord;
            }
            existingUser.FullName = userReq.FullName ?? existingUser.FullName;
            existingUser.Email = userReq.Email ?? existingUser.Email;
            existingUser.Phone = userReq.Phone ?? existingUser.Phone;
            existingUser.Address = userReq.Address ?? existingUser.Address;

            return userRep.UpdateUser(existingUser);
        }

        public void DeleteUser(int id)
        {
            userRep.Delete(id);
        }
        public void DeleteUserByUserName(string username)
        {
            userRep.DeleteByUserName(username);
        }
        public SingleRsp UpdateUserRoleByUserName(string username, string role)
        {
            var res = new SingleRsp();
            var existingUser = userRep.GetByUserName(username);
            if (existingUser == null)
            {
                res.SetError("User not found.");
                return res;
            }

            existingUser.Role = role;
            return userRep.UpdateUser(existingUser);
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
