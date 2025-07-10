using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// TicTacToe (User Interface)
// Versie: 0.1
// Datum : 2014-08-10
// Auteur: Arjan Bosse

namespace TicTacToe
{
    public partial class TicTacToe : Form
    {
        // Het lopende TicTacToe spel
        private GameState spel;

        // Font voor winnende buttons, maak tekst 2x zo groot en bold
        private Font winFont;
        private Font oldFont;

        public TicTacToe()
        {
            InitializeComponent();
        }

        //  Maak spel aan en refresh buttons
        //
        private void TicTacToe_Load(object sender, EventArgs e)
        {
            // Maak spel aan
            spel = new GameState();

            // Font voor buttons bij winst
            oldFont = button10.Font;
            winFont = new Font(button10.Font.FontFamily, button10.Font.Size * 2, FontStyle.Bold);

            // Buttons bijwerken
            refresh();
        }

        // Spel status is gewijzigd, werk de buttons bij
        //
        private void refresh()
        {
            String[] bord = spel.getBord();
            String winst = spel.getWinst();
            String winnaar = spel.getWinnaar();

            // Buttons fonts resetten
            button1.Font = oldFont;
            button2.Font = oldFont;
            button3.Font = oldFont;
            button4.Font = oldFont;
            button5.Font = oldFont;
            button6.Font = oldFont;
            button7.Font = oldFont;
            button8.Font = oldFont;
            button9.Font = oldFont;

            // Buttons teksten
            button1.Text = bord[0]; 
            button2.Text = bord[1];
            button3.Text = bord[2];
            button4.Text = bord[3];
            button5.Text = bord[4];
            button6.Text = bord[5];
            button7.Text = bord[6];
            button8.Text = bord[7];
            button9.Text = bord[8];

            // Buttons kleuren
            button1.ForeColor = (bord[0] == "X") ? Color.Red : Color.Green;
            button2.ForeColor = (bord[1] == "X") ? Color.Red : Color.Green;
            button3.ForeColor = (bord[2] == "X") ? Color.Red : Color.Green;
            button4.ForeColor = (bord[3] == "X") ? Color.Red : Color.Green;
            button5.ForeColor = (bord[4] == "X") ? Color.Red : Color.Green;
            button6.ForeColor = (bord[5] == "X") ? Color.Red : Color.Green;
            button7.ForeColor = (bord[6] == "X") ? Color.Red : Color.Green;
            button8.ForeColor = (bord[7] == "X") ? Color.Red : Color.Green;
            button9.ForeColor = (bord[8] == "X") ? Color.Red : Color.Green;

            // Winnende buttons highlighten
            if (winst != "")
            {
                for (int i = 0; i < 3; i++)
                {
                    if (winst[i] == '1') { button1.Font = winFont; }
                    if (winst[i] == '2') { button2.Font = winFont; }
                    if (winst[i] == '3') { button3.Font = winFont; }
                    if (winst[i] == '4') { button4.Font = winFont; }
                    if (winst[i] == '5') { button5.Font = winFont; }
                    if (winst[i] == '6') { button6.Font = winFont; }
                    if (winst[i] == '7') { button7.Font = winFont; }
                    if (winst[i] == '8') { button8.Font = winFont; }
                    if (winst[i] == '9') { button9.Font = winFont; }
                }
            }

            // Status button
            if (winnaar != "")
            {
                button10.Text = "Winnaar " + winnaar;
            }
            else if (spel.isGelijkspel())
            {
                button10.Text = "Gelijkspel";
            }
            else
            {
                button10.Text = "Speel " + spel.getPlayer();
            }

            // Buttons verversen
            button1.Invalidate();
            button2.Invalidate();
            button3.Invalidate();
            button4.Invalidate();
            button5.Invalidate();
            button6.Invalidate();
            button7.Invalidate();
            button8.Invalidate();
            button9.Invalidate();
            button10.Invalidate();
        }

        // Gebruiker speelt zelf
        // 
        private void button1_Click(object sender, EventArgs e) { button1.Text = spel.speel(1); refresh(); }
        private void button2_Click(object sender, EventArgs e) { button2.Text = spel.speel(2); refresh(); }
        private void button3_Click(object sender, EventArgs e) { button3.Text = spel.speel(3); refresh(); }
        private void button4_Click(object sender, EventArgs e) { button4.Text = spel.speel(4); refresh(); }
        private void button5_Click(object sender, EventArgs e) { button5.Text = spel.speel(5); refresh(); }
        private void button6_Click(object sender, EventArgs e) { button6.Text = spel.speel(6); refresh(); }
        private void button7_Click(object sender, EventArgs e) { button7.Text = spel.speel(7); refresh(); }
        private void button8_Click(object sender, EventArgs e) { button8.Text = spel.speel(8); refresh(); }
        private void button9_Click(object sender, EventArgs e) { button9.Text = spel.speel(9); refresh(); }

        // Gebruiker laat computer spelen
        //
        private void button10_Click(object sender, EventArgs e)
        {
            int advies = spel.advies();
            if (advies == 1) { button1.Text = spel.speel(1); }
            if (advies == 2) { button2.Text = spel.speel(2); }
            if (advies == 3) { button3.Text = spel.speel(3); }
            if (advies == 4) { button4.Text = spel.speel(4); }
            if (advies == 5) { button5.Text = spel.speel(5); }
            if (advies == 6) { button6.Text = spel.speel(6); }
            if (advies == 7) { button7.Text = spel.speel(7); }
            if (advies == 8) { button8.Text = spel.speel(8); }
            if (advies == 9) { button9.Text = spel.speel(9); }
            refresh();
        }

        // Neem zet terug
        private void button11_Click(object sender, EventArgs e)
        {
            spel.terug();
            refresh();
        }

        // Neem alle zetten terug
        private void button12_Click(object sender, EventArgs e)
        {
            spel.reset();
            refresh();
        }
    }
}
