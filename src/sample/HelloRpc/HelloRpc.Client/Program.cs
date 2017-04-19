﻿using System;
using System.Text;
using System.Threading.Tasks;
using DotBPE.Protocol.Amp;
using DotBPE.Rpc;
using DotBPE.Rpc.Codes;
using DotBPE.Rpc.Extensions;
using DotBPE.Rpc.Netty;
using Microsoft.Extensions.DependencyInjection;
using HelloRpc.Common;

namespace HelloRpc.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            DotBPE.Rpc.Environment.SetLogger(new DotBPE.Rpc.Logging.ConsoleLogger());

            var client = AmpClient.Create("127.0.0.1:6201");
            var greeter = new GreeterClient(client);
            while (true)
            {
                Console.WriteLine("请输入你的名称");
                string name = Console.ReadLine();
                if ("bye".Equals(name))
                {
                    break;
                }
                try
                {
                    var reply = greeter.HelloPlusAsnyc(new HelloRequest()
                    {
                        Name = name
                    },3000).Result;

                    DotBPE.Rpc.Environment.Logger.Debug($"---------------收到服务端返回:{reply.Message}-----------");
                }
                catch(Exception ex){
                    DotBPE.Rpc.Environment.Logger.Error("发生错误："+ex.Message);
                }
            }
        }
    }


}