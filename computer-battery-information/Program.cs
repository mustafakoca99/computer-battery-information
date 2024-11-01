using System;
using System.Diagnostics;

namespace computer_battery_information
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Mustafa Koca | PC YÖNETİMİ";

            // Batarya Bilgisi
            RunPowerShellCommand("Get-WmiObject Win32_Battery | Select-Object Name, EstimatedChargeRemaining");

            // Çalışan Servisler
            RunPowerShellCommand("Get-WmiObject Win32_Service | Where-Object { $_.State -eq 'Running' -and $_.StartMode -eq 'Manual' } | Select-Object Name");

            // Çalışan Processler
            RunPowerShellCommand("Get-Process | Select-Object Name");

            // İşlemci Bilgisi
            RunPowerShellCommand("Get-WmiObject Win32_Processor | Select-Object Name");

            Console.ReadLine();
        }

        static void RunPowerShellCommand(string command)
        {
            using (Process powerShell = new Process())
            {
                powerShell.StartInfo.FileName = "powershell.exe";
                powerShell.StartInfo.Arguments = "-Command " + command;
                powerShell.StartInfo.RedirectStandardOutput = true;
                powerShell.StartInfo.UseShellExecute = false;
                powerShell.StartInfo.CreateNoWindow = true;

                powerShell.Start();

                string output = powerShell.StandardOutput.ReadToEnd();
                Console.WriteLine(output);

                powerShell.WaitForExit();
            }
        }
    }
}
