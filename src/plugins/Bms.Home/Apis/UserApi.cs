using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webex.Platform;

namespace Bms.Home.Apis
{
    public class UserApi
    {
        private static readonly IEnumerable<UserModel> SimpleDatas;
        static UserApi()
        {
            SimpleDatas = new List<UserModel>(GA.CreateMockModels<UserModel>(500));
        }

        private readonly IAppStorage _appStorage;

        public UserApi(IAppStorage appStorage)
        {
            this._appStorage = appStorage;
        }

        [NoAuth]
        public IResult Login(string username, string password)
        {
            if(username == "admin" && password == "123456".ToMd5())
            {
                this._appStorage.Identity = new BmsIdentity
                {
                    Id = Snowflake<BmsIdentity>.NextIdString(),
                    Nickname = "超级管理员",
                    Username = username
                };
                return Result.Success();
            }
            return Result.Fail("账号或密码错误。");
        }

        [NoAuth]
        public void Logout()
        {
            this._appStorage.Identity = null;
        }

        public IResult<IAppIdentity> GetIdentity()
        {
            return Result.Success(this._appStorage.Identity);
        }

        public Task<IResult<PageData<UserModel>>> FindAllAsync(FindAllArguments args)
        {
            var table = SimpleDatas;

            table = table.Where(t => t.Status != ComStatus.Deleted);
            if(args.NicknameLike.IsNotNull()) table = table.Where(t => t.Nickname.Contains(args.NicknameLike));
            if(args.UsernameLike.IsNotNull()) table = table.Where(t => t.Username.Contains(args.UsernameLike));

            table = table.OrderBy(r => r.CreateTime).Skip((args.PageNumber - 1) * args.PageSize).Take(args.PageSize);
            var value = new PageData<UserModel>(SimpleDatas.Count(), table.ToArray());

            return Task.FromResult(Result.Success(value));
        }
    }

    public class FindAllArguments : PageParameters
    {
        public string NicknameLike { get; set; }

        public string UsernameLike { get; set; }
    }

    public class UserModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Nickname { get; set; }
        public DateTime CreateTime { get; set; }
        public ComStatus Status { get; set; }
    }

    /// <summary>
    /// 定义实体的状态。
    /// </summary>
    public enum ComStatus
    {
        /// <summary>
        /// 表示实体已被逻辑删除。
        /// </summary>
        Deleted = -1,
        /// <summary>
        /// 表示实体已禁用。
        /// </summary>
        Disabled = 0,
        /// <summary>
        /// 表示实体已启用。
        /// </summary>
        Enabled = 1,
    }
}
