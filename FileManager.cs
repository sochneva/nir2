using nir2.DataModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nir2
{
    static class FileManager
    {
        //загрузка модели данных
        public static DataModel ReadFromFile(string foldername)
        {
            try
            {
                if (File.Exists(foldername + @"\param.txt"))
                    return new ParamFunction(ReadFromFileWithParam(foldername));
                else
                    return new SimpleFunction(ReadFromFileWithoutParam(foldername));
            }
            catch (Exception e)
            {
                Console.WriteLine("Не удалось загрузить файл: " + e.Message);
                return null;
            }
        }

        //Загрузка с параметром
        public static Dictionary<double, double[,]> ReadFromFileWithParam(string foldername)
        {
            try
            {
                Dictionary<double, double[,]> data = new Dictionary<double, double[,]>();
                double[] parametrs = ReadParam(foldername + @"\param.txt");
                //var dir = new DirectoryInfo(foldername);
                int j = 0;
                foreach (FileInfo file in new DirectoryInfo(foldername).GetFiles("*.txt"))
                {
                    if (file.Name == "param.txt") continue;

                    int i = 0;
                    string line = string.Empty;
                    double[,] localData = new double[2, File.ReadLines(file.FullName).Count()];

                    using (StreamReader sr = new StreamReader(file.FullName))
                    {

                        while ((line = sr.ReadLine()) != null)
                        {
                            var param = GetXY(line);
                            localData[0, i] = param.Item1;
                            localData[1, i] = param.Item2;
                            i++;
                        }
                    }
                    data.Add(parametrs[j], localData);
                    j++;
                }
                return data;
            }
            catch (Exception e)
            {
                Console.WriteLine("Не удалось загрузить файл: " + e.Message);
                return null;
            }
        }

        //Загрузка без параметра
        public static double[,] ReadFromFileWithoutParam(string foldername)
        {
            try
            {
                FileInfo file = new DirectoryInfo(foldername).GetFiles("*.txt")[0];

                int i = 0;
                string line = string.Empty;
                double[,] data = new double[2, File.ReadLines(file.FullName).Count()];

                using (StreamReader sr = new StreamReader(file.FullName))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        var param = GetXY(line);
                        data[0, i] = param.Item1;
                        data[1, i] = param.Item2;
                        i++;
                    }
                }

                return data;
            }
            catch (Exception e)
            {
                Console.WriteLine("Не удалось загрузить файл: " + e.Message);
                return null;
            }
        }

        //Чтение файла с параметром
        public static double[] ReadParam(string filename)
        {
            try
            {
                int i = 0;
                string line = string.Empty;
                double[] data = new double[File.ReadLines(filename).Count()];

                using (StreamReader sr = new StreamReader(filename))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        data[i] = GetParamXY(line);
                        i++;
                    }
                }
                return data;
            }
            catch (Exception e)
            {
                Console.WriteLine("Не удалось загрузить файл: " + e.Message);
                return null;
            }
        }

        private static (double, double) GetXY(string line)
        {
            string[] param = line.Replace('.', ',').Trim().Split(' ');
            return (Convert.ToDouble(param[0]), Convert.ToDouble(param[1]));
        }

        private static double GetParamXY(string line)
        {
            string param = line.Replace('.', ',').Trim();
            return Convert.ToDouble(param);
        }
    }
}
