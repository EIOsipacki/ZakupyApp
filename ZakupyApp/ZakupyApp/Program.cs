
using ZakupyApp;


internal class Program
//program wyliczani statystyk zakupow w Podanym roku (tu = 2023)
// Po uruchomienu programu mamy Menu z możliwosciu wyboru:
//0 - wprowadzenia nowych paragonów (zachowują sie w list w pamięci i automatycznie
//    zapisują sie do pliku paragony.txt
//1 - wyliczanie i pokazanie Statystyk z pamieci; plus pokazanie listy Min i Max poragonów
//2 - wyliczanie statystyk z zapisanego pliku paragony.txt; plus pokazanie listy Min i Max poragonów z pliku
//3 - Wyjscie z programu
//  Do pliku log.txt dodaje zapisuje sie informacjia o zdrazeniach przy uruchomieniu Programu 
//          przez Eventy przy dodaniu paragonow; przez simple metody zapsiu w plik przy pracy w Menu , Uruchomieniu, Zamknięciu programu 

{
    public static void Main(string[] args)
    {
        
        int choice;
        const string listaParagonowPlik = "paragony.txt";
        List<ShopDataSuma> listaParagonow = new List<ShopDataSuma>();
        ParagonMemory paragonSumaMemory = new ParagonMemory(2023);
        ParagonFile paragonSumaPlik = new ParagonFile(2023);
        paragonSumaMemory.ParagonAddedEvent += ParagonSumaMemoryAdded;
        paragonSumaPlik.ParagonAddedEvent += ParagonAddedPlikAdded;

        void ParagonSumaMemoryAdded(object sender, EventArgs args)
        {
            using (var writer = File.AppendText("log.txt"))
            {
                writer.WriteLine($"{DateTime.Now} : Dodano nowy paragon");
            }
        }
        void ProgramBegin()
            //+ info do pliku log.xtx
        {
            using (var writer = File.AppendText("log.txt"))
            {
                writer.WriteLine($"{DateTime.Now} :Użytkownik '{Environment.UserName}' Uruchomił program na komputerze '{Environment.MachineName}',  system -  '{Environment.OSVersion} '");
            }
        }

        void ParagonAddedPlikAdded(object sender, EventArgs args)
        {
            using (var writer = File.AppendText("log.txt"))
            {
                writer.WriteLine($"{DateTime.Now} : Zapisany nowy paragon do pliku");
            }
        }

        void ProgramVyborMenu0()
        //+ info do pliku log.xtx
        {
            using (var writer = File.AppendText("log.txt"))
            {
                writer.WriteLine($"{DateTime.Now} : Wybrane 0 - Wprowadzenie paragonów do programu ");
            }
        }

        void ProgramVyborMenu1()
        //+ info do pliku log.xtx
        {
            using (var writer = File.AppendText("log.txt"))
            {
                writer.WriteLine($"{DateTime.Now} : Wybrane 1 - Statystyka z pamięci  ");
            }
        }

        void ProgramVyborMenu2()
        //+ info do pliku log.xtx
        {
            using (var writer = File.AppendText("log.txt"))
            {
                writer.WriteLine($"{DateTime.Now} : Wybrane 2 - Statystyka z pliku  ");
            }
        }

        void ProgramVyborMenu3()
        //+ info do pliku log.xtx
        {
            using (var writer = File.AppendText("log.txt"))
            {
                writer.WriteLine($"{DateTime.Now} : Zamknięcie programu  ");
            }
        }

        //ZACZYNA SIE WYKONANIE
        ProgramBegin();
        do
        {
            Console.Clear();
            //MENU
            Console.WriteLine("Menu programu wyliczania statystyk zakupów według paragonów :");
            Console.WriteLine("0. Wprowadzenie paragonów do programu");
            Console.WriteLine("1. Statystyka z pamięci ");
            Console.WriteLine("2. Statystyka z pliku ");
            Console.WriteLine("3. Wyjście");
            Console.Write("Wybierz opcję (1-3): ");

            if (int.TryParse(Console.ReadLine(), out choice))
            {
                switch (choice)
                {
                    case 0:
                        ProgramVyborMenu0();
                        Console.Clear();
                        MetodaInputFromKeyboard();

                        break;
                    case 1:
                        ProgramVyborMenu1();
                        Console.Clear();
                        MetodaParagonFromMemory();
                        break;
                    case 2:
                        ProgramVyborMenu2();
                        Console.Clear();
                        MetodaParagonFromFile();
                        break;
                    case 3:
                        ProgramVyborMenu3();
                        Console.Clear();
                        Console.WriteLine("Wybrano opcję 3 - Do widzenia!");
                        break;
                    default:
                        Console.WriteLine("Niepoprawny wybór. Wybierz liczbę od 0 do 3.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Niepoprawny wybór. Podaj liczbę od 0 do 3.");
            }

            Console.WriteLine();

        } while (choice != 3);

        void MetodaInputFromKeyboard()
            //Wprowadzenie paragonow z klawiatury
        {
            while (true)
            {
                ShopDataSuma paragon = WprowadzParagon();

                if (paragon == null)
                {
                    break;
                }
                else
                {
                    ZapiszDoPliku(paragon);
                    listaParagonow.Add(paragon);
                    paragonSumaMemory.AddParagon(paragon.Suma);
                }
                var statisticsfrommemory = paragonSumaMemory.GetStatistics();
            }
        }

        static ShopDataSuma WprowadzParagon()
            //Wprowadzenie jednego parargonu
        {
            Console.WriteLine("Wprowadź dane paragonu:");
            Console.Write("Nazwa Sklepu:  ('q/Q'- żeby zakończyć wprowadzanie)");
            string nazwaSklepu = Console.ReadLine();
            if (nazwaSklepu.ToUpper() == "Q")
                return null;
            DateTime dataZakupu;
            do
            {
                Console.Write("Data Zakupu (dd.MM.yyyy): ");
                string data = Console.ReadLine();

                if (DateTime.TryParseExact(data, "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out dataZakupu))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Niepoprawny format daty. Spróbuj ponownie.");
                }

            } while (true);

            Console.Write("Suma Zakupu: ");
            decimal sumaZakupu;
            while (!decimal.TryParse(Console.ReadLine(), out sumaZakupu))
            {
                Console.WriteLine("Niepoprawny format liczby. Wprowadź ponownie.");
            }
            return new ShopDataSuma(nazwaSklepu, dataZakupu, sumaZakupu);
        }

        static void ZapiszDoPliku(ShopDataSuma paragon)
            //Dodanie paragonu do txt pliku
        {

            using (var writer = File.AppendText(listaParagonowPlik))
            {

                writer.WriteLine($"{paragon.Shop};{paragon.Date.ToShortDateString()};{paragon.Suma:N2}");
            }
        }

        void MetodaParagonFromMemory()
            //Menu - 1 - Wyliczanie statystyk z pamięci 
        {
            if (listaParagonow.Count == 0)
            {
                Console.WriteLine(" Liasta paragonów pusta, dla wyliczania statystyk, prosze wprowadź kilka Paragonów przez wybor Menu punkt '0' ");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine(" --------------- LISTA PARAGONOW Z PAMIECI ---------------");
                foreach (var paragon in listaParagonow)
                {

                    Console.WriteLine($"{paragon.Shop} : {paragon.Date.ToShortDateString()} ; {paragon.Suma:N2}");
                }

                Console.WriteLine("");
                var statisticsfrommemory = paragonSumaMemory.GetStatistics();
                statisticsfrommemory.WriteLineStatistics();

                ////pokazanie Min i Max paragonow 
                Console.WriteLine("--------------- Minimalne paragony ---------------");
                foreach (var paragon in listaParagonow)
                {
                    if (paragon.Suma == statisticsfrommemory.Min)
                    {
                        Console.WriteLine($"{paragon.Shop} : {paragon.Date.ToShortDateString()} ; {paragon.Suma:N2}");
                    }
                }
                Console.WriteLine(" --------------- Maksymalne paragony ---------------");
                foreach (var paragon in listaParagonow)
                {
                    if (paragon.Suma == statisticsfrommemory.Max)
                    {
                        Console.WriteLine($"{paragon.Shop} : {paragon.Date.ToShortDateString()} ; {paragon.Suma:N2}");
                    }
                }
                Console.WriteLine("");
                Console.WriteLine("Press Any key to continue");
                Console.ReadLine();
            }
        }

        void MetodaParagonFromFile()
            //Wyliczanie stytstyk z txt pliku 
        {
            string line;

            if (File.Exists(listaParagonowPlik))
            {
                Console.WriteLine(" --------------- LISTA PARAGONOW Z PLIKU ---------------");
                using (var reader = File.OpenText(listaParagonowPlik))
                {
                    int kollines = 1;
                    line = reader.ReadLine();
                    while (line != null)
                    {
                        Console.WriteLine(line);
                        line = reader.ReadLine();
                        kollines++;
                    }
                }
                Console.WriteLine("");
                var statisticsFromFile = paragonSumaPlik.GetStatistics();
                statisticsFromFile.WriteLineStatistics();
                
                //pokazanie Min i Max paragonow z pliku 
                Console.WriteLine("--------------- Minimalne paragony ---------------");
                paragonSumaPlik.WriteLinieFromFileEqualNumaber(statisticsFromFile.Min);
                Console.WriteLine(" --------------- Maksymalne paragony ---------------");
                paragonSumaPlik.WriteLinieFromFileEqualNumaber(statisticsFromFile.Max);
                Console.WriteLine("");
                Console.WriteLine("Press Any key to continue");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Plik jest pusty, do wyliczania statystyk, prosze wprowadzić kilka Paragonów przez Menu punkt '0'");
                Console.ReadLine();
            }
        }
    }
}