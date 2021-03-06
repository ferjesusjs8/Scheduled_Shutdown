﻿using System;
using System.Diagnostics;
using System.Threading;

namespace Desligamento_Programado
{
    class Program
    {
        static void Main(string[] args)
        {
            InitializeProcess();
        }

        #region Process

        private static void InitializeProcess()
        {
            CallWelcome();

            string[] chosenTime = GetChoosenTime();

            CalculateShutdown(chosenTime);

            CallEndingProccess(chosenTime);
        }

        #region Methods

        private static void CallEndingProccess(string[] chosenTime)
        {
            Console.WriteLine();
            if (!CancelValidate(chosenTime))
                Console.WriteLine($" => Desligamento programado para as {chosenTime[0]}:{chosenTime[1]}hrs!!!");
            else
                Console.WriteLine("Agendamento Cancelado!");
            Thread.Sleep(4000);
            Console.WriteLine();
            Console.WriteLine("Bye Bye");
            Thread.Sleep(4000);
        }

        private static void ScheduleShutdown(TimeSpan selectedTime)
        {
            var shutdownProcess = new ProcessStartInfo("shutdown", $" /s /t {Math.Round(selectedTime.TotalSeconds).ToString()}");
            shutdownProcess.CreateNoWindow = true;
            shutdownProcess.UseShellExecute = false;
            Process.Start(shutdownProcess);
        }

        private static void CalculateShutdown(string[] choosenTime)
        {
            if (int.TryParse(choosenTime[0], out int choosenHour))
            {
                int.TryParse(choosenTime[1], out int choosenMinutes);

                DateTime.TryParse(DateTime.Now.ToString("MM/dd/yyyy"), out DateTime choosedHour);
                choosedHour = choosedHour.AddHours(choosenHour).AddMinutes(choosenMinutes);
                var timeChoosed = choosedHour.Subtract(DateTime.Now);

                ScheduleShutdown(timeChoosed);
            }
            else if (CancelValidate(choosenTime))
            {
                CancelShutdown();
            }
        }

        private static bool CancelValidate(string[] choosenTime) => choosenTime[0].Contains("C", StringComparison.InvariantCultureIgnoreCase);

        private static void CancelShutdown()
        {
            var shutdownProccess = new ProcessStartInfo("shutdown", $" /a");
            shutdownProccess.CreateNoWindow = true;
            shutdownProccess.UseShellExecute = false;
            Process.Start(shutdownProccess);
        }

        private static string[] GetChoosenTime()
        {
            Console.WriteLine("Digite o horário desejado para desligamento no formato 24 horas. Ex. ( 19:56 ).");
            Console.WriteLine("ou 'C' para cancelar algum agendamento!");
            Console.WriteLine();

            string writenValue = Console.ReadLine();
            string[] convertedWritenValue = { "" };

            if (writenValue.Contains(":"))
                convertedWritenValue = writenValue.Split(':');
            else
                convertedWritenValue.SetValue("C", 0);

            return convertedWritenValue;
        }

        private static void CallWelcome()
        {
            Console.WriteLine();
            Console.WriteLine(" -= Bem vindo ao sistema de desligamento automático =-");
            Console.WriteLine();
        }

        #endregion

        #endregion
    }
}
