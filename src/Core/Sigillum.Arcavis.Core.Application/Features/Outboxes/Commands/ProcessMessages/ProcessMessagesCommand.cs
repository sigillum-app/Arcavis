using Sigillum.Arcavis.Core.Application.Common;
using Sigillum.Arcavis.Core.Application.CQRS;

namespace Sigillum.Arcavis.Core.Application.Features.Outboxes.Commands.ProcessMessages;

public record ProcessMessagesCommand : ICommand, IManualTransactionRequest;
