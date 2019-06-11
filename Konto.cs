using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DealOrNoDeal
{
    public class Konto : Form
    {
        public double kontostand;

        public double Auflösen()
        {
            MessageBoxButtons knopf = MessageBoxButtons.YesNo;
            DialogResult entscheidung;
            entscheidung = MessageBox.Show("Wollen Sie Ihr Konto wirklich auflösen?", "", knopf);

            if (entscheidung == System.Windows.Forms.DialogResult.Yes)
            {
                System.IO.File.WriteAllText(@"C:\Users\Public\Documents\Kontostand.txt", String.Empty);

                System.IO.File.WriteAllText(@"C:\Users\Public\Documents\Kontostand.txt", Convert.ToString(0));

                using (System.IO.StreamReader lesen = new System.IO.StreamReader(@"C:\Users\Public\Documents\Kontostand.txt"))
                {
                    kontostand = Convert.ToDouble(lesen.ReadLine());
                }

                return kontostand;
            }

            else
            {
                return kontostand;
            }
        }

        public void Speichern()
        {
            System.IO.File.WriteAllText(@"C:\Users\Public\Documents\Kontostand.txt", String.Empty);

            System.IO.File.WriteAllText(@"C:\Users\Public\Documents\Kontostand.txt", Convert.ToString(kontostand));
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Konto
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "Konto";
            this.Load += new System.EventHandler(this.Konto_Load);
            this.ResumeLayout(false);

        }

        private void Konto_Load(object sender, EventArgs e)
        {

        }
    }
}
