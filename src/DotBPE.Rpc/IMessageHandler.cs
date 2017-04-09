﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DotBPE.Rpc.Codes;

namespace DotBPE.Rpc
{
    public interface IMessageHandler<TMessage> where TMessage : IMessage
    {
        Task ReceiveAsync(IRpcContext<TMessage> context, TMessage message);
    }
}
