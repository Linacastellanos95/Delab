using Delab.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace Delab.AccessData.ModelConfig;

public class StateConfig : IEntityTypeConfiguration<State>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<State> builder)
    {
        builder.HasKey(e => e.StateId);
        builder.HasIndex(e => new { e.Name, e.CountryId }).IsUnique();
        //Protección de Borrado en Cascada
        builder.HasOne(e => e.Country).WithMany(e => e.States).OnDelete(DeleteBehavior.Restrict);
    }
}
