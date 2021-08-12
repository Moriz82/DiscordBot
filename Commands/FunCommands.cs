using System.Collections.Generic;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Net.Models;

namespace DiscordBot.Commands
{
    public class FunCommands : BaseCommandModule
    {
        [Command("ping")]
        public async Task Ping(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("Pong").ConfigureAwait(false);
            
        }
        [Command("Goob")]
        public async Task SpamGoob(CommandContext ctx)
        {
            IReadOnlyList<DiscordMember> g = await ctx.Guild.SearchMembersAsync("goob").ConfigureAwait(false);
            DiscordMember goob = g[0];
            while (true)
            {
                await goob.SendMessageAsync("JUST GOOBIN LOL").ConfigureAwait(false);
                //await ctx.Channel.SendMessageAsync("test").ConfigureAwait(false);
                await Task.Delay(1);
            }
        }
    }
}