﻿using Microsoft.EntityFrameworkCore;
using Ordering.Core.Domain.Models;

namespace Ordering.Core.Application.Data;

public interface IApplicationDbContext
{
    public DbSet<Customer> Customers { get;}
    public DbSet<Product> Products { get;}
    public DbSet<Order> Orders { get;}
    public DbSet<OrderItem> OrderItems { get;}

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}