using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.CQRS
{
    //this is for the command which not returns anything
    public interface ICommand : ICommand<Unit>
    {

    }
    // this is for the command which returns response 
    public interface ICommand<out TResponse>:IRequest<TResponse>
    {

    }
}
