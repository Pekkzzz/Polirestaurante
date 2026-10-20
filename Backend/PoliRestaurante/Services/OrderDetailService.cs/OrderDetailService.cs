using PoliRestaurante.Models.Entity;

public class OrderDetailService : IOrderDetailService
{
    private readonly IOrderDetailRepository _repository;

    //Services
    private readonly IProductService _productService;
    private readonly IOrderService _orderService;

    public OrderDetailService(IOrderDetailRepository Repository, 
    IProductService productService, IOrderService orderService)
    {
        _repository = Repository;
        _productService = productService;
        _orderService = orderService;
    }

    public OrderDetailService_Response Create(OrderDetail_CreateDto order_CreateDto)
    {
        OrderDetailService_Response response = new OrderDetailService_Response();
        OrderDetail newOrderDetail = OrdersMapper.OrderDetailToEnity(order_CreateDto);

        response = ComprobateOrderDetail(newOrderDetail);
        if (!response.Response)
        {
            return response;
        }

        ProductService_Response productService_Response = _productService.GetByID(newOrderDetail.ProductId);
        if (!productService_Response.Response)
        {
            response.Response = false;
            response.Description = productService_Response.Description;
            response.OrderDetail = null;
        }

        OrderService_Response orderService_Response = _orderService.GetByID(newOrderDetail.OrderId);
        if (!orderService_Response.Response)
        {
            response.Response = false;
            response.Description = orderService_Response.Description;
            response.OrderDetail = null;
        }

        return response;
        

    }

    public OrderDetailService_Response GetByID(int id)
    {
        OrderDetailService_Response response = new OrderDetailService_Response();

        if (id <= 0)
        {
            response.Description = "OrderDetail id cant be negative or 0";
            return response;
        }

        OrderDetail? OrderDetail = _repository.Get(id);
        if (OrderDetail == null)
        {
            response.Description = "OrderDetail id cant be finded";
            return response;
        }
        else
        {
            response.Response = true;
            response.Description = "OrderDetail Finded";
            response.OrderDetail = OrderDetail;
            return response;
        }
    }

    public OrderDetailService_Response Update(OrderDetail_UpdateDto order_UpdateDto)
    {
        OrderDetailService_Response response = new OrderDetailService_Response();
        if (!_repository.Exists(order_UpdateDto.Id))
        {
            response.Response = false;
            response.Description = "Product id cant be finded";
            response.OrderDetail = null;
            return response;
        }

        OrderDetail orderDetail = OrdersMapper.OrderDetailToEnity(order_UpdateDto);
        if (!_repository.Update(orderDetail))
        {
            response.Response = false;
            response.Description = "Order cant be updated by a system error";
            response.OrderDetail = null;
            return response;
        }

        response.Response = true;
        response.Description = "Order updated";
        response.OrderDetail = orderDetail;
        return response;
    }

    public OrderDetailService_Response Detele(int id)
    {
        OrderDetailService_Response OrderDetailService_Response = GetByID(id);
        if (!OrderDetailService_Response.Response)
        {
            return OrderDetailService_Response;
        }
        if (OrderDetailService_Response.OrderDetail == null)
        {
            OrderDetailService_Response.Response = false;
            OrderDetailService_Response.Description = "Something is wrong";
            OrderDetailService_Response.OrderDetail = null;
            return OrderDetailService_Response;
        }
        else
        {
            OrderDetailService_Response.Response = _repository.Delete(OrderDetailService_Response.OrderDetail);
            if (!OrderDetailService_Response.Response)
            {
                OrderDetailService_Response.Description = "User Cant Be Deleted";
                OrderDetailService_Response.OrderDetail = null;
                return OrderDetailService_Response;
            }
            else
            {
                return OrderDetailService_Response;
            }
            
        }

        
    }

    //----Utility----
    private OrderDetailService_Response ComprobateOrderDetail(OrderDetail newOrderDetail)
    {
        
        return new OrderDetailService_Response(true, "User Role is available", null);
    }
}