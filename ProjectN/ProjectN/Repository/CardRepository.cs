using ProjectN.Model;
using System.Data;
using Dapper;
using ProjectN.Parameter;

namespace ProjectN.Repository
{
    /// <summary>
    /// 卡片資料操作
    /// </summary>
    public class CardRepository
    {
        private readonly IDbConnection _conn;
        public CardRepository(IDbConnection configuration)
        {
            _conn = configuration;
        }
        /// <summary>
        /// 查詢卡片列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Card> GetList()
        {
            string sql = "SELECT * FROM Card";
            var result = _conn.Query<Card>(sql);
            return result;
        }

        /// <summary>
        /// 查詢卡片
        /// </summary>
        /// <returns></returns>
        public Card Get(int id)
        {
            string query = "SELECT TOP 1 * FROM Card Where Id = @id";
            var result = _conn.QueryFirstOrDefault<Card>(
                query,
                new
                {
                    Id = id,
                });
            return result;
        }
        /// <summary>
        /// 新增卡片
        /// </summary>
        /// <param name="parameter">參數</param>
        /// <returns></returns>
        public int Create(CardParameter parameter)
        {
            var sql = @"
        INSERT INTO Card 
        (
            [Name]
           ,[Description]
           ,[Attack]
           ,[Health]
           ,[Cost]
        ) 
        VALUES 
        (
            @Name
           ,@Description
           ,@Attack
           ,@Health
           ,@Cost
        );
        
        SELECT @@IDENTITY;
    ";
            var result = _conn.QueryFirstOrDefault<int>(sql, parameter);
            return result;

        }

        /// <summary>
        /// 修改卡片
        /// </summary>
        /// <param name="id">卡片編號</param>
        /// <param name="parameter">參數</param>
        /// <returns></returns>
        public bool Update(int id, CardParameter parameter)
        {
            var sql =
                @"
        UPDATE Card
        SET 
             [Name] = @Name
            ,[Description] = @Description
            ,[Attack] = @Attack
            ,[Health] = @Health
            ,[Cost] = @Cost
        WHERE 
            Id = @id
    ";
            var result = _conn.Execute(sql, new
            {
                id,
                parameter.Attack,
                parameter.Name,
                parameter.Description,
                parameter.Health,
                parameter.Cost
            });

            return result > 0;
        }
        /// <summary>
        /// 刪除卡片
        /// </summary>
        /// <param name="id">卡片編號</param>
        /// <returns></returns>
        public void Delete(int id)
        {
            var sql =
                @"
        DELETE FROM Card
        WHERE Id = @Id
    ";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, System.Data.DbType.Int32);
            var result = _conn.Execute(sql, parameters);

        }
    }
}
