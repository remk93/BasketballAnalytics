﻿using BA.Core.Queries;
using System.Linq.Expressions;

namespace BA.Core.Commands.File;

public class GetQuery : BaseQuery<Domain.Entities.File>
{
    public int Id { get; set; }

    public override Expression<Func<Domain.Entities.File, bool>> GetExpression()
    {
        return filter => filter.Id == Id;
    }
}