using PoliRestaurante;
using PoliRestaurante.Models;

public static class UsersMapper
{
    public static User_Dto UserToDto(User user)
    {
        return new User_Dto
        {
            Id = user.Id,
            Username = user.Username,
            Name = user.Name,
            Email = user.Email
        };
    }

    public static User UserToEnity(User_CreateDto createDto)
    {
        return new User
        {
            Username = createDto.Username,
            Name = createDto.Name,
            Email = createDto.Email,
            PasswordHash = createDto.Password
        };
    }

    public static User UserToEnity(User_UpdateDto updateDto)
    {
        return new User
        {
            Id = updateDto.ID,
            Username = updateDto.Username,
            Name = updateDto.Name,
            Email = updateDto.Email,
            PasswordHash = updateDto.Password,
            CreationDate = updateDto.CreationDate,
            IsDeleted = updateDto.IsDeleted,
            DeletedDate = updateDto.DeletedDate,
            UserRoleId = updateDto.UserRoleId
        };
    }

    public static UserRole_Dto UserRoleToDto(UserRole userRole)
    {
        return new UserRole_Dto
        {
            Id = userRole.Id,
            Name = userRole.Name,
            Description = userRole.Description
        };
    }

    public static UserRole UserRoleToEnity(UserRole_CreateDto userRole)
    {
        return new UserRole
        {
            Name = userRole.Name,
            Description = userRole.Description,
            IsOwner = userRole.IsOwner,
            CanEditSystems = userRole.CanEditSystems,
            CanEditOrders = userRole.CanEditOrders,
            CanSetOrdersStatus = userRole.CanSetOrdersStatus,
            CanSetToPickUpStatus = userRole.CanSetToPickUpStatus,
            CanSetProductStock = userRole.CanSetProductStock
        };
    }

    public static UserRole UserRoleToEnity(UserRole_UpdateDto userRole)
    {
        return new UserRole
        {
            Id = userRole.Id,
            Name = userRole.Name,
            Description = userRole.Description
        };
    }
}