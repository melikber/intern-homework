using HtmlAgilityPack;
using System;
using System.IO;
using System.Linq;
using System.Net;

namespace SanctionScannerStajOdev
{
    class Program
    {
        private static string htmlFileName = "sahibindenTest.html";

        static void Main(string[] args)
        {
            var path = System.IO.Path.GetFullPath(@"..\..\..\");

            GetDataAndShowVitrinTitles(path + htmlFileName);
        }

        private static void GetDataAndShowVitrinTitles(string path)
        {
            string htmlPageSource = File.ReadAllText(path);

            var decodedHtmlPage = WebUtility.HtmlDecode(htmlPageSource);
            HtmlDocument htmlDocument = new HtmlDocument();

            htmlDocument.LoadHtml(decodedHtmlPage);

            var firstVitrinList = htmlDocument.DocumentNode.Descendants().Where(x => (x.Name == "ul" && x.Attributes["class"] != null &&
                                                                    x.Attributes["class"].Value.Equals("vitrin-list clearfix"))).ToList();
            
            var listHtmlString = firstVitrinList[0].InnerHtml;

            var innerHtml = new HtmlDocument();
            innerHtml.LoadHtml(listHtmlString);

            var vitrinList = innerHtml.DocumentNode.Descendants().Where(c=>c.Name == "li").ToList();

            foreach (var vitrinItem in vitrinList)
            {
                HtmlDocument vitrinHtml = new HtmlDocument();
                vitrinHtml.LoadHtml(vitrinItem.InnerHtml);

                var title = vitrinHtml.DocumentNode.InnerText;
                Console.WriteLine(title.Trim());
            }
        }
    }
}
