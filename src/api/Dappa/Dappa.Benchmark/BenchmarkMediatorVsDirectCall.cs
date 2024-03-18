using BenchmarkDotNet.Attributes;
using Dappa.Core.Common.Pipeline;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Dappa.Benchmark;

[KeepBenchmarkFiles(true)]
[MarkdownExporterAttribute.GitHub]
public class BenchmarkMediatorVsDirectCall
{
    private readonly IMediator _mediator;
    private readonly DoWorkService _service;
    private readonly DoWorkCommand _command = new () { Message = "Hello, World!" };
    
    public BenchmarkMediatorVsDirectCall()
    {
        var di = new ServiceCollection();
        var assembly = typeof(BenchmarkMediatorVsDirectCall).Assembly;
        di
            .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly))
            .AddSingleton<DoWorkCommandValidator>()
            .AddTransient<DoWorkService>()
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>))
            .AddValidatorsFromAssembly(assembly);;
        
        var provider = di.BuildServiceProvider();
        _mediator = provider.GetRequiredService<IMediator>();
        _service = provider.GetRequiredService<DoWorkService>();
    }
    
    [Benchmark]
    public async Task BenchmarkDirectCall()
    {
        await _service.DoWork(_command);
    }
    
    [Benchmark]
    public async Task BenchmarkMediatr()
    {
        await _mediator.Send(_command);
    }
}

public class DoWorkCommand : IRequest<DoWorkCommandResponse>
{
    public string Message { get; set; }
}

public class DoWorkCommandHandler : IRequestHandler<DoWorkCommand, DoWorkCommandResponse>
{
    public async Task<DoWorkCommandResponse> Handle(DoWorkCommand request, CancellationToken cancellationToken)
    {
        return new DoWorkCommandResponse { Id = Guid.NewGuid() };
    }
}

public class DoWorkCommandResponse
{
    public Guid Id { get; set; }
}

public class DoWorkCommandValidator : AbstractValidator<DoWorkCommand>
{
    public DoWorkCommandValidator()
    {
        RuleFor(x => x.Message).NotEmpty();
        RuleFor(x => x.Message).MaximumLength(250);
    }
}

public class DoWorkService
{
    public DoWorkService(DoWorkCommandValidator validator)
    {
        _validator = validator;
    }

    private readonly DoWorkCommandValidator _validator;
    public async Task<DoWorkCommandResponse> DoWork(DoWorkCommand command)
    {
        var validationResult = await _validator.ValidateAsync(command);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        return new DoWorkCommandResponse() { Id = Guid.NewGuid() };
    }
}
