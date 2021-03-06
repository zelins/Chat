﻿using Client.CommandsRelated.Resolvers;
using Commands;
using Commands.Abstracts;
using Entities;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using Client.Messages;
using Utils;

namespace Client
{
    public class ChatClient : BindableBase, IDisposable
    {
        #region Fields

        private readonly TcpClient client;

        private readonly DelegateCommand loginCommand;

        private readonly DelegateCommand sendMessageCommand;

        private readonly ClientCommandsResolver resolver;

        private readonly Dispatcher dispatcher;

        private ImmutableQueue<IChatCommand> commandsQueue;

        #endregion Fields

        #region Properties

        public User User { get; private set; }

        public string NickName { get; set; }

        public bool LoginWindowEnabled { get; set; }

        public string MessageText { get; set; }

        public ObservableCollection<ChatMessage> Messages { get; set; }

        public ICommand SendMessageCommand => this.sendMessageCommand;

        public ICommand LoginCommand => this.loginCommand;

        #endregion Properties

        #region Commands

        private bool CanLogin() => NickName.Length > 1;

        private async void Login()
        {
            await Login(NickName);

            LoginWindowEnabled = false;
        }

        private bool CanSendMessage() => MessageText.Any();

        private void SendMessage()
        {
            EnqueueMessage(MessageText);
            MessageText = string.Empty;
        }

        #endregion Commands

        #region Methods

        public async Task Login(string name)
        {
            await this.client.ConnectAsync("127.0.0.1", 1234);
            User = new User
            {
                Id = Guid.NewGuid(),
                Nickname = name
            };
            IChatCommand command = new ConnectCommand
            (
                User,
                DateTime.Now,
                true
            );

            this.commandsQueue = this.commandsQueue.Enqueue(command);

            Thread thread = new Thread(HandleCommands)
            {
                IsBackground = true
            };
            thread.Start();
        }

        private async void HandleCommands()
        {
            while (true)
            {
                IChatCommand command = null;
                if (!this.commandsQueue.IsEmpty)
                {
                    this.commandsQueue = this.commandsQueue.Dequeue(out command);
                }

                try
                {
                    var stream = this.client.GetStream();

                    if (command != null)
                    {
                        await stream.WriteCommandAsync(command);
                    }

                    if (stream.DataAvailable)
                    {
                        var retrievedCommand = await stream.ReadCommandAsync();
                        this.dispatcher.Invoke(() =>
                        {
                            retrievedCommand.Execute(this.resolver, Messages);
                        });
                    }
                }
                catch
                {
                    break;
                }
            }
        }

        public void EnqueueMessage(string message)
        {
            var messageCommand = new MessageCommand
            (
                new Message
                {
                    Author = User,
                    Id = Guid.Empty,
                    Content = message
                },
                DateTime.Now
            );
            this.commandsQueue = this.commandsQueue.Enqueue(messageCommand);
        }

        #endregion Methods

        #region Constructor

        public ChatClient()
        {
            this.loginCommand = new DelegateCommand(Login, CanLogin)
                .ObservesProperty(() => NickName);
            this.sendMessageCommand = new DelegateCommand(SendMessage, CanSendMessage)
                .ObservesProperty(() => MessageText);
            this.client = new TcpClient();
            this.resolver = new ClientCommandsResolver();
            this.dispatcher = Dispatcher.CurrentDispatcher;
            this.commandsQueue = ImmutableQueue<IChatCommand>.Empty;
            LoginWindowEnabled = true;
            NickName = string.Empty;
            MessageText = string.Empty;
            Messages = new ObservableCollection<ChatMessage>();
        }

        #endregion Constructor

        #region IDisposable implementation

        public void Dispose()
        {
            this.client.Close();
        }

        #endregion IDisposable implementation
    }
}