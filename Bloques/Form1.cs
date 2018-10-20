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
        int BlockSize = 100;

        public Form1()
        {
            InitializeComponent();
            Renew();
        }

        int[] orden = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
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
                    c.Size = new Size(BlockSize, BlockSize);
                    c.Location = new Point(col * BlockSize, row * BlockSize);
                    c.Font = new Font(FontFamily.GenericMonospace, (BlockSize / 50) * 15, FontStyle.Bold);
                    i++;
                    col++;
                    if(col > 3)
                    {
                        row++;
                        col = 0;
                    }

                }
            libre = new int[] { col * BlockSize, row * BlockSize };
            Verificar();
        }

        static int[] Randomize(int[] original)
        {
            int[] numeros = new int[original.Length];
            for (int i = 0; i < original.Length; i++)
                numeros[i] = original[i];

            Random R = new Random();
            for (int t = 0; t < numeros.Length; t++)
            {
                int tmp = numeros[t];
                int r = R.Next(t, numeros.Length);
                numeros[t] = numeros[r];
                numeros[r] = tmp;
            }
            return numeros;
        }

        int[] libre = new int[2];
        enum Direction { None, Top, Right, Down, Left };
        void Mover(object sender, EventArgs e)
        {
            int x = ((Button)sender).Location.X;
            int y = ((Button)sender).Location.Y;
            Direction D = Direction.None;

            // Si estamos en la misma fila del libre
            if (y == libre[1])
            {
                // Checamos hacia donde nos moveremos de acuerdo a nuestra posición.
                D = libre[0] > x ? Direction.Right : Direction.Left;

                //  Recorremos todos los controles
                foreach (object c in Controls)
                {
                    //  Que sean botones y no sean el sender
                    if (c is Button && c != sender)
                    {
                        Button B = ((Button)c);
                        //  Checamos que este en el mismo y
                        if (B.Location.Y == y)
                        {
                            //  Si es que vamos a la derecha
                            if (D == Direction.Right)
                            {
                                //  Verificamos que el control este delante de nosotros pero antes del libre
                                if (B.Location.X > x && B.Location.X < libre[0])
                                    //Lo movemos
                                    B.Location = new Point(B.Location.X + BlockSize, y);
                            }
                            //  Si es que vamos a la izquierda
                            if (D == Direction.Left)
                            {
                                //  Verificamos que el control este detras de nosotros y despues del libre
                                if (x > B.Location.X && libre[0] < B.Location.X)
                                    //Lo movemos
                                    B.Location = new Point(B.Location.X - BlockSize, y);
                            }
                        }
                    }
                }
                                
                //  Una vez que los hemos movido, movemos el nuestro a su posición
                            

                int nx = (D == Direction.Right ? x + BlockSize : x - BlockSize);
                ((Button)sender).Location = new Point(nx, y);
                libre = new int[] { x, y };
            }
            else if (x == libre[0])
            {
                // Checamos hacia donde nos moveremos de acuerdo a nuestra posición.
                D = libre[1] > y ? Direction.Down : Direction.Top;

                //  Recorremos todos los controles
                foreach (object c in Controls)
                {
                    //  Que sean botones y no sean el sender
                    if (c is Button && c != sender)
                    {
                        Button B = ((Button)c);
                        //  Checamos que este en el mismo y
                        if (B.Location.X == x)
                        {
                            //  Si es que vamos a la derecha
                            if (D == Direction.Down)
                            {
                                //  Verificamos que el control este delante de nosotros pero antes del libre
                                if (B.Location.Y > y && B.Location.Y < libre[1])
                                    //Lo movemos
                                    B.Location = new Point(x, B.Location.Y + BlockSize);
                            }
                            //  Si es que vamos a la izquierda
                            if (D == Direction.Top)
                            {
                                //  Verificamos que el control este detras de nosotros y despues del libre
                                if (y > B.Location.Y && libre[1] < B.Location.Y)
                                    //Lo movemos
                                    B.Location = new Point(x, B.Location.Y - BlockSize);
                            }
                        }
                    }
                }

                int ny = (D == Direction.Down ? y + BlockSize : y - BlockSize);
                ((Button)sender).Location = new Point(x, ny);
                libre = new int[] { x, y };
            }
            Verificar();
        }
        void Verificar()
        {
            int ready = 0;
            foreach (Control c in Controls)
                if (c is Button)
                {
                    Button B = (Button)c;
                    int num = int.Parse(B.Text) - 1;
                    int row = num / 4;
                    int col = num - (row * 4);

                    if (B.Location.X == col * BlockSize && B.Location.Y == row * BlockSize)
                    {
                        B.ForeColor = Color.DarkGreen;
                        ready++;
                    }
                    else
                        B.ForeColor = Color.Black;
                }
            
            
            if(ready >= 15)
                MessageBox.Show("Ganastesssss");

        }

        private void KeyUps(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.R)
            {
                if (DialogResult.Yes == MessageBox.Show("Se va a reiniciar el juego. ¿Continuar?", "Reiniciando", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    Renew();
            }
        }
    }
}
