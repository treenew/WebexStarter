using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aoite.Data;
using Aoite.Web.Plugins;
using Microsoft.Extensions.Configuration;

namespace Bms.DbDemo.Apis
{
    public class TestTableApi
    {
        private readonly PluginProjectInfo _pluginProjectInfo;
        private readonly IDbEngine _engine;

        public TestTableApi(PluginProjectInfo pluginProjectInfo)
        {
            this._pluginProjectInfo = pluginProjectInfo;
            var dbPath = pluginProjectInfo.Metadate.GetValue<string>("dbPath");
            var dbFile = pluginProjectInfo.FolderProvider.GetFileInfo(dbPath);
            var provider = new SqliteEngineProvider("Data Source=" + dbFile.PhysicalPath);
            this._engine = new DbEngine(provider);
            if(!dbFile.Exists)
            {
                var dbFolder = Path.GetDirectoryName(dbFile.PhysicalPath);
                if(!Directory.Exists(dbFolder)) Directory.CreateDirectory(dbFolder);
                SqliteEngineProvider.CreateDatabase(dbFile.PhysicalPath);
                const string CREATE_SQL = "CREATE TABLE TestTable(Id nvarchar(255) PRIMARY KEY,Username nvarchar(255), Memo nvarchar(255))";
                const string INSERT_SQL = "INSERT INTO TestTable(Id,UserName) VALUES (@id, @username)";

                this._engine.Execute(CREATE_SQL).ToNonQuery();

                for(int i = 0; i < 15; i++)
                {
                    this._engine.Execute(INSERT_SQL, "@id", Snowflake<TestTableEntity>.NextIdString(), "@username", "user" + (i + 1)).ToNonQuery();
                }
            }
        }

        public async Task<IResult<PageData>> FindAllAsync(FindAllArguments args)
        {
            var table = this._engine.Table<TestTableEntity>();
            if(args.UsernameLike.IsNotNull()) table = table.Where(t => t.Username.Contains(args.UsernameLike));

            var query = from t in table
                        //orderby t.Id
                        select new
                        {
                            t.Id,
                            t.Username
                        };
            query = query.OrderByDescending(t => t.Id);
            return Result.Success(await query.ToPageAsync(args));
        }

        public async Task<IResult> RemoveAsync(string id)
        {
            await this._engine.Table<TestTableEntity>().Where(t => t.Id == id).RemoveAsync();
            return Result.Success();
        }

        public async Task<IResult<TestTableEntity>> FindAsync(string id)
        {
            return Result.Success(await this._engine.Table<TestTableEntity>().FirstOrDefaultAsync(t => t.Id == id));
        }

        public async Task<IResult> ManageAsync(TestTableEntity args)
        {
            if(args.Id.IsNull())
            {
                args.Id = Snowflake<TestTableEntity>.NextIdString();
                await this._engine.Table<TestTableEntity>().AddAsync(args);
            }
            else
            {
                await this._engine.Table<TestTableEntity>().ModifyAsync(args);
            }
            return Result.Success();
        }
    }

    public class FindAllArguments : PageParameters
    {
        public string UsernameLike { get; set; }
    }

    [System.ComponentModel.DataAnnotations.Schema.Table("TestTable")]
    public class TestTableEntity
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Memo { get; set; }
    }
}
