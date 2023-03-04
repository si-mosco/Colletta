using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Colletta
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            listView1.Columns.Add("Nome", 100);
            listView1.Columns.Add("Saldo", 100);
        }

        Dictionary<string, double> colletta = new Dictionary<string, double>();
        string[] alunni = new string[] { "Bassi", "Borelli", "Colombi", "Crotti", "Cutinella", "Ferrari", "Ghilardi A.", "Ghilardi N.", "Ghirardi", "Lin", "Manca", "Mensah", "Messi", "Mosconi", "Panseri", "Patelli", "Rossi", "Todeschini", "Verzeri", "Vita" };
        double SaldoTot = 0;

        private void Form1_Load(object sender, EventArgs e)
        {           
            for (int i = 0; i < alunni.Length; i++)
                colletta.Add(alunni[i], 0);

            listView1.Items.Clear();
            listView1.View = View.Details;
            listView1.FullRowSelect = true;

            Reload_ListView();
        }

        public void Reload_ListView()
        {
            foreach (KeyValuePair<string, double> kvp in colletta)
            {
                string[] val = new string[] { kvp.Key, Convert.ToString(kvp.Value) };

                ListViewItem item = new ListViewItem(val);
                listView1.Items.Add(item);

                label3.Text = $"Saldo totale: {SaldoTot}";
            }
        }

        private void button1_Click(object sender, EventArgs e)//aggiungi
        {
            if (!String.IsNullOrWhiteSpace(textBox1.Text) || !String.IsNullOrWhiteSpace(textBox2.Text))
            {
                try
                {
                    double quota = double.Parse(textBox2.Text);
                    bool ver = false;
                    for (int i = 0; i < alunni.Length; i++)
                        if (textBox1.Text == alunni[i])
                            ver = true;

                    if (ver)
                    {
                        colletta[textBox1.Text]+=quota;
                    }
                    else
                    {
                        textBox1.Text = String.Empty;
                        throw new Exception("Persona non Esistente");
                    }

                    SaldoTot += quota;
                    listView1.Items.Clear();
                    Reload_ListView();

                    textBox1.Text = String.Empty;
                    textBox2.Text = String.Empty;
                }
                catch
                {
                    textBox2.Text = String.Empty;
                    throw new Exception("Quota non Valida");
                }
            }
            else
                throw new Exception("Inserire Valori");
        }

        private void button2_Click(object sender, EventArgs e)//trogli
        {
            if (!String.IsNullOrWhiteSpace(textBox1.Text) || !String.IsNullOrWhiteSpace(textBox2.Text))
            {
                try
                {
                    double quota = double.Parse(textBox2.Text);
                    bool ver = false;
                    for (int i = 0; i < alunni.Length; i++)
                        if (textBox1.Text == alunni[i])
                            ver = true;

                    if (ver)
                    {
                        if (colletta[textBox1.Text] >= quota)
                            colletta[textBox1.Text] -= quota;
                        else
                            throw new Exception("Valore troppo alto");
                    }
                    else
                    {
                        textBox1.Text = String.Empty;
                        throw new Exception("Persona non Esistente");
                    }

                    SaldoTot -= quota;
                    listView1.Items.Clear();
                    Reload_ListView();

                    textBox1.Text = String.Empty;
                    textBox2.Text = String.Empty;
                }
                catch
                {
                    textBox2.Text = String.Empty;
                    throw new Exception("Quota non Valida");
                }
            }
            else
                throw new Exception("Inserire Valori");
        }
    }
}
