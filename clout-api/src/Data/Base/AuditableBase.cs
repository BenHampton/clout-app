using System.ComponentModel.DataAnnotations;
using AutoMapper;
using clout_api.Data.Base;

namespace clout_api.Data.Models.Base;

public abstract class AuditableBase : ModelBase
{
    // This simply invokes the mapper in a single location just to make writing
    // models a little bit easier. We still have to implement OnModelUpdate, but
    // that should mostly be a copy/paste exercise
    public virtual TAudit GetAuditEntity<TParent, TAudit>(TParent parent, IMapper mapper)
        => mapper.Map<TParent, TAudit>(parent);

    public abstract void OnModelUpdate(PostgresContext context, IMapper mapper);
}
