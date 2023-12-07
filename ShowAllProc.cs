using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DobruyVecherYaDispetcher
{
    internal enum knopochki
    {
        closeEverything = -3,
        close = -2,
        flashback = -4
    }


    internal static class ShowAllProc
    {
        public static void ShowProc()
        {
           
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("                                               >> Диспетчер задач <<");
                Console.WriteLine("----------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine("      Название                         Физ. память                           Опер.Память                   Приоритет   ");
                Console.ResetColor();


                Process[] processes = Process.GetProcesses();

                foreach (Process process in processes)
                {
                    double phys = process.WorkingSet64 / 1024 / 1024;
                    double oper = process.VirtualMemorySize64 / 1024 / 1024;

                    Console.WriteLine("  " + process.ProcessName.ToString().PadRight(40) + phys.ToString().PadLeft(3) + " МБ ".PadLeft(3) + oper.ToString().PadLeft(35) + " МБ " + process.BasePriority.ToString().PadLeft(25));
               
                }


            int down = processes.Length + 2;
            int pos = ArrowsMenu.Show(3, 3, down);

            if (pos >= 0 && pos < processes.Length)
            {
                Console.Clear();
                ShowInProc(processes[pos-3]); 
              
            }

        }

        private static void ShowInProc(Process process)
        {
            knopochki click = 0;
            bool ex = true;

            while (true)
            {


                try
                {
                    Console.WriteLine(process);
                    Console.WriteLine("--------------------------------------------------------------------------------------------");
                    Console.WriteLine("                                > Информация о процессе <");
                    Console.WriteLine("--------------------------------------------------------------------------------------------");
                    Console.WriteLine("  Название процесса: " + process.ProcessName);
                    Console.WriteLine("  Идентификатор процесса: " + process.Id);
                    Console.WriteLine("  Физическая память: " + (process.WorkingSet64 / 1024 / 1024) + " МБ");
                    Console.WriteLine("  Оперативная память: " + (process.VirtualMemorySize64 / 1024 / 1024) + " МБ");
                    Console.WriteLine("  Приоритет: " + process.BasePriority);
                    Console.WriteLine("  Класс приоритета: " + process.PriorityClass);
                    Console.WriteLine("  Время использования процесса: " + process.UserProcessorTime);
                    Console.WriteLine("  Всё время использования: " + process.TotalProcessorTime);
                    Console.WriteLine("--------------------------------------------------------------------------------------------");
                }
                catch (Exception)
                
                {
             
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("  '   ' Отказано в доступе - остальная информация/данный процесс недоступны для отображения");
                Console.ResetColor();
                ex = false;
         

                }

                finally
                {
                    if (process.Responding == true)
                    {

                        Console.WriteLine("  Статус: запущен");

                    }
                    
                    if (process.Responding !=true)
                    {
                         Console.WriteLine(" Статус: не запущен");
                    }

                    Console.WriteLine("--------------------------------------------------------------------------------------------");
                }

                Console.WriteLine("  D - Завершить процесс");
                Console.WriteLine("  Del - Завершить все процессы с таким именем");

 

                if (ex == true)
                {
                    click = (knopochki)ArrowsMenu.Show(4, 4, 12);
                }

                if (ex != true)
                {
                    click = (knopochki)ArrowsMenu.Show(4, 4, 12);
                }
             
                
                switch (click)
                {
                 
                    case knopochki.flashback:
                        Console.Clear();
                        Program.Main();
                    return;
                    case knopochki.close:

                        Console.Clear();
                        if (ex == true)
                        {
                            process.Kill();
                            Console.ForegroundColor= ConsoleColor.Green;
                            Console.WriteLine("Процесс успешно завершен");
                            Console.ResetColor();
                            Console.WriteLine("Нажмите что-то, чтобы продолжить и вернуться в меню диспетчера задач");
                            Console.ReadLine();
                            Console.Clear();
                            Program.Main();

                        }
                        else
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Отказано в доступе");
                            Console.ResetColor();
                            Console.WriteLine("Нажмите что-то, чтобы продолжить и вернуться в меню диспетчера задач");
                            Console.ReadLine();
                            Console.Clear();
                            Program.Main();
                        }
                    return;

                    case knopochki.closeEverything:
                        var name = process.ProcessName;
                        Process[] forend = Process.GetProcessesByName(name);

                        try
                        {
                            foreach (Process procs in forend)
                            {
                                Console.Clear();
                                procs.Kill();
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine($"Процессы с именем {name} успешно завершены");
                                Console.ResetColor();
                                Console.WriteLine("Нажмите что-то, чтобы продолжить и вернуться в меню диспетчера задач");
                                Console.ReadLine();
                                Console.Clear();
                                Program.Main();

                            }
                        }

                        catch
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine($"Процессы с именем {name} нельзя завершить");
                            Console.ResetColor();
                            Console.WriteLine("Нажмите что-то, чтобы продолжить и вернуться в меню диспетчера задач");
                            Console.ReadLine();
                            Console.Clear();
                            Program.Main();
                        }


                        return;
                }
            }

        }
    }
}