using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.CQRS
{
    // This is for the query which always returns a result
    public interface IQuery<out TResponse>:IRequest<TResponse>
        where TResponse:notnull
    {

    }
}
