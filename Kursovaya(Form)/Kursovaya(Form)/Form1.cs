using kursovayaConsole;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kursovaya_Form_
{
    public partial class Form1 : Form
    {
        Cinema cinema = new Cinema("Kinomax");
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = cinema.showInformationAboutCinema();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bool load;
            int examination;
            if (textBox1.Text != string.Empty && textBox17.Text != string.Empty && (load = int.TryParse(textBox17.Text, out examination))==true)
            {
                cinema.addFirst(textBox1.Text, Convert.ToInt32(textBox17.Text));
                textBox1.Text = string.Empty;
                textBox17.Text = string.Empty;
            }
            else
            {
                MessageBox.Show("ERROR. Input correct data!");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            bool load;
            int examination;
            if (textBox3.Text != string.Empty && textBox18.Text != string.Empty && textBox2.Text != string.Empty && (load = int.TryParse(textBox18.Text, out examination))==true)
            {
                cinema.addMovie(textBox3.Text, Convert.ToInt32(textBox18.Text), textBox2.Text);
                textBox3.Text = string.Empty;
                textBox18.Text = string.Empty;
                textBox2.Text = string.Empty;
            }
            else
            {
                MessageBox.Show("ERROR. Input correct data!");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            bool load;
            int examination;
            if (textBox4.Text != string.Empty && textBox9.Text != string.Empty && textBox8.Text != string.Empty && textBox5.Text != string.Empty &&
            textBox6.Text != string.Empty && textBox7.Text != string.Empty && (load = int.TryParse(textBox9.Text, out examination))==true &&
            (load = int.TryParse(textBox8.Text, out examination)) == true && (load = int.TryParse(textBox5.Text, out examination)) == true &&
            (load = int.TryParse(textBox6.Text, out examination)) == true && (load = int.TryParse(textBox7.Text, out examination)) == true)
            {
                cinema.findMovies(textBox4.Text).addSession(Convert.ToInt32(textBox9.Text), Convert.ToInt32(textBox8.Text),
                    Convert.ToInt32(textBox5.Text), Convert.ToInt32(textBox6.Text), Convert.ToInt32(textBox7.Text));
                textBox4.Text = string.Empty;
                textBox9.Text = string.Empty;
                textBox8.Text = string.Empty;
                textBox5.Text = string.Empty;
                textBox6.Text = string.Empty;
                textBox7.Text = string.Empty;
            }
            else
            {
                MessageBox.Show("ERROR. Input correct data!");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox10.Text != string.Empty)
            {
                cinema.delMovie(cinema.findMovies(textBox10.Text));
                textBox10.Text = string.Empty;
            }
            else{
                MessageBox.Show("ERROR. Input correct data!");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            bool load;
            int examination;
            if (textBox11.Text != string.Empty && textBox12.Text != string.Empty && textBox13.Text != string.Empty && textBox14.Text != string.Empty &&
            textBox15.Text != string.Empty && textBox16.Text != string.Empty && (load = int.TryParse(textBox11.Text, out examination)) == true &&
            (load = int.TryParse(textBox12.Text, out examination)) == true && (load = int.TryParse(textBox13.Text, out examination)) == true &&
            (load = int.TryParse(textBox14.Text, out examination)) == true && (load = int.TryParse(textBox15.Text, out examination)) == true)
            {
                int second = 0;
                DateTime date = new DateTime(Convert.ToInt32(textBox11.Text), Convert.ToInt32(textBox12.Text),
                        Convert.ToInt32(textBox15.Text), Convert.ToInt32(textBox14.Text), Convert.ToInt32(textBox13.Text), second);
                cinema.findMovies(textBox16.Text).dellSession(date);
                textBox11.Text = string.Empty;
                textBox12.Text = string.Empty;
                textBox13.Text = string.Empty;
                textBox14.Text = string.Empty;
                textBox15.Text = string.Empty;
                textBox16.Text = string.Empty;
            }
            else
            {
                MessageBox.Show("ERROR. Input correct data!");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            richTextBox1 .Text = string.Empty;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string path = saveFileDialog1.FileName;
                File.WriteAllText(path, cinema.saveStringFile());
                MessageBox.Show("OK, information were saved");
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string path = openFileDialog1.FileName;
                string[] buffer = File.ReadAllLines(path);
                string[] segment;
                string[] session;
                string[] dateTime;
                string[] date, time;
                int years, months, days, hours, minutes, seconds=0;
                Movies movie;
                for(int i = 0; i < buffer.Length; i++)
                {
                    segment = buffer[i].Split('@');
                    movie = new Movies(segment[1], Convert.ToInt32(segment[0]));
                    cinema.addMovie(movie);
                    if (segment[2] != "")
                    {
                        for (int j = 2; j < segment.Length; j++)
                        {
                            session = segment[j].Split('|');
                            for (int k = 0; k < session.Length; k++)
                            {
                                if (session[k] != "")
                                {
                                    dateTime = session[k].Split('^');
                                    date = dateTime[0].Split('.');
                                    time = dateTime[1].Split(':');
                                    years = Convert.ToInt32(date[2]);
                                    months = Convert.ToInt32(date[1]);
                                    days = Convert.ToInt32(date[0]);
                                    hours = Convert.ToInt32(time[0]);
                                    minutes = Convert.ToInt32(time[1]);
                                    cinema.findMovies(movie.getMovies()).addSession(years, months, days, hours, minutes, seconds);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
