using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace DataBase
{
    public partial class Form1 : Form
    {

        
        SQLiteConnection poloczenie = new SQLiteConnection(string.Format("DataSource={0}", Path.Combine(Application.StartupPath, "base.db")));
        SQLiteCommand komenda;
        string zapytanie = "";



        public Form1()
        {
            InitializeComponent();
           
            
            poloczenie.Open();
            if (poloczenie.State == ConnectionState.Open)
            {
                
                List<string>[] list;
                list = SelectALL();
                dataGridView1.Rows.Clear();
                for (int i = 0; i < list[0].Count; i++)
                {
                    int number = dataGridView1.Rows.Add();
                    dataGridView1.Rows[number].Cells[0].Value = list[0][i];


                }
            }
            else
            {
                MessageBox.Show("Poloczenie nieudane", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            poloczenie.Close();

        }

       

        private void create_table(object sender, EventArgs e)
        {
            poloczenie.Open();
            if (poloczenie.State == ConnectionState.Open)
            {
                zapytanie = string.Format("create table stroje(Id integer primary key autoincrement, Nr integer, Description text)");

                komenda = new SQLiteCommand(zapytanie, poloczenie);
                komenda.ExecuteNonQuery();
                MessageBox.Show("Dodałeś tabele", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            poloczenie.Close();
        }
        public List<string>[] SelectALL()
        {
            //poloczenie.Open();
            List<string>[] list = new List<string>[2];
            list[0] = new List<string>();
            list[1] = new List<string>();


            if (poloczenie.State == ConnectionState.Open)
            {
                zapytanie = string.Format("SELECT Nr FROM stroje ORDER BY Nr ASC");


                komenda = new SQLiteCommand(zapytanie, poloczenie);
                komenda.ExecuteNonQuery();
                SQLiteDataReader datareader = komenda.ExecuteReader();

                while (datareader.Read())
                {
                    list[0].Add(datareader["Nr"] + "");


                }
                datareader.Close();
                poloczenie.Close();
                return list;
            }

            else
            {
                return list;
            }

        }
       

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void add_click(object sender, EventArgs e)
        {

            Dodaj dod = new Dodaj();

            dod.Show();
        }

        
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void refresh_click(object sender, EventArgs e)
        {
            dataGridView1.Refresh();
        }
    }
}
