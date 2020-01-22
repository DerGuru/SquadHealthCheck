using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SquadHealthCheck
{
    /// <summary>
    /// Holds the company colors from style guide
    /// </summary>
    public static class Colors
    {
        /// <summary>
        /// holds the color pairs Key = Logo Color, Value = Transparent color for the background
        /// </summary>
        private static Dictionary<String, String> _colors = InitColors();

        private static Dictionary<string, string> InitColors()
        {
            var colors = new Dictionary<String, String>();
        
            colors.Add("33, 160, 210, 1", "33, 160, 210, 0.4"); //hellblaub
            colors.Add("0, 74, 150, 1", "0, 74, 150, 0.4"); //dunkelblau
            colors.Add("101, 172, 30, 1", "101, 172, 30, 0.4"); //hellgrün
            colors.Add("0, 121, 58, 1", "0, 121, 58, 0.4"); //dunklgrün
            return colors;
        }

        /// <summary>
        /// Gets a randomly selected ColorPair (Key Logo, Value = Background) 
        /// </summary>
        private static KeyValuePair<String, String> ColorPair
        {
            get
            {
                return _colors.ElementAt((DateTime.Now.DayOfYear) % _colors.Count);
            }
        }

        /// <summary>
        /// Gets the fore ground color of the color pair
        /// </summary>
        /// <value>
        /// The fore ground color of the color pair
        /// </value>
        public static String ForeGround { get { return $"rgba({ColorPair.Key})"; } }

        /// <summary>
        /// Gets the back ground color of the color pair.
        /// </summary>
        /// <value>
        /// The back ground color of the color pair.
        /// </value>
        public static String BackGround { get { return $"rgba({ColorPair.Value})"; } }

    }
}
