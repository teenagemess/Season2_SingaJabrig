using System;
using System.Data.SqlClient;
using System.Data;

namespace season2_singajabrig
{
    class Program
    {
        static void Main(string[] args)
        {
            Program pr = new Program();
            while (true)
            {
                try
                {
                    Console.WriteLine("Koneksi ke Database\n");
                    Console.WriteLine("Masukkan User ID :");
                    string user = Console.ReadLine();
                    Console.WriteLine("Masukkan Password :");
                    string pass = Console.ReadLine();
                    Console.WriteLine("Masukkan database tujuan :");
                    string db = Console.ReadLine();
                    Console.Write("\nKetik K untuk Terhubung ke Database: ");
                    char chr = Convert.ToChar(Console.ReadLine());
                    switch (chr)
                    {
                        case 'K':
                            {
                                SqlConnection conn = null;
                                string strKoneksi = "Data source = HP\\RAYNANDA_AQIYAS; " +
                                    "initial catalog = {0}; " + "User ID = {1}; password = {2}";
                                conn = new SqlConnection(string.Format(strKoneksi, db, user, pass));
                                conn.Open();
                                Console.Clear();
                                while (true)
                                {
                                    try
                                    {
                                        Console.WriteLine("\nMenu");
                                        Console.WriteLine("1. Melihat Seluruh Data");
                                        Console.WriteLine("2. Tambah Data");
                                        Console.WriteLine("3. Hapus Data");
                                        Console.WriteLine("4. Keluar");
                                        Console.Write("\nEnter your choice (1-3): ");
                                        char ch = Convert.ToChar(Console.ReadLine());
                                        switch (ch)
                                        {
                                            case '1':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("DATA PEMBELI\n");
                                                    Console.WriteLine();
                                                    pr.baca(conn);
                                                }
                                                break;
                                            case '2':
                                                {
                                                    Console.WriteLine(" Masukkan ID : ");
                                                    string ID = Console.ReadLine();
                                                    Console.WriteLine("NAMA : ");
                                                    string NAMA = Console.ReadLine();
                                                    Console.WriteLine("Kecamatan : ");
                                                    string KEC = Console.ReadLine();
                                                    Console.WriteLine("Jalan : ");
                                                    string JAL = Console.ReadLine();
                                                    Console.WriteLine("Masukkan No Telepon :");
                                                    string TLP = Console.ReadLine();
                                                    try
                                                    {
                                                        pr.insert(ID, NAMA, KEC, JAL, TLP, conn);
                                                    }
                                                    catch
                                                    {
                                                        Console.WriteLine("\nAnda tidak memiliki " + "akses untuk menambah data");
                                                    }

                                                }
                                                break;
                                            case '3':
                                                try
                                                {
                                                    pr.delete();
                                                }
                                                catch
                                                {
                                                    Console.WriteLine("\nAnda tidak memiliki " + "akses untuk menghapus data");
                                                }
                                                break;
                                            case '4':
                                                conn.Close();
                                                return;
                                            default:
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("\nInvalid option");
                                                }
                                                break;
                                        }
                                    }
                                    catch
                                    {
                                        Console.WriteLine("\nCheck for the value entered.");
                                    }
                                }
                            }
                        default:
                            {
                                Console.WriteLine("\nInvalid option");
                            }
                            break;
                    }
                }
                catch
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Tidak Dapat Mengakses Database Menggunakan User Tersebut\n");
                    Console.ResetColor();
                }
            }
        }

        private void delete()
        {
            throw new NotImplementedException();
        }

        public void baca(SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand("Select*From pembeli", con);
            SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                for (int i = 0; i < r.FieldCount; i++)
                {
                    Console.WriteLine(r.GetValue(i));
                }
                Console.WriteLine();
            }
        }

        public void insert(string ID, string NAMA, string KEC, string JAL, string TLP, SqlConnection con)
        {
            string str = "";
            str = "insert into pembeli (ID, NAMA, KEC, JAL, TLP)values(@id_pembeli, @nama_pembeli, @kecamatan, @jalan, @telepon)";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add(new SqlParameter("id_pembeli", ID));
            cmd.Parameters.Add(new SqlParameter("nama_pembeli", NAMA));
            cmd.Parameters.Add(new SqlParameter("kecamatan", KEC));
            cmd.Parameters.Add(new SqlParameter("jalan", JAL));
            cmd.Parameters.Add(new SqlParameter("telepon", TLP));
            cmd.ExecuteNonQuery();
            Console.WriteLine("Data Berhasil Ditambahkan");
        }

        public void delete(string ID, string NAMA, string KEC, string JAL, string TLP, SqlConnection con)
        {
            string str = "";
            str = "delete from pembeli (ID, NAMA, KEC, JAL, TLP)values(@id_pembeli, @nama_pembeli, @kecamatan, @jalan, @telepon)";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add(new SqlParameter("id_pembeli", ID));
            cmd.Parameters.Add(new SqlParameter("nama_pembeli", NAMA));
            cmd.Parameters.Add(new SqlParameter("kecamatan", KEC));
            cmd.Parameters.Add(new SqlParameter("jalan", JAL));
            cmd.Parameters.Add(new SqlParameter("telepon", TLP));
            cmd.ExecuteNonQuery();
            Console.WriteLine("Data Berhasil Dihapus");
        }
    }
}