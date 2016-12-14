using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiveGFX_FO3
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Bunifu.Framework.License.Authenticate("beereditor@gmail.com", "Oi4FYitIZTiP+tmaMFeOTZf/64l8apCkbnO3fUaBlC0=");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LiveGFX());
        }
    }
}
