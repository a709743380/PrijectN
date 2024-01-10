using ProjectN.Service.Dtos.Condition;
using ProjectN.Service.Dtos.DataModel;
using ProjectN.Service.Interface;
using System.Data;
using Dapper;

namespace ProjectN.Repository.Implement
{
    public class CardRepository : ICardRepository
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
        public IEnumerable<CardDataModel> GetList()
        {
            string sql = "SELECT * FROM Card";
            var result = _conn.Query<CardDataModel>(sql);
            return result;
        }

        /// <summary>
        /// 查詢卡片
        /// </summary>
        /// <returns></returns>
        public CardDataModel Get(int id)
        {
            string query = "SELECT TOP 1 * FROM Card Where Id = @id";
            var result = _conn.QueryFirstOrDefault<CardDataModel>(
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
        public int Insert(CardCondition parameter)
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
        public bool Update(int id, CardCondition parameter)
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
        public bool Delete(int id)
        {
            var sql =
                @"
        DELETE FROM Card
        WHERE Id = @Id
    ";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, System.Data.DbType.Int32);
            var result = _conn.Execute(sql, parameters);
            return result > 0;
        }
    }
}
