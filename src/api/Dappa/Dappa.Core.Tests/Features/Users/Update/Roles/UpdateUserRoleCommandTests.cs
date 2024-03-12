using Dappa.Core.Exceptions;
using Dappa.Core.Features.Users.Update.Roles;
using Dappa.Core.Models;
using Dappa.Core.Models.Enums;
using Dappa.Core.Tests.Extensions;

namespace Dappa.Core.Tests.Features.Users.Update.Roles;

public class UpdateUserRoleCommandTests : BaseHandlerTest
{
    [Fact]
    public async Task UpdateUserRoleCommandHandler_WhenUserExists_ShouldUpdateUserRole()
    {
        var user = new User { Role = Role.User };
        await Db.AddAsync(user);
        await Db.SaveChangesAsync();
        
        var command = new UpdateUserRoleCommand
        {
            UserId = user.Id,
            Role = Role.Admin,
        };
        command.SetActingUserAndIdentity(TestAdmin, TestIdentity);
        
        Assert.Equal(Role.User, user.Role);

        await Mediatr.Send(command);
        
        Assert.Equal(Role.Admin, user.Role);
    }
    
    [Fact]
    public async Task UpdateUserRoleCommandHandler_WhenUserDoesNotExist_ShouldThrowNotFoundException()
    {
        var user = new User { Role = Role.User };
        
        var command = new UpdateUserRoleCommand
        {
            UserId = user.Id,
            Role = Role.Admin,
        };
        command.SetActingUserAndIdentity(TestAdmin, TestIdentity);

        var exception = await Assert.ThrowsAsync<NotFoundException>(() => Mediatr.Send(command));
        Assert.Equal($"User Id '{user.Id}' not found.", exception.Message);
    }
    
    [Fact]
    public async Task UpdateUserRoleCommandHandler_WhenActingUserIsNotAdmin_ShouldThrowForbiddenException()
    {
        var user = new User { Role = Role.User };
        await Db.AddAsync(user);
        await Db.SaveChangesAsync();
        
        var command = new UpdateUserRoleCommand
        {
            UserId = user.Id,
            Role = Role.Admin,
        };
        command.SetActingUserAndIdentity(TestUser, TestIdentity);

        await Assert.ThrowsAsync<ForbiddenException>(() => Mediatr.Send(command));
    }
    
    [Fact]
    public async Task UpdateUserRoleCommandHandler_WhenUserIdEqualsActingUserId_ShouldThrowValidationException()
    {
        var user = new User { Role = Role.Admin };
        await Db.AddAsync(user);
        await Db.SaveChangesAsync();
        
        var command = new UpdateUserRoleCommand
        {
            UserId = user.Id,
            Role = Role.Admin,
        };
        command.SetActingUserAndIdentity(user, TestIdentity);

        var exception = await Assert.ThrowsAsync<BadRequestException>(() => Mediatr.Send(command));
        Assert.Equal($"'User Id' must not be equal to '{user.Id}'.", exception.Errors.Single().ErrorMessage);
    }
}
