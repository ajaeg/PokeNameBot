using PokeNameBot.Extensions;
using PokeNameBot.Helper;
using PokeNameBot.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace PokeNameBot
{
  internal class Program
  {
    private static ITelegramBotClient botClient;

    public static List<PokemonName> PokemonNames { get; set; } = new List<PokemonName>();

    private static void Main(string[] args)
    {
      botClient = new TelegramBotClient(FileHelper.LoadBotKey(FileHelper.BotKeyFile));
      Telegram.Bot.Types.User bot = botClient.GetMeAsync().Result;
      //Console.WriteLine($"Hello, World! I am user {bot.Id} and my name is {bot.FirstName}.");      

      LoadFromCsv();

      botClient.OnMessage += Bot_OnMessage;
      botClient.StartReceiving();
      Thread.Sleep(int.MaxValue);
    }

    private static void LoadFromCsv()
    {
      PokemonNames = FileHelper.LoadFromCsv(FileHelper.EnglishGermanNamesCsv);

      Console.WriteLine($"Loaded {PokemonNames.Count} entries");
    }

    private static async void Bot_OnMessage(object sender, MessageEventArgs e)
    {
      Console.WriteLine($"Received a text message in chat {e.Message.Chat.Id}.");
      string messageText = e.Message.Text;
      string result = "";

      if (messageText != null)
      {
        // Handles special keywords.
        if (messageText.ContainsSpecialKeyword())
        {
          // TODO handle special key word.
          return;
        }

        // If no special keywords are found, the text is scanned for names.
        List<PokemonName> foundPokemonNames = messageText.GetContainedNames(PokemonNames);

        if (foundPokemonNames.Count > 0)
        {
          foreach (PokemonName pokemonName in foundPokemonNames)
          {
            result += $"{pokemonName.English} - {pokemonName.German}\n";
          }

          await botClient.SendTextMessageAsync(chatId: e.Message.Chat, text: result);
        }
      }
    }


  }
}
