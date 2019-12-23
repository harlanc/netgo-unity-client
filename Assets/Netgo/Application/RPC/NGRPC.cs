using UnityEngine;
using System.Collections.Generic;
using System.Reflection;
using System;
using Google.Protobuf;
using Netgo.Library;

using System.Linq;
using SType = System.Type;
using Netgo.Client;
using Netgo.Network;

//public class 
//https://blog.csdn.net/zhong_chongfeng/article/details/79644701
public class NGRPCMethod : Attribute
{

}


public class NGRPC
{

    public static Dictionary<string, MethodInfo> name2Method = new Dictionary<string, MethodInfo>();


    public void CollectAllRpcFuncs()
    {
        Assembly[] assemblies = System.AppDomain.CurrentDomain.GetAssemblies();

        List<MethodInfo> methods = new List<MethodInfo>();
        foreach (var assembly in assemblies)
        {
            var methodsinass = assembly.GetTypes().SelectMany(x => x.GetMethods())
               .Where(y => y.GetCustomAttributes()
                .OfType<NGRPCMethod>().Any()).ToArray();

            methods.AddRange(methodsinass);
            foreach (var method in methodsinass)
            {
                if (name2Method.ContainsKey(method.Name))
                {
                    NGLogger.LogError("Error happens when collecting RFC methods.");
                }
                else
                {
                    name2Method.Add(method.Name, method);
                }
            }
        }
    }
    //Send RPC to server
    public static void SendRPC(uint viewID, string methodname, RPCTarget target, params NGAny[] parameters)
    {
        SendMessage message = new SendMessage();
        message.MsgType = MessageType.Rpc;
        message.RpcParams = new RPCParams();
        message.RpcParams.MethodName = methodname;
        message.RpcParams.Target = target;
        message.RpcParams.ViewID = viewID;
        message.RpcParams.Parameters.AddRange(parameters);

        var buf = NGMessageCodec.Encode(message.ToByteArray());
        NGNetwork.Socket.Send(buf);
    }
    //Excute RPC from server
    public static void ExcuteRPC(uint viewID, string methodname, params NGAny[] parameters)
    {
        NGViewContainer container = new NGViewContainer();
        NGView view = container.GetViewByID(viewID);

        List<Component> components = view.ViewComponents;


        foreach (var item in components)
        {
            SType type = item.GetType();
            BindingFlags flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly;
            MethodInfo[] methods = type.GetMethods(flags);

            foreach (var method in methods)
            {
                if (method.Name.Equals(methodname))
                {
                    method.Invoke(item, new object[1] { parameters });
                    return;
                }
            }

        }



        // SType type = view.GetType();
        // BindingFlags flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly;
        // MethodInfo[] methods = type.GetMethods(flags);

        // foreach (var method in methods)
        // {
        //     if (method.Name.Equals(methodname))
        //     {
        //         method.Invoke(null, parameters);
        //         break;
        //     }
        // }

    }
}








