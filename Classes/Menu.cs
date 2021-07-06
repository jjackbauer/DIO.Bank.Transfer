using System.Collections.Generic;
using System;
namespace DIO.Bank
{
    public abstract class Menu
    {
        public List<String> Headers = new List<String>();
        public List<Option> Options = new List<Option>();
        public char Terminator = 'X';

    
        public void Run()
        {   
            String userOption = "";
            
            do
            {
                bool InvalidOption=true;
                ListOptions();
                userOption = Console.ReadLine().ToUpper();
                

                foreach(Option o in Options)
                {
                    if(o.Command == (userOption.ToUpper())[0])
                    {
                        InvalidOption=false;
                        o.Handler();
                        break;
                    }

                }

                if(InvalidOption && userOption.ToUpper()[0] != Terminator)
                    throw new ArgumentOutOfRangeException();

            }while(userOption[0] != Terminator);

            Console.WriteLine("Thank you for using our services!");
            WaitForKey();

             
        }
        public Option setOption(String OptionName, char Command, bool Enabled, del Handler)
        {
            var aux = new Option();

            aux.OptionName=OptionName;
            aux.Command=Command;
            aux.Enabled=Enabled;
            aux.Handler=Handler;

            return aux;
        }
        
        public void ListOptions()
        {   
            Clear();

            foreach(String s in Headers)
                Console.WriteLine(s);
            

            foreach(Option o in Options)
            {
                Console.WriteLine("["+o.Command+"] - "+ o.OptionName);
            }

            Console.WriteLine("["+this.Terminator+"] - Exit Menu");

            Console.Write("Choose Wisely >");
        }

        public static void Clear()
        {
            for(int i=0; i<80; i++)
                Console.WriteLine("");
        }
        public static void WaitForKey(bool clear = true)
        {  
            Console.WriteLine("Press enter to continue...");
            String s = Console.ReadLine();

            if(clear)
                Clear();
        }

    }

    public struct Option
    {
        public del Handler;
        public String OptionName;
        public char Command;
        public bool Enabled;
    }

    public delegate void del();
}