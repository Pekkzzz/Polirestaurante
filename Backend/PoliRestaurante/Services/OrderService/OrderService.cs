using PoliRestaurante.Models;
using PoliRestaurante.Models.Entity;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repository;
    private readonly IOrderStatusRepository _orderStatusRepository;
    private readonly IPaymentStatusRepository _paymentStatusRepository;

    //Services
    private readonly IOrderTypeService _orderTypeService;
    private readonly IUserService _userService;
    private readonly IUserRoleService _userRoleService;
    private readonly ITableService _tableService;

    public OrderService(IOrderRepository Repository, IOrderStatusRepository orderStatusRepository,
    IPaymentStatusRepository paymentStatusRepository,
    IOrderTypeService orderTypeService, IUserService userService, IUserRoleService userRoleService, ITableService tableService)
    {
        _repository = Repository;
        _orderStatusRepository = orderStatusRepository;
        _paymentStatusRepository = paymentStatusRepository;
        _orderTypeService = orderTypeService;

        _orderTypeService = orderTypeService;
        _userService = userService;
        _userRoleService = userRoleService;

        _tableService = tableService;

    }

    public OrderService_Response Create(Order_CreateDto order_CreateDto)
    {
        OrderService_Response response = new OrderService_Response();
        Order newOrder = OrdersMapper.OrderToEnity(order_CreateDto);
        response = ComprobateOrder(newOrder);
        if (!response.Response)
        {
            return response;
        }

        newOrder.CreationDate = DateTime.Now;
        OrderType? oderType;
        OrderStatus? orderStatus;
        PaymentStatus? paymentStatus;

        if (order_CreateDto.OrderTypeId <= 0 )
        {
            if (!_orderTypeService.GetByName(DefaultConfiguration.DefaultOrderTypeName).Response)
            {
                response.Response = false;
                response.Description = $"Order Cant be created: OrderType {DefaultConfiguration.DefaultOrderTypeName} is Invalid";
                response.Order = null;
                return response;
            }
            oderType = _orderTypeService.GetByName(DefaultConfiguration.DefaultOrderTypeName).OrderType;
            
        }
        else
        {
            oderType = _orderTypeService.GetByID(order_CreateDto.OrderTypeId).OrderType;
        }
        
        
        if (!_orderStatusRepository.Exists(DefaultConfiguration.DefaultOrderStatusName))
        {
            response.Response = false;
            response.Description = "Order Cant be created: OrderStatus Invalid";
            response.Order = null;
            return response;
        }
        orderStatus = _orderStatusRepository.Get(DefaultConfiguration.DefaultOrderStatusName);
        
        if (!_paymentStatusRepository.Exists(DefaultConfiguration.DefaultPaymentStatusName))
        {
            response.Response = false;
            response.Description = "Order Cant be created: PaymentSTATUS Invalid";
            response.Order = null;
            return response;
        }
        paymentStatus = _paymentStatusRepository.Get(DefaultConfiguration.DefaultPaymentStatusName);

        if (oderType == null || orderStatus == null || paymentStatus == null)
        {
            response.Response = false;
            response.Description = "Order Cant be created: Any OrderType Or OrderStatus or PaymentStatus is null";
            response.Order = null;
            return response;
        }
        newOrder.OrderTypeId = oderType.Id;
        newOrder.OrderStatusId = orderStatus.Id;
        newOrder.PaymentStatusId = paymentStatus.Id;


        if (newOrder.TableId != null)
        {
            TableService_Response tableResponse = _tableService.GetByID((int)newOrder.TableId);
            if (!tableResponse.Response || tableResponse.Table == null)
            {
                response.Response = false;
                response.Description = "Order Cant be Created: Table ID dont exists";
                response.Order = null;
                return response;
            }
        }
        

        if (_repository.Create(newOrder))
        {
            response.Response = true;
            response.Description = "Order Created Succesfully";
            response.Order = newOrder;
            return response;
        }
        else
        {
            response.Response = false;
            response.Description = "Internal Error: Somethng is wrong when system try to create a new order";
            response.Order = null;
            return response;
        }
        

    }

    public OrderService_Response GetByID(int id)
    {
        OrderService_Response response = new OrderService_Response();

        if (id <= 0)
        {
            response.Description = "Order id cant be negative or 0";
            return response;
        }

        Order? Order = _repository.Get(id);
        if (Order == null)
        {
            response.Description = "Order id cant be finded";
            return response;
        }
        else
        {
            response.Response = true;
            response.Description = "Order Finded";
            response.Order = Order;
            return response;
        }
    }
    
    public OrderQueueService_Response GetQueue()
    {
        OrderQueueService_Response response = new OrderQueueService_Response();

        List<Order> orders = _repository.GetQueue();

        response.Response = true;
        response.Description = "Order queue retrieved successfully";
        response.Orders = orders;

        return response;
    }

    public OrderService_Response Update(Order_UpdateDto order_UpdateDto)
    {
        OrderService_Response response = new OrderService_Response();
        if (!_repository.Exists(order_UpdateDto.Id))
        {
            response.Response = false;
            response.Description = "Product id cant be finded";
            response.Order = null;
            return response;
        }

        TableService_Response orderTypeResponse = _tableService.GetByID((int)order_UpdateDto.TableId);
        if (!orderTypeResponse.Response)
        {
            response.Response = false;
            response.Description = "OrderType id cant be finded";
            response.Order = null;
            return response;
        }

        TableService_Response table = _tableService.GetByID((int)order_UpdateDto.TableId);
        if (!table.Response)
        {
            response.Response = false;
            response.Description = "Table id cant be finded";
            response.Order = null;
            return response;
        }

        Order order = OrdersMapper.OrderToEnity(order_UpdateDto);
        if (!_repository.Update(order))
        {
            response.Response = false;
            response.Description = "Order cant be updated by a system error";
            response.Order = null;
            return response;
        }

        response.Response = true;
        response.Description = "Order updated";
        response.Order = order;
        return response;
    }

    public OrderService_Response Delete(int id)
    {
        OrderService_Response OrderService_Response = GetByID(id);
        if (!OrderService_Response.Response)
        {
            return OrderService_Response;
        }
        if (OrderService_Response.Order == null)
        {
            OrderService_Response.Response = false;
            OrderService_Response.Description = "Something is wrong";
            OrderService_Response.Order = null;
            return OrderService_Response;
        }
        else
        {
            OrderService_Response.Response = _repository.Delete(OrderService_Response.Order);
            if (!OrderService_Response.Response)
            {
                OrderService_Response.Description = "User Cant Be Deleted";
                OrderService_Response.Order = null;
                return OrderService_Response;
            }
            else
            {
                return OrderService_Response;
            }
            
        }

        
    }



    //----Utility----
    private OrderService_Response ComprobateOrder(Order newOrder)
    {
        
        return new OrderService_Response(true, "User Role is available", null);
    }
}