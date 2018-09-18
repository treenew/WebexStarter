using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bms.Sys.Apis
{
    public class RoleApi
    {
        private static List<BmsRole> Roles;

        static RoleApi()
        {
            Roles = new List<BmsRole>(GA.CreateMockModels<BmsRole>(15)).OrderBy(r => r.Id).ToList();
        }

        public Task<IResult<PageData>> FindAllAsync(FindAllArguments args)
        {
            IEnumerable<BmsRole> table = Roles;

            table = table.Where(t => t.Status != ComStatus.Deleted);
            if(args.NameLike.IsNotNull()) table = table.Where(t => t.Name.Contains(args.NameLike));

            table = table.OrderBy(r => r.Id).Skip((args.PageNumber - 1) * args.PageSize).Take(args.PageSize);

            var value = PageData.Create(Roles.Count(r => r.Status != ComStatus.Deleted), table.Select(r => new { r.Id, r.Name, r.Status }).ToArray());
            return Task.FromResult(Result.Success<PageData>(value));
        }

        public IResult ModifyStatus(string id, ComStatus status)
        {
            var user = Roles.FirstOrDefault(r => r.Id.iEquals(id));
            if(user != null) user.Status = status;
            return Result.Success();
        }

        public IResult<BmsRole> Find(string id)
        {
            return Result.Success(Roles.FirstOrDefault(r => r.Id.iEquals(id)));
        }

        public IResult Manage(BmsRole args)
        {
            if(args.Id.IsNull())
            {
                args.Id = Snowflake<BmsRole>.NextIdString();
                Roles.Add(args);
            }
            else
            {
                var user = Roles.FirstOrDefault(r => r.Id.iEquals(args.Id));
                if(user != null) args.CopyTo(user);
            }
            Roles = Roles.OrderBy(r => r.Id).ToList();
            return Result.Success();
        }
    }

    public class FindAllArguments : PageParameters
    {
        public string NameLike { get; set; }
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

    /// <summary>
    /// 表示一个平台角色。
    /// </summary>
    public partial class BmsRole
    {

        /// <summary>
        /// 设置或获取一个值，表示主键。
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 设置或获取一个值，表示角色名称。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 设置或获取一个值，表示状态（-1删除，0禁用，1启用）。
        /// </summary>
        public ComStatus Status { get; set; }

        /// <summary>
        /// 设置或获取一个值，表示扩展数据。
        /// </summary>
        public string Exdata { get; set; }

    }
}
