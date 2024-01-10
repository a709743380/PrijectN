using AutoMapper;
using ProjectN.Service.Dtos.CardResultModel;
using ProjectN.Service.Dtos.Condition;
using ProjectN.Service.Dtos.DataModel;
using ProjectN.Service.Dtos.Info;
using ProjectN.Dtos.Parameter;
namespace ProjectN.Service.Mappings
{
    public class ServiceMappings : Profile
    {
        public ServiceMappings()
        {
            // Info -> Condition
            this.CreateMap<CardInfo, CardCondition>();
            this.CreateMap<CardSearchInfo, CardSearchCondition>();
            this.CreateMap<CardDataModel, CardResultModel>();

            this.CreateMap<CardParameter, CardInfo>();
            // DataModel -> ResultModel
            //this.CreateMap<List<CardDataModel>, List<CardResultModel>>();
        }
    }
}
