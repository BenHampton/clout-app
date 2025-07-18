using clout_api.Data.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace clout_api.Data.Models.Base;

public abstract class AuditRecordBase : ModelBase
{
    public int ReferenceId { get; set; }

    public static void BuildAuditBaseEntity<T>(EntityTypeBuilder<T> entity) where T : AuditRecordBase
    {
        entity.Property(e => e.ReferenceId)
            .HasColumnName("reference_id");
    }
}
