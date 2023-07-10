using AutoMapper;
using MediatR;
using PledgeVault.Core.Contracts.Entities.Validators;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Enums;
using PledgeVault.Core.Models;
using PledgeVault.Persistence;
using PledgeVault.Services.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PledgeVault.Services.Handlers.Comments;

public sealed class UpdateCommandHandler : IRequestHandler<UpdateCommand<UpdateCommentRequest, CommentResponse>, CommentResponse>
{
    private readonly PledgeVaultContext _context;
    private readonly ICommentEntityValidator _entityValidator;
    private readonly IMapper _mapper;

    public UpdateCommandHandler(PledgeVaultContext context, ICommentEntityValidator entityValidator, IMapper mapper)
    {
        _context = context;
        _entityValidator = entityValidator;
        _mapper = mapper;
    }

    public async Task<CommentResponse> Handle(UpdateCommand<UpdateCommentRequest, CommentResponse> command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Comment>(command.Request);
        await _entityValidator.ValidateAllRules(EntityValidatorType.Update, entity, cancellationToken);
        entity.EntityModified = DateTime.Now;
        _context.Comments.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<CommentResponse>(entity);
    }
}