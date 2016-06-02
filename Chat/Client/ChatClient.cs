using Commands;
using Commands.Abstracts;
using Entities;
using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using Utils;

namespace Client
{
    public class ChatClient : BindableBase, IDisposable
    {
        #region Properties

        public User User { get; private set; }

        public string NickName { get; set; }

        public bool LoginWindowEnabled { get; set; }

        #endregion Properties

        #region Commands

        private DelegateCommand loginCommand;

        public ICommand LoginCommand => this.loginCommand;

        #endregion Commands

        #region IDisposable implementation

        public void Dispose()
        {
            this.client.Dispose();
        }

        #endregion IDisposable implementation

        #region Fields

        private readonly TcpClient client = new TcpClient();

        private ImmutableQueue<IChatCommand> commandsQueue = ImmutableQueue<IChatCommand>.Empty;

        #endregion Fields

        #region Constructor

        public ChatClient()
        {
            this.loginCommand = new DelegateCommand(Login, CanLogin)
                .ObservesProperty(() => NickName);
            LoginWindowEnabled = true;
        }

        #endregion Constructor

        #region Methods

        private bool CanLogin() => NickName != null && NickName.Length > 1;

        private async void Login()
        {
            await Login(NickName);

            LoginWindowEnabled = false;
        }

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
                        var c = await stream.ReadCommandAsync();
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
    }
}