using PoliRestaurante.Models;
using PoliRestaurante.Models.Entity;

public class OrderTypeService : IOrderTypeService
{
    private readonly IOrderRepository _repository;
    private readonly IOrderTypeRepository _orderTypeRepository;
    private readonly IOrderStatusRepository _orderStatusRepository;
    private readonly IPaymentStatusRepository _paymentStatusRepository;

    //Services
    private readonly IUserService _userService;
    private readonly IUserRoleService _userRoleService;
    private readonly ITableService _tableService;

    public OrderTypeService(IOrderRepository Repository, IOrderTypeRepository orderTypeRepository, IOrderStatusRepository orderStatusRepository,
    IPaymentStatusRepository paymentStatusRepository,
    IUserService userService, IUserRoleService userRoleService, ITableService tableService)
    {
        _repository = Repository;
        _orderTypeRepository = orderTypeRepository;
        _orderStatusRepository = orderStatusRepository;
        _paymentStatusRepository = paymentStatusRepository;

        _userService = userService;
        _userRoleService = userRoleService;
        _tableService = tableService;

    }

    public OrderService_Response Create(OrderType_CreateDto OrderType_CreateDto)
    {
        OrderService_Response response = new OrderService_Response();
        OrderType newOrderType = OrdersMapper.OrderTypeToEnity(OrderType_CreateDto);
        response = ComprobateOrder(newOrderType);
        if (!response.Response)
        {
            return response;
        }

        newOrder.CreationDate = DateTime.Now;
        OrderType? oderType;
        OrderStatus? orderStatus;
        PaymentStatus? paymentStatus;

        if (OrderType_CreateDto.OrderTypeId <= 0 )
        {
            if (!_orderTypeRepository.Exists(DefaultConfiguration.DefaultOrderTypeName))
            {
                response.Response = false;
                response.Description = $"Order Cant be created: OrderType {DefaultConfiguration.DefaultOrderTypeName} is Invalid";
                response.Order = null;
                return response;
            }
            oderType = _orderTypeRepository.Get(DefaultConfiguration.DefaultOrderTypeName);
            
        }
        else
        {
            oderType = _orderTypeRepository.Get(OrderType_CreateDto.OrderTypeId);
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

    public OrderService_Response Update(OrderType_UpdateDto OrderType_UpdateDto)
    {
        OrderService_Response response = new OrderService_Response();
        if (!_repository.Exists(OrderType_UpdateDto.Id))
        {
            response.Response = false;
            response.Description = "Product id cant be finded";
            response.Order = null;
            return response;
        }

        TableService_Response table = _tableService.GetByID((int)OrderType_UpdateDto.TableId);
        if (!table.Response)
        {
            response.Response = false;
            response.Description = "Table id cant be finded";
            response.Order = null;
            return response;
        }

        Order order = OrdersMapper.OrderToEnity(OrderType_UpdateDto);
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

    public OrderService_Response Detele(int id)
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