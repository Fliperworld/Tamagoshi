using System.Drawing.Text;
using System.Text;
using Tamagoshi;

namespace Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.WriteLine("Initializing...");
            var total = await TamagoshiLib.LoadAndGetPokemonsCount();
            Console.WriteLine($"Pokemon Client Initialized!!!\r\nFound {total} Pokemons.");
            
            Day2();
        }

        private void Separator()
        {
            Console.WriteLine(""); 
            Console.WriteLine("=============================================");
        }
        private async void Day2()
        {
            Separator();
            Console.WriteLine("Teste Day 2\r\n");
            string name = "wurmple";//  "EeVEe";
            var mascote = await TamagoshiLib.GetPokemonInfo(name);

            Console.WriteLine(mascote.ToString());

            if(mascote.Evolutions != null)
            {
                foreach(var n in mascote.Evolutions)
                {
                    Console.WriteLine();
                    var masc = await TamagoshiLib.GetPokemonInfo(n);
                    Console.WriteLine(masc.ToString());
                }
            }

        }
    }
}