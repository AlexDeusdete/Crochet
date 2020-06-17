using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Crochet.Extensions
{
    public struct HueSeparation
    {
        public HueSeparation(string HueName, int DegreesStart, int DegreesEnd)
        {
            this.HueName = HueName;
            this.DegreesStart = DegreesStart;
            this.DegreesEnd = DegreesEnd;
        }
        public string HueName { get; set; }
        public float DegreesStart { get; set; }
        public float DegreesEnd { get; set; }
    }
    public static class ColorExtensions
    {
        private static readonly List<HueSeparation> _hueSeparations = new List<HueSeparation>
                                                                {
                                                                    new HueSeparation("Tons de Vermelho",0,40),
                                                                    new HueSeparation("Tons de Amarelo",41,80),
                                                                    new HueSeparation("Tons de Verde",81,160),
                                                                    new HueSeparation("Tons de Azul",161,270),
                                                                    new HueSeparation("Tons de Violeta",271,330),
                                                                    new HueSeparation("Tons de Vermelho",331,360)
                                                                };
        public static string GetHueName(this Color color)
        {
            float hue = color.GetHue();
            var result = _hueSeparations
                        .Where(x => x.DegreesStart <= hue && x.DegreesEnd >= hue)
                        .FirstOrDefault()
                        .HueName;

            if (color.GetBrightness() >= 0.98 || color.GetBrightness() <= 0.155)
                result = "Tons Escuros e Claros";
            
            return result;
        }
    }
}
