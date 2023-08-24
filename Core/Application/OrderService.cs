﻿using Core.Domain;
using Core.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Core.Application;

public class OrderService : IOrderService
{
    IProductRepository _productRepo;
    IOrderRepository _orderRepo;
    IUserRepository _userRepo;
    public OrderService(IProductRepository productRepo, IOrderRepository orderRepo, IUserRepository userRepo)
    {
        _productRepo = productRepo;
        _orderRepo = orderRepo;
        _userRepo = userRepo;
    }


    public async Task<bool> Buy(long userId, long productId)
    {
        var product = await _productRepo.GetByIdAsync(productId);
        if (product == null)
            throw new Exception("product not found");

        var user = await _userRepo.GetByIdAsync(userId);
        if (user == null)
            throw new Exception("user not found");


        using var transaction = await _orderRepo.BeginTransactionAsync();
        try
        {
            var newOrder = new Order() { BuyerId = userId, ProductId = productId };
            await _orderRepo.CreateNew(newOrder);

            product.InventoryCount--;

            await _orderRepo.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
        }



        return true;
    }


    public async Task<List<Order>> ListAsync(int pageNumber, int pagesize)
    {
        pagesize = pagesize < 10 ? 10 : pagesize;

        return await _orderRepo.PageAsync(pageNumber, pagesize)
            .ToListAsync();
    }

}
