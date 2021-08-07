using System;
using System.Linq;
using System.Diagnostics;

namespace Processes
{
    internal static class Program : Object
    {
        private static void Main(String[] args)
        { }

        private static void GetInfoAboutAllProcesses()
        {
            var processes = from process in Process.GetProcesses()
                            orderby process.Id
                            select process;
            
            foreach (Process process in processes)
            {
                Console.WriteLine("Process ID: {1}{0}" +
                    "Process name: {2}{0}" +
                    "Process base priority: {3}{0}",
                    Environment.NewLine,
                    process.Id,
                    process.ProcessName,
                    process.BasePriority);
            }
        }

        private static void GetInfoAboutThreadsOfProcessByName(String processName)
        {
            var process = Process.GetProcessesByName(processName)[0];

            var processThreads = process.Threads;

            foreach (ProcessThread thread in processThreads)
            {
                Console.WriteLine("Thread ID: {1}{0}" +
                    "Start time: {2}{0}",
                    Environment.NewLine,
                    thread.Id,
                    thread.StartTime);
            }
        }

        private static void GetInfoAboutModulesOfProcessByName(String processName)
        {
            var process = Process.GetProcessesByName(processName)[0];

            var processModules = process.Modules;

            foreach (ProcessModule module in processModules)
            {
                Console.WriteLine("Name: {1}{0}" +
                    "Memory size: {2}{0}",
                    Environment.NewLine,
                    module.FileName,
                    module.ModuleMemorySize);
            }
        }

        private static void StartProcess(String fileName, String arguments = null)
        {
            var processStartInfo = new ProcessStartInfo(fileName);

            processStartInfo.Arguments = arguments;

            Process.Start(processStartInfo);
        }
    }
}