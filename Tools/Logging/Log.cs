using System;
using System.IO;
using System.Collections.Generic;
using RGiesecke.DllExport;
using System.Runtime.InteropServices;

namespace Tools.Logging
{   
    public class Log
    {
        public Dictionary<string,string> UrgencyList;
        public Log(){
            UrgencyList = new Dictionary<string, string>();
        }
        public void AddUrgency(string name, string description){
            UrgencyList.Add(name, description);
        }
        public void WriteError(Exception e, string name)
        {   
            string helpMsg = UrgencyList[name];
            string errorLevel = name;
        
            Console.WriteLine($"A(n) {errorLevel.ToUpper()} error has occured. Please check the log.txt file for details.");
            Console.WriteLine(helpMsg);
            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            using (StreamWriter writer = new StreamWriter("log.txt", true))
            {
                writer.WriteLine($"A(n) {errorLevel.ToUpper()} Error occurred for {userName} at {DateTime.Now.ToString()}\n"); 
                writer.WriteLine($"Reason ------> {e.Message}\n");
                writer.WriteLine($"Exception ------> {e.ToString()}\n");
                writer.WriteLine($"Helpful Message ------> {helpMsg}\n");
                writer.WriteLine("------------------------END OF ERROR------------------------\n\n");
            }
        }
    }


    //Full COM support.
    public static class PythonLogger
    {
        [DllExport("add", CallingConvention = CallingConvention.Cdecl)]
        public static void WriteError(string trace, string error, string errorLevel, string helpMsg)
        {
            Console.WriteLine($"A(n) {errorLevel.ToUpper()} error has occured. Please check the log.txt file for details.");
            Console.WriteLine(helpMsg);
            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            using (StreamWriter writer = new StreamWriter("log.txt", true))
            {
                writer.WriteLine($"A(n) {errorLevel.ToUpper()} Error occurred for {userName} at {DateTime.Now.ToString()}\n");
                writer.WriteLine($"Reason ------> {error}\n");
                writer.WriteLine($"Exception ------> {trace}\n");
                writer.WriteLine($"Helpful Message ------> {helpMsg}\n");
                writer.WriteLine("------------------------END OF ERROR------------------------\n\n");
            }
        }
    }
}
