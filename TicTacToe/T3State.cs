using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// TicTacToe (Game Logic)
// Versie: 0.1
// Datum : 2014-08-14
// Auteur: Arjan Bosse

namespace TicTacToe
{
    // Het lopende TicTacToe spel
    //
    class T3State
    {
        private String[] bord;
        private String aanzet;
        private String winnaar;
        private String winst;
        private int zettenGedaan;
        private int[] voorkeur;
        private int[] history;

        // Conctructor
        //
        public T3State()
        {
            bord = new String[9] { "", "", "", "", "", "", "", "", "" };
            aanzet = "X";
            winnaar = "";
            winst = "";
            zettenGedaan = 0;
            history = new int[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            voorkeur = new int[9] { 4, 0, 8, 2, 6, 1, 7, 3, 5 };
        }

        // Maak laatste zet ongedaan
        // Retourneer veld waarop was gespeeld
        //
        public int terug()
        {
            int veld = 0;
            if (zettenGedaan > 0)
            {
                zettenGedaan--;
                veld = history[zettenGedaan] + 1;
                bord[veld-1] = "";
                history[zettenGedaan] = 0;
                winnaar = "";
                winst = "";
                aanzet = (aanzet == "X") ? "O" : "X";
            }
            return veld;
        }

        // Maak alle zetten ongedaan
        //
        public void reset()
        {
            while (terug() > 0) { }
        }

        // Nagaan of de gedane zet winstgevend is
        // Retourneer winnende velden
        //
        private String getUitslag(String gezet)
        {
            // Winst horizontaal
            if (gezet == bord[0] && bord[0] == bord[1] && bord[1] == bord[2]) return "123";
            if (gezet == bord[3] && bord[3] == bord[4] && bord[4] == bord[5]) return "456";
            if (gezet == bord[6] && bord[6] == bord[7] && bord[7] == bord[8]) return "789";

            // Winst verticaal
            if (gezet == bord[0] && bord[0] == bord[3] && bord[3] == bord[6]) return "147";
            if (gezet == bord[1] && bord[1] == bord[4] && bord[4] == bord[7]) return "258";
            if (gezet == bord[2] && bord[2] == bord[5] && bord[5] == bord[8]) return "369";

            // Winst diagonaal
            if (gezet == bord[0] && bord[0] == bord[4] && bord[4] == bord[8]) return "159";
            if (gezet == bord[2] && bord[2] == bord[4] && bord[4] == bord[6]) return "357";

            // Geen winst
            return "";
        }

        // Get functies
        //
        public String getPlayer() { return aanzet; }
        public String getWinnaar() { return winnaar; }
        public String getWinst() { return winst; }
        public Boolean isGelijkspel() { return winnaar == "" && zettenGedaan == 9; }
        public String[] getBord() { return bord; }

        // Speel veld en werk status bij
        // Retourneer gespeelde zet
        //
        public String speel(int veld)
        {
            // Als zet niet mogelijk dan retourneer leeg
            if (winnaar != "") return "";
            if (veld < 1 || veld > 9) return "";
            if (bord[veld-1] != "") return "";

            // Er is een zet gedaan, werk bij
            bord[veld-1] = aanzet;

            // Geschiedenis
            history[zettenGedaan] = veld-1;

            // Na 9 zetten zonder winst is het gelijkspel
            zettenGedaan++;

            // Ga na or er een winst is
            winst = getUitslag(aanzet);

            // Winnaar
            if (winst != "") winnaar = aanzet;

            // Volgende aan zet
            aanzet = (aanzet == "O") ? "X" : "O";

            // Gespeelde kleur
            return bord[veld-1];
        }

        //  Datatype voor analyse methode
        //
        private struct Result
        {
            public int veld;
            public int score;
        }

        // Geef beste zet en score terug
        // Bord en gedane zetten van het lopende spel worden gebruikt
        // Na afloop zijn alle bijwerkingen weer ongedaan gemaakt
        // Achtergrond: dit is een recursieve implementatie van het minimax algoritme
        //
        private Result analyse(String speler)
        {
            Result r = new Result();
            String tegenstander = (speler == "X") ? "O" : "X";

            // Als tegenstander een winnende zet heeft gedaan, dan heeft speler verloren
            // Laat de mate van verlies of winst afhangen van het aantal gespeelde zetten
            // Zodoende probeert de winnaar snel te winnen en de verliezer langzaam te verliezen
            String w = getUitslag(tegenstander);
            if (w != "") { r.veld = 0; r.score = zettenGedaan - 10; return r; }

            // Als geen zet mogelijk, dan gelijkspel
            if (zettenGedaan == 9) { r.veld = 0; r.score = 0; return r; }

            // Zoek beste zet voor speler, dat is de zet die het slechtst scoort bij de tegenstander
            r.veld = 0;
            r.score = -10; // erger dan maximaal verlies, dan is er altijd een betere zet

            // Probeer alle beschikbare velden
            for (int i = 0; i < 9; i++)
            {
                // Heuristiek: eerst het midden, dan de hoeken, tenslotte de zijkanten
                int j = voorkeur[i];

                // Als leeg veld dan speel en ga na of dit een goede zet is
                if (bord[j] == "")
                {
                    // Doe zet voor speler
                    bord[j] = speler;
                    zettenGedaan++;

                    // Speler heeft gezet, nu is de tegenstander aan de beurt
                    // Bepaal score gezien vanuit de tegenstander (recursieve aanroep)
                    Result r2 = analyse(tegenstander);

                    // Draai teken van de score om (+ is winst, - is verlies, 0 is gelijkspel)
                    // Een negatieve score bij de tegenstander is een positieve score bij de speler
                    r2.score = -r2.score;

                    // Zet onthouden als deze beter is dan de beste tot nu toe
                    if (r2.score > r.score) { r.score = r2.score; r.veld = j + 1; }

                    // Neem zet terug, de speler moet de volgende proberen
                    zettenGedaan--;
                    bord[j] = "";
                }
            }

            // Geef beste zet en score terug
            return r;
        }

        // Adviseer een zet
        // Retourneer een veld
        //
        public int advies()
        {
            // Bord leeg, zet willekeurig
            if (zettenGedaan == 0)
            {
                return new Random().Next(1, 9);
            }

            // Zoek beste zet
            Result r = analyse(aanzet);
            return r.veld;
        }
    }
}
