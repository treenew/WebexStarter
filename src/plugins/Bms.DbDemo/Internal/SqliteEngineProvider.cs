using System;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq.Expressions;
using System.Text;
using Aoite.Data.Linq;
using Aoite.Data.Linq.Expressions;

namespace Aoite.Data
{
    /// <summary>
    /// 表示一个基于 Sqlite 数据源查询与交互引擎的提供程序。
    /// </summary>
    [DbProvider("sqlite")]
    public class SqliteEngineProvider : SqlEngineProvider
    {
        /// <summary>
        /// 获取用于创建提供程序对数据源类的实现的实例。
        /// </summary>
        public override DbProviderFactory ProviderFactory { get { return SQLiteFactory.Instance; } }
        /// <summary>
        /// 获取 SQL 语法提供程序的实例。
        /// </summary>
        public override ISyntaxProvider SyntaxProvider => SqliteSyntaxProvider.Instance;

        /// <summary>
        /// 获取一个值，表示当前数据提供程序的名称。
        /// </summary>
        public override string Name { get { return "sqlite"; } }

        /// <summary>
        /// 指定数据库的连接字符串，初始化一个 <see cref="SqliteEngineProvider"/> 类的新实例。
        /// </summary>
        /// <param name="connectionString">数据源的连接字符串。</param>
        public SqliteEngineProvider(string connectionString) : base(connectionString) { }

        /// <summary>
        /// 创建新数据源。
        /// </summary>
        public static void CreateDatabase(string file)
        {
            SQLiteConnection.CreateFile(file);
        }
    }
    class SqliteSyntaxProvider : SqlSyntaxProvider
    {
        public override string MethodNameLongCount => this.MethodNameCount;
        new public readonly static SqlSyntaxProvider Instance = new SqliteSyntaxProvider();
        public override bool SupportSelectTop => false;

        public override string KeyDelete => base.KeyDelete + " FROM";

        protected SqliteSyntaxProvider()
        {
            this.ExpressionVisitor = new SqliteExpressionVisitor(this);
        }

        protected override string OnCreateSkipTakeCommand(int skipCount, int takeCount, string commandText, bool checkOrderBy)
        {
            return commandText + $" LIMIT {skipCount},{takeCount}";
        }
    }

    class SqliteExpressionVisitor : SqlExpressionVisitor
    {
        public SqliteExpressionVisitor(ISyntaxProvider syntaxProvider) : base(syntaxProvider) { }

        protected override string AnalysisTableIdentifier(VisitContext context, Expression node, int index)
        {
            var isUploadOrDelete = context.SqlNode.SyntaxType == SqlSyntaxType.Update || context.SqlNode.SyntaxType == SqlSyntaxType.Delete;
            if(isUploadOrDelete)
            {
                if(node is SqlExpression sn && sn.From.Metadate is Linq.Metadates.ITableMetadate tm)
                {
                    return tm.TableName;
                }
                return string.Empty;
            }
            return base.AnalysisTableIdentifier(context, node, index);
        }

        protected override void AnalysisFrom(VisitContext context, SqlExpression node, StringBuilder builder)
        {
            if(node.SyntaxType == SqlSyntaxType.Retrieve) base.AnalysisFrom(context, node, builder);
        }
    }
}
