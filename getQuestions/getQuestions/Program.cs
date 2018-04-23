using HtmlAgilityPack;
using System;

namespace getQuestions
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            do
            {
                HtmlWeb hw = new HtmlWeb();
                HtmlDocument doc = hw.Load(@"https://db.chgk.info/random/answers/complexity1/limit1");

                // //*[@id="main"]/div[3]/div/div/div === для раздатки
                // //*[@id="main"]/div[3]/div/img  === для картинки
                string allText, question, answer, setOff, comment, source, author, result, tournament, image;

                HtmlNode node = doc.DocumentNode.SelectSingleNode("//*[@id=\"main\"]/div[3]/div/img");

                try
                {
                    image = node.InnerText;
                }
                catch
                {
                    node = doc.DocumentNode.SelectSingleNode("//*[@id=\"main\"]/div[3]/div");

                    allText = node.InnerText;
                    node = doc.DocumentNode.SelectSingleNode("//*[@id=\"main\"]/div[3]/div/p[1]");

                    tournament = node.InnerText;
                    answer = getText(5, 'О', doc);
                    comment = getText(5, 'К', doc);
                    author = getText(5, 'А', doc);
                    result = getText(5, 'Р', doc);
                    source = getText(5, 'И', doc);
                    setOff = getText(5, 'З', doc);

                    question = delExtra(allText, answer, setOff, comment, source, author, result, tournament);

                    Console.WriteLine(question);
                }
            } while (true);
        }

        private static string getText(int position, char literal, HtmlDocument doc)
        {
            string temp;
            for (int i = 2; i <= 10; i++)
            {
                HtmlNode node = doc.DocumentNode.SelectSingleNode("//*[@id=\"main\"]/div[3]/div/p[" + i + "]");
                try
                {
                    temp = node.InnerText;
                    char[] text = temp.ToCharArray();
                    if (text[position] == literal)
                    {
                        return temp;
                    }
                }
                catch
                {
                }
            }
            return "...";
        }

        private static string delExtra(string allText, string answer, string setOff,
            string comment, string source, string author, string result, string tournament)
        {
            string question;

            question = allText.Replace(tournament, "");
            question = question.Replace(answer, "");
            question = question.Replace(comment, "");
            question = question.Replace(author, "");
            question = question.Replace(result, "");
            question = question.Replace(source, "");
            question = question.Replace(setOff, "");
            question = question.Replace("&nbsp;", "");
            question = question.Replace("&mdash", "");

            return question;
        }
    }
}