using System.Text;

Console.WriteLine("Enter your input string");
string input = Console.ReadLine();
List<string> foundWords = new List<string>();
Dictionary<string, string> dictionary = new Dictionary<string, string>();
string vowels = "aeiouy";

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
    Console.WriteLine("starting");
    var anagrams = GenerateAnagram(input);
    FindWordsInAnagram(anagrams);
    Console.WriteLine("done");
    //for (int i = 0; i < input.Length; i++)
    //{
    //    if (!vowels.Contains(input[i]))
    //    {
    //        input = input.Remove(i, 1);
    //        break;
    //    }
    //}
}

foreach(string word in foundWords)
{
    Console.WriteLine(word);
}

void FindWordsInAnagram(IEnumerable<string> anagrams)
{
    foreach (string word in anagrams)
    {
        if (word == input)
            continue;

        if (dictionary.ContainsKey(word))
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