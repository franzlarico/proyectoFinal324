using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


using System.Runtime.InteropServices;

namespace Ejercicio3
{
    

    public partial class Form1 : Form
    {
        string a1, a2, a3, a4, a5, a6;
        int cR, cG, cB;
        int rtx, btx, gtx;
        int rnrc, rngc, rnbc;
        int idr;

        public Bitmap bmpt;
        public Bitmap bmpg;
        public Form1()
        {
            InitializeComponent();
        }

        public Form1(Bitmap bm, Bitmap bt)
        {
            InitializeComponent();
            bmpt = bt;
            pictureBox1.Image = bm;

        }
        public void select2()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            string conexion = "server=localhost;database=colores;user=root;password=;";
            MySqlConnection conn = new MySqlConnection(conexion);
            conn.Open();
            String sql = "select * from textura";
            var cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();


            while (rdr.Read())
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = rdr.GetInt32(0);
                dataGridView1.Rows[n].Cells[1].Value = rdr.GetString(1);
                dataGridView1.Rows[n].Cells[2].Value = rdr.GetInt32(2);
                dataGridView1.Rows[n].Cells[3].Value = rdr.GetInt32(3);
                dataGridView1.Rows[n].Cells[4].Value = rdr.GetInt32(4);

                dataGridView1.Rows[n].Cells[5].Value = rdr.GetInt32(5);
                dataGridView1.Rows[n].Cells[6].Value = rdr.GetInt32(6);
                dataGridView1.Rows[n].Cells[7].Value = rdr.GetInt32(7);

            }
            conn.Close();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string conexion = "server=localhost;database=colores;user=root;password=;";
            MySqlConnection con = new MySqlConnection(conexion);
            con.Open();
            int id = Int32.Parse(dataGridView1.Rows[idr].Cells[0].Value + "");

            String sql = "delete from textura where id = '"+id+"'";

            var cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();

            con.Close();


            select2();
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            idr = e.RowIndex;

