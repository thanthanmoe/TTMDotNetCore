﻿using TTMDotNetCore.ConsoleApp.RefitExamples;
using System;
using System.Threading.Tasks;
using TTMDotNetCore.ConsoleApp.AdoDotNetCoreExamples;
using TTMDotNetCore.ConsoleApp.DrapperExamples;
using TTMDotNetCore.ConsoleApp.HttpClientExamples;

namespace TTMDotNetCore.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // F5 => Run
            Console.WriteLine("Hello World!");

            // server name
            // database name
            // user name
            // password

            // Ctrl + ., enter => suggestion 
            // F9 => Breakpoint
            // Ctrl + R, R => Rename
            // F10 => summary trace
            // F11 => detail trace

            /* AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
             adoDotNetExample.Run();*/
            /* DrapperExample drapperExample = new DrapperExample();
             drapperExample.Run();*/
            Console.WriteLine("waiting for api... ");
            Console.ReadKey();
            RefitExample httpClientExample = new RefitExample();
            await httpClientExample.Run();

            Console.WriteLine("Press any key to continue... ");
            Console.ReadKey();
            //Console.ReadLine();

            // UI BL DA => Db
            // User Interface
            // Business Logic
            // Data Access
            // Database // CRUD -- Read, Create, Edit, Update, Delete
        }
    }
}

