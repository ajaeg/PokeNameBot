using PokeNameBot.Models;
using System.Collections.Generic;

namespace PokeNameBot.Extensions
{
  public static class StringExtensions
  {
    /// <summary>
    /// Checks if the given text contains special keywords.
    /// </summary>
    /// <param name="text">The text to check.</param>
    /// <returns></returns>
    public static bool ContainsSpecialKeyword(this string text)
    {
      // TODO Implement check for special keywords[];
      return false;
    }

    /// <summary>
    /// Gets a list of found <see cref="PokemonName"/> in the given text.
    /// </summary>
    /// <param name="text">The given text</param>
    /// <param name="pokemonNames">The list of <see cref="PokemonName"/>.</param>
    /// <returns></returns>
    public static List<PokemonName> GetContainedNames(this string text, List<PokemonName> pokemonNames)
    {
      List<PokemonName> result = new List<PokemonName>();

      foreach (var pokemonName in pokemonNames)
      {
        foreach (var word in text.Split(' '))
        {
          if (pokemonName.English.ToLower().Equals(word) || pokemonName.German.ToLower().Equals(word))
          {
            result.Add(pokemonName);
          }
        }
      }
      return result;
    }
  }
}
