﻿using System;
using Microsoft.Extensions.Logging;
using Utility.Extensions;
using Utility.Logger;
using WebWeChat.Im.Core;
using WebWeChat.Im.Module.Impl;
using WebWeChat.Im.Service.Interface;

namespace WebWeChat.Im.Service.Impl
{
    public class WeChatLogger : SimpleConsoleLogger, IWeChatLogger
    {
        public IWeChatContext Context { get; set; }

        public WeChatLogger(IWeChatContext context, LogLevel minLevel = LogLevel.Information) : base("WeChat", minLevel)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            Context = context;
        }

        public override string GetMessage(string message, Exception exception)
        {
            var userName = Context.GetModule<AccountModule>().User?.NickName;
            var prefix = userName.IsNullOrEmpty() ? string.Empty : $"[{userName}]";
            return $"{DateTime.Now:HH:mm:ss}> {prefix}{message}";
        }

        public void Dispose()
        {
        }
    }
}
