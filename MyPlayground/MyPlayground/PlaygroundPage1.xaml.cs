using Microsoft.Win32;
using MyPlayground.ApiRequests;
using MyPlayground.MyPlaygroundReadExcel;
using MyPlayground.MyPlaygroundReadSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace MyPlayground
{
    /// <summary>
    /// Interaction logic for PlaygroundPage1.xaml
    /// </summary>
    public partial class PlaygroundPage1 : Page
    {
        public PlaygroundPage1()
        {
            InitializeComponent();
        }


        // SQL Queries

        string context;

        private void BtSql_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                context = TbSql.Text;

                var accessDb = new DBContextFactory();

                List<string> listedData = accessDb.getData(context);

                int count = listedData.Count;

                string record1 = listedData[0].ToString();

                string record2 = listedData[1].ToString();

                Panel.AppendText("\r\n" + record1.ToString() + "\r\n" + record2);
            }
            catch(Exception err)
            {
                if (err != null)
                {
                    Panel.Text = err.ToString();
                }
            }
        }


        // Excel Reader

        public string fileLocation;
        public string record;

        private void BtBrowse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog chosenFile = new OpenFileDialog();
            chosenFile.Filter = "All Excel Files (*.*)|*.*";
            if (chosenFile.ShowDialog() == true)
            {
                fileLocation = chosenFile.FileName;
            }

            TbFilePath.Text = fileLocation;
        }

        private void BtExtract_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var accessHelpIds = new ExcelReader();

                IList<Entity> objEntityH = accessHelpIds.ReadExcel(fileLocation);

                int count = objEntityH.Count;

                int i = 0;

                while (i < count)
                {
                    record = objEntityH[i].Property.ToString();

                    int ii = i + 1;

                    Panel.AppendText("\r\n" + ii.ToString() + "---" + record);

                    i++;
                }
                if (i == count)
                {
                    MessageBox.Show("Complete", "Status");
                }
                else
                {
                    MessageBox.Show("Error", "Error");
                }
            }
            catch(Exception err)
            {
                if (err != null)
                {
                    Panel.Text = err.ToString();
                }
            }
        }


        string body;
        string InvDetId;

        private async void BtPatch_Click(object sender, RoutedEventArgs e)
        {
            body = Panel.Text;
            InvDetId = TbInvDetId.Text;
            //var bodyPM = JObject.Parse(panel.Text);

            try
            {
                await Task.Run(() =>
                {
                    var request = new PatchRequest();

                    request.SendRequest(body, InvDetId);

                    MessageBox.Show(request.statusCode, "Status Code");
                });
            }
            catch(Exception err)
            {
                if (err != null)
                {
                    Panel.Text = err.ToString();
                }
            }

        }
    }
}
