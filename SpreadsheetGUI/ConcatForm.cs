using SS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpreadsheetGUI
{
    /// <summary>
    /// This is used for our extra feature. You will be able to concatenate cells to display in a message box
    /// </summary>
    public partial class ConcatForm : Form
    {
        private MainForm form; 
        /// <summary>
        /// Constructor for concatform
        /// </summary>
        /// <param name="form"></param>
        public ConcatForm(MainForm form)
        {
            InitializeComponent();
            this.form = form;
            
        }

        /// <summary>
        /// Allows you to select the cells you want to concatenate
        /// </summary>
        /// <param name="sp"></param>
        public void OnSelectingSpreadsheet(SpreadsheetPanel sp)
        {
            int col, row; 
            sp.GetSelection(out col, out row);
            string newcelltext = ""; 
            if (CalculationCell.Text == "")
            {
                newcelltext  = form.ConvertCoordinatesToCellName(col, row);
            }
            else
            {
                newcelltext = ", " + form.ConvertCoordinatesToCellName(col, row);
            }

            CalculationCell.Text += newcelltext; 
        }

        /// <summary>
        /// Result box for concatenation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Perform_Click(object sender, EventArgs e)
        {
            string[] cellnames = Regex.Split(CalculationCell.Text, ", ");
            
            string concatedresult = "";

            foreach (string name in cellnames)
            {
                concatedresult += form.GetSpreadsheet().GetCellValue(name);
            }

            MessageBox.Show("The result from concatnation is: " + concatedresult, "Concatnation Result"); 
        }
    }
}
