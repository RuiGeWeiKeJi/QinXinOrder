using System;
using System . Collections . Generic;
using System . Linq;
using System . Windows . Forms;
using DevExpress . UserSkins;
using DevExpress . Skins;
using DevExpress . LookAndFeel;

namespace SiftingData
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main ( )
        {
            Application . EnableVisualStyles ( );
            Application . SetCompatibleTextRenderingDefault ( false );

            DevExpress . Skins . SkinManager . EnableFormSkins ( );
            FastReport . Utils . Res . LoadLocale ( Application . StartupPath + "\\Chinese (Simplified).frl" );

            BonusSkins . Register ( );
            SkinManager . EnableFormSkins ( );
            Application . Run ( new FormMain ( ) );
        }
    }
}
