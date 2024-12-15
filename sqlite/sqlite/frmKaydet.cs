using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sqlite
{
    public partial class frmKaydet : Form
    {
        int? id=null;
        public bool Kaydedildi = false;
        public frmKaydet(int? Id = null)
        {
            InitializeComponent();
            if(Id != null) { 
                this.id = Id;
                
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if(id == null)
            {
                string sql = "Insert into Bilgiler(Adsoyad, Telefon, Maas, Tarih) Values('"+txtAdSoyad.Text+"','"+txtTelefon.Text+"','"+txtMaas.Text+"','"+dateTimePicker1.Value+"')";
                if (CRUD.ESG(sql))
                {
                    Kaydedildi=true;
                    MessageBox.Show("Ekleme işlemi başarıyla gerçekleştirildi.");
                    this.Close();
                }               
            }
            else
            {
                string sql = "Update Bilgiler set Adsoyad='" + txtAdSoyad.Text + "', Telefon='" + txtTelefon.Text + "', Maas='" + txtMaas.Text + "', Tarih='" + dateTimePicker1.Value + "' Where Id='"+id+"' ";
                if (CRUD.ESG(sql))
                {
                    Kaydedildi = true;
                    MessageBox.Show("Güncelleme işlemi başarıyla gerçekleştirildi.");
                    this.Close();
                }
            }
        }
    }
}
