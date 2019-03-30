﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace StaffManagementVisualApplication
{
    public partial class YonetimBirimiApplication : Form
    {
        List<employee> employees = new List<employee>();
        public YonetimBirimiApplication()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lstviewİsci.Columns.Add("ID", 150);
            lstviewİsci.Columns.Add("ISIM", 50);
            lstviewİsci.Columns.Add("SOYISIM", 80);
            lstviewİsci.Columns.Add("MAAS", 80);
            lstviewİsci.Columns.Add("BMO", 80);
        }   

        private void btnAdd_Click(object sender, EventArgs e)
        {

            FormAddUpdate formaddupdate = new FormAddUpdate();
            formaddupdate.ShowDialog();
            employee yeni = formaddupdate.Staff;
            formaddupdate.Dispose();
            string[] isciozellik = new string[5];
            isciozellik[0] = yeni._id.ToString();
            isciozellik[1] = yeni._isim;
            isciozellik[2] = yeni._soyisim;
            isciozellik[3] = yeni._maas.ToString();
            isciozellik[4] = yeni.Bmoo.ToString();
            ListViewItem isci = new ListViewItem(isciozellik);
            lstviewİsci.Items.Add(isci);
            employees.Add(yeni);

        }

       
        private void btnYükle_Click(object sender, EventArgs e)
        {
            string yol = "";
            OpenFileDialog Yükle = new OpenFileDialog();
            
            Yükle.InitialDirectory = "D:\\";
            Yükle.Title = "Eleman listesinin bulunduğu dosyayı seçin";
            Yükle.Filter = "Comma Seperated File|*.csv|Tab Seperated File|*.tsv";
            
            if (Yükle.ShowDialog()==DialogResult.OK)
            {
                yol = Yükle.FileName;
            }
            int yüklefilter = Yükle.FilterIndex;

            using (var reader = new StreamReader(yol, Encoding.GetEncoding("iso-8859-9"), false))
            {
                while (!reader.EndOfStream)
                {
                    string[] Parcalanmisdesen;
                    string mydesen = reader.ReadLine();
                    if (yüklefilter == 1)
                    {
                        Parcalanmisdesen = mydesen.Split(',');
                    }
                    else if (yüklefilter == 2)
                    { 
                    Parcalanmisdesen = mydesen.Split('\t');
                    }
                    else
                    {
                        break;
                    }
                    employee yeni = new employee();

                    yeni._id = Convert.ToInt32(Parcalanmisdesen[0]);
                    yeni._isim = Parcalanmisdesen[1];
                    yeni._soyisim = Parcalanmisdesen[2];
                    yeni._adres = Parcalanmisdesen[3];
                    yeni._maas = Convert.ToInt32(Parcalanmisdesen[4]);
                    yeni._tecrube = Convert.ToInt32(Parcalanmisdesen[5]);
                    yeni._sehir = Convert.ToInt32(Parcalanmisdesen[6]);
                    yeni._ogrenim_seviyesi = Convert.ToInt32(Parcalanmisdesen[7]);
                    yeni._belge_ingilizce = Convert.ToBoolean(Parcalanmisdesen[8]);
                    yeni._okul_ingilizce = Convert.ToBoolean(Parcalanmisdesen[9]);
                    yeni._yabanci_dil_sayisi = Convert.ToInt32(Parcalanmisdesen[10]);
                    yeni._yoneticilik_gorevi = Convert.ToInt32(Parcalanmisdesen[11]);
                    yeni._evli_mi = Convert.ToBoolean(Parcalanmisdesen[12]);
                    yeni._kucuk_cocuk = Convert.ToInt32(Parcalanmisdesen[13]);
                    yeni._ortanca_cocuk = Convert.ToInt32(Parcalanmisdesen[14]);
                    yeni._buyuk_cocuk = Convert.ToInt32(Parcalanmisdesen[15]);
                    yeni._esi_calismiyomu = Convert.ToBoolean(Parcalanmisdesen[16]);
                    yeni.Bmoo = yeni.bmo();
                    string[] isciozellik = new string[5];
                    isciozellik[0] = yeni._id.ToString();
                    isciozellik[1] = yeni._isim;
                    isciozellik[2] = yeni._soyisim;
                    isciozellik[3] = yeni._maas.ToString()                        ;
                    isciozellik[4] = yeni.Bmoo.ToString();
                    employees.Add(yeni);
                    ListViewItem isci = new ListViewItem(isciozellik);
                    
                    employee.Counter = yeni._id;
                    lstviewİsci.Items.Add(isci);
                }
             
            }

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (lstviewİsci.CheckedItems.Count != 0)
            {
                for (int i = 0; i < lstviewİsci.CheckedItems.Count; i++)
                {
                    int index=lstviewİsci.Items.IndexOf(lstviewİsci.CheckedItems[i]);
                    employees.RemoveAt(index);
                    lstviewİsci.Items.Remove(lstviewİsci.CheckedItems[i]);

                    i = i - 1;
                }

                
            }
           
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SaveFileDialog Kaydet = new SaveFileDialog();
            Kaydet.Title = "Eleman listesinin bulunduğu dosyayı seçin";
            Kaydet.Filter = "Comma Seperated File|*.csv|Tab Seperated File|*.tsv";
            
            if (Kaydet.ShowDialog() == DialogResult.OK)
            {
                StreamWriter writer = new StreamWriter(Kaydet.OpenFile());
                int index = Kaydet.FilterIndex;
                if (index == 1)
                {
                    foreach (employee S in employees)
                    {
                        writer.WriteLine(S._id + "," + S._isim + "," + S._soyisim + "," + S._adres + "," + S._maas + "," + S._tecrube + "," + S._sehir + "," + S._ogrenim_seviyesi + "," + S._belge_ingilizce + "," + S._okul_ingilizce + "," + S._yabanci_dil_sayisi + "," + S._yoneticilik_gorevi + "," + S._evli_mi + "," + S._kucuk_cocuk + "," + S._ortanca_cocuk + "," + S._buyuk_cocuk + "," + S._esi_calismiyomu);
                    }
                    writer.Close();
                }
                if(index==2)
                {
                    foreach (employee S in employees)
                    {
                        writer.WriteLine(S._id + "\t" + S._isim + "\t" + S._soyisim + "\t" + S._adres + "\t" + S._maas + "\t" + S._tecrube + "\t" + S._sehir + "\t" + S._ogrenim_seviyesi + "\t" + S._belge_ingilizce + "\t" + S._okul_ingilizce + "\t" + S._yabanci_dil_sayisi + "\t" + S._yoneticilik_gorevi + "\t" + S._evli_mi + "\t" + S._kucuk_cocuk + "\t" + S._ortanca_cocuk + "\t" + S._buyuk_cocuk + "\t" + S._esi_calismiyomu);
                    }
                    writer.Close();

                }
            }
            
        }

        private void lstviewİsci_DoubleClick(object sender, EventArgs e)
        {

        }
    }
}
