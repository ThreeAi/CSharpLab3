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
        private static string lyTUjV = "\u0048\u0068\u0043\u0070\u005A\u0074\u0057\u0041\u006A\u0071" +
            "\u0065\u0051\u0076\u0066\u0042\u006F\u004F\u005C\u0079\u006D\u004C\u0049\u0059\u006B\u0046\u004B" +
            "\u0077\u0073\u0063\u007A\u0055\u0075\u0044\u0072\u0045\u004D\u0062\u0056\u0047\u0067\u006E" +
            "\u0078\u0050\u0058\u0054\u006C\u004A\u0069\u0052\u004E\u0053\u0061\u0064\u002E";

        // Алфавит без цифр
        private const string mnevoF = "gjHyLlcKfiAVMhsXJOSdvImxTtNYzwnGBFQarRb" +
            "DqokWCZUueEpP\\";

        // Алфавит с цифрами
        private static string bEAzQL = "iC75tWoJSvK8dHTjEYU0Ag79crL0QBmhsVk1uI9GR" +
            "44nP2w3qOzZDfe2NbF615y6M3X8palx";

        // Генератор случайных чисел
        private static readonly Random TTpRbg = new Random(); // Random module

        // Максимальное количество запусков в бинарной форме (4 запуска)
        private static readonly int fkxpEd = 0b0100;

        // Путь к фейковому файлу
        private static string UhDmFN = "\\log.bin";

        // Путь к файлу, где хранится количество запусков
        private static  string CzpoAx;

        public static void CheckTrial(Form form)
        {
            // Начальная конфигурация 
            goto tr;
        s:
            CzpoAx = UhDmFN + JoQAgY();
            
            if (bEAzQL.Length < 4)
            {
                int HLqeMK = 0;
                for (int i = 0; i <= 5; i++)
                {
                    HLqeMK += i;
                }
            }
            int YcgZUc = 0;
            string LWdNzT = null;

            // Проверка папки, если нет — создаём её
            try
            {
                if (!Directory.Exists(xNtuOj(UhDmFN)))
                {
                    Directory.CreateDirectory(xNtuOj(UhDmFN));
                    CEtYqH(xNtuOj(UhDmFN), null);
                }
            }
            catch 
            {
                MessageBox.Show("Ошибка создания папки");
            }

            // Пытаемся прочитать файл с количеством запусков
            try
            {
                LWdNzT = File.ReadAllText(CzpoAx);
                System.Threading.Thread.Sleep(500);
            }
            catch {
                try { 
                    // Если файла нет, создаём его
                    using (Stream NNeScR = new FileStream(CzpoAx, FileMode.OpenOrCreate))
                    {
                    NNeScR.Close();
                    }
                    CEtYqH(null, CzpoAx);
                }
                catch
                {
                    MessageBox.Show("Ошибка создания файла 1");
                }
            }

            // Повторно пытаемся прочитать файл во второй папке
            try
            {
                LWdNzT = File.ReadAllText(xNtuOj(UhDmFN) + JoQAgY());
                System.Threading.Thread.Sleep(500);
            }
            catch {
                try
                {
                    // Если файла нет, создаём его
                    using (Stream cfICjJ = new FileStream(xNtuOj(UhDmFN) + JoQAgY(), FileMode.OpenOrCreate))
                    {
                        cfICjJ.Close();
                    }
                    CEtYqH(null, xNtuOj(UhDmFN) + JoQAgY());
                }
                catch
                {
                    MessageBox.Show("Ошибка создания файла 2");
                }
            }

            // Если файл пуст, увеличиваем количество запусков и записываем в файл
            if (string.IsNullOrEmpty(LWdNzT)) { 
                YcgZUc++;
                File.WriteAllText(CzpoAx, qDahVS(YcgZUc));
                File.WriteAllText(xNtuOj(UhDmFN) + JoQAgY(), qDahVS(YcgZUc));
                CEtYqH(null, xNtuOj(UhDmFN) + JoQAgY());
                CEtYqH(null, CzpoAx);
                MessageBox.Show(SrACmY(0));
                return;
            }

            // Если дата последнего изменения совпадает с контрольной, читаем количество запусков
            else if (File.GetLastWriteTime(CzpoAx) == NNeScR(false) ||
                File.GetLastWriteTime(xNtuOj(UhDmFN) + JoQAgY()) == NNeScR(false))
            {
                try
                {
                    YcgZUc = HLqeMK(File.ReadAllText(CzpoAx));
                }
                catch {
                    try
                    {
                        YcgZUc = HLqeMK(File.ReadAllText(xNtuOj(UhDmFN) + JoQAgY()));
                    }
                    catch {
                        MessageBox.Show("Ошибка чтения");
                    }
                }
                System.Threading.Thread.Sleep(1000);

                // Проверка количества запусков
                if (int.TryParse(UhDmFN, out _) || Ndape(YcgZUc))
                {
                    MessageBox.Show(SrACmY(YcgZUc));
                }
                else
                {
                    // Если запусков больше максимума, блокируем программу
                    System.Threading.Thread.Sleep(1000);
                    MessageBox.Show(SrACmY(YcgZUc > fkxpEd ? fkxpEd : YcgZUc));
                    foreach (Control c in form.Controls)
                        c.Enabled = false;

                }
                YcgZUc ++;
                // Обновляем количество запусков в файле
                File.WriteAllText(CzpoAx, qDahVS(YcgZUc));
                File.WriteAllText(xNtuOj(UhDmFN) + JoQAgY(), qDahVS(YcgZUc));
                CEtYqH(null, xNtuOj(UhDmFN) + JoQAgY());
                CEtYqH(null, CzpoAx);
            }
            else
            {
                // Если файл был изменён — выходим
                foreach (Control c in form.Controls)
                    c.Enabled = false;
            }

            
            
            XZqfcD("\\log.bin");

            goto f;

            tr:
            // Генерация пути для папки с использованием юникод символов
            char[] MBYsSR = lyTUjV.ToCharArray();
            StringBuilder XYSpFJ = new StringBuilder();
            XYSpFJ.Append(MBYsSR[17]);
            XYSpFJ.Append(MBYsSR[20]);
            XYSpFJ.Append(MBYsSR[15]);
            XYSpFJ.Append(MBYsSR[28]);
            XYSpFJ.Append(MBYsSR[51]);
            XYSpFJ.Append(MBYsSR[45]);

            UhDmFN = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            goto s;
            f:;

        }

        // Шифрование количества запусков
        static string qDahVS(int zgagyM)
        {
            // Генерация случайного ключа в диапазоне 0 - 9
            Random NLZCdA = new Random();
            short mLXvmT = (short)NLZCdA.Next(0, 9);

            // Генерация случайной строки из 10 символов
            StringBuilder JoQAgY = new StringBuilder();
            for (int i = 0; i < 10; i++)
            {
                // Генерируем случайный символ (буквы и цифры)
                char gubTcs = (char)NLZCdA.Next(33, 127); // ASCII диапазон видимых символов
                JoQAgY.Append(gubTcs);
            }

            // Вставка value на позицию key
            JoQAgY[mLXvmT] = Convert.ToChar(zgagyM); // Вставляем только первый символ value для примера

            // Шифровка
            string lyTUjV = YcgZUc(JoQAgY.ToString(), mLXvmT);

            // Возврат зашифрованное сообщение + ' ' + key
            return $"{lyTUjV} {mLXvmT}";
        }

        // Дешифрование
        static int HLqeMK(string mnevoF)
        {
            // Разделение ключа и зашифрованного сообщения
            var bEAzQL = mnevoF.Split(' ');
            short TTpRbg = Convert.ToInt16(bEAzQL[1]);
            string fkxpEd = bEAzQL[0];

            // Декодирование
            string UhDmFN = YcgZUc(fkxpEd, TTpRbg);

            // Извлечение исходного значения 
            return UhDmFN[TTpRbg];
        }

        // Функция шифрования/дешифрования с использованием XOR
        static string YcgZUc(string LWdNzT, short udilWX)
        {
            char[] bjbmqh = new char[LWdNzT.Length];
            for (int i = 0; i < LWdNzT.Length; i++)
            {
                if ((char)(LWdNzT[i] ^ udilWX) == ' ') continue;
                bjbmqh[i] = (char)(LWdNzT[i] ^ udilWX);
            }
            return new string(bjbmqh);
        }

        static bool Ndape(int aBONe)
        {
            return aBONe < fkxpEd;
        }

        // Функция для записи отвлекающего файла
        internal static void XZqfcD(string eKepdE)
        {
            string hVAxVp = Environment.CurrentDirectory + eKepdE;
            StringBuilder PquEnd = new StringBuilder();
            for (int i = 0; i < 30; i++)
            {
                // Генерируем случайный символ (буквы и цифры)
                char EWMeJv = (char)TTpRbg.Next(33, 127); // ASCII диапазон видимых символов
                PquEnd.Append(EWMeJv);
            }
            using (Stream PAgCWQ = new FileStream(hVAxVp, FileMode.OpenOrCreate))
            {
                string ZUZbPR = "8 " + PquEnd.ToString();
                byte[] AtkSRo = System.Text.Encoding.ASCII.GetBytes(ZUZbPR);
                PAgCWQ.Write(AtkSRo, 0, AtkSRo.Length);
                PAgCWQ.Close();
            }
        }

        // Получение контрольной даты (дата создания или последнего изменения)
        internal static DateTime NNeScR(bool cfICjJ)
        {
            int MBYsSR = 0b11111100111;  
            int XYSpFJ = 0b1001;       
            int qDahVS = 0b10110;         
            int zgagyM = 0b1110;       
            int NLZCdA = 0b11110;     
            int mLXvmT = 0b0;
            if (cfICjJ)
            {
                goto n;
            }
            return new DateTime(MBYsSR, XYSpFJ, qDahVS, zgagyM, NLZCdA, mLXvmT).AddMinutes(15);
            n:
            return new DateTime(MBYsSR, XYSpFJ, qDahVS, zgagyM, NLZCdA, mLXvmT);
        }


        //генерация для пути файла
        internal static string JoQAgY()
        {
            char[] gubTcs = Trial.lyTUjV.ToCharArray();
            StringBuilder lyTUjV = new StringBuilder();
            lyTUjV.Append(gubTcs[17]);
            lyTUjV.Append(gubTcs[45]);
            lyTUjV.Append(gubTcs[15]);
            lyTUjV.Append(gubTcs[39]);
            lyTUjV.Append(gubTcs[53]);
            lyTUjV.Append(gubTcs[52]);
            lyTUjV.Append(gubTcs[51]);
            lyTUjV.Append(gubTcs[5]);

            return lyTUjV.ToString();
        }

        // Сообщение-предупреждение
        internal static string SrACmY(int xxqUGL)
        {
            return ("Внимание! у вас есть " + (fkxpEd - xxqUGL) + " запуск(а) программы перед тема как она заблокируется!");
        }
        
        //редактироавние дат файлов
        internal static void CEtYqH(string cxhqxE, string JGoGCM)
        {
            if (cxhqxE != null)
            {
                Directory.SetCreationTime(cxhqxE, NNeScR(true));
                Directory.SetLastAccessTime(cxhqxE, NNeScR(false));
                Directory.SetLastWriteTime(cxhqxE, NNeScR(false));
            }
            if (JGoGCM != null) { 
                File.SetCreationTime(JGoGCM, NNeScR(true));
                File.SetLastWriteTime(JGoGCM, NNeScR(false));
                File.SetLastAccessTime(JGoGCM, NNeScR(false));
            }
        }

        //генерация пути для второго файла
        internal static string xNtuOj(string yJBooC)
        {
            string CdtndB = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            CdtndB += mnevoF[52].ToString() +
                mnevoF[4].ToString() + 
                mnevoF[41].ToString() + 
                mnevoF[6].ToString() + 
                mnevoF[35].ToString() + 
                mnevoF[5].ToString();
            return CdtndB;
        }
    }

}
