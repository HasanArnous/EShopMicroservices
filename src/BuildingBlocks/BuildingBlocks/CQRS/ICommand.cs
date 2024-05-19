﻿using MediatR;

namespace BuildingBlocks.CQRS;

public interface ICommand:IRequest<Unit> { } // The Unit is a type from the MediatR that represent a void type
public interface ICommand<out TResponse> : IRequest<TResponse> { }