using System;
using System.Collections.Generic;
using System.Text;
using VisionModels.Messages;

namespace VisionDbService.MessageHandlers
{
    public interface IMessageHandler<T> where T : IMessage
    {
        string Handle(T message);
    }
}
