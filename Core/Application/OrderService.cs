using Core.Domain;
using Core.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application;

public class OrderService
{
    ProductRepository _productRepo;
    OrderRepository _orderRepo;
    UserRepository _userRepo;
    public OrderService(ProductRepository productRepo, OrderRepository orderRepo, UserRepository userRepo)
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
