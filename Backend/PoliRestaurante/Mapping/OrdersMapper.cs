using PoliRestaurante.Models;
using PoliRestaurante.Models.Entity;

public static class OrdersMapper
{
    public static Order OrderToEnity(Order_CreateDto order)
    {
        return new Order
        {
            OrderTypeId = order.OrderTypeId,
            TableId = order.TableId
        };
    }

    public static Order OrderToEnity(Order_UpdateDto order)
    {
        return new Order
        {
            Id = order.Id,
            OrderTypeId = order.OrderTypeId,
            TableId = order.TableId
        };
    }

    public static Order_Dto OrderToDto(Order order)
    {
        return new Order_Dto
        {
            Id = order.Id, 

            TotalPrice = order.TotalPrice,

            Payments = order.Payments,

            TotalBalance = order.TotalBalance,

            CreationDate = order.CreationDate,

            PaymentStatusId = order.PaymentStatusId,

            OrderStatusId = order.OrderStatusId,

            OrderTypeId = order.OrderTypeId,

            TableId = order.TableId
        };
    }

    public static OrderDetail OrderDetailToEnity(OrderDetail_CreateDto orderDetail)
    {
        return new OrderDetail
        {
            Description = orderDetail.Description,
            ProductId = orderDetail.ProductId,
            OrderId = orderDetail.OrderId,
        };
    }

    public static OrderDetail OrderDetailToEnity(OrderDetail_UpdateDto orderDetail)
    {
        return new OrderDetail
        {
            Id = orderDetail.Id,
            Description = orderDetail.Description,
            Observations = orderDetail.Observations,
            ProductId = orderDetail.ProductId,
            OrderId = orderDetail.OrderId,
            OrderDetailStatusId = orderDetail.OrderDetailStatusId
        };
    }


}