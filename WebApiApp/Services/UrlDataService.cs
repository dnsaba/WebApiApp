using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using WebApiApp.Models.Domain;

namespace WebApiApp.Services
{
    public class UrlDataService
    {
        public UrlData GetCards (string url)
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            UrlData model = new UrlData();
            model.CardsInfo = new List<Card>();
            //assigning url to model
            model.Url = url;

            //loading html from provided url
            var htmlWeb = new HtmlWeb();
            HtmlDocument document = null;
            try
            {
                document = htmlWeb.Load(model.Url);

                List<string> cardNamesList = new List<string>();
                var xpath = "//span[@class='card_status']/strong";
                foreach (HtmlNode a in document.DocumentNode.SelectNodes(xpath))
                {
                    string name = a.InnerHtml;
                    cardNamesList.Add(name);
                }

                //List<string> cardDescriptions = new List<string>();
                //var xpath2 = "//dd[@class='box_card_text']";
                //foreach (HtmlNode a in document.DocumentNode.SelectNodes(xpath2))
                //{
                //    string des = a.InnerHtml;
                //    cardDescriptions.Add(des);
                //}

                var xpath3 = "//span[@class='atk_power']";
                List<int> attacks = new List<int>();
                foreach (HtmlNode a in document.DocumentNode.SelectNodes(xpath3))
                {
                    string des = a.InnerHtml.Substring(4);
                    attacks.Add(Int32.Parse(des));
                }

                var xpath4 = "//span[@class='def_power']";
                List<int> def = new List<int>();
                foreach (HtmlNode a in document.DocumentNode.SelectNodes(xpath4))
                {
                    string des = a.InnerHtml.Substring(4);
                    def.Add(Int32.Parse(des));
                }

                for (int i = 0; i<cardNamesList.Count; i++)
                {
                    Card cmodel = new Card();
                    cmodel.Name = cardNamesList[i];
                    //cmodel.Description = cardDescriptions[i];
                    cmodel.AttackLevel = attacks[i];
                    cmodel.DefenseLevel = def[i];
                    model.CardsInfo.Add(cmodel);
                }
            }
            catch (Exception ex)
            {
                model.Url = null;
            }
            return model;
        }
    }
}