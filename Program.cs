using Npgsql;
using System.Diagnostics;
using System.Security;

namespace ConsoleApp5
{
    internal class Program
    {

        static void Main(string[] args)
        {
            try
            {
                // Параметры подключения к базе данных PostgreSQL
                string host = "localhost";
                int port = 5432;
                string database = "Testdb";
                string username = "postgres";
                string password = "1"; // Укажите свой пароль

                // Путь для сохранения резервной копии
                string backupPath = Environment.CurrentDirectory;
                var qs = $" --host = localhost --port = 5432 --username = postgres  --verbose --format=c --blobs {database}";

                // Генерация имени файла резервной копии с текущей датой и временем
                string backupFile = Path.Combine(backupPath, $"{Guid.NewGuid()}.sql");

                // Формирование аргументов команды pg_dump
                string arguments = $"--host={host} --port={port} --username={username} --password=1 --verbose --format=c --blobs  --dbname={database} --file={backupFile}";
                SecureString secureString = new SecureString();
                secureString.InsertAt(0, '1');

                // Создание процесса для выполнения команды pg_dump
                var process = new Process();
                process.StartInfo.FileName = "pg_dump";
                process.StartInfo.Arguments = arguments;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.EnvironmentVariables["PGPASSWORD"] = password;

                process.Start();
                process.StandardInput.WriteLine(password);
                process.StandardInput.Flush();
                process.StandardInput.Close();
                process.WaitForExit(5000); 
                Console.WriteLine($"Резервная копия успешно создана: {backupFile}");
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.Message.ToString());
            }
     //       KeySend.KeyPress(Keys.P);

            Console.WriteLine("Нажмите  чтобы завершить");
            Console.ReadKey();
            //  Console.ReadLine();
            Environment.Exit(0);

            
            // Закрытие окна консольного приложения
          //  TimeSpan timeSpan = TimeSpan.FromSeconds(0.2d);


            // Чтение вывода и ожидание завершения процесса

            //string output = process.StandardOutput.ReadToEnd() ;
            //    //string error = process.StandardError.ReadToEnd();
            //process.Close();
            //  process.Kill();

            // Получение вывода стандартного потока и потока ошибок
            //      string errors = process .StandardInput.NewLine ="1";
            // string error = process.StandardError.ReadToEnd();
            // Ожидание завершения процесса
            //         process.WaitForExit();

            //var processStartInfo = new ProcessStartInfo
            //{
            //    Arguments = @"--dbname=postgresql://user_name:pass_word@Localhost:5432/bd_name_to_save -F c -b -f output_bd_name",
            //    CreateNoWindow = true,
            //    FileName = "pg_dump",
            //    UseShellExecute = false,
            //    WindowStyle = ProcessWindowStyle.Hidden,
            //    RedirectStandardInput = true
            //};
            //  process.();

            //Process processы = new Process() { StartInfo = processStartInfo, EnableRaisingEvents = true };



            //    process.WaitForExit ()  ;  

            // Console.ReadLine();


            //  process.WaitForExit();
            //   process.Close();
            // Проверка наличия ошибок

            //if (process.ExitCode == 0)
            //{
            //}
            //else
            //{
            //    Console.WriteLine("Ошибка при создании резервной копии: {error}");
            //}
            // Console.ReadLine();
        }
    }
}

