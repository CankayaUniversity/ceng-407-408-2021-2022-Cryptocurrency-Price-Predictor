using Application.Domain;
using Entities.User;
using AutoMapper;
using Shared.Entities.Common;
using Shared.Mapping;
using Entities.ModelPrediction;

namespace Application.Mapping
{
    public class MappingProfile:Profile,IMapFrom
    {
        public MappingProfile()
        {
            #region Login

            CreateMap<User, UserEntity>();
                
            CreateMap<User, CurrentUserEntity>();
            //CreateMap<User, CurrentUserEntity>()
            //    .ForMember(dest => dest.Id, mo => mo.MapFrom(x => x.Id))
            //    .ForMember(dest => dest.Email, mo => mo.MapFrom(x => x.Email))
            //    .ForMember(dest => dest.UserName, mo => mo.MapFrom(x => x.UserName));

            CreateMap<RegisterEntity, User>()
                .ForMember(dest => dest.Email, mo => mo.MapFrom(x => x.Email.Trim()))
                .ForMember(dest => dest.UserName, mo => mo.MapFrom(x => x.UserName.Trim()));

            CreateMap<CreateUserEntity, User>()
                .ForMember(dest => dest.Email, mo => mo.MapFrom(x => x.Email.Trim()))
                .ForMember(dest => dest.UserName, mo => mo.MapFrom(x => x.UserName.Trim()));

            #endregion

        }


        public int Order => 0;
    }
}
