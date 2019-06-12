using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DealOrNoDeal
{
    public class Spiel
    {
        Random zufall = new Random();
        public double[] geld = { 1, 10, 50, 100, 300, 500, 1000, 2500, 5000, 7500, 10000, 15000, 30000, 50000, 75000, 150000, 350000, 500000, 750000, 1000000 };
        double[] speicher = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        int speicherstatus = 0;
        double wert;
        public int durchlauf = 0;

        //diese methode ist ein zufallsgenerator, welcher einen geldbetrag in geld auswählt und zurückgibt
        public double Auswahl()
        {   
            int schleife = 0;

            while (schleife == 0)
            {
                wert = geld[zufall.Next(geld.Length)];

                for (int i = 0; i < geld.Length; i++)
                {
                    if (wert == speicher[i])
                    {
                        i = geld.Length;
                    }

                    if (i == geld.Length - 1)
                    {
                        speicher[speicherstatus] = wert;
                        speicherstatus++;
                        schleife++;
                    }
                }
            }

            durchlauf++;
            return wert;
        }

        //hier werden alle werte zurückgesetzt um das spiel von vorne zu berinnen
        public void SpielReset()
        {
            speicherstatus = 0;
            durchlauf = 0;

            for (int i = 0; i < geld.Length; i++)
            {
                speicher[i] = 0;
            }
        }
    }
}
