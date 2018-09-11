using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace kfe.Mars.Console
{
    class Program

    {

        private static HttpClient _httpClient;

        static void Main(string[] args)
        {
            ConsoleKeyInfo keyPress = new ConsoleKeyInfo();

            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5000/api/v1/marsImaging/");
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));



            while (keyPress.Key != ConsoleKey.Escape)
            {
                System.Console.Clear();
                System.Console.WriteLine("      Welcome to");
                System.Console.WriteLine("Mars Imaging Retrieval");
                System.Console.WriteLine();
                System.Console.WriteLine("Please make your choice from the following Options");
                System.Console.WriteLine("1.  Retrieve Rover Images by Rover by Earth Date");
                System.Console.WriteLine("2.  Retrieve Rover Images by Rover by Mars Sol");
                System.Console.WriteLine("3.  Retrieve Rover Images by All Rovers by Earth Date");
                System.Console.WriteLine("4.  Retrieve Rover Images by All Rovers by Mars Sol");
                System.Console.WriteLine("5.  Retrieve Images by Text File");
                System.Console.WriteLine("Esc to Exit Mars Imaging Retrieval");
                System.Console.WriteLine();
                System.Console.WriteLine("Please Make your Selection now");

                keyPress = System.Console.ReadKey();
                System.Console.WriteLine();

                switch (keyPress.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        GetImagesByRoverAndEarthDate();
                        break;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        GetImagesByRoverAndSol();
                        break;
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        GetImagesForAllRoversByEarthDate();
                        break;
                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        GetImagesForAllRoversBySol();
                        break;
                    case ConsoleKey.D5:
                    case ConsoleKey.NumPad5:
                        GetImagesFromTextFile();
                        break;
                    default:
                        System.Console.WriteLine($"Invalid choice: {keyPress.Key.ToString()} ");
                        System.Console.WriteLine($"Please try again");
                        break;
                }
            }
        }

        private static async void GetImagesByRoverAndEarthDate()
        {
            System.Console.Clear();

            System.Console.WriteLine("Get Mars Images By Rover and Earth Date");
            System.Console.WriteLine();

            var selectedRover = GetRover();

            var earthDate = GetEarthDate();

            System.Console.WriteLine("Retrieving Images");

            var uri = $"earthDate?rover={selectedRover}&earthDate={earthDate}";

            await _httpClient.GetAsync(uri);

            System.Console.ReadKey();
        }

        private static async void GetImagesByRoverAndSol()
        {
            System.Console.Clear();
            System.Console.WriteLine("Get Mars Images By Rover and Mars Sol");
            System.Console.WriteLine();

            var selectedRover = GetRover();

            var selectedSol = GetSolDate();

            System.Console.WriteLine("Retrieving Images");

            var uri = $"solDate?rover={selectedRover}&solDate={selectedSol}";

            await _httpClient.GetAsync(uri);

            System.Console.ReadKey();
        }

        private static async void GetImagesForAllRoversByEarthDate()
        {

            System.Console.Clear();
            System.Console.WriteLine("Get Mars Images for all Rovers and Earth Date");
            System.Console.WriteLine();

            var earthDate = GetEarthDate();

            System.Console.WriteLine("Retrieving Images");

            var uri = $"earthDate?rover=All&earthDate={earthDate}";

            await _httpClient.GetAsync(uri);

            System.Console.ReadKey();
        }

        private static async void GetImagesForAllRoversBySol()
        {
            System.Console.Clear();
            System.Console.WriteLine("Get Mars Images for all Rovers and Mars Sol");
            System.Console.WriteLine();

            var earthDate = GetSolDate();

            System.Console.WriteLine("Retrieving Images");

            var uri = $"solDate?rover=All&solDate={earthDate}";

            await _httpClient.GetAsync(uri);

            System.Console.ReadKey();
        }

        private static void GetImagesFromTextFile()
        {
            System.Console.Clear();
            System.Console.WriteLine("Get Mars Images from input file");
            System.Console.WriteLine();

            var fileExists = false;

            string filename = string.Empty;

            while (!fileExists)
            {
                System.Console.Write("Please enter or paste the import file here: ");

                filename = System.Console.ReadLine();
                if (string.IsNullOrWhiteSpace(filename))
                {
                    return;
                }

                fileExists = File.Exists(filename);

                if (!fileExists)
                {
                    System.Console.WriteLine($"File {filename} does not exist, please try again");
                }
            }

            System.Console.WriteLine("Retrieving Images");

            using (StreamReader sr = new StreamReader(filename))
            {
                while (!sr.EndOfStream)
                {
                    var inpLine = sr.ReadLine().Split("|");
                    DateTime importDate;

                    DateTime.TryParse(inpLine[0], out importDate);

                    if (importDate != DateTime.MinValue)
                    {
                        CallTheEndpoint(
                            importDate,
                            (Constants.Rovers)Enum.Parse(typeof(Constants.Rovers), inpLine[1])
                            );
                    }
                    else
                    {
                        System.Console.WriteLine($"Really?? Everyone knows that's a bad Date: {inpLine[0]}");
                    }
                }
            }

            System.Console.ReadLine();

        }

        private static string GetRover()
        {

            int selectedValue = 0;

            while (selectedValue < 1 || selectedValue > 3)
            {
                System.Console.WriteLine("1.  Curiosity");
                System.Console.WriteLine("2.  Opportunity");
                System.Console.WriteLine("3.  Spirit");
                System.Console.Write("Please select Rover:");

                var selectedRover = System.Console.ReadKey();
                System.Console.WriteLine();

                Int32.TryParse(selectedRover.KeyChar.ToString(), out selectedValue);
                if (selectedValue < 0 || selectedValue > 3)
                {
                    System.Console.WriteLine($"Invalid Choice {selectedRover.ToString()}");
                }
            }

            switch (selectedValue )
            {
                case 1:
                    return "Curiosity";
                case 2:
                    return "Opportunity";
                case 3:
                    return "Spirit";
            }

            return "unknown";
          

        }

        private static DateTime GetEarthDate()
        {
            bool validDate = false;
            DateTime validatedDate = new DateTime();

            while (!validDate)
            {

                System.Console.Write("Please enter the Earth Date:");

                var selectedDate = System.Console.ReadLine();

                validDate = DateTime.TryParse(selectedDate, out validatedDate);

                if (!validDate)
                {
                    System.Console.WriteLine("Invalid Date");
                }

            }

            return validatedDate;
        }

        private static int GetSolDate()
        {
            int solDate = 0;

            while (solDate <= 0)
            {
                System.Console.Write("Please enter Mars Sol:");
                var selectedSol = System.Console.ReadLine();

                bool validSol = Int32.TryParse(selectedSol, out solDate);
                if (!validSol)
                {
                    System.Console.WriteLine($"Invalid Mars Sol {selectedSol}");
                }
            }

            return solDate;
        }


        private static async void CallTheEndpoint(DateTime earthDate, Constants.Rovers rover)
        {
            var uri = $"earthDate?rover={rover}&earthDate={earthDate}";

            var response = await _httpClient.GetAsync(uri);

        }

    }
}
