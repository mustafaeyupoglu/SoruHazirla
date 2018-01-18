
SoruHazirla is an application for teachers to prepare exams for English articles. 
And users can solve the exams created in application.

#### Articles Source

This app is getting articles using rss data from [wired.com](wired.com). 

```
public List<Rss> HaberCek()
        {
            feeds.Clear();
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;//SSL için güvenli kanal oluşmadığı için çözüm

            string RssFeedUrl = "https://www.wired.com/feed/rss";
            

            XDocument xDoc = new XDocument();
            xDoc = XDocument.Load(RssFeedUrl);
            var items = (from x in xDoc.Descendants("item")
                         select new
                         {
                             title = x.Element("title").Value,
                             link = x.Element("link").Value,
                             catagory = x.Element("category").Value
                         }).Take(20);
            if (items != null)
            {
                foreach (var i in items)
                {
                    Uri url = new Uri(i.link);
                    WebClient client = new WebClient();
                    string html = client.DownloadString(url);
                    HtmlAgilityPack.HtmlDocument dokuman = new HtmlAgilityPack.HtmlDocument();
                    dokuman.LoadHtml(html);
                    HtmlNodeCollection basliklar = dokuman.DocumentNode.SelectNodes("//article");

                    Rss f = new Rss
                    {
                        baslik = i.title,
                        link = i.link,
                        category = i.catagory,
                        text = basliklar[0].InnerText
                    };
                    feeds.Add(f);
                }

            }
            return feeds;
        }
```

### User Guide

###### First of all user must be log in

###### After that all created exams will be listed. User can select;
* Sınava Başla(Start the exam)
* Sınav Oluştur(Create an exam)

###### When user want to create an exam;
  * User choose article first,
  * After that user write questions and answers for exam,
  * Create the exam.

###### When user want to solve an exam;
 * User choose exam first,
 * Then select the answers and complete the exam


#### Used technologies
- Asp.net Entity framework
- HtmlAgilityPack
- Ajax

Developers can install these technologies in NuGet Package Manager



https://steemit.com/@primus1

https://utopian.io/@primus1

Contribution link https://utopian.io/utopian-io/@primus1/soruhazirla-creating-exam-app
