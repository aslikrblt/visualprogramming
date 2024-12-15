using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
namespace KütüphaneOtomasyon
{
    public partial class Kitap_Alma_İşlemleri : Form
    {
        public void LoadChartData()
        {
            using (OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\Tablolar.mdb"))
            {
                baglanti.Open();

                // Kitap alım sayısını sorgulayan SQL ifadesi
                string query = "SELECT kayitkitapad AS KitapAdi, COUNT(*) AS AlimSayisi FROM kayit GROUP BY kayitkitapad";
                OleDbDataAdapter adapter = new OleDbDataAdapter(query, baglanti);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // Grafik Ayarları
                chart1.Series.Clear();
                chart1.Titles.Clear();
                chart1.Legends.Clear();

                // Grafik Başlığı
                chart1.Titles.Add("Kitap Alım Sayıları");

                // Grafik Serisi
                Series series = new Series("Alım Sayıları")
                {
                    ChartType = SeriesChartType.Column,
                    IsValueShownAsLabel = true
                };

                // Verileri Seriye Ekleme
                foreach (DataRow row in dataTable.Rows)
                {
                    string kitapAdi = row["KitapAdi"].ToString();
                    int alimSayisi = Convert.ToInt32(row["AlimSayisi"]);
                    series.Points.AddXY(kitapAdi, alimSayisi);
                }

                chart1.Series.Add(series);
            }
        }

        public Kitap_Alma_İşlemleri()
        {
            InitializeComponent();
        }

        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\Tablolar.mdb");
        DataTable OgrenciTablo = new DataTable();
        DataTable KitapTablo = new DataTable();


        public void Ogrencigoruntule()
        {
            baglanti.Open();
            OleDbDataAdapter ogrenciadptr = new OleDbDataAdapter("select * from ogrenciler", baglanti);
            ogrenciadptr.Fill(OgrenciTablo);
            odataGridView.DataSource = OgrenciTablo;
            baglanti.Close();
        }


        public void KitapGoruntule()
        {
            baglanti.Open();
            OleDbDataAdapter kitapadptr = new OleDbDataAdapter("select * from kitaplar", baglanti);
            kitapadptr.Fill(KitapTablo);
            kdataGridView.DataSource = KitapTablo;
            baglanti.Close();
        }





        private void Kitap_Alma_İşlemleri_Load(object sender, EventArgs e)
        {
            KitapGoruntule();
            Ogrencigoruntule();
            LoadChartData();

        }

        private void odataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ogrAdtxt.Text = odataGridView.CurrentRow.Cells["ogrenciad"].Value.ToString();
            ogrSoyadtxt.Text = odataGridView.CurrentRow.Cells["ogrencisoyad"].Value.ToString();
            ogrTctxt.Text = odataGridView.CurrentRow.Cells["ogrencitc"].Value.ToString();
            ogrTeltxt.Text = odataGridView.CurrentRow.Cells["ogrencitel"].Value.ToString();
        }

        private void kdataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            kitapAdtxt.Text = kdataGridView.CurrentRow.Cells["kitapadi"].Value.ToString();
            kitapYazartxt.Text = kdataGridView.CurrentRow.Cells["kitapyazari"].Value.ToString();
            kitapSerinotxt.Text = kdataGridView.CurrentRow.Cells["kitapserino"].Value.ToString();

        }

      

        private void button1_Click_1(object sender, EventArgs e)
        {
           

            baglanti.Open();
            OleDbCommand kayitcmd = new OleDbCommand("insert into kayit(kayitkitapad,kayitkitapyazari,kayitkitapserino,kayitogrenciad,kayitogrencitel,kayitogrencitc,kayitogrencisoyad,tarih) values ('" + kitapAdtxt.Text + "','" + kitapYazartxt.Text + "','" + kitapSerinotxt.Text + "','" + ogrAdtxt.Text + "','" + ogrTeltxt.Text + "','" + ogrTctxt.Text + "','" + ogrSoyadtxt.Text + "','" + tarihpicker.Value + "')", baglanti); // kayıt tablosuna veri eklemek için
            kayitcmd.ExecuteNonQuery(); //EKLE FONKSİYONUNU PROGRAMA  TANITMA FONKSİYONU
            baglanti.Close();
            MessageBox.Show("KAYIT OLUŞTURULDU!");
            KitapTablo.Clear();
            OgrenciTablo.Clear();
            Ogrencigoruntule();
            KitapGoruntule();
        }
    }
}

