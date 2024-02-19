using System.IO;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Xml;

namespace wintree
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public XmlDataProvider DataProvider { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            DataProvider = new XmlDataProvider();


            LoadTree();

            this.Left = SystemParameters.PrimaryScreenWidth - this.Width;
            this.Top = (SystemParameters.PrimaryScreenHeight - this.Height) -40;
        }


        private void LoadTree() //Load the XML Tree File
        {

            try
            {
                //Get XML file
                string file = System.IO.Path.GetFileName(Settings.Default.TreeFile);
                string path = System.IO.Path.GetDirectoryName(Settings.Default.TreeFile);

                if (path == "")
                    path = Directory.GetCurrentDirectory();

                //Initiliaze XmlDataProvider 
                DataProvider.Source = new Uri(path + @"\" + file);
                DataProvider.XPath = "/Tree";
                this.treeview.DataContext = DataProvider;

                //Add FileSystemWatcher to update treeview on change
                FileSystemWatcher fsWatcher = new FileSystemWatcher(path, file); //path, filter
                fsWatcher.NotifyFilter = NotifyFilters.LastWrite;
                fsWatcher.Changed += new FileSystemEventHandler(fsWatcher_Changed);
                fsWatcher.EnableRaisingEvents = true;

            }


            catch (Exception e)
            {
                MessageBox.Show("Problem loading XML Tree File: \n\n" + e.ToString());
            }

        }


        private (string, string, string) GetSelectedItem()
        {
            try
            {
                XmlElement xmlElement = (XmlElement)treeview.SelectedItem;

                string name = (xmlElement.GetAttribute("Name").ToString());
                string command = (xmlElement.GetAttribute("Command").ToString());
                string parameters = (xmlElement.GetAttribute("Parameters").ToString());


                return (name, command, parameters);

            }


            catch (Exception e)
            {
                MessageBox.Show("Something went wrong:\n" + e.Message);
                throw new Exception("Couldn't get item");


            }

        }




        void fsWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            DataProvider.Refresh();
        }


        private void Launch_Click(object sender, RoutedEventArgs e)
        {

            var (name, command, parameters) = GetSelectedItem();
            var session = new Session(name, command, parameters);

        }


        private void Open_Click(object sender, RoutedEventArgs e)
        {


            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".xml";
            dlg.Filter = "XML Files (*.xml)|*.xml";
            bool? result = dlg.ShowDialog();


            if ((bool)result)
            {
                Settings.Default.TreeFile = dlg.FileName;
                Settings.Default.Save();
                LoadTree();
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(Settings.Default.Editor, Settings.Default.TreeFile);
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            DataProvider.Refresh();
        }

        private void Log_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(Settings.Default.Editor, Settings.Default.LogFile);
        }


        private void OnTop_Click(object sender, RoutedEventArgs e)
        {
            this.Topmost = !(this.Topmost);

        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("wintree v1.0 \n https://github.com/dmaccormac");
        }


        private void Item_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            var (name, command, parameters) = GetSelectedItem();
            var session = new Session(name, command, parameters);
        }


    }
}