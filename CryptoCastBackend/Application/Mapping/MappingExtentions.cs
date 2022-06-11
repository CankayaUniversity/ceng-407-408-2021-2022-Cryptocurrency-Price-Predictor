using Application.Domain;
using Entities.ModelPrediction;
using Entities.User;
using Shared.Entities.Common;
using Shared.Mapping;

namespace Application.Mapping
{
    public static class MappingExtentions
    {   
        public static TDestination MapTo<TSource, TDestination>(this TSource source)
        {
            return MappingConfiguration.Mapper.Map<TSource, TDestination>(source);
        }

        #region Login

        public static CurrentUserEntity CurrentUserToModel(this User data)
        {
            return data.MapTo<User, CurrentUserEntity>();
        }

        public static User ToModel(this RegisterEntity data)
        {
            return data.MapTo<RegisterEntity, User>();
        }

        #endregion

        public static ModelPredictionEntity ToModel(this ModelPrediction data)
        {
            return data.MapTo<ModelPrediction, ModelPredictionEntity>();
        }

        public static ModelPrediction ToModel(this ModelPredictionEntity data)
        {
            return data.MapTo<ModelPredictionEntity, ModelPrediction>();
        }
    }
}
