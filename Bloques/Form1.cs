using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bloques
{
    public partial class Form1 : Form
    {
        int BlockSize = 50;
        public Form1()
        {
            InitializeComponent();
            Renew();
        }

        int[] orden = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
        void Renew()
        {
            int[] numeros = Randomize(orden);


            int i = 0;
            int row = 0;
            int col = 0;
            foreach (Control c in Controls)
                if (c is Button)
                {
                    c.Text = numeros[i].ToString();
                    c.Location = new Point(col * 50, row * 50);
                    i++;
                    col++;
                    if(col > 3)
                    {
                        row++;
                        col = 0;
                    }
                    //posiciones[i] = new int[] { c.Location.X, c.Location.Y };
                }

            libre = new int[] { col * 50, row * 50 };
        }

        int[] Randomize(int[] numeros)
        {
            for (int t = 0; t < numeros.Length; t++)
            {
                int tmp = numeros[t];
                int r = new Random().Next(t, numeros.Length);
                numeros[t] = numeros[r];
                numeros[r] = tmp;
            }
            return numeros;
        }

        int[] libre = new int[2];
        private void Mover(object sender, EventArgs e)
        {

            int x = ((Button)sender).Location.X;
            int y = ((Button)sender).Location.Y;
            

            if( (x+50 == libre[0] || x-50 == libre[0]) && y == libre[1])
            {
                int nx = (x + 50 == libre[0] ? x + 50 : x - 50);
                ((Button)sender).Location = new Point(nx, y);
                libre = new int[] { x, y };
            }
            else if ((y + 50 == libre[1] || y - 50 == libre[1]) && x == libre[0])
            {
                int ny = (y + 50 == libre[1] ? y + 50 : y - 50);
                ((Button)sender).Location = new Point(x, ny);
                libre = new int[] { x, y };
            }
            Verificar();
        }
        void Verificar()
        {
            foreach (Control c in Controls)
                if (c.Location == new Point(0, 0) && c.Text == "1")
                    MessageBox.Show("Si jala weee");

        }
    }
}
