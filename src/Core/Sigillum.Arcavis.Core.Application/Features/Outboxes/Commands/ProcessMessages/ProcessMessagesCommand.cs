using Mediator;
using Sigillum.Arcavis.Core.Application.Common;

namespace Sigillum.Arcavis.Core.Application.Features.Outboxes.Commands.ProcessMessages;

public record ProcessMessagesCommand : ICommand, IManualTransactionRequest;
