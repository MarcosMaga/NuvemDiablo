using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NuvemDiablo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string gameDir = "";
            string saveGameDir = "";
            string cloudDir = "";
            string gameProcces = "";

            try
            {
                StreamReader config = new StreamReader("config.txt");
                cloudDir = config.ReadLine();
                saveGameDir = config.ReadLine();
                gameDir = config.ReadLine();
                gameProcces = config.ReadLine();
            }
            catch
            {
                Environment.Exit(1);
            }

            try
            {
                var arch = Directory.GetFiles(cloudDir);

                foreach (string arq in arch)
                {
                    string arqDestino = Path.GetFileName(arq);
                    File.Copy(arq, $"{saveGameDir}\\{arqDestino}", true);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Environment.Exit(1);
            }

            Process.Start(gameDir);
            Thread.Sleep(3000);

            while (true)
            {
                Thread.Sleep(5000);
                Process[] all = Process.GetProcesses();
                List<string> names = new List<string>();
                foreach(Process proc in all)
                    names.Add(proc.MainWindowTitle);

                if (!names.Contains(gameProcces))
                {
                    Thread.Sleep(3000);
                    try
                    {
                        var arch = Directory.GetFiles(saveGameDir);

                        foreach (string arq in arch)
                        {
                            string arqDestino = Path.GetFileName(arq);
                            File.Copy(arq, $"{cloudDir}\\{arqDestino}", true);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        Environment.Exit(1);
                    }
                    finally
                    {
                        Environment.Exit(1);
                    }

                }
            }      
        }
    }
}
