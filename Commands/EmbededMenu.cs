using System;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using Newtonsoft.Json;

namespace DiscordBot.Commands
{
    public class EmbededMenu : BaseCommandModule
    {
        [Command("Menu")]
        [Description("Displays the Product Menu")]
        public async Task ShowMenu(CommandContext ctx)
        {
            var json = string.Empty;

            await using (var fs = File.OpenRead("config.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = await sr.ReadToEndAsync().ConfigureAwait(false);

            var configJson = JsonConvert.DeserializeObject<ConfigJson>(json);

            DiscordEmbedBuilder builder = new DiscordEmbedBuilder();

            String list = "Nothing On the Menu";
            
            if (configJson.Menu != "")
            {
                String[] menu1 = configJson.Menu.Split("~~~~");
                MenuObject[] menu = new MenuObject[menu1.Length];
            
                for (int i = 0; i < menu1.Length; i++)
                {
                    String[] items = menu1[i].Split("~");
                    menu[i] = new MenuObject(items[0], items[1]);
                }
                list = "";
                for (int i = 0; i < menu.Length; i++)
                {
                    list += menu[i].Name + " $" + menu[i].Price + "\n";
                }
            }

            builder.Color = DiscordColor.DarkGreen;
            builder.Title = "Product Menu";
            builder.Description = list;

            DiscordEmbed embed = builder.Build();

            await ctx.Channel.SendMessageAsync(embed).ConfigureAwait(false);
        }

        [Command("+Menu")]
        [Description("Add item to the Product Menu")]
        [RequirePermissions(Permissions.Administrator)]
        public async Task AddToMenu(CommandContext ctx, 
            [Description("Name of Item")]String Item, 
            [Description("Price of Item")]int Price)
        {
            var json = string.Empty;

            await using (var fs = File.OpenRead("config.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = await sr.ReadToEndAsync().ConfigureAwait(false);

            var configJson = JsonConvert.DeserializeObject<ConfigJson>(json);

            if (configJson.Menu != "")
            {
                string jason = File.ReadAllText("config.json");
                dynamic jsonObj = JsonConvert.DeserializeObject(jason);
                jsonObj["menu"] = configJson.Menu + "~~~~"+Item+"~"+Price;
                string output = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);
                File.WriteAllText("config.json", output);
            }
            else
            {
                string jason = File.ReadAllText("config.json");
                dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(jason);
                jsonObj["menu"] = configJson.Menu + Item+"~"+Price;
                string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText("config.json", output);
            }

            DiscordEmbedBuilder builder = new DiscordEmbedBuilder();
            builder.Color = DiscordColor.Green;
            builder.Title = "added item = " + Item + " $" + Price;
            DiscordEmbed msg = builder.Build();
            
            await ctx.Channel.SendMessageAsync(msg).ConfigureAwait(false);
        }

        [Command("-Menu")]
        [Description("Remove item from the Product Menu")]
        [RequirePermissions(Permissions.Administrator)]
        public async Task RemoveFromMenu(CommandContext ctx,
            [Description("Name of Item")]String Item)
        {
            var json = string.Empty;

            await using (var fs = File.OpenRead("config.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = await sr.ReadToEndAsync().ConfigureAwait(false);

            var configJson = JsonConvert.DeserializeObject<ConfigJson>(json);

            String list = "";
            
            if (true)//configJson.Menu != "")
            {
                String[] menu1 = configJson.Menu.Split("~~~~");
                MenuObject[] menu = new MenuObject[menu1.Length];
            
                for (int i = 0; i < menu1.Length; i++)
                {
                    String[] items = menu1[i].Split("~");
                    if (!items[0].Equals(Item))
                    {
                        menu[i] = new MenuObject(items[0], items[1]);
                        list += "~~~~" + items[0] + "~" + items[1];
                    }
                }

                if (menu.Length != 0 && list != "")
                {
                    list = list.Remove(0, 4);
                }
                
                string jason = File.ReadAllText("config.json");
                dynamic jsonObj = JsonConvert.DeserializeObject(jason);
                jsonObj["menu"] = list;
                string output = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);
                File.WriteAllText("config.json", output);
                
                DiscordEmbedBuilder builder = new DiscordEmbedBuilder();
                builder.Color = DiscordColor.Green;
                builder.Title = "Removed "+Item;
                DiscordEmbed msg = builder.Build();
            
                await ctx.Channel.SendMessageAsync(msg).ConfigureAwait(false);
            }
            
            
            
        }
    }

    class MenuObject
    {
        public String Name { get; set; }
        public String Price { get; set; }

        public MenuObject(String Name, String Price)
        {
            this.Price = Price;
            this.Name = Name;
        }
    }
}