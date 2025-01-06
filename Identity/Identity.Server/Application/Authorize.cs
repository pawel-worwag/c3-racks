using MediatR;

namespace Identity.Server.Application;

public record AuthorizeRequest : IRequest
{
    
}

internal class AuthorizeRequestHandler : IRequestHandler<AuthorizeRequest>
{
    public async Task Handle(AuthorizeRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}