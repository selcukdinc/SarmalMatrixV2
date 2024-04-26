namespace SarmalSezgiselV2
{
    internal class Program
    {
        class Yazdir
        {
            public Yazdir(int x, int y)
            {
                string[,] YolHaritasi = new string[x, y];
                int[,] AsilMatris = new int[x, y];
                this.Sonsuz(ref YolHaritasi, ref AsilMatris);
            }

            private static void Baslik(string[,] YolH)
            {
                int i = YolH.GetLength(0), j = YolH.GetLength(1);
                Console.Write("\t--\t{0}x{1} Matrisin Sarmal Döngüsü\t--\n\n", i, j);
            }

            private static void SayacV2(ref int Sayac, ref Konum KK, ref int AdmSyc)
            {
                Console.Write("Adım Sayısı: {0}\t Konum: {1}\t AdımSayacı: {2}\tSonCase",Sayac, KK, AdmSyc);
            }
            private static void MatrisYazdir(ref string[,] YolH,ref int[,] AsilM)
            {
                for (int i = 0; i < AsilM.GetLength(0); i++)
                {
                    for (int j = 0; j < AsilM.GetLength(1); j++)
                    {
                        Console.Write("{0}\t", YolH[i, j]);
                    }
                    Console.Write("\t\t");
                    for (int j = 0; j < AsilM.GetLength(1); j++)
                    {
                        Console.Write("{0}\t", AsilM[i, j]);
                    }
                    Console.Write("\n\n");
                }
            }

            private static bool WDoKontrol(int CalısmaModu)
            {
                ConsoleKeyInfo Secim;
                Secim = Console.ReadKey();
                Console.Clear();
                switch (CalısmaModu)
                {
                    case 1:
                        if (Secim.Key == ConsoleKey.Enter)
                            return true;
                        break;
                    case 2:
                        return true;
                }
                return false;
            }

            private static void BoslukKontrol(ref string[,] YolH,ref int[,] AsilM, int SonDeg) 
            {
                for (int i = 0; i < YolH.GetLength(0); i++)
                {
                    for (int j = 0; j < YolH.GetLength(1); j++)
                    {
                        if (!(YolH[i,j] == "T"))
                        {
                            YolH[i, j] = "T";
                            AsilM[i, j] = SonDeg;
                        }
                    }
                }
            }

            private void Sonsuz(ref string[,] YolH, ref int[,] AsilM)
            {
                int YBx = 0, YBy = 0, Sayac = 1, AdimSayaci = 1;
                Konum YolBulucu = Konum.UstSatir;
                
                do
                {
                    Baslik(YolH);
                    MatrisYazdir(ref YolH, ref AsilM);
                    int SonCase = YolH.GetLength(0) * YolH.GetLength(1);
                    if (AdimSayaci == SonCase)
                    {
                        BoslukKontrol(ref YolH, ref AsilM, AdimSayaci);
                    }
                    else
                    {
                        YolHaritasiİsleme(ref YolH, ref YBx, ref YBy, ref YolBulucu, ref Sayac, ref AsilM, ref AdimSayaci);
                        SayacV2(ref Sayac, ref YolBulucu, ref AdimSayaci);
                    }
                } while (WDoKontrol(2));
            }

            enum Konum
            {
                UstSatir, SagSutun, AltSatir, SolSutun
            }

            private static bool YBKontrol(int X, int Y, ref string[,] YolH, Konum KK)
            {
                int i = YolH.GetLength(0), j = YolH.GetLength(1);
                switch (KK)
                {
                    case Konum.UstSatir:
                        if (Y + 1 == j || YolH[X, Y + 1] == "T")
                        {
                            return true;
                        }
                        break;
                    case Konum.SagSutun:
                        if (X + 1 == i || YolH[X + 1, Y] == "T")
                        {
                            return true;
                        }
                        break;
                    case Konum.AltSatir:
                        if (Y - 1 == -1 || YolH[X, Y - 1] == "T")
                        {
                            return true;
                        }
                        break;
                    case Konum.SolSutun:
                        if (X - 1 == -1 || YolH[X - 1, Y] == "T")
                        {
                            return true;
                        }
                        break;
                }
                return false;
            }
            private static void YolHaritasiİsleme(ref string[,] YolH, ref int YBx, ref int YBy, ref Konum YolBulucu, ref int Sayac, ref int[,] AsilM, ref int AdimSyc)
            {
                switch (YolBulucu)
                {
                    case Konum.UstSatir:
                        if (YBKontrol(YBx, YBy, ref YolH, YolBulucu))
                        {
                            YolBulucu = Konum.SagSutun;
                        }
                        else
                        {
                            YolH[YBx, YBy] = "T";
                            AsilM[YBx, YBy] = Sayac++;
                            YBy++;
                            AdimSyc++;
                        } 
                        break;
                    
                    case Konum.SagSutun:
                        if (YBKontrol(YBx, YBy, ref YolH, YolBulucu))
                        {
                            YolBulucu = Konum.AltSatir;
                        }
                        else
                        {
                            YolH[YBx, YBy] = "T";
                            AsilM[YBx, YBy] = Sayac++;
                            YBx++;
                            AdimSyc++;
                        }
                        break;
                    
                    case Konum.AltSatir:
                        if (YBKontrol(YBx, YBy, ref YolH, YolBulucu))
                        {
                            YolBulucu = Konum.SolSutun;
                        }
                        else
                        {
                            YolH[YBx, YBy] = "T";
                            AsilM[YBx, YBy] = Sayac++;
                            YBy--;
                            AdimSyc++;
                        }
                        break;

                    case Konum.SolSutun:
                        if (YBKontrol(YBx, YBy, ref YolH, YolBulucu))
                        {
                            YolBulucu = Konum.UstSatir;
                        }
                        else
                        {
                            YolH[YBx, YBy] = "T";
                            AsilM[YBx, YBy] = Sayac++;
                            YBx--;
                            AdimSyc++;
                        }
                        break;
                }
            }
        }
        static void Main(string[] args)
        {
            Console.Write("nxm matrisi\nn:");
            int n = Convert.ToInt32(Console.ReadLine());
            Console.Write("m:");
            int m = Convert.ToInt32(Console.ReadLine());
            Yazdir SarmalMatris = new Yazdir(n, m);
            Console.Write("\nAna Satır Bloğuna Geldin!");
        }
    }
}
