using System;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;

class Program
{
    static async Task Main()
    {
        string url = "https://weather.com/weather/tenday/l/Austintown+OH?canonicalCityId=cd8ed603e32da2bdefab02a5bf1e6af8d415b4d6f5e99f6a10d0bb7a8952e1fd";

        using (HttpClient client = new HttpClient())
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string htmlContent = await response.Content.ReadAsStringAsync();

                    var doc = new HtmlDocument();
                    doc.LoadHtml(htmlContent);

                    
                    var temperatureXPath = "/html/body/div[1]/main/div[2]/main/div[1]/section/div[2]/div[2]/details[1]/div/div[1]/div/div[1]/span";
                    var conditionsXPath = "/html/body/div[1]/main/div[2]/main/div[1]/section/div[2]/div[2]/details[1]/div/div[1]/p";
                   
                    var temperatureNode = doc.DocumentNode.SelectSingleNode(temperatureXPath);
                    var conditionsNode = doc.DocumentNode.SelectSingleNode(conditionsXPath);

                    if (temperatureNode != null && conditionsNode != null)
                    {
                        var temperature = temperatureNode.InnerText.Trim();
                        var conditions = conditionsNode.InnerText.Trim();


                        Console.WriteLine($"Temperature: {temperature}");
                        Console.WriteLine($"Conditions: {conditions}");
                    }
                    else
                    {
                        Console.WriteLine("Error: Unable to locate weather data on the page.");
                    }
                }
                else
                {
                    Console.WriteLine("Error: Unable to fetch data from the website.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
