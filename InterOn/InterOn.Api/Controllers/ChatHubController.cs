using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterOn.Repo;
using InterOn.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;


namespace InterOn.Api.Controllers
{
    public class ChatHubController : Hub
    {
        public override Task OnConnectedAsync()
        {
            var conversationName = Context.GetHttpContext().Request.Query["conversation"];
            Groups.AddToGroupAsync(Context.ConnectionId, conversationName);
            //uList.Add(new UsersInGorps
            //{
            //    connId = Context.ConnectionId,
            //    groupName = conversationName,
            //});
            return base.OnConnectedAsync();
        }

        public void RemoveFromGroup(string groupName)
        {
            var user = Context.ConnectionId;
            Groups.RemoveFromGroupAsync(user, groupName);
        }

        public void SendChatMessage(string user, string message)
        {
            //string name = Context.User.Identity.Name;
            var conversationName = Context.GetHttpContext().Request.Query["conversation"];
            var g = Groups;
            Clients.Group(conversationName).SendAsync("RecieveMessage", user, message);
            //Clients.Group("2@2.com").addChatMessage(name, message);
        }
    }

}
