namespace FileFin3
{
    internal class Program
    {
        static long size = 0; 
        static long delsize = 0;
        static long delcount = 0;
        const double timelimit = 30;
        static void Main(string[] args)
        {
            Console.WriteLine("Текущее время: " + DateTime.Now);
            Console.Write("Введите путь до папки: ");
            string path = Console.ReadLine();
            DirFileSize(path);
            Console.WriteLine("Первоначальный размер: " + size);
            size = 0;
            DirFileDelete(path);
            Console.WriteLine("Размер удаленных файлов: " + delsize);
            Console.WriteLine("Количество удаленных файлов: " + delcount);
            DirFileSize(path);
            Console.WriteLine("Конечный размер: " + size);
        }
        static void DirFileSize(string dirName)
        {
            if (Directory.Exists(dirName))
            {
                try
                {
                    string[] dirs = Directory.GetDirectories(dirName);
                    foreach (string d in dirs)
                    {
                        DirFileSize(d);
                    }
                    string[] files = Directory.GetFiles(dirName);
                    foreach (string s in files)
                    {
                        var fileinfo = new FileInfo(s);
                        long filesize = fileinfo.Length;
                        Console.WriteLine(s + " размер: " + filesize);
                        size += filesize;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex}");
                }
            }
        }
        static void DirFileDelete(string dirName)
        {
            if (Directory.Exists(dirName))
            {
                try
                {
                    string[] dirs = Directory.GetDirectories(dirName);
                    foreach (string d in dirs)
                    {
                        bool rez = (DateTime.Now - File.GetLastWriteTime(d)).TotalMinutes >= timelimit;
                        Console.WriteLine(d + " Время: " + File.GetLastWriteTime(d) + " Подходит? " + rez);
                        DirFileDelete(d);
                        if (rez == true) Directory.Delete(d, true);
                        
                    }
                    string[] files = Directory.GetFiles(dirName);
                    foreach (string s in files)
                    {
                        bool rez = (DateTime.Now - File.GetLastWriteTime(s)).TotalMinutes >= timelimit;
                        Console.WriteLine(s + " Время: " + File.GetLastWriteTime(s) + " Подходит? " + rez);
                        if (rez == true)
                        {
                            var fileinfo = new FileInfo(s);
                            delsize += fileinfo.Length;
                            delcount++;
                            fileinfo.Delete();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex}");
                }
            }
        }
    }
}