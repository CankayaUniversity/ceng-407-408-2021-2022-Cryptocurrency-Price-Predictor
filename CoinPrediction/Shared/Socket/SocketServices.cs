using System;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace Shared.Mapping
{
    public class SocketServices : IDisposable
    {
        HubConnection _connection;
        public SocketServices(string url)
        {
            _connection = new HubConnectionBuilder().WithUrl(url).WithAutomaticReconnect().Build();
            try
            {
                _connection.StartAsync();
            }
            catch
            {

            }
        }

        public async Task<HubConnection> Connection()
        {
            while (true)
            {
                try
                {
                    await _connection.StartAsync();
                    return _connection; // yay! connected
                }
                catch (Exception e) { /* bugger! */}
            }

        }

        public async void Dispose()
        {
            await _connection.StopAsync();
        }
        /// <summary>
        /// message and groupName properties of SocketServiceModel should be provided. 
        /// </summary>
        /// <param name="messageModel"></param>
        public async void SendMessageGroup(SocketServiceModel messageModel)
        {
            try
            {
                //if (_connection.State == HubConnectionState.Connecting)
                //{
                await _connection.InvokeAsync("SendMessageGroup", messageModel.message, messageModel.groupName);
                await _connection.StopAsync();
                //}
            }
            catch (Exception ex)
            {

            }
        }
        /// <summary>
        /// obj and groupName properties of SocketServiceModel should be provided. 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="groupName"></param>
        public async void SendObjGroup(SocketServiceModel messageModel)
        {
            try
            {
                //if (connection.State == HubConnectionState.Connecting)
                //{
                await _connection.InvokeAsync("SendObjectGroup", messageModel.obj, messageModel.groupName);
                await _connection.StopAsync();
                //}
            }
            catch (Exception)
            {

            }
        }
        /// <summary>
        /// obj, eventName and groupName properties of SocketServiceModel should be provided. 
        /// </summary>
        /// <param name="messageModel"></param>
        public async void SendObjGroupWithEventName(SocketServiceModel messageModel)
        {
            try
            {
                //if (connection.State == HubConnectionState.Connecting)
                //{
                await _connection.InvokeAsync("SendObjectGroupWithEvent", messageModel.obj, messageModel.eventName, messageModel.groupName);
                await _connection.StopAsync();
                //}
            }
            catch (Exception)
            {

            }
        }
    }

    public class SocketServiceModel
    {
        public object obj { get; set; }
        public string eventName { get; set; }
        public string message { get; set; }
        public string groupName { get; set; }
    }
}
