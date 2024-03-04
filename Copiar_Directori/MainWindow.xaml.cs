using Microsoft.Win32;
using System.Diagnostics;
using System.IO;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Copiar_Directori
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnOrigen_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFolderDialog();
            if ((bool)ofd.ShowDialog())
            {
                tbOrigen.Text = ofd.FolderName;
            }
        }

        private void btnDesti_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFolderDialog();
            if ((bool)ofd.ShowDialog())
            {
                tbDesti.Text = ofd.FolderName;
            }
        }

        private void btnCopiar_Click(object sender, RoutedEventArgs e)
        {
            // ejecutra cmd o poweshell

            var pathOrigen = this.tbOrigen.Text;
            var pathDesti = this.tbDesti.Text;

            if (!(Directory.Exists(pathOrigen) && Directory.Exists(pathDesti)))
            {
                throw new Exception("no existen los directorios, wey");
            }

            Process p = new Process();
            p.StartInfo.FileName = "powershell";
            p.StartInfo.Arguments = $" -command xcopy {pathOrigen} {pathDesti} /s /y"; // modo sobreescribir 
            p.StartInfo.UseShellExecute = false;
            p.Start();
            p.WaitForExit(6000);

            if (p.HasExited)
            {
                if (p.ExitCode != 0) tbResultat.Text = "Copia incorrecta";
                else tbResultat.Text = "Copia correcta";
            }
            else tbResultat.Text = "Has tardado mas de 1 min en copiar";
        }

        private async void btnAsincrona_Click(object sender, RoutedEventArgs e)
        {
            // ejecutra cmd o poweshell

            var pathOrigen = this.tbOrigen.Text;
            var pathDesti = this.tbDesti.Text;

            if (!(Directory.Exists(pathOrigen) && Directory.Exists(pathDesti)))
            {
                throw new Exception("no existen los directorios, wey");
            }

            Process p = new Process();
            p.StartInfo.FileName = "powershell";
            p.StartInfo.Arguments = $" -command xcopy {pathOrigen} {pathDesti} /s /y"; // modo sobreescribir 
            p.StartInfo.UseShellExecute = false;
            p.Start();
            await p.WaitForExitAsync();

            if (p.HasExited)
            {
                if (p.ExitCode != 0) tbResultat.Text = "Copia incorrecta";
                else tbResultat.Text = "Copia correcta";
            }
            else tbResultat.Text = "Has tardado mas de 1 min en copiar";
        }

        private async void copiaAsincrona()
        {

        }


    }
}