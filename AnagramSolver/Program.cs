//using NetSpell.SpellChecker;
//using NetSpell.SpellChecker.Dictionary;

//WordDictionary oDict = new WordDictionary();

//oDict.DictionaryFile = Path.Combine(Environment.CurrentDirectory, "dictionaries/en-US.dic");
//oDict.Initialize();
//Spelling oSpell = new Spelling();
//oSpell.Dictionary = oDict;

Console.WriteLine("Enter your input string");
string input = Console.ReadLine();
List<string> foundWords = new List<string>();

while (input.Length > 2)
{
    var anagrams = GenerateAnagram(input);
    FindWordsInAnagram(anagrams);
    input = input.Remove(input.Length - 1, 1);
}

foreach (string word in foundWords)
{
    Console.WriteLine(word);
}

void FindWordsInAnagram(IEnumerable<string> anagrams)
{
    foreach (string word in anagrams)
    {
        if (word == input)
            continue;

        HttpClient httpClient = new HttpClient();
        httpClient.Timeout = TimeSpan.FromSeconds(5);

        string url = "https://api.dictionaryapi.dev/api/v2/entries/en_US/" + word;
        var message = new HttpRequestMessage(HttpMethod.Get, url);
        message.Content = new StringContent(string.Empty);

        var response = httpClient.Send(message);

        if (response.IsSuccessStatusCode)
        {
            foundWords.Add(word);
        }

        //if (oSpell.TestWord(word))
        //{
        //    foundWords.Add(word);
        //}
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