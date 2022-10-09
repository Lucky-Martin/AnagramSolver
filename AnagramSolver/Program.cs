using System.Collections;
using System.Text;

Console.WriteLine("Enter your input string");
string input = Console.ReadLine();
List<string> foundWords = new List<string>();
IDictionary dictionary = new Dictionary<string, string>();
string _vowels = "aeiouy";

string fileName = "english_dictionary.txt";
using (var fileStream = File.OpenRead(fileName))
using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true))
{
    String line;
    while ((line = streamReader.ReadLine()) != null)
    {
        try
        {
            dictionary.Add(line.ToLower(), line.ToLower());
        }
        catch (Exception) { }
    }
}

while (input.Length > 2)
{
    var anagrams = GenerateAnagram(input);
    FindWordsInAnagram(anagrams);
    
    List<int> consonants = findConsonants();
    List<int> vowels = findVowels();

    if (consonants.Count > vowels.Count)
    {
        input = input.Remove(consonants[0], 1);
    }
    else
    {
        input = input.Remove(vowels[0], 1);
    }
    Console.WriteLine("Removed a letter");
}

foreach(string word in foundWords)
{
    Console.WriteLine(word);
}

List<int> findConsonants()
{
    List<int> consonants = new List<int>();
    for(int i = 0; i < input.Length; i++)
    {
        if (!_vowels.Contains(input[i]))
        {
            consonants.Add(i);
        }
    }

    return consonants;
}

List<int> findVowels()
{
    List<int> vowels = new List<int>();
    for (int i = 0; i < input.Length; i++)
    {
        if (_vowels.Contains(input[i]))
        {
            vowels.Add(i);
        }
    }

    return vowels;
}

void FindWordsInAnagram(IEnumerable<string> anagrams)
{
    foreach (string word in anagrams)
    {
        if (word == input)
            continue;

        if (dictionary.Contains(word))
        {
            foundWords.Add(word);
        }
    }
}

IEnumerable<string> GenerateAnagram(string src)
{
    if (src.Length == 0) yield break;
    if (src.Length == 1) yield return src;

    foreach (string rest in GenerateAnagram(src.Substring(1)))
    {
        for (int i = 0; i < src.Length; i++)
        {
            string temp = rest.Substring(0, i) + src[0] + rest.Substring(i);
            yield return temp;
        }
    }
}