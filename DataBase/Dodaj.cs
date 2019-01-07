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
using System.Windows.Forms;

namespace DataBase
{
    public partial class Dodaj : Form
    {
        private readonly Form1 _f1;
        string NR, OPIS;
        SQLiteConnection poloczenie = new SQLiteConnection(string.Format("DataSource={0}", Path.Combine(Application.StartupPath, "base.db")));
        SQLiteCommand komenda;
        string zapytanie = "";



        public Dodaj()
        {
            
            InitializeComponent();
            
        }

        private void add(object sender, EventArgs e)
        {
            poloczenie.Open();
            if (poloczenie.State == ConnectionState.Open)
            {

                NR = NrBox.Text;
                OPIS = DescBox.Text;

                zapytanie = string.Format("INSERT INTO stroje(Nr, Description) VALUES('" + NR + "', '" + OPIS + "')");


                komenda = new SQLiteCommand(zapytanie, poloczenie);
                komenda.ExecuteNonQuery();
                MessageBox.Show("Dodałeś element", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
               
            }
            poloczenie.Close();
           
            Close();
            
            
        }
    }
}
