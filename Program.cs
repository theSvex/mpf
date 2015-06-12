using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MPF
{
    class Program
    {

        public static double GetCoal(double temperature)
        {
            return (912 - temperature) / 245.45;
        }

        public static void wypisz(double[] tab, double[] tab2)
        {
            for (int i = 0; i < tab.Length; i++)
                Console.Write(Math.Round(tab[i],4) + "\t");
            Console.WriteLine();
            for (int i = 0; i < tab2.Length; i++)
                Console.Write(Math.Round(tab2[i],4) + "\t");
            Console.WriteLine();
        }

        public static void wypisz(double[] tab)
        {
            for (int i = 0; i < tab.Length; i++)
                Console.Write(Math.Round(tab[i], 4) + "\t");
            Console.WriteLine();
        }

        public static void wypisz(double tmp)
        {
            Console.WriteLine(tmp);
        }

        static void Main(string[] args)
        {
            double[] tab0 = new double[25];
            for (int i = 0; i < tab0.Length; i++)
            {
                if (i <= 4)
                {
                    tab0[i] = 0.67;
                }
                else
                {
                    tab0[i] = 0.02;
                }
            }
            double[] tab;
            double[] tab2;
            double temp = 780;
            double temp_k = temp + 273;
            double Q = 140000;
            double R = 8.3144;
            double d0 = 0.000041;
            double dx2 = 0.01;
            double Vn = 2;
            double D = d0 * Math.Exp(-Q / (R * temp_k)) * 1e8;
            double wspDyfuzji = 0;
            int ksi = 5;

            while (ksi < tab0.Length)
            {
                tab = new double[ksi + 1];
                tab2 = new double[ksi + 1];
                for (int i = 0; i < ksi + 1; i++)
                {
                    tab[i] = tab0[i];
                    tab2[i] = 0;
                }
                double dt = 0.1;
                while (tab[ksi] < GetCoal(temp))
                {
                     if ((wspDyfuzji = (D * dt) / dx2) >= 0.5) break;
                    for (int i = 1; i < tab.Length - 1; i++)
                        tab2[i] = (1 - 2 * D) * tab[i] + D * (tab[i - 1] + tab[i + 1]);
                    tab2[0] = (1 - 2 * D) * tab.First() + D * (tab.First() + tab[1]);
                    tab2[tab2.Length - 1] = (1 - 2 * D) * tab.Last() + D * (tab2[tab2.Length - 2] + tab.Last());
                    tab = tab2;
                    dt += Vn;
                }

                for (int i = 0; i < ksi + 1; i++)
                    tab0[i] = tab2[i];  
                ksi++;
            }
        }
    }
}