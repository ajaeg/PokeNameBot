using PokeNameBot.Models;
using System.Collections.Generic;
using System.IO;

namespace PokeNameBot.Helper
{
  internal class FileHelper
  {
    internal static readonly string EnglishGermanNamesCsv = @"..\..\..\Data\EnglishGermanNames.csv";

    internal static readonly string BotKeyFile = @"..\..\..\Data\botkey.txt";

    /// <summary>
    /// Loads the static name data from a csv file.
    /// </summary>
    /// <param name="path">The path</param>
    /// <returns>A list of <see cref="PokemonName"/>.</returns>
    internal static List<PokemonName> LoadFromCsv(string path)
    {
      List<PokemonName> temp = new List<PokemonName>();

      using (StreamReader reader = new StreamReader(path))
      {
        while (!reader.EndOfStream)
      {
        string line = reader.ReadLine();
        string[] content = line.Split(';');

        temp.Add(new PokemonName { English = content[0], German = content[1] });
      }
      }


      return temp;
    }

    /// <summary>
    /// Gets the bot key from the given path.
    /// Make sure to store your key in an existing file, before calling this method. 
    /// </summary>
    /// <param name="path">The path.</param>
    /// <returns></returns>
    internal static string LoadBotKey(string path)
    {
      try
      {
        return File.ReadAllText(path);
      }
      catch (System.Exception ex)
      {
        throw new System.Exception($"Bot key file Missing.\n Please make sure to place a file with valid telegram bot key in the Data folder.\n {ex.Message}");
      }
    }
  }
}
