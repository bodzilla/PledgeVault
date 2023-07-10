using AutoMapper;
using MediatR;
using PledgeVault.Core.Contracts.Entities.Validators;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Enums;
using PledgeVault.Core.Models;
using PledgeVault.Persistence;
using PledgeVault.Services.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace PledgeVault.Services.Handlers.Comments;

public sealed class AddCommandHandler : IRequestHandler<AddCommand<AddCommentRequest, CommentResponse>, CommentResponse>
{
    private readonly PledgeVaultContext _context;
    private readonly ICommentEntityValidator _entityValidator;
    private readonly IMapper _mapper;

    public AddCommandHandler(PledgeVaultContext context, ICommentEntityValidator entityValidator, IMapper mapper)
    {
        _context = context;
        _entityValidator = entityValidator;
        _mapper = mapper;
    }

    public async Task<CommentResponse> Handle(AddCommand<AddCommentRequest, CommentResponse> command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Comment>(command.Request);
        await _entityValidator.ValidateAllRules(EntityValidatorType.Add, entity, cancellationToken);
        await _context.Comments.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<CommentResponse>(entity);
    }
}