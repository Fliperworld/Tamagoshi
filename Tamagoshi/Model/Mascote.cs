using System;
using System.Collections.Generic;

namespace Tamagoshi.Model
{
    public class Mascote
    {
        private const string ERROR = "error";
        public string LowerName { get; set; } = ERROR;
        public int Altura { get; set; } = -1;
        public int Peso { get; set; } = -1;
        public int ID { get; set; } = -1;

        public string Base64Icon { get; set; } = null;
        public string Base64Sprite { get; set; } = null;

        public string Name { get; set; } = ERROR;
        public string JapaneseName { get; set; } = ERROR;

        public EGrowthRate GrowthRate { get; set; } = EGrowthRate.Undefined;
        public EHabitat Habitat { get; set; } = EHabitat.Undefined;
        public string RegressTo { get; set; } = null;
        public string[] Evolutions { get; set; } = null;
        public EvolutionRequeriments RequerimentsForEvolution { get; set; } = new EvolutionRequeriments();
        private string GetEvolutionsNames()
        {
            if (Evolutions == null || Evolutions.Length == 0)
                return "None";
           
            return string.Join(", ", Evolutions);
        }
        public override string ToString()
        {
            string s = RegressTo == null ? "Morte" : RegressTo;
            string[] Lines = new string[]
            {
            $"Nome: {Name} ({JapaneseName})",
            $"Altura: {Altura}",
            $"Peso: {Peso}",
            $"Velocidade de Crescimento: {GrowthRate}",
            $"Regride para {s}",
            $"Possível Evolução: {GetEvolutionsNames()}"
            };

            return string.Join(Environment.NewLine, Lines);
        }
    }
}
