using QLMP.Common.DAL;
using QLMP.Common.Rsp;
using QLMP.DAL.Models;
using System.Linq;

namespace QLMP.DAL
{
    public class UserRep : GenericRep<QuanLyMyPhamContext, User1>
    {
        public override User1 Read(int id)
        {
            var res = All.FirstOrDefault(u => u.Id == id);
            return res;
        }

        public User1 Read(string username)
        {
            var res = All.FirstOrDefault(u => u.UserName == username);
            return res;
        }

        public SingleRsp CreateUser(User1 user)
        {
            var res = new SingleRsp();
            using (var context = new QuanLyMyPhamContext())
            {
                using (var tran = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Users1.Add(user);
                        context.SaveChanges();
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        res.SetError(ex.StackTrace);
                    }
                }
            }
            return res;
        }
    }
}
