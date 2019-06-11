using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DealOrNoDeal
{
    public partial class Form1 : Form
    {
        Spiel spiel1 = new Spiel();
        Konto konto1 = new Konto();
        public int spezialkoffer = 0;
        public int nächstesAngebot = 6;
        public int nächstesÄngebotMultiplikator = 0;
        public int nächstesAngebotEntscheidung = 0;
        public string währung = "€";
        public int schriftgrösse1 = 36;
        public int schriftgrösse2 = 18;
        public string schriftstiel = "Tahoma";
        public string währungsabstand1 = ""; //leerlaste
        public string währungsabstand2 = ""; //leertase oder zeilenumbruch

        public void Koffer(int x)
        {
            //ÄNDERN BEI KOFFERADDITION
            Button[] buttonKoffer = { button1, button2, button3, button4, button5, button6, button7, button8, button9, button10, button11, button12, button13, button14, button15, button16, button17, button18, button19, button20 };
            int i = 0;
            x = x - 1;

            nächstesAngebot--;

            if (spiel1.durchlauf == spiel1.geld.Length - 3)
            {
                labelAnzeige.Text = " - Welchen Koffer nehmen Sie - ";

                while (i < spiel1.geld.Length)
                {
                    if (buttonKoffer[i].BackColor == Color.DarkGreen || buttonKoffer[i].BackColor == Color.Gold)
                    {
                        buttonKoffer[i].BackColor = Color.Green;
                        buttonKoffer[i].Enabled = true;
                    }

                    else
                    {
                        i++;
                    }
                }

                i = 0;
            }

            if (spezialkoffer == 0)
            {
                buttonKoffer[x].Enabled = false;
                buttonKoffer[x].BackColor = Color.DarkGreen;
                spezialkoffer = 1;
            }

            else
            {
                buttonKoffer[x].Enabled = false;
                double a = spiel1.Auswahl();
                buttonKoffer[x].Text = Convert.ToString(a + währungsabstand2 + währung);
                listBoxGeld.Items.Remove(a + währungsabstand1 + währung);
                buttonKoffer[x].BackColor = Color.Gray;
                buttonKoffer[x].Font = new Font(schriftstiel, schriftgrösse2);

                if (spiel1.durchlauf == spiel1.geld.Length - 1)
                {
                    buttonKoffer[x].BackColor = Color.DarkGreen;
                    labelAnzeige.Text = " - Sie haben " + a + währungsabstand1 + währung + " gewonnen - ";
                    konto1.kontostand = konto1.kontostand + a;
                    labelKonto.Text = Convert.ToString(konto1.kontostand) + währungsabstand2 + währung;

                    while (i < spiel1.geld.Length)
                    {
                        if (buttonKoffer[i].BackColor == Color.Green)
                        {
                            buttonKoffer[i].Enabled = false;
                            a = spiel1.Auswahl();
                            buttonKoffer[i].Text = Convert.ToString(a) + währungsabstand2 + währung;
                            listBoxGeld.Items.Remove(a + währungsabstand1 + währung);
                            buttonKoffer[i].BackColor = Color.Gray;
                            buttonKoffer[i].Font = new Font(schriftstiel, schriftgrösse2);
                        }

                        i++;
                    }

                    listBoxGeld.BackColor = Color.Gray;
                }
            }

            if (spiel1.durchlauf != spiel1.geld.Length)
            {
                if (spiel1.durchlauf == spiel1.geld.Length - 3 || spiel1.durchlauf == spiel1.geld.Length - 2)
                {
                    labelAnzeige.Text = "";
                }

                else
                {
                    labelAnzeige.Text = " - Noch " + nächstesAngebot + " Koffer bis zum Angebot - ";
                }

                if (spiel1.durchlauf == 17 || spiel1.durchlauf == 14 || spiel1.durchlauf == 10 || spiel1.durchlauf == 5)
                {
                    Angebot();
                }
            }
        }

        public void Angebot()
        {
            //ÄNDERN BEI KOFFERADDITION
            Button[] buttonKoffer = { button1, button2, button3, button4, button5, button6, button7, button8, button9, button10, button11, button12, button13, button14, button15, button16, button17, button18, button19, button20 };
            double angebot = 0;
            char[] chararray = währung.ToCharArray();
            int i = 0;

            for (i = 0; i < spiel1.geld.Length; i++)
            {
                if (buttonKoffer[i].BackColor == Color.Gold)
                {
                    buttonKoffer[i].Enabled = false;
                }
            }

            i = 0;

            for (i = 0; i < listBoxGeld.Items.Count; i++)
            {
                listBoxGeld.SetSelected(i, true);
                angebot = angebot + Convert.ToDouble(Convert.ToString(listBoxGeld.SelectedItem).TrimEnd(chararray));
            }

            i = 0;

            listBoxGeld.SetSelected(listBoxGeld.Items.Count - 1, false);
            angebot = angebot / Convert.ToDouble(listBoxGeld.Items.Count);

            labelAnzeige.Text = " - Nehmen Sie das Angebot an - \r" + Convert.ToString(Math.Truncate(angebot)) + währungsabstand1 + währung;
            buttonAngebotEntscheidungJa.Visible = true;
            buttonAngebotEntscheidungNein.Visible = true;

            while (nächstesAngebotEntscheidung == 0)
            {
                System.Threading.Thread.Sleep(10);
                Application.DoEvents();
            }

            if (nächstesAngebotEntscheidung == 1)
            {
                konto1.kontostand = konto1.kontostand + Math.Truncate(angebot);
                labelKonto.Text = (Convert.ToString(konto1.kontostand) + währungsabstand1 + währung);
                labelAnzeige.Text = " - Sie haben " + Math.Truncate(angebot) + währungsabstand1 + währung + " gewonnen - ";

                for (i = 0; i < spiel1.geld.Length; i++)
                {
                    buttonKoffer[i].BackColor = Color.Gray;
                    buttonKoffer[i].Enabled = false;
                }

                i = 0;

                listBoxGeld.Items.Clear();
                listBoxGeld.BackColor = Color.Gray;
            }

            else
            {
                for (i = 0; i < spiel1.geld.Length; i++)
                {
                    if (buttonKoffer[i].BackColor == Color.Gold)
                    {
                        buttonKoffer[i].Enabled = true;
                    }
                }

                i = 0;

                nächstesAngebot = 5 - nächstesÄngebotMultiplikator;
                nächstesÄngebotMultiplikator++;
                buttonAngebotEntscheidungJa.Visible = false;
                buttonAngebotEntscheidungNein.Visible = false;
                nächstesAngebotEntscheidung = 0;

                if (spiel1.durchlauf != 17)
                {
                    labelAnzeige.Text = " - Noch " + nächstesAngebot + " Koffer bis zum Angebot - ";
                }

                else
                {
                    labelAnzeige.Text = "";
                }
            }
        }

        public void Ausblenden()
        {
            //ÄNDERN BEI KOFFERADDITION
            Button[] buttonKoffer = { button1, button2, button3, button4, button5, button6, button7, button8, button9, button10, button11, button12, button13, button14, button15, button16, button17, button18, button19, button20 };

            listBoxGeld.Visible = false;
            buttonSchliessen.Visible = false;
            buttonSpeichern.Visible = false;
            buttonZurücksetzen.Visible = false;
            buttonEinstellungen.Visible = false;
            labelAnzeige.Visible = false;
            labelKonto.Visible = false;

            for (int i = 0; i < spiel1.geld.Length; i++)
            {
                buttonKoffer[i].Visible = false;
            }
        }

        public void Einblenden()
        {
            //ÄNDERN BEI KOFFERADDITION
            Button[] buttonKoffer = { button1, button2, button3, button4, button5, button6, button7, button8, button9, button10, button11, button12, button13, button14, button15, button16, button17, button18, button19, button20 };

            listBoxGeld.Visible = true;
            buttonSchliessen.Visible = true;
            buttonSpeichern.Visible = true;
            buttonZurücksetzen.Visible = true;
            buttonEinstellungen.Visible = true;
            labelAnzeige.Visible = true;
            labelKonto.Visible = true;

            for (int i = 0; i < spiel1.geld.Length; i++)
            {
                buttonKoffer[i].Visible = true;
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            buttonEuro.BackColor = Color.DarkGreen;
            buttonEuro.Enabled = false;
            labelKonto.Text = "0" + währungsabstand2 + währung;

            using (System.IO.StreamReader lesen = new System.IO.StreamReader(@"C:\Users\Public\Documents\Kontostand.txt"))
            {
                konto1.kontostand = Convert.ToDouble(lesen.ReadLine());
            }

            labelKonto.Text = Convert.ToString(konto1.kontostand) + währungsabstand2 + währung;

            for (int i = 0; i < spiel1.geld.Length; i++)
            {
                listBoxGeld.Items.Add(spiel1.geld[i] + währungsabstand1 + währung);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Koffer(1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Koffer(2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Koffer(3);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Koffer(4);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Koffer(5);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Koffer(6);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Koffer(7);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Koffer(8);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Koffer(9);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Koffer(10);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Koffer(11);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Koffer(12);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Koffer(13);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Koffer(14);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Koffer(15);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            Koffer(16);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            Koffer(17);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            Koffer(18);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            Koffer(19);
        }

        private void button20_Click(object sender, EventArgs e)
        {
            Koffer(20);
        }

        private void buttonEinstellungen_Click(object sender, EventArgs e)
        {
            Ausblenden();
            buttonEinstellungenZurück.Visible = true;
            buttonEuro.Visible = true;
            buttonDollar.Visible = true;
            buttonAuflösen.Visible = true;
        }

        private void buttonEinstellungenZurück_Click(object sender, EventArgs e)
        {
            buttonEinstellungenZurück.Visible = false;
            buttonEuro.Visible = false;
            buttonDollar.Visible = false;
            buttonAuflösen.Visible = false;
            Einblenden();
        }

        private void buttonEuro_Click(object sender, EventArgs e)
        {
            MessageBoxButtons knopf = MessageBoxButtons.OKCancel;
            DialogResult entscheidung;
            entscheidung = MessageBox.Show("Dies setzt das aktuelle Spiel zurück", "", knopf);

            if (entscheidung == System.Windows.Forms.DialogResult.OK)
            {
                buttonEuro.Enabled = false;
                buttonDollar.Enabled = true;
                buttonEuro.BackColor = Color.DarkGreen;
                buttonDollar.BackColor = Color.Silver;
                währung = "€";
                labelKonto.Text = konto1.kontostand + währungsabstand2 + währung;
                buttonZurücksetzen_Click(0, e);
            }
        }

        private void buttonDollar_Click(object sender, EventArgs e)
        {
            MessageBoxButtons knopf = MessageBoxButtons.OKCancel;
            DialogResult entscheidung;
            entscheidung = MessageBox.Show("Dies setzt das aktuelle Spiel zurück", "", knopf);

            if (entscheidung == System.Windows.Forms.DialogResult.OK)
            {
                buttonEuro.Enabled = true;
                buttonDollar.Enabled = false;
                buttonEuro.BackColor = Color.Silver;
                buttonDollar.BackColor = Color.DarkGreen;
                währung = "$";
                labelKonto.Text = konto1.kontostand + währungsabstand2 + währung;
                buttonZurücksetzen_Click(0, e);
            }
        }

        private void buttonAngebotEntscheidungJa_Click(object sender, EventArgs e)
        {
            nächstesAngebotEntscheidung = 1;
        }

        private void buttonAngebotEntscheidungNein_Click(object sender, EventArgs e)
        {
            nächstesAngebotEntscheidung = 2;
        }

        private void buttonSchliessen_Click(object sender, EventArgs e)
        {
            nächstesAngebotEntscheidung = 2;
            Close();
        }

        private void buttonSpeichern_Click(object sender, EventArgs e)
        {
            konto1.Speichern();
        }

        private void buttonAuflösen_Click(object sender, EventArgs e)
        {
            labelKonto.Text = Convert.ToString(konto1.Auflösen()) + währungsabstand2 + währung;
        }

        private void buttonZurücksetzen_Click(object sender, EventArgs e)
        {
            //ÄNDERN BEI KOFFERADDITION
            Button[] buttonKoffer = { button1, button2, button3, button4, button5, button6, button7, button8, button9, button10, button11, button12, button13, button14, button15, button16, button17, button18, button19, button20 };

            spezialkoffer = 0;
            nächstesAngebot = 6;
            nächstesÄngebotMultiplikator = 0;
            nächstesAngebotEntscheidung = 0;
            spiel1.SpielReset();
            labelAnzeige.Text = " - Wählen Sie Ihren Spezialkoffer - ";
            buttonAngebotEntscheidungJa.Visible = false;
            buttonAngebotEntscheidungNein.Visible = false;
            listBoxGeld.BackColor = Color.Silver;
            listBoxGeld.Items.Clear();

            for (int i = 0; i < spiel1.geld.Length; i++)
            {
                listBoxGeld.Items.Add(spiel1.geld[i] + währungsabstand1 + währung);
            }

            for (int i = 0; i < spiel1.geld.Length; i++)
            {
                buttonKoffer[i].Enabled = true;
                buttonKoffer[i].BackColor = Color.Gold;
                buttonKoffer[i].Font = new Font(schriftstiel, schriftgrösse1);
                buttonKoffer[i].Text = Convert.ToString(i + 1);
            }
        }

        private void LabelKonto_Click(object sender, EventArgs e)
        {

        }
    }
}
