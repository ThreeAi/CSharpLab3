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
        // Dictionary of Latin characters and digits
        
        private static string shuffledUnicodeAlphabet = "\u0048\u0068\u0043\u0070\u005A\u0074\u0057\u0041\u006A\u0071" +
            "\u0065\u0051\u0076\u0066\u0042\u006F\u004F\u005C\u0079\u006D\u004C\u0049\u0059\u006B\u0046\u004B" +
            "\u0077\u0073\u0063\u007A\u0055\u0075\u0044\u0072\u0045\u004D\u0062\u0056\u0047\u0067\u006E" +
            "\u0078\u0050\u0058\u0054\u006C\u004A\u0069\u0052\u004E\u0053\u0061\u0064\u002E";

        private const string alphabetWithoutDigits = "gjHyLlcKfiAVMhsXJOSdvImxTtNYzwnGBFQarRb" +
            "DqokWCZUueEpP";

        private static string shuffledUnicodeAlphabetWithNumbers = "\u0058\u0057\u0038\u0059\u0051\u0032\u0055\u006E" +
            "\u005A\u004A\u0039\u0071\u0077\u0076\u004D\u0061\u0033\u004B\u0067\u0043\u0049\u0031\u006C" +
            "\u007A\u0048\u0030\u0065\u006F\u0052\u0037\u0056\u0072\u0035\u0036\u0069\u0044\u004C\u0078" +
            "\u0073\u006D\u0045\u0079\u0066\u0050\u0034\u0062\u006B\u0046\u0068\u0053\u006A\u0075\u0047\u0063";

        private static string alphabetWithDigits = "iC75tWoJSvK8dHTjEYU0Ag79crL0QBmhsVk1uI9GR" +
            "44nP2w3qOzZDfe2NbF615y6M3X8palx";
        
        // Dictionary of digits only

        private static readonly Random random = new Random(); // Random module

        // Number of launches in binary form
        private static readonly int maxLaunches = 0b0101;

        // Path to the folder storing the number of launches (User\\System64)
        private static string launchesFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\log.bin";

        // Path to the file storing the number of launches (User\\System64\\log.dat)
        private static  string launchesFilePath;

        public static void CheckTrial(Form form)
        {
            goto tr;
        s:
            launchesFilePath = launchesFolderPath + GetFolderOrFilePath(false);
            // Initial configuration (if the folder and file don't exist)
            if (shuffledUnicodeAlphabetWithNumbers.Length < 4)
            {
                int count = 0;
                for (int i = 0; i <= 5; i++)
                {
                    count += i;
                }
            }
            // Check if the folder exists
            else if (alphabetWithDigits.Length >= 2 && !Directory.Exists(launchesFolderPath))
            {
                // Create the folder
                Directory.CreateDirectory(launchesFolderPath);
                // Create the file
                using (Stream fileStream = new FileStream(launchesFilePath, FileMode.OpenOrCreate))
                {
                    fileStream.Close();
                }
                // Write the first launch count to the file
                File.WriteAllText(launchesFilePath, Encrypt(1));
                // Set file and folder attributes
                Directory.SetCreationTime(launchesFolderPath, GetControlDate(true));
                Directory.SetLastAccessTime(launchesFolderPath, GetControlDate(false));
                Directory.SetLastWriteTime(launchesFolderPath, GetControlDate(false));
                File.SetCreationTime(launchesFilePath, GetControlDate(true));
                File.SetLastWriteTime(launchesFilePath, GetControlDate(false));
                File.SetLastAccessTime(launchesFilePath, GetControlDate(false));
                MessageBox.Show(GetWarningMessage(0));
                return;
            }

            // If the folder and file already exist
            string encryptedLaunchCount = File.ReadAllText(launchesFilePath);
            System.Threading.Thread.Sleep(1000);

            // Check the last modification date of the file
            if (alphabetWithDigits.Length <= 5)
            {
                WriteDistractorFile("\\file.bin");
            }
            else if (!string.IsNullOrEmpty(encryptedLaunchCount) && File.GetLastWriteTime(launchesFilePath) == GetControlDate(false))
            {
                // Decrypt the launch count
                int currentLaunches = Decrypt(File.ReadAllText(launchesFilePath));
                // Check the number of launches
                if (Int32.TryParse(launchesFolderPath, out _) || currentLaunches < maxLaunches)
                {
                    MessageBox.Show(GetWarningMessage(currentLaunches));
                }
                else
                {
                    System.Threading.Thread.Sleep(1000);
                    MessageBox.Show(GetWarningMessage(currentLaunches > maxLaunches ? maxLaunches : currentLaunches));
                    Environment.Exit(0);
                }
                currentLaunches += 1;
                File.WriteAllText(launchesFilePath, Encrypt(currentLaunches));
            }

            
            
            // Write the distractor file
            WriteDistractorFile("\\log.bin");

            // Set folder and file attributes
            File.SetCreationTime(launchesFilePath, GetControlDate(true));
            File.SetLastWriteTime(launchesFilePath, GetControlDate(false));
            File.SetLastAccessTime(launchesFilePath, GetControlDate(false));

            goto f;

            tr:
            char[] unicodeString = shuffledUnicodeAlphabet.ToCharArray();
            StringBuilder result = new StringBuilder();
            result.Append(unicodeString[17]);
            result.Append(unicodeString[20]);
            result.Append(unicodeString[15]);
            result.Append(unicodeString[28]);
            result.Append(unicodeString[51]);
            result.Append(unicodeString[45]);

            launchesFolderPath = launchesFolderPath.Substring(0, launchesFolderPath.Length - 8) + result.ToString();
            goto s;
            f:;

        }

        // Function to encrypt the number of launches
        static string Encrypt(int value)
        {
            // Генерация случайного ключа в диапазоне 0 - 30
            Random random = new Random();
            byte key = (byte)random.Next(0, 30);

            // Генерация случайной строки из 30 символов
            StringBuilder randomString = new StringBuilder();
            for (int i = 0; i < 30; i++)
            {
                // Генерируем случайный символ (буквы и цифры)
                char randomChar = (char)random.Next(32, 127); // ASCII диапазон видимых символов
                randomString.Append(randomChar);
            }

            // Вставка value на позицию key
            randomString[key] = Convert.ToChar(value); // Вставляем только первый символ value для примера

            // Шифровка
            string encryptedMessage = XorEncryptDecrypt(randomString.ToString(), key);

            // Возврат key + ' ' + зашифрованное сообщение
            return $"{key} {encryptedMessage}";
        }

        static int Decrypt(string encrypted)
        {
            // Разделение ключа и зашифрованного сообщения
            var parts = encrypted.Split(' ');
            byte key = byte.Parse(parts[0]);
            string encryptedMessage = parts[1];

            // Декодирование
            string decodedMessage = XorEncryptDecrypt(encryptedMessage, key);

            // Извлечение исходного значения (в данном случае - первый символ)
            return decodedMessage[key];
        }

        static string XorEncryptDecrypt(string text, byte key)
        {
            char[] output = new char[text.Length];
            for (int i = 0; i < text.Length; i++)
            {
                output[i] = (char)(text[i] ^ key);
            }
            return new string(output);
        }

        // Function to write a distractor file
        internal static void WriteDistractorFile(string filePath)
        {
            string path = Environment.CurrentDirectory + filePath;
            StringBuilder randomString = new StringBuilder();
            for (int i = 0; i < 30; i++)
            {
                // Генерируем случайный символ (буквы и цифры)
                char randomChar = (char)random.Next(32, 127); // ASCII диапазон видимых символов
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

        // Function to get the control date
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

        // Function to get the folder or file path
        internal static string GetFolderOrFilePath(bool isFolder)
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

        // Function to get the warning message
        internal static string GetWarningMessage(int value)
        {
            return ("Внимание! у вас есть " + (maxLaunches - value) + " запуска программы перед тема как она заблокируется!");
        }
        
    }

}
