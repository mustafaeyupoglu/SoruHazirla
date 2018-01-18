using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using SoruHazirla.Models;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using HtmlAgilityPack;

namespace SoruHazirla.Controllers
{
    public class HomeController : Controller
    {
       static List<Rss> feeds = new List<Rss>();
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
        // GET: Home
        public ActionResult Index()
        {
            
                ViewBag.titles = new SelectList(HaberCek(), "text", "baslik");

                return View();
            }
        SoruHazirlaEntities entity = new SoruHazirlaEntities();
        [HttpPost]
        public ActionResult Index(Sinavlar sinav)
        {
            sinav.Icerik = feeds.Find(x => x.baslik == sinav.Title).text.ToString();
            sinav.Tarih = DateTime.Now.ToString();
            sinav.Hazirlayan = Session["KullaniciAdi"].ToString() ;
            entity.Sinavlar.Add(sinav);
            entity.SaveChanges();


            ViewBag.titles = new SelectList(HaberCek(), "text", "baslik");
            return RedirectToAction("SinavListesi");
        }



        public ActionResult UyeOl()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UyeOl(Kullanici kullanici)
        {
            var query = entity.Kullanici.Find(kullanici.KullaniciAdi);
          
            if (query==null)
            {

                entity.Kullanici.Add(kullanici);
                entity.SaveChanges();
                return RedirectToAction("Login");

            }
            else
            {
                return RedirectToAction("UyeOl");
            }


        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Kullanici kullanici)
        {
            Kullanici usr = entity.Kullanici.Find(kullanici.KullaniciAdi);
            
            if (usr != null)
            {
                if (usr.Parola==kullanici.Parola)
                {
                    Session["KullaniciAdi"] = usr.KullaniciAdi;                  
                }
                
                return RedirectToAction("LoggedIn");
            }
            else
            {
                ModelState.AddModelError("", "Kullanıcı adı ve Şifre yanlış!");
            }

            return View();
        }
        public ActionResult LoggedIn() //login işlemin sonrası
        {
            if (Session["KullaniciAdi"] != null)
            {
                return RedirectToAction("SinavListesi");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult SinavListesi()
        {
            List<Sinavlar> query = (from s in entity.Sinavlar
                         select s).ToList<Sinavlar>();
            return View(query);

        }
        public ActionResult SinavSil(int id)
        {
            Sinavlar s = entity.Sinavlar.Find(id);  
            entity.Sinavlar.Remove(s);
            entity.SaveChanges();
            return RedirectToAction("SinavListesi");

        }
        public ActionResult Sinav(int id)
        {
            Sinavlar s=(from sinav in entity.Sinavlar
                        where sinav.SinavId == id
                        select (sinav)).First<Sinavlar>();
            return View(s);
        }

        [HttpPost]
        public string ShowSonuc(string c1, string c2, string c3, string c4,int sId)
        {
           
            Sinavlar s = (from sinav in entity.Sinavlar
                          where sinav.SinavId == sId
                          select (sinav)).First<Sinavlar>();
            var renk = new Dictionary<string, string>();
            renk["s1a"] = "#FFFFFF";
            renk["s1b"] = "#FFFFFF";
            renk["s1c"] = "#FFFFFF";
            renk["s1d"] = "#FFFFFF";

            renk["s2a"] = "#FFFFFF";
            renk["s2b"] = "#FFFFFF";
            renk["s2c"] = "#FFFFFF";
            renk["s2d"] = "#FFFFFF";

            renk["s3a"] = "#FFFFFF";
            renk["s3b"] = "#FFFFFF";
            renk["s3c"] = "#FFFFFF";
            renk["s3d"] = "#FFFFFF";

            renk["s4a"] = "#FFFFFF";
            renk["s4b"] = "#FFFFFF";
            renk["s4c"] = "#FFFFFF";
            renk["s4d"] = "#FFFFFF";

            string key1= "s1"+c1;
            string key2= "s2"+c2;
            string key3= "s3"+c3;
            string key4= "s4"+c4;

            if(c1==s.s1cevap){               
                renk[key1]="#00FF00";
            }
            else
	        {               
                renk[key1]="#FF0000";
	        }

             if(c2==s.s2cevap){               
                renk[key2]="#00FF00";
            }
            else
	        {               
                renk[key2]="#FF0000";
	        }

             if(c3==s.s3cevap){               
                renk[key3]="#00FF00";
            }
            else
	        {               
                renk[key3]="#FF0000";
	        }

             if(c4==s.s4cevap){               
                renk[key4]="#00FF00";
            }
            else
	        {               
                renk[key4]="#FF0000";
	        }

             string html = "<table><tr>            <td>1)" + s.s1 + "</td>            <td>2)" + s.s1 + "</td>        </tr>        <tr>            <td bgcolor=\"" + renk["s1a"] + "\">a)<input type=\"radio\" value=\"a\"  class=\"group1\" name=\"group1\">" + s.s1a + "</td>            <td bgcolor=\"" + renk["s2a"] + "\">a)<input type=\"radio\" value=\"a\"  class=\"group2\" name=\"group2\">" + s.s2a + "</td>        </tr>        <tr>            <td bgcolor=\"" + renk["s1b"] + "\">b)<input type=\"radio\" value=\"b\"  class=\"group1\" name=\"group1\">" + s.s1b + "</td>            <td bgcolor=\"" + renk["s2b"] + "\">b)<input type=\"radio\" value=\"b\"  class=\"group2\" name=\"group2\">" + s.s2b + "</td>        </tr>        <tr>            <td bgcolor=\"" + renk["s1c"] + "\" >c)<input type=\"radio\" value=\"c\" class=\"group1\" name=\"group1\">" + s.s1c + "</td>            <td bgcolor=\"" + renk["s2c"] + "\">c)<input type=\"radio\" value=\"c\"  class=\"group2\" name=\"group2\">" + s.s2c + "</td>        </tr>        <tr>            <td bgcolor=\"" + renk["s1d"] + "\">d)<input type=\"radio\" value=\"d\"  class=\"group1\" name=\"group1\">" + s.s1d + "</td>            <td bgcolor=\"" + renk["s2d"] + "\">d)<input type=\"radio\" value=\"d\" class=\"group2\" name=\"group2\">" + s.s2d + "</td>        </tr>        <tr>            <td>3)" + s.s3 + "</td>        <td>4)" + s.s4 + "</td>    </tr>    <tr>        <td bgcolor=\"" + renk["s3a"] + "\">a)<input type=\"radio\" value=\"a\" class=\"group3\" name=\"group3\">" + s.s3a + "</td>        <td bgcolor=\"" + renk["s4a"] + "\">a)<input type=\"radio\" value=\"a\" class=\"group4\" name=\"group4\">" + s.s4a + "</td>    </tr>    <tr>        <td bgcolor=\"" + renk["s3b"] + "\">b)<input type=\"radio\" value=\"b\" class=\"group3\" name=\"group3\">" + s.s3b + "</td>        <td bgcolor=\"" + renk["s4b"] + "\">b)<input type=\"radio\" value=\"b\" class=\"group4\" name=\"group4\">" + s.s4b + "</td>    </tr>    <tr>        <td bgcolor=\"" + renk["s3c"] + "\">c)<input type=\"radio\" value=\"c\" class=\"group3\" name=\"group3\">" + s.s3c + "</td>        <td bgcolor=\"" + renk["s4c"] + "\">c)<input type=\"radio\" value=\"c\" class=\"group4\" name=\"group4\">" + s.s4c + "</td>    </tr>    <tr>        <td bgcolor=\"" + renk["s4c"] + "\">d)<input type=\"radio\" value=\"d\"  class=\"group3\" name=\"group3\">" + s.s3d + "</td>        <td bgcolor=\"" + renk["s4d"] + "\">d)<input type=\"radio\" value=\"d\" class=\"group4\" name=\"group4\">"+s.s4d+"</td>    </tr></table>";

            return html;
        }

    }
}