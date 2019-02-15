using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PhotoCollective
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            listFileGrid.SelectionMode = SelectionMode.Multiple;
        }

        private void CollectClick(object sender, RoutedEventArgs e)
        {
            listFileGrid.Items.Clear();
            List<string> driveDir = new List<string>();

            if (driveC.IsChecked == true)
            {
                driveDir.Add(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "/");
            }
            if (driveD.IsChecked == true)
            {
                driveDir.Add("D:/");
            }
            if (driveE.IsChecked == true)
            {
                driveDir.Add("E:/");
            }
            try
            {
                List<string> allAccessableDir = new List<string>();
                foreach (var drive in driveDir)
                {
                    var files = getFileMethod.GetAllAccessibleFiles(drive);
                    foreach (var file in files)
                    {
                        listFileGrid.Items.Add(file);
                    }
                }
                listFileGrid.SelectAll();
            }
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show("That drive doesn't exist on your computer, check again");
            }
        }

        private void UploadClick(object sender, RoutedEventArgs e)
        {
            string folderId = folderID.Text;
            try
            {
                foreach (var itm in listFileGrid.Items)
                {
                    if(!itm.ToString().Contains(folderId))
                    File.Copy(itm.ToString(), folderId + '/' + System.IO.Path.GetFileName(itm.ToString()), true);
                }
            }catch(IOException ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("Done!");
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBox.Show("RICE BYE :D");
        }
    }
}
