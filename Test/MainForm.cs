using System.Text;
using Tamagoshi;
using Tamagoshi.Model;

namespace Test
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.WriteLine("Initializing...");
            var total = await TamagoshiLib.LoadAndGetPokemonsCount();
            Console.WriteLine($"Pokemon Client Initialized!!!\r\nFound {total} Pokemons.");

            await Day2();
            await Day3();
        }

        private async void ShowTmagoshiSelector()
        {
            using (TamagoshiSelectorForm f = new TamagoshiSelectorForm())
            {
                var result = f.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Console.WriteLine("Mascote selecionado com sucesso:");
                    Console.WriteLine(TamagoshiSelectorForm.SelectedMascote);
                }
                else
                {
                    Console.WriteLine("Que pena, vc não escolheu um mascote.");
                }
            }
        }

        private void Separator()
        {
            Console.WriteLine("");
            Console.WriteLine("=============================================");
        }
        private async Task<bool> Day2()
        {
            Separator();
            Console.WriteLine("Test Day 2\r\n");
            string name = "wurmple";//  "EeVEe";
            var mascote = await TamagoshiLib.GetPokemonInfo(name);

            Console.WriteLine(mascote.ToString());

            if (mascote.Evolutions != null)
            {
                foreach (var n in mascote.Evolutions)
                {
                    Console.WriteLine();
                    var masc = await TamagoshiLib.GetPokemonInfo(n);
                    Console.WriteLine(masc.ToString());
                }
            }
            return true;
        }
        private async Task<bool> Day3()
        {
            Separator();
            Console.WriteLine("Test Day 3\r\n");
            Console.WriteLine("Select a pokemon from Tamagoshi Selector...");
            ShowTmagoshiSelector();
            return true;
        }
    }
}