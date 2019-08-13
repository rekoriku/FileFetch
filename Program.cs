using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;

namespace FileFetch
{
    class Program
    {
        static readonly HttpClient client = new HttpClient();
        
        static async Task Main()
        {
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                Stream response = await client.GetStreamAsync("http://127.0.0.1:3000/file");
                await SaveStream(response, "./file.txt");
         
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }


        public static async Task CopyStream(Stream input, Stream output)
        {
            await input.CopyToAsync(output);
        }

        public static async Task SaveStream(Stream stream,string location)
        {
            FileStream fs =  File.Create(location);
            await CopyStream(stream, fs);
            fs.Close();
        }

    }
}
