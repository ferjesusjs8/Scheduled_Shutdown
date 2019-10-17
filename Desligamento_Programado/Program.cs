using System;
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

            string[] choosenTime = GetChoosenTime();

            CalculateShutdown(choosenTime);

            CallEndingProccess(choosenTime);
        }

        #region Methods

        private static void CallEndingProccess(string[] choosenTime)
        {
            Console.WriteLine();
            if (!ValidateIfCancel(choosenTime))
                Console.WriteLine($" => Desligamento programado para as {choosenTime[0]}:{choosenTime[1]}hrs!!!");
            else
                Console.WriteLine("Agendamento Cancelado!");
            Thread.Sleep(4000);
            Console.WriteLine();
            Console.WriteLine("Bye Bye");
            Thread.Sleep(4000);
        }

        private static void ScheduleShutdown(TimeSpan timeChoosed)
        {
            var shutdownProccess = new ProcessStartInfo("shutdown", $" /s /t {Math.Round(timeChoosed.TotalSeconds).ToString()}");
            shutdownProccess.CreateNoWindow = true;
            shutdownProccess.UseShellExecute = false;
            Process.Start(shutdownProccess);
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
            else if (ValidateIfCancel(choosenTime))
            {
                CancelShutdown();
            }
        }

        private static bool ValidateIfCancel(string[] choosenTime) => choosenTime[0].Contains("C", StringComparison.InvariantCultureIgnoreCase);

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
