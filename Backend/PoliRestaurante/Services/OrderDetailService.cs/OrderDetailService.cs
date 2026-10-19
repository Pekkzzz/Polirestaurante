using PoliRestaurante.Models.Entity;

public class OrderDetailService : IOrderDetailService
{
    private readonly IOrderDetailRepository _repository;

    //Services
    private readonly IUserService _userService;
    private readonly IUserRoleService _userRoleService;

    public OrderDetailService(IOrderDetailRepository Repository, IOrderDetailRepository orderTypeRepository, IOrderDetailStatusRepository orderStatusRepository,
    IPaymentStatusRepository paymentStatusRepository, ITableRepository tableRepository,
    IUserService userService, IUserRoleService userRoleService)
    {
        _repository = Repository;

        _userService = userService;
        _userRoleService = userRoleService;
    }

    public OrderDetailService_Response Create(OrderDetail_CreateDto order_CreateDto)
    {
        OrderDetailService_Response response = new OrderDetailService_Response();
        User_LoginDto user_LoginDto = new User_LoginDto()
        {
            Username = order_CreateDto.username,
            Password = order_CreateDto.Password
        };
        UserService_Response userService_Response = _userService.ComprobateUser(user_LoginDto);
        if (!userService_Response.Response || userService_Response.user == null)
        {
            response = new OrderDetailService_Response()
            {
                Response = false,
                Description = "Something went wrong with user comprobation",
                OrderDetail = null
            };
            return response;
        }
        
        UserRoleService_Response userRoleService_Response =  _userRoleService.GetByID(userService_Response.user.UserRoleId);
        UserRole? userRole = userRoleService_Response.userRole;
        if (!userRoleService_Response.Response || userRole == null)
        {
            response = new OrderDetailService_Response()
            {
                Response = false,
                Description = "Something went wrong with user role comprobation",
                OrderDetail = null
            };
            return response;
        }
        
        if (!userRole.CanEditOrders || !userRole.IsOwner)
        {
            response = new OrderDetailService_Response()
            {
                Response = false,
                Description = "User has not the neccesary permission level",
                OrderDetail = null
            };
            return response;
        }


        OrderDetail newOrderDetail = OrdersMapper.OrderDetailToEnity(order_CreateDto);
        response = ComprobateOrderDetail(newOrderDetail);
        if (!response.Response)
        {
            return response;
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