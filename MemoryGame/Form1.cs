using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemoryGame
{
    public partial class Form1 : Form
    {
        List<Bitmap> pictures = new List<Bitmap>()
        {
            Properties.Resources.img1,
            Properties.Resources.img1,
            Properties.Resources.img2,
            Properties.Resources.img2,
            Properties.Resources.img3,
            Properties.Resources.img3,
            Properties.Resources.img4,
            Properties.Resources.img4,
            Properties.Resources.img5,
            Properties.Resources.img5,
            Properties.Resources.img6,
            Properties.Resources.img6,
            Properties.Resources.img7,
            Properties.Resources.img7,
            Properties.Resources.img8,
            Properties.Resources.img8,
            Properties.Resources.img9,
            Properties.Resources.img9,
            Properties.Resources.img10,
            Properties.Resources.img10
        };


        private int tries = 0;
        private int totalSeconds = 0;
        PictureBox first, second;
        bool done = false;
        private void Form1_Load(object sender, EventArgs e)
        {
            AssignImages();
        }

        public Form1()
        {
            InitializeComponent();
        }

        

        private void Tags(int index)
        {
            switch (index)
            {
                case 0:
                case 1:
                    pictures[index].Tag = "1";
                    break;
                case 2:
                case 3:
                    pictures[index].Tag = "2";
                    break;
                case 4:
                case 5:
                    pictures[index].Tag = "3";
                    break;
                case 6:
                case 7:
                    pictures[index].Tag = "4";
                    break;
                case 8:
                case 9:
                    pictures[index].Tag = "5";
                    break;
                case 10:
                case 11:
                    pictures[index].Tag = "6";
                    break;
                case 12:
                case 13:
                    pictures[index].Tag = "7";
                    break;
                case 14:
                case 15:
                    pictures[index].Tag = "8";
                    break;
                case 16:
                case 17:
                    pictures[index].Tag = "9";
                    break;
                case 18:
                case 19:
                    pictures[index].Tag = "10";
                    break;
            }
        }

        private void AssignImages()
        {
            Random r = new Random();

            for (int i = 0; i < pictures.Count; i++)
            {
                Tags(i);
            }

            foreach (Control c in this.Controls)
            {
                int j = r.Next(pictures.Count);
                PictureBox p = (PictureBox)c;
                p.InitialImage = pictures[j];
                p.Tag = pictures[j].Tag;
                pictures.RemoveAt(j);
            }
        }



        private void picture_Click(object sender, EventArgs e)
        {
            

            if (!totalTimer.Enabled)
            {
                totalTimer.Start();
            }

            PictureBox cast = (PictureBox)sender;
            

            if (cast.BackgroundImage == null && done == false)
            {
                if (first == null)
                {
                    first = cast;
                    first.BackgroundImage = first.InitialImage;
                    return;
                }
                second = cast;
                second.BackgroundImage = second.InitialImage;
                done = true;
                timer1.Start();
            }
        }


        private void Timer(object sender, EventArgs e)
        {
            timer1.Stop();

            if (first.Tag.ToString() == second.Tag.ToString())
            {
                CheckForWinner();
            }
            else if (first.Tag.ToString() != second.Tag.ToString())
            {
                AnimateCard(first);
                AnimateCard(second);
                //first.BackgroundImage = null;
                //second.BackgroundImage = null;
            }
            first = null;
            second = null;
            done = false;
            tries += 1;
        }

        private void AnimateCard(PictureBox picture)
        {
            Size defaultSize = picture.Size;
            Size temp = defaultSize;


            for (int w = defaultSize.Width; w >= 0; w -= 5)
            {
                temp.Width = w;
                picture.Size = temp;
                picture.BackgroundImage = null;
                picture.Refresh();
                Thread.Sleep(10);
            }


            for (int w = temp.Width; w <= defaultSize.Width; w += 5)
            {
                temp.Width = w;
                picture.Size = temp;
                picture.BackgroundImage = null;
                picture.Refresh();
                Thread.Sleep(10);
            }
        }

        private void CheckForWinner()
        {
            foreach (Control c in this.Controls)
            {
                PictureBox pb = (PictureBox)c;
                if (pb.BackgroundImage == null) return;
            }
            string messageQuit = $"Συγχαρητήρια κέρδισες.\r\nΧρόνος: {totalSeconds} δευτερόλεπτα, Προσπάθειες: {tries} \r\nΘέλεις να ξαναπαίξεις;" ;
            string titleQuit = "Win!";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(messageQuit, titleQuit, buttons);
            if (result == DialogResult.Yes)
            {
                this.Hide();
                var form = new Form1();

                form.Show();
                
            }
            else
            {
                Application.Exit();
            }
                
        }


        private void totalTimer_Tick(object sender, EventArgs e)
        {
            totalSeconds += 1;
        }

       
    }
}
