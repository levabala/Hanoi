using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Balls
{
    public partial class PanelDoubleBuffered : UserControl
    {
        public PanelDoubleBuffered()
        {
            InitializeComponent();
        }

        private void PanelDoubleBuffered_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;
        }
    }
}
