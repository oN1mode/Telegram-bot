using System;
using System.Net;
using System.Xml;
using System.IO;


namespace Module_9
{
    /// <summary>
    /// Класс для получения данных по курсу валют с ЦБ РФ
    /// </summary>
    public static class CurrencyExchangeRate
    {
        public static string startUrl { get; }
        public static DateTime todayData { get; }
        public static double USD { get; set; }
        public static double EUR { get; set; }

        static CurrencyExchangeRate()
        {
            startUrl = @"http://www.cbr.ru/scripts/XML_daily.asp?date_req=";
            todayData = Convert.ToDateTime(DateTime.Today.ToShortDateString());
            ResponseProcessingCurrencyExchangeRate();
        }
        
        /// <summary>
        /// Метод для отправки запроса, получения ответа и присвоения значения актуальных курсов по валютам USD и EUR
        /// </summary>
        private static void ResponseProcessingCurrencyExchangeRate()
        {
            string url = String.Format(startUrl + todayData);

            WebRequest wRequest = WebRequest.Create(url);
            WebResponse wResponse = wRequest.GetResponse();
            if (wResponse == null) return;
            using (var stream = new StreamReader (wResponse.GetResponseStream()))
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(stream);

                XmlElement xRoot = xDoc.DocumentElement;

                foreach (XmlNode xNode in xRoot)
                {
                    if (xNode.Attributes.Count > 0)
                    {
                        XmlNode attr = xNode.Attributes.GetNamedItem("ID");
                        if (attr != null && attr.Value == "R01235") //USD
                        {
                            foreach (XmlNode childNode in xNode.ChildNodes) 
                                if (childNode.Name == "Value") USD = Double.Parse(childNode.InnerText);
                        }
                        else if (attr != null && attr.Value == "R01239") //EUR
                        {
                            foreach (XmlNode childNode in xNode.ChildNodes) 
                                if (childNode.Name == "Value") EUR = Double.Parse(childNode.InnerText);
                        }
                    }
                }
            }
        }
    }
}
