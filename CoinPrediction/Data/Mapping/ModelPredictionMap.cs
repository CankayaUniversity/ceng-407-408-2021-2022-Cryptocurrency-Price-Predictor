using Application.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Data.Mapping
{
    public class ModelPredictionMap : IEntityTypeConfiguration<ModelPrediction>
    {
        public void Configure(EntityTypeBuilder<ModelPrediction> builder)
        {
            builder.ToTable("ModelPrediction");

            builder.HasKey(x => x.Id);
        }
    }
}
