using System;
using System.Data.SqlClient;

namespace OrderCancellerIO
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SqlConnection sqlConnection;
            string connectionString = "Data Source=DESKTOP-NEF9RON\\SQLEXPRESS;Initial Catalog=TestDB;Integrated Security=True";
            try
            {
                sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                Console.WriteLine("Connection established.");
                string userName = "sa";
                string insertQuery = "declare @SiparisNoYaziniz nvarchar(100)= \r\n-----------------------------------------------------\r\n-----------------------------------------------------\r\n-----------------------------------------------------\r\nupdate POSSiparis set SiparisDurumu= 8,Odendi = 0 , Kapandi = 1, SysAktif=0 where SiparisNo=@SiparisNoYaziniz;\r\n\r\ninsert into SistemTarihce\r\n(KayitId\r\n,Tablo\r\n,Tarih\r\n,RowVersion\r\n,SysAktif\r\n,SysKayitTarihi\r\n,SysKaydedenKullanici\r\n,Aciklama)\r\n\r\nselect \r\ns.Id,'POSSiparis',GETDATE(),0,1,GETDATE(),'ManuelKapatildi','güncelleme' from POSSiparis s where s.SiparisNo=@SiparisNoYaziniz";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}