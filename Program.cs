using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;
using System.Globalization;

namespace PractiseTask2
{
    /// <summary> 
    /// Абстрактный класс
    /// </summary>
    /// <remarks>
    /// Содержит метод вычисления значения в заданой точке.
    /// </remarks>
    public abstract class Function
    {
        /// <summary>
        /// Вычисление значения функции в точке х.
        /// </summary>
        /// <param name="x">Точка х.</param>
        /// <returns>Значение в точке х.</returns>
        public abstract double Count(double x);
    }

    /// <summary>
    /// Класс для константной функции.
    /// </summary>
    public class Single : Function
    {
        public double A;
        public Single(double a)
        {
            A = a;
        }

        /// <summary>
        /// Вычисление значения константной функции в точке х.
        /// </summary>
        /// <param name="x">Точка х.</param>
        /// <returns>Значение в точке х.</returns>
        public override double Count(double x)
        {
            return A;
        }
    }

    /// <summary>
    /// Линейная функция от переменной х.
    /// </summary>
    /// <remarks>
    /// Вид функции: А * х + В
    /// </remarks>
    public class Line : Function
    {
        public double A;
        public double B;

        public Line(double a, double b)
        {
            A = a;
            B = b;
        }

        /// <summary>
        /// Вычисление значения линейной функции в точке х.
        /// </summary>
        /// <param name="x">Точка х.</param>
        /// <returns>Значение в точке х.</returns>
        public override double Count(double x)
        {
            return A * x + B;
        }
    }


    /// <summary>
    /// Квадратичная функция от переменной х.
    /// </summary>
    /// <remarks>
    /// Вид функции: А * х^2 + В * x + C
    /// </remarks>
    public class Quadratic : Function
    {
        public double A;
        public double B;
        public double C;

        public Quadratic(double a, double b, double c)
        {
            A = a;
            B = b;
            C = c;
        }

        /// <summary>
        /// Вычисление значения квадратичной функции в точке х.
        /// </summary>
        /// <param name="x">Точка х.</param>
        /// <returns>Значение в точке х.</returns>
        public override double Count(double x)
        {
            return A * x * x + B * x + C;
        }
    }

    /// <summary>
    /// Кубическая функция от переменной х.
    /// </summary>
    /// <remarks>
    /// Вид функции: А * х^3 + В * x^2 + C * x + D
    /// </remarks>
    public class Cubic : Function
    {
        public double A;
        public double B;
        public double C;
        public double D;

        public Cubic(double a, double b, double c, double d)
        {
            A = a;
            B = b;
            C = c;
            D = d;
        }

        /// <summary>
        /// Вычисление значения кубической функции в точке х.
        /// </summary>
        /// <param name="x">Точка х.</param>
        /// <returns>Значение в точке х.</returns>
        public override double Count(double x)
        {
            return A * x * x * x + B * x * x + C * x + D;
        }

    }

    /// <summary>
    /// Класс для работы с файлами.
    /// </summary>
    public class WorkWithFile
    {
        /// <summary>
        /// Входной файл по умолчанию.
        /// </summary>
        public string pathIn;

        /// <summary>
        /// Выходной файл по умолчанию.
        /// </summary>
        public string pathOut;

        /// <summary> 
        /// Конструктор класса.</summary>
        /// <param name="pathIn">Путь, откуда нужно будет прочитать информацию.</param>
        /// <param name="pathOut">Пусть, куда нужно будет записать результат.</param>
        public WorkWithFile(string pathIn, string pathOut)
        {
            this.pathIn = pathIn;
            this.pathOut = pathOut;
        }

        /// <summary> 
        /// Метод, осуществляющий чтение строк из файла.</summary>
        /// <returns>
        /// Список строк, которые были считаны из файла.</returns>
        public List<String> ReadFl()
        {
            using var sr = new StreamReader(@pathIn, Encoding.Default);

            string line = "";
            List<String> result = new List<string>();

            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                if (line == "" || line == ";" || line == " ")
                    throw new Exception("Обнаружена недопустимая строка");
                result.Add(line);
            }

            if (result.Count == 0)
            {
                throw new FormatException("Входной файл пуст. Вычисление невозможно");
            }

            return result;
        }

