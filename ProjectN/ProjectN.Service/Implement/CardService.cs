using AutoMapper;
using ProjectN.Service.Dtos.CardResultModel;
using ProjectN.Service.Dtos.Condition;
using ProjectN.Service.Dtos.Info;
using ProjectN.Service.Interface;

namespace ProjectN.Service.Implement
{
    public class CardService : ICardService
    {
        private readonly IMapper _mapper;
        private readonly ICardRepository _cardRepository;

        /// <summary>
        /// 建構式
        /// </summary>
        public CardService(ICardRepository cardRepository, IMapper mapper)
        {
            _cardRepository = cardRepository;
            _mapper = mapper;
        }

        public bool Delete(int id)
        {
            return _cardRepository.Delete(id);
        }

        public CardResultModel Get(int id)
        {
            CardResultModel result = new CardResultModel();
            var data = _cardRepository.Get(id);
            _mapper.Map(data, result);
            return result;
        }

        public IEnumerable<CardResultModel> GetList()
        {
            var data = _cardRepository.GetList().ToList();
            List<CardResultModel> temp = new List<CardResultModel>();
            _mapper.Map(data, temp);
            IEnumerable<CardResultModel> result = temp;
            return result;
        }

        public int Insert(CardInfo info)
        {
            CardCondition insertInfo = new CardCondition();
            _mapper.Map(info, insertInfo);
            var cardData = _cardRepository.Insert(insertInfo);
            return cardData;
        }

        public bool Update(int id, CardInfo info)
        {
            CardCondition insertInfo = new CardCondition();
            _mapper.Map(info, insertInfo);
            var cardData = _cardRepository.Update(id, insertInfo);
            return cardData;
        }
    }
}
