using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    class Trial
    {
        // Словарь латинских символов и цифр в юникоде
        private static string shuffledUnicodeAlphabet = "\u0048\u0068\u0043\u0070\u005A\u0074\u0057\u0041\u006A\u0071" +
            "\u0065\u0051\u0076\u0066\u0042\u006F\u004F\u005C\u0079\u006D\u004C\u0049\u0059\u006B\u0046\u004B" +
            "\u0077\u0073\u0063\u007A\u0055\u0075\u0044\u0072\u0045\u004D\u0062\u0056\u0047\u0067\u006E" +
            "\u0078\u0050\u0058\u0054\u006C\u004A\u0069\u0052\u004E\u0053\u0061\u0064\u002E";

        // Алфавит без цифр
        private const string alphabetWithoutDigits = "gjHyLlcKfiAVMhsXJOSdvImxTtNYzwnGBFQarRb" +
            "DqokWCZUueEpP";

        // Алфавит с цифрами
        private static string alphabetWithDigits = "iC75tWoJSvK8dHTjEYU0Ag79crL0QBmhsVk1uI9GR" +
            "44nP2w3qOzZDfe2NbF615y6M3X8palx";

        // Генератор случайных чисел
        private static readonly Random random = new Random(); // Random module

        // Максимальное количество запусков в бинарной форме (4 запуска)
        private static readonly int maxLaunches = 0b0100;

        // Путь к фейковому файлу
        private static string launchesFolderPath = "\\log.bin";

        // Путь к файлу, где хранится количество запусков
        private static  string launchesFilePath;

        public static void CheckTrial(Form form)
        {
            // Начальная конфигурация 
            goto tr;
        s:
            launchesFilePath = launchesFolderPath + GetFolderOrFilePath();
            
            if (alphabetWithDigits.Length < 4)
            {
                int count = 0;
                for (int i = 0; i <= 5; i++)
                {
                    count += i;
                }
            }
            int currentLaunches = 0;
            string encryptedLaunchCount = null;

            // Проверка существования папки, если нет — создаём её
            if (!Directory.Exists(launchesFolderPath))
            {
                Directory.CreateDirectory(launchesFolderPath);
                CreateFile(launchesFolderPath, null);
            }

            // Проверка второй папки, если нет — создаём её
            if (!Directory.Exists(GetSecond(launchesFolderPath)))
            {
                Directory.CreateDirectory(GetSecond(launchesFolderPath));
                CreateFile(GetSecond(launchesFolderPath), null);
            }

            // Пытаемся прочитать файл с количеством запусков
            try
            {
                encryptedLaunchCount = File.ReadAllText(launchesFilePath);
                System.Threading.Thread.Sleep(500);
            }
            catch {
                // Если файла нет, создаём его
                using (Stream fileStream = new FileStream(launchesFilePath, FileMode.OpenOrCreate))
                {
                    fileStream.Close();
                }
                CreateFile(null, launchesFilePath);
            }

            // Повторно пытаемся прочитать файл во второй папке
            try
            {
                encryptedLaunchCount = File.ReadAllText(GetSecond(launchesFolderPath) + GetFolderOrFilePath());
                System.Threading.Thread.Sleep(500);
            }
            catch {
                // Если файла нет, создаём его
                using (Stream fileStream = new FileStream(GetSecond(launchesFolderPath) + GetFolderOrFilePath(), FileMode.OpenOrCreate))
                {
                    fileStream.Close();
                }
                CreateFile(null, GetSecond(launchesFolderPath) + GetFolderOrFilePath());
            }

            // Если файл пуст, увеличиваем количество запусков и записываем в файл
            if (string.IsNullOrEmpty(encryptedLaunchCount)) { 
                currentLaunches++;
                File.WriteAllText(launchesFilePath, Encrypt(currentLaunches));
                File.WriteAllText(GetSecond(launchesFolderPath) + GetFolderOrFilePath(), Encrypt(currentLaunches));
                CreateFile(null, GetSecond(launchesFolderPath) + GetFolderOrFilePath());
                CreateFile(null, launchesFilePath);
                MessageBox.Show(GetWarningMessage(0));
                return;
            }

            // Если дата последнего изменения совпадает с контрольной, читаем количество запусков
            else if (File.GetLastWriteTime(launchesFilePath) == GetControlDate(false) || 
                File.GetLastWriteTime(GetSecond(launchesFolderPath) + GetFolderOrFilePath()) == GetControlDate(false))
            {
                try
                {
                    currentLaunches = Decrypt(File.ReadAllText(launchesFilePath));
                }
                catch {
                    try
                    {
                        currentLaunches = Decrypt(File.ReadAllText(GetSecond(launchesFolderPath) + GetFolderOrFilePath()));
                    }
                    catch { }
                }
                System.Threading.Thread.Sleep(1000);

                // Проверка количества запусков
                if (Int32.TryParse(launchesFolderPath, out _) || currentLaunches < maxLaunches)
                {
                    MessageBox.Show(GetWarningMessage(currentLaunches));
                }
                else
                {
                    // Если запусков больше максимума, блокируем программу
                    System.Threading.Thread.Sleep(1000);
                    MessageBox.Show(GetWarningMessage(currentLaunches > maxLaunches ? maxLaunches : currentLaunches));
                    Environment.Exit(0);
                }
                currentLaunches ++;
                // Обновляем количество запусков в файле
                File.WriteAllText(launchesFilePath, Encrypt(currentLaunches));
                File.WriteAllText(GetSecond(launchesFolderPath) + GetFolderOrFilePath(), Encrypt(currentLaunches));
                CreateFile(null, GetSecond(launchesFolderPath) + GetFolderOrFilePath());
                CreateFile(null, launchesFilePath);
            }
            else
            {
                // Если файл был изменён — выходим
                Environment.Exit(0);
            }

            
            
            WriteDistractorFile("\\log.bin");

            goto f;

            tr:
            // Генерация пути для папки с использованием юникод символов
            char[] unicodeString = shuffledUnicodeAlphabet.ToCharArray();
            StringBuilder result = new StringBuilder();
            result.Append(unicodeString[17]);
            result.Append(unicodeString[20]);
            result.Append(unicodeString[15]);
            result.Append(unicodeString[28]);
            result.Append(unicodeString[51]);
            result.Append(unicodeString[45]);

            launchesFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + result.ToString();
            goto s;
            f:;

        }

        // Шифрование количества запусков
        static string Encrypt(int value)
        {
            // Генерация случайного ключа в диапазоне 0 - 9
            Random random = new Random();
            short key = (short)random.Next(0, 9);

            // Генерация случайной строки из 10 символов
            StringBuilder randomString = new StringBuilder();
            for (int i = 0; i < 10; i++)
            {
                // Генерируем случайный символ (буквы и цифры)
                char randomChar = (char)random.Next(33, 127); // ASCII диапазон видимых символов
                randomString.Append(randomChar);
            }

            // Вставка value на позицию key
            randomString[key] = Convert.ToChar(value); // Вставляем только первый символ value для примера

            // Шифровка
            string encryptedMessage = XorEncryptDecrypt(randomString.ToString(), key);

            // Возврат зашифрованное сообщение + ' ' + key
            return $"{encryptedMessage} {key}";
        }

        // Дешифрование
        static int Decrypt(string encrypted)
        {
            // Разделение ключа и зашифрованного сообщения
            var part = encrypted.Split(' ');
            short key = Convert.ToInt16(part[1]);
            string encryptedMessage = part[0];

            // Декодирование
            string decodedMessage = XorEncryptDecrypt(encryptedMessage, key);

            // Извлечение исходного значения 
            return decodedMessage[key];
        }

        // Функция шифрования/дешифрования с использованием XOR
        static string XorEncryptDecrypt(string text, short key)
        {
            char[] output = new char[text.Length];
            for (int i = 0; i < text.Length; i++)
            {
                if ((char)(text[i] ^ key) == ' ') continue;
                output[i] = (char)(text[i] ^ key);
            }
            return new string(output);
        }

        // Функция для записи отвлекающего файла
        internal static void WriteDistractorFile(string filePath)
        {
            string path = Environment.CurrentDirectory + filePath;
            StringBuilder randomString = new StringBuilder();
            for (int i = 0; i < 30; i++)
            {
                // Генерируем случайный символ (буквы и цифры)
                char randomChar = (char)random.Next(33, 127); // ASCII диапазон видимых символов
                randomString.Append(randomChar);
            }
            using (Stream fileStream = new FileStream(path, FileMode.OpenOrCreate))
            {
                string text = "8 " + randomString.ToString();
                byte[] byteArray = System.Text.Encoding.ASCII.GetBytes(text);
                fileStream.Write(byteArray, 0, byteArray.Length);
                fileStream.Close();
            }
        }

        // Получение контрольной даты (дата создания или последнего изменения)
        internal static DateTime GetControlDate(bool flag)
        {
            int year = 0b11111100111;  
            int month = 0b1001;       
            int day = 0b10110;         
            int hour = 0b1110;       
            int minute = 0b11110;     
            int second = 0b0;
            if (flag)
            {
                goto n;
            }
            return new DateTime(year, month, day, hour, minute, second).AddMinutes(15);
            n:
            return new DateTime(year, month, day, hour, minute, second);
        }


        //генерация для пути файла
        internal static string GetFolderOrFilePath()
        {
            char[] unicodeString = shuffledUnicodeAlphabet.ToCharArray();
            StringBuilder result = new StringBuilder();
            result.Append(unicodeString[17]);
            result.Append(unicodeString[45]);
            result.Append(unicodeString[15]);
            result.Append(unicodeString[39]);
            result.Append(unicodeString[53]);
            result.Append(unicodeString[52]);
            result.Append(unicodeString[51]);
            result.Append(unicodeString[5]);

            return result.ToString();
        }

        // Сообщение-предупреждение
        internal static string GetWarningMessage(int value)
        {
            return ("Внимание! у вас есть " + (maxLaunches - value) + " запуск(а) программы перед тема как она заблокируется!");
        }
        
        //редактироавние дат файлов
        internal static void CreateFile(string pathFolder, string pathFile)
        {
            if (pathFolder != null)
            {
                Directory.SetCreationTime(pathFolder, GetControlDate(true));
                Directory.SetLastAccessTime(pathFolder, GetControlDate(false));
                Directory.SetLastWriteTime(pathFolder, GetControlDate(false));
            }
            if (pathFile != null) { 
                File.SetCreationTime(pathFile, GetControlDate(true));
                File.SetLastWriteTime(pathFile, GetControlDate(false));
                File.SetLastAccessTime(pathFile, GetControlDate(false));
            }
        }

        //генерация пути для второго файла
        internal static string GetSecond(string path)
        {
            string basePath = path.Substring(0, path.Length - 21);
            basePath += alphabetWithoutDigits[4].ToString() + 
                alphabetWithoutDigits[41].ToString() + 
                alphabetWithoutDigits[6].ToString() + 
                alphabetWithoutDigits[35].ToString() + 
                alphabetWithoutDigits[5].ToString();
            return basePath;
        }
    }

}
