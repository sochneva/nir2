using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nir2
{
    public partial class nir2 : Form
    {
        PTModel ptModel = new PTModel();
        public nir2()
        {
            InitializeComponent();

            TvdTrackBar.Minimum = 0;
            PvdTrackBar.Minimum = 0;
            TndTrackBar.Minimum = 0;
            PkTrackBar.Minimum = 0;

            TvdTrackBar.Maximum = PTModel.TVD.Length-1;
            PvdTrackBar.Maximum = PTModel.PVD.Length -1;
            TndTrackBar.Maximum = PTModel.TND.Length -1;
            PkTrackBar.Maximum = PTModel.PK.Length -1; 
        }

        private void trackBarChange(object sender, EventArgs e)
        {
            TrackBar trackBar = (TrackBar)sender;
            ptModel.updateParam(trackBar.Name, trackBar.Value);
            NTextBox.Text = ptModel.GrossPower.ToString();
            DkTextBox.Text = ptModel.ConsumptionSteam.ToString();
        }
    }
}
