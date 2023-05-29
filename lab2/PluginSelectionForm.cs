using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab2
{
    public partial class PluginSelectionForm : Form
    {
        public string Selected { get; set; }

        public PluginSelectionForm(List<string> values)
        {
            InitializeComponent();
            Selected = "Do not encode";
            values.Add(Selected);
            comboBox1.Items.AddRange(values.ToArray());
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            button1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Selected = (string)comboBox1.SelectedItem;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
