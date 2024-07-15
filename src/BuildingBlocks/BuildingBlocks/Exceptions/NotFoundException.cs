﻿namespace BuildingBlocks.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) :base(message)
    { }
    public NotFoundException(string entityName, object key) : base($"Entity \"{entityName}\" ({key}) Was not Found!")
    { }
}
