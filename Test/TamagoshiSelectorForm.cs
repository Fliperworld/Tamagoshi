using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Tamagoshi;
using Tamagoshi.Model;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Test
{
    public partial class TamagoshiSelectorForm : Form
    {

        private static List<PokemonName> pokemonNames = null;
        private static int lastSelectedIndex = 0;
        public static Mascote SelectedMascote = null;

        private bool busy = false;
        private Dictionary<string, PokemonName> PokemonLinks = new Dictionary<string, PokemonName>();
        public TamagoshiSelectorForm()
        {
            InitializeComponent();
            InitializePokemonNames();
        }

        private async void InitializePokemonNames()
        {
            if (pokemonNames == null)
            {
                pokemonNames = await TamagoshiLib.GetPokemonsNames();
                comboBoxPokemonName.Text = "Select...";
            }

            comboBoxPokemonName.Items.AddRange(pokemonNames.ToArray());
            comboBoxPokemonName.SelectedIndex = lastSelectedIndex;
            comboBoxPokemonName.SelectedIndexChanged += ComboBoxPokemonName_SelectedIndexChanged;
            ComboBoxPokemonName_SelectedIndexChanged(null, EventArgs.Empty);
        }

        private void AddLink(string str)
        {

            var lowerName = str.ToLower();
            int index = pokemonNames.FindIndex(p => p.Identifier == lowerName);

            if (index >= 0)
            {
                LinkLabel link = new LinkLabel();
                link.AutoSize = true;
                link.Location = textBoxInfos.GetPositionFromCharIndex(textBoxInfos.TextLength - 1);
                link.LinkColor = Color.Red;
                link.Text = str;
                link.Click += PokemonLink_Click;
                textBoxInfos.Controls.Add(link);

                if (!PokemonLinks.ContainsKey(str))
                    PokemonLinks.Add(str, pokemonNames[index]);
            }

            textBoxInfos.Text += str;
        }
        private void PokemonLink_Click(object? sender, EventArgs e)
        {
            var label = sender as LinkLabel;
            if (label != null && PokemonLinks.TryGetValue(label.Text, out var pokemon))
            {
                comboBoxPokemonName.SelectedItem = pokemon;
            }
        }

        private async void ComboBoxPokemonName_SelectedIndexChanged(object? sender, EventArgs e)
        {
            var idx = comboBoxPokemonName.SelectedIndex;
            if (busy)
            {
                lastSelectedIndex = idx;
                return;
            }
            busy = true;
            textBoxInfos.Enabled = false;
            lastSelectedIndex = idx;

            //Cleanup old resources.
            if (pictureBoxIcon.Image != null)
            {
                pictureBoxIcon.Image.Dispose();
                pictureBoxIcon.Image = null;
            }
            textBoxInfos.Controls.Clear();
            textBoxInfos.Text = "Loading...";
            PokemonLinks.Clear();

            var pn = (PokemonName)(comboBoxPokemonName.SelectedItem);
            SelectedMascote = await TamagoshiLib.GetPokemonInfo(pn.Identifier);

            //Update Display Name.
            comboBoxPokemonName.Items[idx] = pokemonNames[idx] = SelectedMascote.Name;

            //Update Icon.
            var bytes = await TamagoshiLib.GetMascoteIcon(SelectedMascote);
            pictureBoxIcon.Image = Image.FromStream(new MemoryStream(bytes));

            //Update TextBox.
            textBoxInfos.Text = $"Nome: {SelectedMascote.Name}{Environment.NewLine}";
            textBoxInfos.Text += $"Altura: {SelectedMascote.Altura}{Environment.NewLine}";
            textBoxInfos.Text += $"Peso: {SelectedMascote.Peso}{Environment.NewLine}";
            textBoxInfos.Text += $"Velocidade de Crescimento: {SelectedMascote.GrowthRate}{Environment.NewLine}";
            if (SelectedMascote.RegressTo == null)
            {
                textBoxInfos.Text += $"Regride para Morte{Environment.NewLine}";
            }
            else
            {
                textBoxInfos.Text += "Regride para ";
                AddLink(SelectedMascote.RegressTo);
                textBoxInfos.Text += Environment.NewLine;
            }
            if (SelectedMascote.Evolutions != null)
            {
                textBoxInfos.Text += "Possível Evolução: ";
                AddLink(SelectedMascote.Evolutions[0]);
                for (int i = 1; i < SelectedMascote.Evolutions.Length; i++)
                {
                    textBoxInfos.Text += ", ";
                    AddLink(SelectedMascote.Evolutions[i]);
                }
                textBoxInfos.Text += ".";
            }

            textBoxInfos.Enabled = true;


            busy = false;
            if (comboBoxPokemonName.SelectedIndex != lastSelectedIndex)
                ComboBoxPokemonName_SelectedIndexChanged(null, EventArgs.Empty);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

    }

}
