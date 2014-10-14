using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ToguisController.Points;

namespace ToguisViewTester
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PointOfInterestController cont = new PointOfInterestController();

            cont.GetPoints("raven9t@yahoo.com",1, true, false, false, false, false, false, false, false, 1);

        }
    }
}
