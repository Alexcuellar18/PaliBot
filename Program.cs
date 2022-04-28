using System.Linq;
using System.Text.RegularExpressions;

namespace PaliBot
{
    class Program
    {
        //Pali examples to try out. 
        //Poor Dan is in a droop. Madam, in Eden, I’m Adam.
        //Was it a car or a cat I saw. A nut for a jar of tuna. Madam, in Eden, I’m Adam. This is not a palindrome but I am using it as a sentence for my paragraph. Racecar palindrome test.
        //

        public static string removeSymbols(string word){
            //Clean up any symbols before checking if palindrome. 
            Regex rgx = new Regex("[^a-zA-Z0-9 -]");
            return rgx.Replace(word, "");
        }
        public static bool isStringPali(string word){
            word = word.ToLower();
            var lastIndexInWord = word.Length - 1;

           for(int frontIndex = 0, lastIndex = lastIndexInWord; frontIndex < lastIndexInWord; frontIndex++, lastIndex--){
               var front = word[frontIndex];
               var last = word[lastIndex];
               if(front != last) return false;
           }
           return true;
        } 
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to PaliBot!!");
            Console.WriteLine("Please submit a clean single spaced paragraph that you would like us to analyze:");
            var paragraph = Console.ReadLine();
            Console.WriteLine("Analyzing...");
            //variables to track
            var numOfPaliWords = 0;
            var numOfPaliSentences = 0;
            Dictionary<string, int> uniqueWordsWithCount = new Dictionary<string, int>();
            List<string> wordsContainingLetter = new List<string>();

            if(paragraph == null) return;
            var reverse = string.Empty;
                var allWords = paragraph.Split(" ");
                //Assuming sentences are broken up by periods;
                var allSentences = paragraph.Split(".");

                if(allWords.Length == 1){
                    var cleanedWord = removeSymbols(paragraph);
                    bool isPali = isStringPali(cleanedWord);
                    if(!isPali){
                        Console.WriteLine("You only entered one word and it is not a palindrone! Good bot.");
                        return;
                    }
                    else{
                        Console.WriteLine("You only entered one word and it is a palindrone! Good bot.");
                        return;
                    }
                }
                else{
                    Console.WriteLine("Please also enter a letter to list all words that contain that letter.");
                    bool enteredMoreThanOne = true;
                    var letterEntered = string.Empty;
                    while(enteredMoreThanOne){
                         letterEntered = Console.ReadLine();
                        if(letterEntered == null) return;
                        Console.WriteLine();
                        if(letterEntered.Length == 1){
                            enteredMoreThanOne = false;
                        }
                        else{
                            Console.WriteLine("Please only enter ONE letter.");
                        }
                    }
                    foreach(var word in allWords){
                        var cleanedWord = removeSymbols(word);
                        if(word.IndexOf(letterEntered) != -1){
                            wordsContainingLetter.Add(word);
                        }
                        if(isStringPali(cleanedWord)) numOfPaliWords++; 
                        //check if word is being used again and increment value
                        if(uniqueWordsWithCount.ContainsKey(cleanedWord)){
                         uniqueWordsWithCount[cleanedWord] = uniqueWordsWithCount[cleanedWord] + 1;
                        }  
                        else{
                         //register unique word
                         uniqueWordsWithCount.Add(cleanedWord,1);
                        }
                    }

                   
                    if(numOfPaliWords != 0){
                        Console.WriteLine("You have "+ numOfPaliWords + " palindrome words in your paragraph");
                    }
                    else{
                        Console.WriteLine("No palindrome words found. Good bot.");
                        return;
                    }
                 

                    foreach(var sentences in allSentences){
                        if(string.IsNullOrWhiteSpace(sentences)) continue;
                        var newSentence = sentences.Replace(" ", "").ToLower();
                        //Clean up any symbols before checking if palindrome. 
                        newSentence = removeSymbols(newSentence);
                        var isSentencePali = isStringPali(newSentence);
                        if(isSentencePali) numOfPaliSentences++;
                    }
                    Console.WriteLine("You have " + numOfPaliSentences +" palindrome sentences in your paragraph.");

                    foreach(var word in wordsContainingLetter){
                        Console.WriteLine(word +" contains the ONE letter you entered previously: " + letterEntered);
                    }

                      /*PLEASE REVIEW
                    List the unique words of a paragraph with the count of the word instance
                    Not sure what is meant by "the count of the word instance", so I listed how many different unque values there were. Sorry for the confusion */

                    var uniqueValues = uniqueWordsWithCount.Where(x=>x.Value == 1).ToList(); 
                    foreach(var keyPair in uniqueValues){
                       Console.WriteLine(keyPair.Key + " is a unique word in this paragraph.");
                    }
                    Console.WriteLine("In total there are " + uniqueValues.Count() + " unique words in the paragraph entered.");
                    Console.WriteLine("Thank you for using Palibot!");
                }
               
           
        }
    
    }
}