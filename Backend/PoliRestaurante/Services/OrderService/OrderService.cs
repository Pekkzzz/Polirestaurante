using PoliRestaurante.Models;
using PoliRestaurante.Models.Entity;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repository;
    private readonly IOrderTypeRepository _orderTypeRepository;
    private readonly IOrderStatusRepository _orderStatusRepository;
    private readonly IPaymentStatusRepository _paymentStatusRepository;
    private readonly ITableRepository _tableRepository;

    //Services
    private readonly IUserService _userService;
    private readonly IUserRoleService _userRoleService;

    public OrderService(IOrderRepository Repository, IOrderTypeRepository orderTypeRepository, IOrderStatusRepository orderStatusRepository,
    IPaymentStatusRepository paymentStatusRepository, ITableRepository tableRepository,
    IUserService userService, IUserRoleService userRoleService)
    {
        _repository = Repository;
        _orderTypeRepository = orderTypeRepository;
        _orderStatusRepository = orderStatusRepository;
        _paymentStatusRepository = paymentStatusRepository;
        _tableRepository = tableRepository;

        _userService = userService;
        _userRoleService = userRoleService;
    }

    public OrderService_Response Create(Order_CreateDto order_CreateDto)
    {
        OrderService_Response response = new OrderService_Response();
        User_LoginDto user_LoginDto = new User_LoginDto()
        {
            Username = order_CreateDto.username,
            Password = order_CreateDto.Password
        };
        UserService_Response userService_Response = _userService.ComprobateUser(user_LoginDto);
        if (!userService_Response.Response || userService_Response.user == null)
        {
            response = new OrderService_Response()
            {
                Response = false,
                Description = "Something went wrong with user comprobation",
                Order = null
            };
            return response;
        }
        
        UserRoleService_Response userRoleService_Response =  _userRoleService.GetByID(userService_Response.user.UserRoleId);
        UserRole? userRole = userRoleService_Response.userRole;
        if (!userRoleService_Response.Response || userRole == null)
        {
            response = new OrderService_Response()
            {
                Response = false,
                Description = "Something went wrong with user role comprobation",
                Order = null
            };
            return response;
        }
        
        if (!userRole.CanEditOrders || !userRole.IsOwner)
        {
            response = new OrderService_Response()
            {
                Response = false,
                Description = "User has not the neccesary permission level",
                Order = null
            };
            return response;
        }


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
            oderType = _orderTypeRepository.Get(order_CreateDto.OrderTypeId);
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
            if (!_tableRepository.TableExists((int)newOrder.TableId))
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

    public OrderService_Response Update(Order_UpdateDto order_UpdateDto)
    {
        OrderService_Response OrderService_Response = GetByID(order_UpdateDto.Id);
        if (!OrderService_Response.Response)
        {
            return OrderService_Response;
        }

        Order Order = OrdersMapper.OrderToEnity(order_UpdateDto);

        OrderService_Response.Response = _repository.Update(Order);
        if (!OrderService_Response.Response)
        {
            OrderService_Response.Order = null;
            OrderService_Response.Description = "Order cant be updated";
            return OrderService_Response;
        }
        
        OrderService_Response = GetByID(order_UpdateDto.Id);
        OrderService_Response.Description = "Order Updated";
        return OrderService_Response;
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