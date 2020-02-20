using System;
using System.Windows.Forms;

namespace FirstProject {

    /* [PROGRESS] The client must read a city, read the name of a station and display all the information about it.
     * [DONE] Access to the list of the available cities, 
     * [DONE] the list of the station names for one of these cities. 
     */


    public partial class Form1 : Form {
        private String API_KEY = "8542dbcf60bced995bb88e18829cc836c7ec5afe";
        public Form1() {
            InitializeComponent();
        } 

        private void textBox1_TextChanged(object sender, EventArgs e) {
            if (textBox1.Text != "") {
                textBox2.Enabled = true;
            } else {
                textBox2.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e) {

            JCDecaux_Stations_API.getInstance.setCity(textBox1.Text);
            JCDecaux_Stations_API.getInstance.setAPI(API_KEY);
            try {
                JCDecaux_Stations_API.getInstance.feedStations();
            } catch (System.Net.WebException ex) {
                if (ex.Message.Contains("400")) {
                    MessageBox.Show("La ville est introuvable", "Veuillez rentrer une nouvelle ville", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }

            feedFirstListView();
        }
        private void feedListBox() {  
            foreach (Contract contract in JCDecaux_Contracts_API.getInstance.Contracts) {
                listBox1.Items.Add(contract.Name.Remove(1).ToUpper() + contract.Name.Substring(1));
            }
        }


        private void feedSecondListView() {
            bool hasFound = false;

            //Clearing listView first
            listView3.Items.Clear();


            foreach (Station station in JCDecaux_Stations_API.getInstance.Stations) {
                if (station.StationName.Contains(textBox2.Text)) {
                    hasFound = true;
                    ListViewItem item = new ListViewItem(station.Number.ToString());
                    item.SubItems.Add(station.City.ToString());
                    item.SubItems.Add(station.StationName.ToString());
                    item.SubItems.Add(station.Address.ToString());
                    item.SubItems.Add(station.Position.ToString());
                    item.SubItems.Add(station.Banking.ToString());
                    item.SubItems.Add(station.Bonus.ToString());
                    item.SubItems.Add(station.TotalSpace.ToString());
                    item.SubItems.Add(station.FreeSpace.ToString());
                    item.SubItems.Add(station.AvailableBikes.ToString());
                    item.SubItems.Add(station.Status.ToString());
                    item.SubItems.Add(JCDecaux_Stations_API.timestampToDate(station.LastUpdate).ToString());
                    listView3.Items.Add(item);
                }
            }

            if (!hasFound) {
                MessageBox.Show("Impossible de trouver la station", "La station est introuvable", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void feedFirstListView() {
            //Clearing listview first
            listView1.Items.Clear();

            bool hasFound = false;
            foreach (Station station in JCDecaux_Stations_API.getInstance.Stations) {
                hasFound = true;
                ListViewItem item = new ListViewItem(station.Number.ToString());
                item.SubItems.Add(station.City.ToString());
                item.SubItems.Add(station.StationName.ToString());
                item.SubItems.Add(station.Address.ToString());
                item.SubItems.Add(station.Position.ToString());
                item.SubItems.Add(station.Banking.ToString());
                item.SubItems.Add(station.Bonus.ToString());
                item.SubItems.Add(station.TotalSpace.ToString());
                item.SubItems.Add(station.FreeSpace.ToString());
                item.SubItems.Add(station.AvailableBikes.ToString());
                item.SubItems.Add(station.Status.ToString());
                item.SubItems.Add(JCDecaux_Stations_API.timestampToDate(station.LastUpdate).ToString());
                listView1.Items.Add(item);
            }

            if (!hasFound) {
                MessageBox.Show("Impossible de trouver la station", "La station est introuvable", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e) {
        }

        private void textBox2_TextChanged(object sender, EventArgs e) {
            if (textBox1.Text != "") {
                listView3.Enabled = true;
            } else {
                listView3.Enabled = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e) {
            JCDecaux_Contracts_API.getInstance.setAPI(API_KEY);
            JCDecaux_Contracts_API.getInstance.feedContracts();
            feedListBox();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e) {
            textBox1.Text = listBox1.SelectedItem.ToString();
        }

        private void button2_Click(object sender, EventArgs e) {
            JCDecaux_Stations_API.getInstance.setCity(textBox3.Text);
            JCDecaux_Stations_API.getInstance.setAPI(API_KEY);
            try {
                JCDecaux_Stations_API.getInstance.feedStations();
            } catch (System.Net.WebException ex) {
                if (ex.Message.Contains("400")) {
                    MessageBox.Show("La ville est introuvable", "Veuillez rentrer une nouvelle ville", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }
            feedSecondListView();
        }

        private void textBox3_TextChanged(object sender, EventArgs e) {
            if (!textBox3.Text.Equals("")) {
                textBox2.Enabled = true;
            }
        }
    }
}
