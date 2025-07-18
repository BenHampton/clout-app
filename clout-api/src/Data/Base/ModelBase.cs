using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace clout_api.Data.Base;

public abstract class ModelBase
{
    public DateTimeOffset CreatedOn { get; set; }
    public int CreatedBy { get; set; }
    public DateTimeOffset UpdatedOn { get; set; }
    public int UpdatedBy { get; set; }

    protected abstract Action<ModelBuilder> OnBuildAction { get; }


    public virtual void OnModelCreating(ModelBuilder modelBuilder)
    {
        if (OnBuildAction is null)
        {
            throw new ValidationException("Model Builder must be set in the derived class");
        }

        OnBuildAction.Invoke(modelBuilder);
    }

    public static void BuildModelBaseEntity<T>(EntityTypeBuilder<T> entity) where T : ModelBase
    {
        entity.Property(e => e.UpdatedBy)
            .HasColumnName("updated_by");

        entity.Property(e => e.UpdatedOn)
            .HasDefaultValueSql("now()")
            .HasColumnType("timestamp with time zone")
            .HasColumnName("updated_on");

        entity.Property(e => e.CreatedBy)
            .HasColumnName("created_by");

        entity.Property(e => e.CreatedOn)
            .HasDefaultValueSql("now()")
            .HasColumnType("timestamp with time zone")
            .HasColumnName("created_on");
    }
}
