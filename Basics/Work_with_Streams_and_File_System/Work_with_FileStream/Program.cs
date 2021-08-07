using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Work_with_FileStream
{
    internal static class Program : Object
    {
        private static async Task Main(String[] args)
        {
            var path = "Temp";

            // создаем каталог для файла
            CreateDirectory(path);

            await WriteInFileAsync(path, "note");

            await ReadFromFileAsync(path, "note");

            Test_Seek_Method();
        }

        private static void CreateDirectory(String path)
        {
            if (path == null)
            {
                throw new ArgumentNullException("dirName");
            }

            if (!Directory.Exists(path))
            {
                var dirInfo = new DirectoryInfo(path);

                dirInfo.Create();
            }
        }

        private static async Task WriteInFileAsync(String path, String fileName)
        {
            Console.WriteLine("Введите строку для записи в файл:");
            var input = Console.ReadLine();

            // запись в файл
            using (var fs = new FileStream($@"{path}\{fileName}.txt", FileMode.OpenOrCreate))
            {
                // преобразуем строку в массив байтов
                var array = Encoding.UTF8.GetBytes(input);

                // запись массива байтов в файл
                await fs.WriteAsync(array, 0, array.Length);

                Console.WriteLine("Текст записан в файл");
            }
        }

        private static async Task ReadFromFileAsync(String path, String fileName)
        {
            // чтение из файла
            using (var fs = File.OpenRead($@"{path}\{fileName}.txt"))
            {
                // преобразуем строку в байты
                var array = new byte[fs.Length];

                // считываем данные
                await fs.ReadAsync(array, 0, array.Length);

                // декодируем байты в строку
                string textFromFile = Encoding.UTF8.GetString(array);

                Console.WriteLine($"Текст из файла: {textFromFile}");
            }
        }

        private static void Test_Seek_Method()
        {
            var text = "Hello, World!";

            // запись в файл
            using (var fs = new FileStream(@"Temp/temp.txt", FileMode.OpenOrCreate))
            {
                // преобразуем строку в байты
                var input = Encoding.UTF8.GetBytes(text);

                // запись массива байтов в файл
                fs.Write(input, 0, input.Length);
                Console.WriteLine("Текст записан в файл");

                // перемещаем указатель в конец файла, до конца файла -пять байт
                fs.Seek(-5, SeekOrigin.End); // минус 5 символов с конца потока

                // считываем четыре символов с текущей позиции
                var output = new byte[4];
                fs.Read(output, 0, output.Length);

                // декодируем байты в строку
                var textFromFile = Encoding.UTF8.GetString(output);
                Console.WriteLine($"Текст из файла: {textFromFile}"); // orld

                // заменим в файле слово World на словосочетание My Lovely World!
                var replaceText = "My Lovely World!";
                fs.Seek(-6, SeekOrigin.End); // минус 6 символов с конца потока

                input = Encoding.UTF8.GetBytes(replaceText);
                fs.Write(input, 0, input.Length);

                // считываем весь файл
                // возвращаем указатель в начало файла
                fs.Seek(0, SeekOrigin.Begin);
                output = new byte[fs.Length];
                fs.Read(output, 0, output.Length);

                // декодируем байты в строку
                textFromFile = Encoding.UTF8.GetString(output);
                Console.WriteLine($"Текст из файла: {textFromFile}"); // Hello, My Lovely World!
            }
        }
    }
}

/*
 * 
 *FileStream fstream = null;
 *
 *try
 *{
 *    fstream = new FileStream(@"D:\note3.dat", FileMode.OpenOrCreate);
 *    // операции с потоком
 *}
 *catch(Exception ex)
 *{
 * 
 *}
 *finally
 *{
 *    if (fstream != null)
 *    {
 *        fstream.Close();
 *    }
 *}
 *
 */