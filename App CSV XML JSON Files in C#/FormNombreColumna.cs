using System;
using System.Windows.Forms;

namespace App_CSV_XML_JSON_Files_in_C_
{
    public partial class FormNombreColumna : Form
    {
        public string NombreColumna => NombreColumnaTXT.Text;

        public FormNombreColumna()
        {
            InitializeComponent();
        }

        private void AceptarBTM_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void NombreColumnaTXT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                AceptarBTM.PerformClick();
            }
        }
    }
}
