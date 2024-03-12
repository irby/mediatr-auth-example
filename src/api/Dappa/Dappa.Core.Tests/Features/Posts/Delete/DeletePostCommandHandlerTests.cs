using Dappa.Core.Exceptions;
using Dappa.Core.Features.Posts.Delete;
using Dappa.Core.Models;
using Dappa.Core.Models.Enums;
using Dappa.Core.Tests.Extensions;

namespace Dappa.Core.Tests.Features.Posts.Delete;

public class DeletePostCommandHandlerTests : BaseHandlerTest
{
    [Fact]
    public async Task DeletePostCommandHandler_WhenActingUserOwnsPost_ShouldDeletePost()
    {
        var post = new Post { User = TestUser };
        await Db.AddRangeAsync(post);
        await Db.SaveChangesAsync();

        var command = new DeletePostCommand
        {
            Id = post.Id,
        };
        command.SetActingUserAndIdentity(TestUser, TestIdentity);

        Assert.NotNull(Db.Posts.SingleOrDefault(p => p.Id == post.Id));
        
        await Mediatr.Send(command);
        
        Assert.Null(Db.Posts.SingleOrDefault(p => p.Id == post.Id));
    }
    
    [Fact]
    public async Task DeletePostCommandHandler_WhenActingUserIsAdmin_ShouldDeletePost()
    {
        var post = new Post { User = TestUser };
        await Db.AddRangeAsync(post);
        await Db.SaveChangesAsync();

        var command = new DeletePostCommand
        {
            Id = post.Id,
        };
        command.SetActingUserAndIdentity(TestAdmin, TestIdentity);

        Assert.NotNull(Db.Posts.SingleOrDefault(p => p.Id == post.Id));
        
        await Mediatr.Send(command);
        
        Assert.Null(Db.Posts.SingleOrDefault(p => p.Id == post.Id));
    }
    
    [Fact]
    public async Task DeletePostCommandHandler_WhenPostIdDoesNotExist_ShouldThrowNotFoundException()
    {
        var post = new Post { User = TestUser };

        var command = new DeletePostCommand
        {
            Id = post.Id,
        };
        command.SetActingUserAndIdentity(TestUser, TestIdentity);

        var exception = await Assert.ThrowsAsync<NotFoundException>(() => Mediatr.Send(command));
        Assert.Equal($"Post Id '{post.Id}' not found.", exception.Message);
    }
    
    [Fact]
    public async Task DeletePostCommandHandler_WhenActingUserDoesNotHavePermissionToDeletePost_ShouldThrowForbiddenException()
    {
        var user = new User { Role = Role.User };
        var post = new Post { User = TestUser };
        await Db.AddRangeAsync(user, post);
        await Db.SaveChangesAsync();

        var command = new DeletePostCommand
        {
            Id = post.Id,
        };
        command.SetActingUserAndIdentity(user, TestIdentity);

        var exception = await Assert.ThrowsAsync<ForbiddenException>(() => Mediatr.Send(command));
        Assert.Equal("You are not allowed to delete this post.", exception.Message);
    }
}
