﻿using DotBPE.Rpc.Codes;
using DotBPE.Rpc.DefaultImpls;
using DotBPE.Rpc.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotBPE.Rpc.Extensions
{
    public static class RpcHostBuilderExtensions
    {
        public static IRpcHostBuilder AddRpcCore<TMessage>(this IRpcHostBuilder builder) where TMessage : InvokeMessage
        {
            builder.ConfigureServices((services) =>
            {
                services.AddSingleton<IMessageHandler<TMessage>, DefaultMessageHandler<TMessage>>()
                    .AddSingleton<IServerHost, DefaultServerHost>();
            });
            return builder;
        }

        public static IRpcHostBuilder UseConfiguration(this IRpcHostBuilder builder,IConfiguration config)
        {
            foreach(var setting in config.AsEnumerable()){
               builder.UseSetting(setting.Key,setting.Value);
            }

            return builder;
        }

        public static IRpcHostBuilder UseServer(this IRpcHostBuilder builder,string ip,int port)
        {
            builder.UseServer(string.Format("{0}:{1}",ip,port));
            return builder;
        }
        public static IRpcHostBuilder UseServer(this IRpcHostBuilder builder,string address)
        {

            builder.UseSetting("hostAddress",address);

            return builder;
        }

    }
}