            String nom = dataGridView1.Rows[idr].Cells[1].Value + "";
            String rt = dataGridView1.Rows[idr].Cells[2].Value + "";
            String gt = dataGridView1.Rows[idr].Cells[3].Value + "";
            String bt = dataGridView1.Rows[idr].Cells[4].Value + "";
            String rc = dataGridView1.Rows[idr].Cells[5].Value + "";
            String gc = dataGridView1.Rows[idr].Cells[6].Value + "";
            String bc = dataGridView1.Rows[idr].Cells[7].Value + "";
            nomText.Text = nom;
        }

        public List<String[]> select()
        {
            List<String[]> textures = new List<String[]>();
            string conexion = "server=localhost;database=colores;user=root;password=;";
            MySqlConnection con = new MySqlConnection(conexion);
            con.Open();
            String sql = "select * from textura";
            var cmd = new MySqlCommand(sql, con);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                String[] txr = new String[8];
                txr[0] = rdr.GetInt32(0) + "";
                txr[1] = rdr.GetString(1) + "";
                txr[2] = rdr.GetInt32(2) + "";
                txr[3] = rdr.GetInt32(3) + "";
                txr[4] = rdr.GetInt32(4) + "";
                txr[5] = rdr.GetInt32(5) + "";
                txr[6] = rdr.GetInt32(6) + "";
                txr[7] = rdr.GetInt32(7) + "";

                textures.Add(txr);
            }

            con.Close();


            return textures;
        }
        private void button6_Click(object sender, EventArgs e)
        {

            Bitmap bmp2 = new Bitmap(bmpg.Width, bmpg.Height);
            List<String[]> textures = select();

            Color c = new Color();
            int interv = 10;

            for (int i = 0; i < bmpt.Width; i+=2)
            {
                for (int j = 0; j < bmpt.Height; j+=2)
                {

                    c = bmpg.GetPixel(i, j);
                    int[] cls = { c.R, c.G, c.B };

                    foreach (String[] k in textures)
                    {
                        bool sw = true;
                        int r = Int32.Parse(k[2]);
                        int g = Int32.Parse(k[3]);
                        int b = Int32.Parse(k[4]);
                        int[] clText = { r, g, b };

                        for (int l = 0; l < clText.Length; l++)
                        {
                            if (!(clText[l] - interv < cls[l] && cls[l] < clText[l] + interv))
                            {
                                sw = false;
                                break;
                            }

                        }

                        if (sw)
                        {
                            int rc = Int32.Parse(k[5]);
                            int gc = Int32.Parse(k[6]);
                            int bc = Int32.Parse(k[7]);


                            Color clr = Color.FromArgb(rc, gc, bc);
                            for (int x = i; x < i + 5; x++)
                                for (int y = j; y < j + 5; y++)
                                {
                                    bmp2.SetPixel(x, y, clr);
                                }

                            break;

                        }
                        else
                        {
                            for (int x = i; x < i + 5; x++)
                                for (int y = j; y < j + 5; y++)
                                {
                                    bmp2.SetPixel(x, y, c);
                                }
                        }
                    }

                }
            }
            pictureBox1.Image = bmp2;

        }

        private void button2_Click(object sender, EventArgs e)
        { /*
            this.Hide();
            Form1 f1 = new Form1(bmpg,bmpt);
            f1.ShowDialog();
            this.Close();
            */

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            pictureBox1.AllowDrop = true;
            dataGridView1.AllowUserToAddRows = false;
            select2();
            dataGridView1.ClearSelection();



            List<String[]> textures = select();
            ListViewGroup texturas = new ListViewGroup("Nombre Textura");
            foreach (String[] i in textures)
            {
                ListViewItem l = new ListViewItem(i[1], texturas);
                int r = Int32.Parse(i[5]);
                int g = Int32.Parse(i[6]);
                int b = Int32.Parse(i[7]);

                l.BackColor = Color.FromArgb(r, g, b);
                l.ForeColor = Color.White;


            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string conexion = "server=localhost;database=colores;user=root;password=;";
            MySqlConnection con = new MySqlConnection(conexion);
            con.Open();

            String nom = nomText.Text;
            String rc = a4;
            String gc = a5;
            String bc = a6;
            String rt = a1;
            String gt = a2;
            String bt = a3;

            String sql = "insert into textura(nom,rt,gt,bt,rc,gc,bc) values ('"+nom+"','"+rt+"','"+gt+"', '"+bt+"','"+rc+"', '"+gc+"','"+bc+"')";
            var cmd = new MySqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();


            select2();

        }

        private void pictureBox1_DragDrop(object sender, DragEventArgs e)
        {
            var data = e.Data.GetData(DataFormats.FileDrop);
            if (data != null)
            {
                var fileName = data as String[];
                if (fileName.Length > 0)
                {
                    Bitmap bmp = new Bitmap(fileName[0]);
                    pictureBox1.Image = bmp;
                    bmpg = new Bitmap(fileName[0]);
                    calcular();
                }
            }

        }

        private void pictureBox1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            Bitmap bmp = new Bitmap(pictureBox1.Image);
            Color c = new Color();
            c = bmp.GetPixel(e.X, e.Y);
            cR = c.R;
            cG = c.G;
            cB = c.B;
            rtx = btx = gtx = 0;

            for (int i = e.X; i < e.X + 5; i++)
                for (int j = e.Y; j < e.Y + 5; j++)
                {
                    c = bmp.GetPixel(i, j);
                    rtx += c.R;
                    gtx += c.G;
                    btx += c.B;
                }
            rtx /= 25;
            gtx /= 25;
            btx /= 25;
            a1 = rtx.ToString();
            a2 = gtx.ToString();
            a3 = btx.ToString();

                Random rnd = new Random();
                rnrc = rnd.Next(256);
                rngc = rnd.Next(256);
                rnbc = rnd.Next(256);
                a4 = rnrc.ToString();
                a5 = rngc.ToString();
                a6 = rnbc.ToString();

        }


        public void calcular()
        {
            int n = 5;
            int n2 = n * n;

            bmpt = new Bitmap(bmpg.Width - n, bmpg.Height - n);

            int ciR, ciG, ciB;
            Color c = new Color();

            for (int i = 0; i < bmpg.Width - n; i++)
            {
                for (int j = 0; j < bmpg.Height - n; j++)
                {
                    ciR = 0;
                    ciG = 0;
                    ciB = 0;

                    for (int x = i; x < i + n; x++)
                        for (int y = j; y < j + n; y++)
                        {
                            c = bmpg.GetPixel(x, y);
                            ciR += c.R;
                            ciG += c.G;
                            ciB += c.B;
                        }
                    ciR /= n2;
                    ciG /= n2;
                    ciB /= n2;

                    bmpt.SetPixel(i, j, Color.FromArgb(ciR, ciG, ciB));

                }
            }
        }

    }
}
