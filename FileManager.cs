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
        public static double[,] ReadFromFile(string filename)
        {
            try
            {
                int i = 0;
                string line = string.Empty;
                double[,] data = new double[2, System.IO.File.ReadAllLines(filename).Length];

                using (StreamReader sr = new StreamReader(filename))
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

        private static (double, double) GetXY(string line)
        {
            string[] param = line.Replace('.', ',').Trim().Split(' ');
            return (Convert.ToDouble(param[0]), Convert.ToDouble(param[1]));
        }
    }
}
