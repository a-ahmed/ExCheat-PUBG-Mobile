﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using ExSharpBase.Modules;
using ExSharpBase.Enums;

namespace ExSharpBase.Events
{
    class EventsManager
    {


        public static void SubscribeToEvents()
        {
            try
            {
                AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(OnProcessException);
                Base.OnTick += Main.OnMain;

                LogService.Log("Successfully Subscribed To Events");
            }
            catch (Exception Ex)
            {
                LogService.Log(Ex.ToString(), LogLevel.Error);
                throw new Exception("EventSubscriptionException");
            }
        }

        private static void OnProcessExit(object sender, EventArgs e)
        {
            Overlay.Drawing.DrawFactory.DisposeGraphicsFactory();
            Program.DrawBase.Close();
        }

        private static void OnProcessException(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine($"Error: {(e.ExceptionObject as Exception).Message}");

            Console.ReadKey();
        }
    }
}
