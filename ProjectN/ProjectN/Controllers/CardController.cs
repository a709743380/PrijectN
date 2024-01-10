using Microsoft.AspNetCore.Mvc;
using ProjectN.Parameter;
using ProjectN.Service.Interface;
using AutoMapper;
using ProjectN.Service.Dtos.Info;
using ProjectN.Service.Dtos.CardResultModel;
using ProjectN.Validator;

namespace ProjectN.Controllers
{
    /// <summary>
    /// CardController
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class CardController : ControllerBase
    {
        private readonly IMapper _mapper;
        /// <summary>
        /// 卡片資料操作
        /// </summary>
        private readonly ICardService _cardService;

        /// <summary>
        /// 建構式
        /// </summary>
        public CardController(ICardService cardService, IMapper mapper)
        {
            _cardService = cardService;
            _mapper= mapper;
        }
        ///// <summary>
        ///// 測試用的資料集合
        ///// </summary>
        //private static List<Card> _cards = new List<Card>();

        /// <summary>
        /// 查詢卡片列表
        /// </summary>
        /// <remarks>remarks查詢卡片列表</remarks>
        /// <returns></returns>
        [HttpGet]
        //加入回傳格式
        [Produces("application/json")]
        public List<CardResultModel> GetList()
        {
            var cardList = _cardService.GetList();
            return cardList.ToList();
        }

        /// <summary>
        /// 查詢卡片
        /// </summary>
        /// <remarks>我是附加說明</remarks>
        /// <param name="id">卡片編號</param>
        /// <returns></returns>
        /// <response code="200">回傳對應的卡片</response>
        /// <response code="404">找不到該編號的卡片</response>          
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CardResultModel), 200)]
        [Route("{id}")]
        public CardResultModel? Get([FromRoute] int id)
        {
            var result = _cardService?.Get(id);

            if (result is null)
            {
                Response.StatusCode = 404;
            }

            return result;
        }

        /// <summary>
        /// 新增卡片
        /// </summary>
        /// <param name="parameter">卡片參數</param>
        /// <response code="200">新增成功</response>
        /// <response code="500">新增失敗</response>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Insert([FromBody] CardParameter parameter)
        {
            CardInfo info = new();
            _mapper.Map(parameter, info);
            var result = _cardService.Insert(info);
            if (result > 0)
            {
                return Ok();
            }

            return StatusCode(500);
        }

        /// <summary>
        /// 更新卡片
        /// </summary>
        /// <param name="id">卡片編號</param>
        /// <param name="parameter">卡片參數</param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public IActionResult Update(
            [FromRoute] int id,
            [FromBody] CardParameter parameter)
        {
            var targetCard = _cardService?.Get(id);
            if (targetCard is null)
            {
                return NotFound();
            }

            // 這邊需要對參數做檢查
            var validator = new CardParameterValidator();
            var validationResult = validator.Validate(parameter);

            // 如果沒有通過檢查，就把訊息串一串丟回去
            if (validationResult.IsValid is false)
            {
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage);
                var resultMessage = string.Join(",", errorMessages);
                return BadRequest(resultMessage); // 直接回傳 400 + 錯誤訊息
            }

            CardInfo info = new();
            _mapper.Map(parameter, info);
            var isUpdateSuccess = _cardService?.Update(id, info);
            if (isUpdateSuccess is true)
            {
                return Ok();
            }

            return StatusCode(500);
        }

        /// <summary>
        /// 刪除卡片
        /// </summary>
        /// <param name="id">卡片編號</param>
        /// <returns>什麼</returns>
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            _cardService.Delete(id);
            return Ok();
        }
    }
}
