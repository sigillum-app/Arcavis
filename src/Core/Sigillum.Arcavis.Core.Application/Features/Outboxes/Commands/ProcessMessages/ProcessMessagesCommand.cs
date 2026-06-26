using Sigillum.Arcavis.Core.Application.Common;
using Sigillum.Arcavis.Core.Application.Common.CQRS;

namespace Sigillum.Arcavis.Core.Application.Features.Outboxes.Commands.ProcessMessages;

public record ProcessMessagesCommand : IAppCommand, IManualTransactionRequest;
