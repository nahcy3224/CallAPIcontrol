using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CallAPIcontrol
{
    class Program
    {
        static void Main(string[] args)
        {
            // Declare variables and then initialize to zero.
            string uri="";string body = ""; string httpresponse = "";


            // Display title as the C# console calculator app.
            Console.WriteLine("Postman is here in C#\r");
            Console.WriteLine("------------------------\n");

            // Ask the user to type the first number.
            Console.WriteLine("Enter your URL:");
            uri = Convert.ToString(Console.ReadLine());
           
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);

            // Ask the user to choose an option.
            Console.WriteLine("Choose HTTP Method:");
            Console.WriteLine("\tg - GET");
            Console.WriteLine("\tp - POST");
            Console.WriteLine("\tput - PUT");
            Console.WriteLine("\td - DELETE");
            Console.Write("Which Method? ");

            // Use a switch statement to do the math.
            switch (Console.ReadLine())
            {
                case "g":
                    request.Method = "GET";
                    request.ContentType = "application/json";
                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    using (Stream stream = response.GetResponseStream())
                    using (StreamReader reader = new StreamReader(stream))
                    {

                        var result = reader.ReadLine();
                        string[] results = result.Split(',');
                        //分割文字斷行
                        foreach (var item in results)
                        {
                            httpresponse += item + "\r\n";
                        }
                    } 
                    //Console.WriteLine(httpresponse);
                    break;
                case "p":
                    Console.WriteLine("Enter your Http Body?");
                    body = Convert.ToString(Console.ReadLine());
                    
                    if (!string.IsNullOrEmpty(body))
                    {
                        request.ContentType = "application/json";
                        request.Method = "POST";

                        using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                        {
                            streamWriter.Write(body);
                            streamWriter.Flush();
                            streamWriter.Close();
                        }
                    }
                    using (HttpWebResponse webresponse = request.GetResponse() as HttpWebResponse)
                    {
                        using (StreamReader reader = new StreamReader(webresponse.GetResponseStream()))
                        {
                            string response = reader.ReadToEnd();

                            //textBox2.Text = response;

                            string[] results = response.Split(',');
                            //分割文字斷行
                            foreach (var item in results)
                            {
                                httpresponse += item + ",\r\n";
                            }
                        }
                    }
                    //Console.WriteLine(httpresponse);
                    break;
                
                case "put":
                    Console.WriteLine("Enter your Http Body?");
                    body = Convert.ToString(Console.ReadLine());

                    if (!string.IsNullOrEmpty(body))
                    {
                        request.ContentType = "application/json";
                        request.Method = "PUT";

                        using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                        {
                            streamWriter.Write(body);
                            streamWriter.Flush();
                            streamWriter.Close();
                        }
                    }
                    using (HttpWebResponse webresponse = request.GetResponse() as HttpWebResponse)
                    {
                        using (StreamReader reader = new StreamReader(webresponse.GetResponseStream()))
                        {
                            string response = reader.ReadToEnd();
                            httpresponse = response;
                        }
                    }
                    //Console.WriteLine(httpresponse);
                    break;

                case "d":
                    request.Method = "DELETE";
                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    using (Stream stream = response.GetResponseStream())
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        var result = reader.ReadToEnd();
                        httpresponse = result;
                    }
                    //Console.WriteLine(httpresponse);
                    break;
            }
            Console.WriteLine(httpresponse);
            // Wait for the user to respond before closing.
            Console.Write("Press any key to close the app...");
            Console.ReadKey();
        }
    }
}