        /// <summary> 
        /// Метод, осуществляющий запись массива строк в файл.</summary>
        /// <param name="answer">Массив строк, который нужно записать в файл.</param>
        public void WriteFl(string[] answer)
        {
            using (StreamWriter sw = new StreamWriter(@pathOut))
            {

                foreach (string str in answer)
                {
                    sw.WriteLine(str);
                }
            }
        }
    }

    class Program
    {

        /// <summary> 
        /// Метод, осуществляющий перевод массива строк в массив типа double.
        /// </summary>
        /// <remarks>
        /// В каждой строке записано число. Разделитель частей числа меняется на (.).
        /// Далее каждая строка (в которой записано число) конвертируется в double.
        /// </remarks>
        /// <param name="input"> Массив строк, который нужно конвертировать.</param>
        /// <returns>
        /// Массив типа double.
        /// </returns>
        public static double[] ToDoubleForMas(String[] input)
        {
            Double[] result = new double[input.Length];/*массив для записи результата*/

            int i = 0;

            try
            {
                for (; i < input.Length; i++)
                {
                    input[i] = input[i].Replace(",", ".");/*замена в каждой строке , на .*/
                    result[i] = Convert.ToDouble(input[i], CultureInfo.InvariantCulture);
                }
            }
            catch
            {
                Console.WriteLine("Найдено недопустимое значение в строке " + (i + 1));
                throw;
            }

            return result;
        }

        /// <summary> 
        /// Метод, осуществляющий конвертацию списка строк в список массивов типа double. 
        /// </summary>
        /// <remarks>
        /// В данный метод поступает список строк, считанных из файла.
        /// Далее из каждой строки выбираются числа (разделитель ;) и записываются в массив строк fnc.
        /// Затем массив строк fnc (массив коэффициентов прямых) переводится в массив типа double 
        /// </remarks>
        /// <param name="inStr"> Список строк, который нужно конвертировать.</param>
        /// <returns>
        /// Список массивов типа double.
        /// </returns>
        public static List<Double[]> Convertation(List<string> inStr)
        {
            List<Double[]> result = new List<Double[]>();

            foreach (string str in inStr)
            {
                /*каждую поступившую строку разбиваем на массив строк (разделитель ;)*/
                String[] fnc = str.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries); ;

                /*под каждый массив создаем массив такого же размера, куда записываются конвертирующиеся значения*/
                Double[] dfnc = new double[fnc.Length];

                dfnc = ToDoubleForMas(fnc);

                result.Add(dfnc);
            }

            return result;
        }

        /// <summary>
        /// Точка входа в приложение.
        /// </summary>
        public static void Main(string[] args)
        {
            double x;
            Console.Write("Введите значение точки, в которой необходимо посчитать значения функци: ");

            try
            {
                x = Convert.ToDouble(Console.ReadLine());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Введено некорректное значение точки х. " + ex.Message);
                return;
            }

            WorkWithFile WWF = new WorkWithFile("input.txt", "output.txt");

            List<String> lstFunc = new List<string>();

            try
            {
                lstFunc = WWF.ReadFl();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка во время чтения файла " + e);
                return;
            }

            List<double[]> lstOfArg = new List<double[]>();

            try
            {
                lstOfArg = Convertation(lstFunc);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка во время конвертации значений " + e);
            }

            Function f;

            String[] answers = new string[lstOfArg.Count()];

            for (int i = 0; i < lstOfArg.Count(); i++)
            {
                switch (lstOfArg[i].Length)
                {
                    case 1:
                        f = new Single(lstOfArg[i][0]);
                        answers[i] = String.Format("значение функции y(x) = {0} в точке х = {1}", f.Count(x), x);
                        break;
                    case 2:
                        f = new Line(lstOfArg[i][0], lstOfArg[i][1]);
                        answers[i] = String.Format("Значение функции y(x) = {0} * x + {1} = {2} в точке х = {3}", lstOfArg[i][0], lstOfArg[i][1], f.Count(x), x);
                        break;
                    case 3:
                        f = new Quadratic(lstOfArg[i][0], lstOfArg[i][1], lstOfArg[i][2]);
                        answers[i] = String.Format("Значение функции y(x) = {0} * x^2 + {1} * x + {2} = {3} в точке х = {4}", lstOfArg[i][0], lstOfArg[i][1], lstOfArg[i][2], f.Count(x), x);
                        break;
                    case 4:
                        f = new Cubic(lstOfArg[i][0], lstOfArg[i][1], lstOfArg[i][2], lstOfArg[i][3]);
                        answers[i] = String.Format("Значение функции y(x) = {0} * x^3 + {1} * x^2 + {2} * x + {3} = {4} в точке х = {5}", lstOfArg[i][0], lstOfArg[i][1], lstOfArg[i][2], lstOfArg[i][3], f.Count(x), x);
                        break;
                    default:
                        answers[i] = "У функции некорректное количество аргументов ";
                        break;
                }

            }

            WWF.WriteFl(answers);

            Console.WriteLine("Программа успешно выполнена");
        }
    }


}
