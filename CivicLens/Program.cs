using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CivicLens
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
            //Application.Run(new DashboardForm());
            //Application.Run(new ViewProfileForm());
            //Application.Run(new EditProfileForm());
            //Application.Run(new SubmitComplaintForm());
            //Application.Run(new MyComplaintsForm());
            //Application.Run(new ComplaintDetailForm());
            //Application.Run(new UpdatePasswordForm());



        }
    }
}
