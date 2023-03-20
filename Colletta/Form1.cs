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

        Dictionary<Persona, Soldi> collette = new Dictionary<Persona, Soldi>();
        Persona temp;
        Soldi tempino;

        string[] alunni = new string[] { "Bassi", "Borelli", "Colombi", "Crotti", "Cutinella", "Ferrari", "Ghilardi A.", "Ghilardi N.", "Ghirardi", "Lin", "Manca", "Mensah", "Messi", "Mosconi", "Panseri", "Patelli", "Rossi", "Todeschini", "Verzeri", "Vita" };
        double SaldoTot = 0;

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < alunni.Length; i++)
            {
                temp = new Persona(alunni[i]);
                tempino = new Soldi(0, "euri");
                collette.Add(temp, tempino);
            }

            listView1.Items.Clear();
            listView1.View = View.Details;
            listView1.FullRowSelect = true;

            Reload_ListView();
        }

        public void Reload_ListView()
        {
            foreach (KeyValuePair<Persona, Soldi> kvp in collette)
            {
                string[] val = new string[] { Convert.ToString(kvp.Key.Nome), Convert.ToString(kvp.Value.Importo) };

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
                        temp = new Persona(textBox1.Text);
                        tempino = collette[temp];
                        double oldimp = tempino.Importo;
                        tempino = new Soldi(oldimp + quota, "€");
                        collette[temp]=tempino;
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

        private void button2_Click(object sender, EventArgs e)//togli
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
                        temp = new Persona(textBox1.Text);
                        tempino = collette[temp];
                        double oldimp = tempino.Importo;

                        if (oldimp >= quota)
                        {
                            tempino = new Soldi(tempino.Importo - quota, "€");
                            collette[temp] = tempino;
                        }
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

        private void button3_Click(object sender, EventArgs e) //ordina per nome
        {
            SortedDictionary<Persona, Soldi> temprino = new SortedDictionary<Persona, Soldi>(collette);
            collette = new Dictionary<Persona, Soldi>(temprino);

            listView1.Items.Clear();
            Reload_ListView();
        }

        private void button4_Click(object sender, EventArgs e) // ordina per importo
        {
            Dictionary<Persona, Soldi> tmp = new Dictionary<Persona, Soldi>();

            foreach (KeyValuePair<Persona, Soldi> val in collette.OrderByDescending(x => x.Value.Importo))
            {
                tmp[val.Key] = val.Value;
            }
            collette = new Dictionary<Persona, Soldi>(tmp);

            listView1.Items.Clear();
            Reload_ListView();
        }

        private void button5_Click(object sender, EventArgs e) //ricerca x importo
        {
            if (String.IsNullOrWhiteSpace(textBox3.Text) || String.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Valore non valido");
                return;
            }

            try
            {
                double valore = double.Parse(textBox3.Text);
                foreach (KeyValuePair<Persona, Soldi> val in collette)
                {
                    double importoPersona = val.Value.Importo;

                    if (valore == importoPersona)
                    {
                        MessageBox.Show($"L'utente che ha versato questo importo è: {val.Key.Nome}");
                        return;
                    }
                }
                MessageBox.Show("Nessun utente trovato");
            }
            catch
            {
                MessageBox.Show("Valore non valido");
                return;
            }

        }
    }
}
