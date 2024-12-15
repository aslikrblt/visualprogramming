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
    public partial class kitap : Form
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

        public kitap()
        {
            InitializeComponent();
            

        }

        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + "\\Tablolar.mdb");
        DataTable KitapTablo = new DataTable();


        public void KitapGoruntule()
        {
            baglanti.Open();
            OleDbDataAdapter kitapadptr = new OleDbDataAdapter("select * from kitaplar", baglanti);
            kitapadptr.Fill(KitapTablo);
            kitapDatagrdv.DataSource = KitapTablo;
            baglanti.Close();
        }


        private void kitap_Load(object sender, EventArgs e)
        {
            KitapGoruntule();
            LoadChartData();


        }

        private void ekleBtn_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand eklecmd = new OleDbCommand("insert into kitaplar(kitapadi,kitapyazari,kitapserino) values ('" + kitapAdtxt.Text + "','" + kitapYazartxt.Text + "','" + kitapSerinotxt.Text + "')", baglanti); // kitaplar tablosuna veri eklemek için
            eklecmd.ExecuteNonQuery(); //EKLE FONKSİYONUNU PROGRAMA  TANITMA FONKSİYONU
            baglanti.Close();
            MessageBox.Show("Kitap Eklendi!");
            KitapTablo.Clear();
            KitapGoruntule();
        }

        private void silBtn_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand silcmd = new OleDbCommand("delete *from kitaplar where kitapadi='" + kitapDatagrdv.CurrentRow.Cells["kitapadi"].Value.ToString() + "'and kitapyazari='" + kitapDatagrdv.CurrentRow.Cells["kitapyazari"].Value.ToString() + "'and kitapserino='" + kitapDatagrdv.CurrentRow.Cells["kitapserino"].Value.ToString() + "'", baglanti); // kitaplar tablosuna veri SİLMEK için
            silcmd.ExecuteNonQuery();  //SİLME FONKSİYONUNU PROGRAMA  TANITMA FONKSİYONU
            baglanti.Close();
            MessageBox.Show("Kitap Silindi!");
            KitapTablo.Clear();
            KitapGoruntule();
        }


        // Data Grid View'de hangi kitaba tıklarsak o kitabın verileri textboxlara geçsin ki güncellemeyi daha kolay yapalım.
        private void kitapDatagrdv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            kitapAdtxt.Text = kitapDatagrdv.CurrentRow.Cells["kitapadi"].Value.ToString();
            kitapYazartxt.Text = kitapDatagrdv.CurrentRow.Cells["kitapyazari"].Value.ToString();
            kitapSerinotxt.Text = kitapDatagrdv.CurrentRow.Cells["kitapserino"].Value.ToString();
        }

        private void güncelleBtn_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand guncellecmd = new OleDbCommand("update kitaplar set kitapadi='" + kitapAdtxt.Text + "',kitapyazari='" + kitapYazartxt.Text +  "'where kitapserino='" + kitapSerinotxt.Text + "'", baglanti); // kitaplar tablosuna veri GÜNCELLEME İÇİN
            guncellecmd.ExecuteNonQuery(); //GÜNCELLE FONKSİYONUNU PROGRAMA  TANITMA FONKSİYONU
            baglanti.Close();
            MessageBox.Show("Kitap Güncellendi!");
            KitapTablo.Clear();
            KitapGoruntule();
        }

       
    }
}
