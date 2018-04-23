using HtmlAgilityPack;
using System;

namespace getQuestions
{
    internal class Program
    {
        private static HtmlWeb hw = new HtmlWeb();
        private static HtmlDocument doc = hw.Load(@"https://db.chgk.info/random/answers/complexity1/limit1");

        private static void Main(string[] args)
        {
            string allText, question, answer, setOff, comment, source, author, result, tournament;

            HtmlNode node = doc.DocumentNode.SelectSingleNode("//*[@id=\"main\"]/div[3]/div");

            allText = node.InnerText;
            node = doc.DocumentNode.SelectSingleNode("//*[@id=\"main\"]/div[3]/div/p[1]");

            tournament = node.InnerText;
            answer = getText(5, 'О');
            comment = getText(5, 'К');
            author = getText(5, 'А');
            result = getText(5, 'Р');
            source = getText(5, 'И');
            setOff = getText(5, 'З');

            question = allText.Replace(tournament, "");
            question = question.Replace(answer, "");
            question = question.Replace(comment, "");
            question = question.Replace(author, "");
            question = question.Replace(result, "");
            question = question.Replace(source, "");
            question = question.Replace(setOff, "");

            Console.WriteLine(question);
        }

        private static string getText(int position, char literal)
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
    }
}