using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        public Form1()
        {
            InitializeComponent();
        }
        //izpis elementov tabele
        public void Izpis(List<int> zb)
        {
            string izpis = "";
            foreach (int element in zb)
            {
                izpis += element + " ";
            }
                textBox2.Text= izpis;
        }
        static List<int> TabStingovVtabInt(string a)
        {
            List<int> zbirkaSt = new List<int>();
            try {string[] tabelaStevilNizi = a.Split(' ');
                
                if (tabelaStevilNizi[0] != "")
                {
                    for (int i = 0; i < tabelaStevilNizi.Length; i++)
                    {

                        zbirkaSt.Add(Convert.ToInt32(tabelaStevilNizi[i]));
                    }
                }
                else
                {
                    MessageBox.Show("Ni podatkov.");
                }
                return zbirkaSt;
            }
            catch { MessageBox.Show("Nepravilen vnos.");
                return zbirkaSt;
            }
        }
        DateTime zacetek, konec;
        string[] tabelaStevilNizi;
        List<int> zbirkaStevil=new List<int>();
        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Vnesi nekaj celih števil v zgornje polje. Loči jih s presledki.");
            if (textBox1.Text != "")//v textboxu dejansko nekaj je, ni prazen
            {
                string stevila = textBox1.Text;
                //razbijemo string števil v tabelo celih števil
                try { tabelaStevilNizi = stevila.Split(' '); }
                catch { MessageBox.Show("Nepravilen vnos."); }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            float fcpu = pCPU.NextValue();
            float fram = pRAM.NextValue();
            metroProgressBarCPU.Value = (int)fcpu;
            metroProgressBarRAM.Value = (int)fram;
            lblCPU.Text = string.Format("{0:0.00}%", fcpu);
            lblRAM.Text = string.Format("{0:0.00}%", fram);
            chart1.Series["CPU"].Points.AddY(fcpu);
            chart1.Series["RAM"].Points.AddY(fram);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Form3 f = new Form3();
            if (f.ShowDialog() == DialogResult.OK)
            {
                string stevila = "";
                Random r = new Random();
                for (int i = 0; i < f.numericUpDown1.Value; i++)
                {
                    if (i == f.numericUpDown1.Value - 1)//zadnje število
                    {//ne doda presledka za zadnjim elementom
                        stevila += r.Next(Convert.ToInt32(f.textBox1.Text), Convert.ToInt32(f.textBox2.Text) + 1);
                    }
                    else
                    {
                        stevila += r.Next(Convert.ToInt32(f.textBox1.Text), Convert.ToInt32(f.textBox2.Text) + 1) + " ";
                    }
                }
                textBox1.Text = stevila;
            }
        }
        //Mehurčno Urejnaje
        static List<int> MehurcnoUrejanje(List<int> zbirka)
        {
            int st;
            for (int p = 0; p <= zbirka.Count - 2; p++)
            {
                for (int i = 0; i <= zbirka.Count - 2; i++)
                {
                    if (zbirka[i] > zbirka[i + 1])

                    {
                        st = zbirka[i + 1];
                        zbirka[i + 1] = zbirka[i];
                        zbirka[i] = st;
                    }
                }
            }
            return zbirka;
        }
        //Shellovo urejanje
        static List<int> shellovoUrejanje(List<int> zbirka, int zbirkaVelikost)
        {
            int i, a, b, c;
            b = 3;
            while (b > 0)
            {
                for (i = 0; i < zbirkaVelikost; i++)
                {
                    a = i;
                    c = zbirka[i];
                    while ((a >= b) && (zbirka[a - b] > c))
                    {
                        zbirka[a] = zbirka[a - b];
                        a = a - b;
                    }
                    zbirka[a] = c;
                }
                if (b / 2 != 0)
                    b = b / 2;
                else if (b == 1)
                    b = 0;
                else
                    b = 1;
            }
            return zbirka;
        }
        //Urejanje z navadnim vstavljanjem
        static List<int> Urejanje_zVstavljanjem(List<int> vnos)
        {
            for (int i = 0; i < vnos.Count - 1; i++)
            {
                for (int j = i + 1; j > 0; j--)
                {
                    if (vnos[j - 1] > vnos[j])
                    {
                        int a = vnos[j - 1];
                        vnos[j - 1] = vnos[j];
                        vnos[j] = a;
                    }
                }
            }
            return vnos;
        }
        //Urejanje s kopico 
        static List<int> UrejanjeSKopico(List<int> tabela)
        {
            var dolzina = tabela.Count;
            for (int i = dolzina / 2 - 1; i >= 0; i--)
            {
                Kopicenje(tabela, dolzina, i);
            }
            for (int i = dolzina - 1; i >= 0; i--)
            {
                int a = tabela[0];
                tabela[0] = tabela[i];
                tabela[i] = a;
                Kopicenje(tabela, i, 0);
            }
            return tabela;
        }
        static void Kopicenje(List<int> tabela, int dolzina, int i)
        {
            int najvecje = i;
            int levo = 2 * i + 1;
            int desno = 2 * i + 2;
            if (levo < dolzina && tabela[levo] > tabela[najvecje])
            {
                najvecje = levo;
            }
            if (desno < dolzina && tabela[desno] > tabela[najvecje])
            {
                najvecje = desno;
            }
            if (najvecje != i)
            {
                int swap = tabela[i];
                tabela[i] = tabela[najvecje];
                tabela[najvecje] = swap;
                Kopicenje(tabela, dolzina, najvecje);
            }
        }
        //Hitro urejanje
        public static List<int> HitroUrejanje(List<int> tabela, int levo, int desno)
        {
            if (levo < desno)
            {
                int pivot = Particija(tabela, levo, desno);

                if (pivot > 1)
                {
                    HitroUrejanje(tabela, levo, pivot - 1);
                }
                if (pivot + 1 < desno)
                {
                    HitroUrejanje(tabela, pivot + 1, desno);
                }
            }
            return tabela;
        }
        private static int Particija(List<int> tabela, int levo, int desno)
        {
            int pivot = tabela[levo];
            while (true)
            {
                while (tabela[levo] < pivot)
                {
                    levo++;
                }
                while (tabela[desno] > pivot)
                {
                    desno--;
                }
                if (levo < desno)
                {
                    if (tabela[levo] == tabela[desno]) return desno;

                    int a = tabela[levo];
                    tabela[levo] = tabela[desno];
                    tabela[desno] = a;
                }
                else
                {
                    return desno;
                }
            }
        }
        private void bubbleSortToolStripMenuItem_Click(object sender, EventArgs e)
        {
                zacetek = DateTime.Now;
                Izpis(MehurcnoUrejanje(TabStingovVtabInt(textBox1.Text)));
                konec = DateTime.Now;
                TimeSpan cas = konec - zacetek;
                textBox3.Text = cas.Seconds + "," + cas.Milliseconds;
        }
        private void shellSortToolStripMenuItem_Click(object sender, EventArgs e)
        {
            zacetek = DateTime.Now;
            List<int> vhodnaZbirka = TabStingovVtabInt(textBox1.Text);
            Izpis(shellovoUrejanje(vhodnaZbirka,vhodnaZbirka.Count));
            konec = DateTime.Now;
            TimeSpan cas = konec - zacetek;
            textBox3.Text = cas.Seconds + "," + cas.Milliseconds;
        }
        private void urejanjeZNavadnimVstavljanjemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            zacetek = DateTime.Now;
            Izpis(Urejanje_zVstavljanjem(TabStingovVtabInt(textBox1.Text)));
            konec = DateTime.Now;
            TimeSpan cas = konec - zacetek;
            textBox3.Text = cas.Seconds + "," + cas.Milliseconds;
        }

        private void UrejanjeSKopicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            zacetek = DateTime.Now;
            Izpis(UrejanjeSKopico(TabStingovVtabInt(textBox1.Text)));
            konec = DateTime.Now;
            TimeSpan cas = konec - zacetek;
            textBox3.Text = cas.Seconds + "," + cas.Milliseconds;
        }

        private void HitroUrejanjeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<int> vhodnaZbirka = TabStingovVtabInt(textBox1.Text);
            zacetek = DateTime.Now;
            Izpis(HitroUrejanje(vhodnaZbirka, 0, vhodnaZbirka.Count - 1 ));
            konec = DateTime.Now;
            TimeSpan cas = konec - zacetek;
            textBox3.Text = cas.Seconds + "," + cas.Milliseconds;
        }
        private void Label6_Click(object sender, EventArgs e)
        {

        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }

        private void izhodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MetroLabel2_Click(object sender, EventArgs e)
        {

        }
    }
}
