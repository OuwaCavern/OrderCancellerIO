using System;
using System.Data.SqlClient;

namespace OrderCancellerIO
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SqlConnection sqlConnection;
            string computerName = Environment.MachineName;
            string? siparisNOInput;
            string connectionString = $"Data Source={computerName}\\DESENERP;Initial Catalog=DesenPOS;Persist Security Info=True;User ID=sa;Password=DesenErp.12345;";
            try
            {
                sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                Console.WriteLine("Bağlantı sağlandı.\nLütfen Sipariş NO'yu giriniz.");
                siparisNOInput = Console.ReadLine();
                try
                {
                    Convert.ToInt32(siparisNOInput);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Lütfen bir sayı giriniz.");
                    siparisNOInput = Console.ReadLine();
                }
                Console.WriteLine("Sipariş iptali için 'i' yazınız. Siparişi teslim edildiye çekmek içinse 't' yazınız.");
                string? iptalVeyaTeslim = Console.ReadLine();
                try
                {
                    iptalVeyaTeslim = iptalVeyaTeslim.ToLower().Trim();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Lütfen boş bırakmayınız.");
                    iptalVeyaTeslim = Console.ReadLine();
                }
                do
                {
                    switch (iptalVeyaTeslim)
                    {
                        case "i":
                            SiparisIptal();
                            break;
                        case "t":
                            SiparisTeslim();
                            break;
                        default:
                            Console.WriteLine("Lütfen size sunulan seçeneklerden birisini yazınız.");
                            iptalVeyaTeslim = Console.ReadLine();
                            break;
                    }
                } while (iptalVeyaTeslim != "i" && iptalVeyaTeslim != "t");

                    void SiparisIptal()
                {
                    string updateQuery = $"declare @SiparisNoYaziniz nvarchar(100)='{siparisNOInput}'\r\nupdate POSSiparis set SiparisDurumu= 8,Odendi = 0 , Kapandi = 1, SysAktif=0 where SiparisNo=@SiparisNoYaziniz;\r\n\r\ninsert into SistemTarihce\r\n(KayitId\r\n,Tablo\r\n,Tarih\r\n,RowVersion\r\n,SysAktif\r\n,SysKayitTarihi\r\n,SysKaydedenKullanici\r\n,Aciklama)\r\n\r\nselect \r\ns.Id,'POSSiparis',GETDATE(),0,1,GETDATE(),'ManuelKapatildi','güncelleme' from POSSiparis s where s.SiparisNo=@SiparisNoYaziniz";
                    SqlCommand insertCommand = new SqlCommand(updateQuery, sqlConnection);
                    insertCommand.ExecuteNonQuery();
                }
                void SiparisTeslim()
                {
                    string updateQuery = $"declare @SiparisNoYaziniz nvarchar(100)='{siparisNOInput}'\r\nupdate POSSiparis set SiparisDurumu= 3,Odendi = 1 , Kapandi = 1, SysAktif=1 where SiparisNo=@SiparisNoYaziniz;\r\n\r\ninsert into SistemTarihce\r\n(KayitId\r\n,Tablo\r\n,Tarih\r\n,RowVersion\r\n,SysAktif\r\n,SysKayitTarihi\r\n,SysKaydedenKullanici\r\n,Aciklama)\r\n\r\nselect \r\ns.Id,'POSSiparis',GETDATE(),0,1,GETDATE(),'ManuelKapatildi','güncelleme' from POSSiparis s where s.SiparisNo=@SiparisNoYaziniz";
                    SqlCommand insertCommand = new SqlCommand(updateQuery, sqlConnection);
                    insertCommand.ExecuteNonQuery();
                }
                
                sqlConnection.Close();
                Console.WriteLine("Process is completed.");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}