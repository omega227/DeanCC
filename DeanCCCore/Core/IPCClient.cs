using System;
using System.IO;
using System.IO.Pipes;

namespace DeanCCCore.Core
{
    public static class IPCClient
    {
        public static string Send(string pipeName, string str)
        {
            string result = string.Empty;
            using (NamedPipeClientStream pipeClient = new NamedPipeClientStream(".", pipeName, PipeDirection.InOut))
            {
                pipeClient.Connect();
                using (StreamReader sr = new StreamReader(pipeClient))
                using (StreamWriter sw = new StreamWriter(pipeClient))
                {
                    sw.WriteLine(str);
                    sw.Flush();
                    result = sr.ReadLine();
                }
            }
            return result;
        }
    }
}
