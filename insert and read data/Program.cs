using System;
using System.Data;
using System.Data.SqlClient;

namespace PABD_5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Program pr = new Program();
            while (true)
            {
                try
                {
                    Console.WriteLine("Masukkan Database Tujuan :");
                    string db = Console.ReadLine();
                    Console.WriteLine("\nKetik K untuk Terhubung ke Database :");
                    char chr = Convert.ToChar(Console.ReadLine());
                    switch (chr)
                    {
                        case 'K':
                        case 'k':
                            {
                                SqlConnection conn = null;
                                string strkoneksi = "Data Source= METEORITE\\SQL2019; " +
                                    "Initial Catalog= Pacarjadi2; " +
                                    "User ID=sa; password= meteorite";
                                conn = new SqlConnection(string.Format(strkoneksi, db));
                                conn.Open();
                                Console.Clear();
                                while (true)
                                {
                                    try
                                    {
                                        Console.WriteLine("\nMenu");
                                        Console.WriteLine("1. Melihat Seluruh Data");
                                        Console.WriteLine("2. Tambah Data");
                                        Console.WriteLine("3. Keluar");
                                        Console.WriteLine("\nEnter your choice (1-3): ");
                                        char ch = Convert.ToChar(Console.ReadLine());
                                        switch (ch)
                                        {
                                            case '1':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("Data Pacar & Mantan");
                                                    Console.WriteLine();
                                                    pr.baca(conn);
                                                }
                                                break;
                                            case '2':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("Input Data Pacar & Mantan Jodi\n");
                                                    Console.WriteLine("Masukkan Nama Pacar / Mantan :");
                                                    string NmaMhs = Console.ReadLine();
                                                    Console.WriteLine("Masukkan No Telepon Pacar / Mantan :");
                                                    string notlpn = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Alamat Pacar / Mantan :");
                                                    string Almt = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Jenis Kelamin (L/P) :");
                                                    string jk = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Status Hubungan Saat Ini (Putus/Masih) :");
                                                    string sts = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Masa Hubungan :");
                                                    string masa = Console.ReadLine();
                                                    try
                                                    {
                                                        pr.insert(NmaMhs, notlpn, Almt, jk, sts, masa, conn);

                                                    }
                                                    catch
                                                    {
                                                        Console.WriteLine("\nAnda tidak memiliki akses untuk menambah data");
                                                    }
                                                }
                                                break;
                                            case '3':
                                                conn.Close();
                                                Console.Clear();
                                                Main(new string[0]);
                                                return;
                                            default:
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("\nInvalid Option");
                                                }
                                                break;
                                        }
                                    }
                                    catch
                                    {
                                        Console.Clear();
                                        Console.WriteLine("\nCheck for the value entered.");
                                    }
                                }
                            }
                            break;
                        default:
                            {
                                Console.WriteLine("\nInvalid Option");
                            }
                            break;
                    }
                }
                catch
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Tidak Dapat Mengakses Database Tersebut\n");
                    Console.ResetColor();
                }
            }
        }

        public void baca(SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand("SELECT NmPCR, NoTlpn, AlmtPCR, JK, STSHBG, MASAHBG FROM DataPacar", con);
            SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                for (int i = 0; i < r.FieldCount; i++)
                {
                    Console.WriteLine(r.GetValue(i));
                }
                Console.WriteLine();
            }
            r.Close();
        }

        public void insert(string Nma, string notlpn, string Almt, string jk, string shb, string mhb, SqlConnection con)
        {
            string str = "INSERT INTO DataPacar (NmPCR, NoTlpn, AlmtPCR, JK, STSHBG, MASAHBG) " +
                "VALUES (@nm, @nt, @a, @jk, @sb, @mb)";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add(new SqlParameter("@nm", Nma));
            cmd.Parameters.Add(new SqlParameter("@nt", notlpn));
            cmd.Parameters.Add(new SqlParameter("@a", Almt));
            cmd.Parameters.Add(new SqlParameter("@jk", jk));
            cmd.Parameters.Add(new SqlParameter("@sb", shb));
            cmd.Parameters.Add(new SqlParameter("@mb", mhb));
            cmd.ExecuteNonQuery();
            Console.WriteLine("\nData Berhasil Ditambahkan");
        }
    }
}