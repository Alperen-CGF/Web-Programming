using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using WebService.Models;
using WebService.Utils;
using System.Data;
using System.Data.SqlClient;

namespace WebService
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class DataManager : System.Web.Services.WebService
    {

        /***
         * https://localhost:44301/datamanager.asmx
         * 
         * 
         * Kullanıcı class oluştur. Retrun olarak o classı dön
         */


        [WebMethod]
        public string Login(string UserName, string Password)
        {
            SqlCommand komut4;
            SqlDataReader dr;
            SqlConnection baglan = new SqlConnection("Data Source=.;Initial Catalog=Filmler;Integrated Security=True");
            komut4 = new SqlCommand();
            baglan.Open();
            komut4.Connection = baglan;
            komut4.CommandText = string.Format("Select * From Kullanici where UserName='{0}' And Password='{1}'",UserName,Password);
            dr = komut4.ExecuteReader();

            if (dr.Read())
            {
                baglan.Close();
                return "Hesap Bilgileri Doğru";
            }
            else
            {
                baglan.Close();
                return "Hesap Bilgileri Yanlış";              
            }
        }

        /***
         * Film Class oluştur ve class türünden nesneyi parametre al         
         */
        

        [WebMethod]
        public string FilmEkle(string Film_Name,string Explan,DateTime Vision_Date,int Imdb,int Time)
        {
            SqlCommand komut;
            SqlConnection baglan = new SqlConnection("Data Source=.;Initial Catalog=Filmler;Integrated Security=True");
            string sorgu = "INSERT INTO Films (Film_Name,Explan,Vision_Date,Imdb,Time) VALUES (@Film_Name,@Explan,@Vision_Date,@Imdb,@Time)";
            komut = new SqlCommand(sorgu, baglan);
            komut.Parameters.AddWithValue("@Film_Name", Film_Name);
            komut.Parameters.AddWithValue("@Explan", Explan);
            komut.Parameters.AddWithValue("@Vision_Date", Vision_Date);
            komut.Parameters.AddWithValue("@Imdb", Imdb);
            komut.Parameters.AddWithValue("@Time", Time);
            baglan.Open();
            komut.ExecuteNonQuery();
            baglan.Close();
            return "Eklendi";
        }

        /***
        * Film Class oluştur ve class türünden nesneyi parametre al         
        */
        [WebMethod]
        public string FilmGuncelle(int Film_Id,string Film_Name, string Explan, DateTime Vision_Date, int Imdb, int Time)
        {
            SqlCommand komut3;
            SqlConnection güncelle = new SqlConnection("Data Source=.;Initial Catalog=Filmler;Integrated Security=True");
            string sorgu = "UPDATE Films SET Film_Name=@Film_Name,Explan=@Explan,Vision_Date=@Vision_Date,Imdb=@Imdb,Time=@Time WHERE Film_Id=@Film_Id";
            komut3 = new SqlCommand(sorgu, güncelle);
            komut3.Parameters.AddWithValue("@Film_Id", Film_Id);
            komut3.Parameters.AddWithValue("@Film_Name", Film_Name);
            komut3.Parameters.AddWithValue("@Explan", Explan);
            komut3.Parameters.AddWithValue("@Vision_Date", Vision_Date);
            komut3.Parameters.AddWithValue("@Imdb", Imdb);
            komut3.Parameters.AddWithValue("@Time", Time);
            güncelle.Open();
            komut3.ExecuteNonQuery();
            güncelle.Close();
            return "Güncellendi";
        }

        [WebMethod]
        public Film FilmDetayGetir(int filmID)
        {
            string query = String.Format("Select Film_Id,Film_Name,Explan,Vision_Date,Imdb,Time FROM Films WHERE Film_Id = '{0}'", filmID);
            DataSet ds = Sorgu(query);
            DataRow rw = ds.Tables[0].Rows[0];
            Film tmp = new Film();
            tmp.Film_ID = Convert.ToInt32(rw["Film_Id"].ToString());
            tmp.Film_Name = rw["Film_Name"].ToString();
            tmp.Explan = rw["Explan"].ToString();
            tmp.Imdb = Convert.ToDecimal(rw["Imdb"].ToString());
            tmp.Vision_Date = Convert.ToDateTime(rw["Vision_Date"].ToString());
            tmp.Time = Convert.ToInt32(rw["Time"].ToString());

            
            return tmp;
        }

        [WebMethod]
        public List<Film> FilmListele()
        {
            string query = "Select Film_Id,Film_Name,Explan,Vision_Date,Imdb,Time FROM Films";
            DataSet ds = Sorgu(query);
            List<Film> sonucListe = new List<Film>();
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                Film gecici = new Film();
                gecici.Film_ID = Convert.ToInt32(item["Film_Id"].ToString());
                gecici.Film_Name = item["Film_Name"].ToString();
                gecici.Explan = item["Explan"].ToString();
                gecici.Imdb = Convert.ToDecimal(item["Imdb"].ToString());
                gecici.Vision_Date = Convert.ToDateTime(item["Vision_Date"].ToString());
                gecici.Time = Convert.ToInt32(item["Time"].ToString());


                sonucListe.Add(gecici);
            }
            
            return sonucListe;
        }

        [WebMethod]
        public string FilmSil(int Film_Id)
        {
            SqlCommand komut2;
            SqlConnection sil = new SqlConnection("Data Source=.;Initial Catalog=Filmler;Integrated Security=True");
            string sorgu = "DELETE FROM Films WHERE Film_Id=@Film_Id";
            komut2 = new SqlCommand(sorgu, sil);
            komut2.Parameters.AddWithValue("@Film_Id", Convert.ToInt32(Film_Id));
            sil.Open();
            komut2.ExecuteNonQuery();
            sil.Close();
            return "Silindi";
        }

        public DataSet Sorgu(string query)
        {
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=Filmler;Integrated Security=True");
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            da.SelectCommand.ExecuteNonQuery();
            DataSet dt = new DataSet();
            da.Fill(dt);
             
            return dt;
        }


    }
}
