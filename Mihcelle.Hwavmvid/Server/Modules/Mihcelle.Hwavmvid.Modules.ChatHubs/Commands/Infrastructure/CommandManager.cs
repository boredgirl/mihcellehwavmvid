﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Composition.Hosting;
using Mihcelle.Hwavmvid.Modules.ChatHubs.Providers;
using Mihcelle.Hwavmvid.Modules.ChatHubs.Hubs;
using Mihcelle.Hwavmvid.Modules.ChatHubs.Repository;
using System.Resources;
using Mihcelle.Hwavmvid.Modules.ChatHubs.Server.Resources;
using System.Globalization;
using Microsoft.AspNetCore.SignalR;
using Oqtane.ChatHubs.Models;
using Mihcelle.Hwavmvid.Shared.Models;

namespace Mihcelle.Hwavmvid.Modules.ChatHubs.Commands
{
    public class CommandManager
    {
        private readonly string _connectionId;
        private readonly string _roomId;
        private readonly string _moduleId;
        private readonly ChatHubUser _caller;
        private readonly ChatHub _chatHub;
        private readonly ChatHubService _chatService;
        private readonly ChatHubRepository _repository;
        private readonly UserManager<Applicationuser> _userManager;

        private static Dictionary<string, dynamic> _commandCache = new Dictionary<string, dynamic>();
        private static readonly Lazy<IList<ICommand>> _commands = new Lazy<IList<ICommand>>(GetCommands);

        public CommandManager(
                            string connectionId,
                            string roomId,
                            string moduleId,
                            ChatHubUser user,
                            ChatHub chatHub,
                            ChatHubService service,
                            ChatHubRepository repository,
                            UserManager<Applicationuser> userManager)
        {
            _connectionId = connectionId;
            _roomId = roomId;
            _moduleId = moduleId;
            _caller = user;
            _chatHub = chatHub;
            _chatService = service;
            _repository = repository;
            _userManager = userManager;
        }

        public string ParseCommand(string commandString, out string[] args)
        {
            var parts = commandString.Substring(1).Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 0)
            {
                args = new string[0];
                return null;
            }

            args = parts.Skip(1).ToArray();
            return parts[0];
        }
        public async Task<bool> TryHandleCommand(string command)
        {
            if (!string.IsNullOrEmpty(command))
            {
                command = command.Trim();
                if (!Regex.IsMatch(command, @"^\/[A-Za-z0-9?]+?"))
                {
                    return false;
                }

                string[] args;
                var commandName = ParseCommand(command, out args);
                return await TryHandleCommand(commandName, args);
            }

            return false;
        }

        public async Task<bool> TryHandleCommand(string commandName, string[] args)
        {
            if (String.IsNullOrEmpty(commandName))
            {
                return false;
            }

            commandName = commandName.Trim();
            if (commandName.StartsWith("/"))
            {
                return false;
            }

            var context = new CommandServicesContext
            {
                ChatHub = _chatHub,
                ChatHubService = _chatService,
                ChatHubRepository = _repository,                
                UserManager = _userManager
            };

            var callerContext = new CommandCallerContext
            {
                ConnectionId = _connectionId,
                UserId = _caller.Id,
                RoomId = _roomId,
                ModuleId = _moduleId,
            };

            ICommand command;
            command = MatchCommand(commandName);
            if(command != null)
            {
                await command.Execute(context, callerContext, args, _caller);
                return true;
            }

            throw new HubException(string.Format("No command found that called like {0}", commandName));
        }

        public ICommand MatchCommand(string commandName)
        {
            ICommand command = null;
            if (!_commandCache.Any())
            {
                var commands = _commands.Value.Where(x => x.GetType().GetCustomAttributes(true).OfType<CommandAttribute>().FirstOrDefault() != null)
                                        .Select(y => new { ResourceName = y.GetType().GetCustomAttributes(true).OfType<CommandAttribute>().FirstOrDefault().ResourceName, Commands = y.GetType().GetCustomAttributes(true).OfType<CommandAttribute>().FirstOrDefault().Commands, Command = y });

                foreach(var c in commands)
                {
                    _commandCache.Add(c.ResourceName, new { Commands = c.Commands, Command = c.Command });
                }
            }

            IList<string> candidates = null;
            foreach (var commandCacheItem in _commandCache)
            {
                string[] commandNames = commandCacheItem.Value.Commands;
                var exactMatches = commandNames.Where(x => x.Equals(commandName, StringComparison.OrdinalIgnoreCase)).ToList();

                if (exactMatches.Count == 1)
                {
                    candidates = exactMatches;
                }

                if(candidates != null)
                {
                    if(candidates.Count == 1)
                    {
                        command = commandCacheItem.Value.Command;
                        commandName = candidates[0];
                        break;
                    }
                }
            }

            return command;
        }

        public static IList<ICommand> GetCommands()
        {
            var configuration = new ContainerConfiguration().WithAssembly(typeof(CommandManager).Assembly);
            var container = configuration.CreateContainer();
            IList<ICommand> iCommands = container.GetExports<ICommand>("ICommand").ToList();
            return iCommands;
        }

        public static IEnumerable<ChatHubCommandMetaData> GetCommandsMetaDataByUserRole(string[] roles)
        {
            var resourceManager = new ResourceManager(typeof(CommandResources));
            IEnumerable<ChatHubCommandMetaData> commands = from c in _commands.Value
                                                     let commandAttribute = c.GetType()
                                                                             .GetCustomAttributes(true)
                                                                             .OfType<CommandAttribute>()
                                                                             .FirstOrDefault()
                                                     where commandAttribute != null && commandAttribute.Roles.Any(item => roles.Contains(item))
                                                     select new ChatHubCommandMetaData
                                                     {
                                                         ResourceName = commandAttribute.ResourceName,
                                                         Arguments = commandAttribute.Arguments,
                                                         Usage = commandAttribute.Usage,
                                                         Roles = commandAttribute.Roles,
                                                         Commands = resourceManager.GetString(commandAttribute.ResourceName, CultureInfo.CurrentCulture).Split(';')
                                                     };
            return commands;
        }

        public static IEnumerable<ChatHubCommandMetaData> GetCommandsMetaData()
        {
            var commandsMetaData = _commands.Value.Where(x => x.GetType().GetCustomAttributes(true).OfType<CommandAttribute>().FirstOrDefault() != null)
                                        .Select(y => new ChatHubCommandMetaData {
                                            Commands = y.GetType().GetCustomAttributes(true).OfType<CommandAttribute>().FirstOrDefault().Commands,
                                            Arguments = y.GetType().GetCustomAttributes(true).OfType<CommandAttribute>().FirstOrDefault().Arguments,
                                            Roles = y.GetType().GetCustomAttributes(true).OfType<CommandAttribute>().FirstOrDefault().Roles,
                                            Usage = y.GetType().GetCustomAttributes(true).OfType<CommandAttribute>().FirstOrDefault().Usage
                                        });

            return commandsMetaData;
        }
        
    }
}