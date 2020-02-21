using System;

namespace Test00
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var ud = new UserData();
            ud.Init();

            bool quit = false;
            
            /* pick up the last 6 months of data
               and put into a list of [month]->list data
             */

            var result = ud.GetHistoricData(DateTime.Now);
            

            while (!quit)
            {
                Console.WriteLine("Enter command:");
                
                string command = Console.ReadLine();

                switch (command.ToLower()[0])
                {
                    case 'x':
                        quit = true;
                        break;
                }
                
            }
        }
    }
}