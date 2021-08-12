using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.Net.Models;
using Newtonsoft.Json;

namespace DiscordBot.Commands
{
    public class OrderCommands : BaseCommandModule
    {
        [Command("Order")]
        [Description("Takes a order from the user")]
        public async Task ShowMenu(CommandContext ctx, [Description("The Items wanted in format of 'Item1-amount,Item2-amount'")]String Items)
        {
            DiscordOverwriteBuilder builder = new DiscordOverwriteBuilder();
            builder.For(ctx.Member);
            builder.Allow(Permissions.SendMessages);
            builder.Allow(Permissions.ReadMessageHistory);
            DiscordOverwriteBuilder badBuilder = new DiscordOverwriteBuilder();
            badBuilder.For(ctx.Guild.EveryoneRole);
            badBuilder.Deny(Permissions.SendMessages);
            badBuilder.Deny(Permissions.ReadMessageHistory);
            badBuilder.Deny(Permissions.All);
            badBuilder.Deny(Permissions.AccessChannels);

            List<DiscordOverwriteBuilder> ob = new List<DiscordOverwriteBuilder>();
            ob.Add(builder);
            ob.Add(badBuilder);
            var channel = await ctx.Guild.CreateChannelAsync(ctx.Message.Author.Username+"s Order", ChannelType.Text, null, null, null, null, ob).ConfigureAwait(false);

            DiscordEmbedBuilder b = new DiscordEmbedBuilder();
            b.Color = DiscordColor.Azure;
            b.Title = ctx.Message.Author.Username+" has ordered : "+ctx.RawArgumentString;
            DiscordEmbed msg = b.Build();

            await channel.SendMessageAsync(msg).ConfigureAwait(false);
        }
    }
    
}