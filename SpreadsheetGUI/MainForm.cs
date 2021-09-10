using SS;

using System;

using System.Collections.Generic;

using System.ComponentModel;

using System.Data;

using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpreadsheetGUI
{
    public partial class MainForm : Form
    {
        private Spreadsheet mainspreadsheet;
        private bool confirmClose;

        /// <summary>
        /// creates a empty spreadsheet form with the starting pointer pointed to F6
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            mainspreadsheet = new Spreadsheet();
            confirmClose = false;
            UserSpreadsheet.SelectionChanged += DisplaySelection;
            UserSpreadsheet.SetSelection(5, 5);
            DisplaySelection(UserSpreadsheet);
        }

        /// <summary>
        /// Creates a spreadsheet that is already been saved to the file named filename
        /// and display the values and content of already existing cells onto the spreadsheet form. 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="version"></param>
        public MainForm(string filename, string version)
        {
            InitializeComponent();
            mainspreadsheet = new Spreadsheet(filename, IsValid, Normalize, version);
            confirmClose = false;
            UserSpreadsheet.SelectionChanged += DisplaySelection;

            HashSet<string> numemptycells = new HashSet<string>(mainspreadsheet.GetNamesOfAllNonemptyCells()); 
            // go through all the cells that are reevaluated due to change in one cell, 
            // and display the reevaluated cells on the screen by quickly switch selection to them 
            foreach (string cell in numemptycells)
            {
                int row, col;
                ConvertCellNameToCoordinates(cell, out col, out row);
                UserSpreadsheet.SetValue(col, row, mainspreadsheet.GetCellValue(cell).ToString());
                UserSpreadsheet.SetSelection(col, row);  // change the selection
                DisplaySelection(UserSpreadsheet);
                // display the selection immediately after the selection is changed
            }

            UserSpreadsheet.SetSelection(5, 5);
            DisplaySelection(UserSpreadsheet);
        }

        /// <summary>
        /// Getter for spreadsheet for other form's usage
        /// </summary>
        /// <returns></returns>
        public Spreadsheet GetSpreadsheet()
        {
            return mainspreadsheet; 
        } 


        /// <summary>
        /// Getter for spreadsheetpanel for other form's usage
        /// </summary>
        /// <returns></returns>
        public SpreadsheetPanel GetSpreadsheetPanel()
        {
            return UserSpreadsheet; 
        }


        /// <summary>
        /// highlight the grid that the user has selected that place, 
        /// and display the cell value if already exist
        /// </summary>
        /// <param name="sp"></param>
        private void DisplaySelection(SpreadsheetPanel sp)
        {
            int row, col;
            sp.GetSelection(out col, out row);
            string cellName = ConvertCoordinatesToCellName(col, row);
            string cellContent = mainspreadsheet.GetCellContents(cellName).ToString();
            CellName.Text = cellName;
            Content.Text = cellContent;
            sp.SetValue(col, row, mainspreadsheet.GetCellValue(cellName).ToString());
        }

        /// <summary>
        /// Creates a new file, a new empty spreadsheet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewFile_Click(object sender, EventArgs e)
        {
            TheApplicationContext.getAppContext().RunForm(new MainForm());
        }

        /// <summary>
        /// Display a file dialog that prompts the user to open a file
        /// Then display the spreadsheet form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Spreadsheet files (*.sprd)|*.sprd|All files (*.*)|*.*";  // to be changed back
            openFile.DefaultExt = ".sprd";
            openFile.Title = "";

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                TheApplicationContext.getAppContext().RunForm(new MainForm(openFile.FileName, openFile.FileName));
            }
        }

        /// <summary>
        /// Allow concatnation feature to happen
        /// Additional feature
        /// Opens up a new form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConcatnationItem_Click(object sender, EventArgs e)
        {
            // run the form 
            ConcatForm concatform = new ConcatForm(this);
            UserSpreadsheet.SelectionChanged += concatform.OnSelectingSpreadsheet;
            TheApplicationContext.getAppContext().RunForm(concatform);
        }


        /// <summary>
        /// Opens up the help menu where user would find useful information in how this works
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HelpToolStripMenuItem_Click(object sender, EventArgs e)
        {

            MessageBox.Show("This Spreadsheet is a tool you can use as an alternative to Microsoft Exel." +
                "This Spreadsheet is a tool you can use as an alternative to Microsoft Exel.It allows" +
                "you to enter words, numbers, and formulas into the spreadsheet using the " +
                "Function bar(F(x):).This is located near the \"Help\" and \"File\" drop down menu.To " +
                "the left of the Function Bar, you have the \"Name\" of the cell that is selected. " +
                "Make sure to click the \"Add To Cell\" button or press enter to update the content of " +
                "the cell that is selected.You will notice that after you click \"Add to Cell\" or press " +
                "enter, the content you entered in the function bar is now in that cell value in the spreadsheet. " +
                "Once you are ready, you have the option to close the program, save your spreadsheet to a.sprd " +
                "file, or open(a) new spreadsheet(s).You have all these options in the \"File\" drop down button. " +
                "If you choose the close button, it will give you option to save the file before closing. If " +
                "a message box pops up on your screen dont be too freightened.Just make sure to read the box and" +
                "then continue with what you are doing.\n \n" +

                "Extra Feature: \n" +
                "As you noticed, there is a \"Functions\" drop-down near the \"Help\" and \"File\". If you select this, you will see an"+
                " the option of \"Concatenation\" which will then open a message box that will give you the option to choose the cells you"+
                "would like to concatenate. You can choose up to four cells. Once you click \"Enter\" another message box will pop up displaying your concatenation.",
                "Help");
        }

        /// <summary>
        /// Opens a file saving dialog to interact with user and determine 
        /// where to store the already existing file. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Spreadsheet Files (*.sprd)|*.sprd|All files (*.*)|*.*";
            saveFile.DefaultExt = ".sprd";
            saveFile.Title = "";
            saveFile.OverwritePrompt = false;
            saveFile.AddExtension = true;

            DialogResult dialog = saveFile.ShowDialog();

            if (dialog == DialogResult.OK)
            {
                mainspreadsheet.Save(saveFile.FileName);
            }

            if (dialog == DialogResult.Cancel)
            {
                saveFile.Dispose();
                return;
            }
        }

        /// <summary>
        /// Additional confirmation before the user close the spreadsheet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseForm_Click(object sender, EventArgs e)
        {
            if (mainspreadsheet.Changed) // if spreadsheet is being changed, then pops out a dialog to confirm
            {
                DialogResult result =
                    MessageBox.Show("You are about to save an unsaved file, " +
                    "do you want to save before closing?", "Confirm",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                {
                    confirmClose = true;
                    Close();
                }
                else if (result == DialogResult.Yes)
                {
                    SaveFile_Click(sender, e);
                    confirmClose = true;
                    Close();
                }
            }
            else
            {
                confirmClose = false;
            }
        }


        /// <summary>
        /// Additional confirmation before the user close the spreadsheet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosed(object sender, FormClosingEventArgs e)
        {
            if (!confirmClose)   // if confirm close is false, then verify close, else, just close
            {
                CloseForm_Click(sender, e);
            }
        }


        /// <summary>
        /// React to the user when the user decide to click the Enter Value button to enter value to the cell
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Submit_Click(object sender, EventArgs e)
        {
            string cellname = CellName.Text;
            IList<string> updatedcells = mainspreadsheet.SetContentsOfCell(cellname, Content.Text);
            int oldcol, oldrow; // make a copy of old selection so could be used to go back
            UserSpreadsheet.GetSelection(out oldcol, out oldrow);

            // go through all the cells that are reevaluated due to change in one cell, 
            // and display the reevaluated cells on the screen by quickly switch selection to them 
            foreach (string cell in updatedcells)
            {
                int row, col;
                ConvertCellNameToCoordinates(cell, out col, out row);
                UserSpreadsheet.SetValue(col, row, mainspreadsheet.GetCellValue(cell).ToString());
                UserSpreadsheet.SetSelection(col, row);  // change the selection
                DisplaySelection(UserSpreadsheet);
                // display the selection immediately after the selection is changed
            }

            UserSpreadsheet.SetSelection(oldcol, oldrow);
            DisplaySelection(UserSpreadsheet);
        }


        /// <summary>
        /// Helper method to convert a cell name to the column and row index of spreadsheet
        /// </summary>
        /// <param name="col"></param>
        /// <param name="row"></param>
        public void ConvertCellNameToCoordinates(string name, out int col, out int row)
        {
            col = name[0];  // The letter, in column. 
            row = int.Parse(name.Substring(1));  // the index, in row. 
            row--;
            int startvalue = col > 96 ? 97 : 65;
            col -= startvalue;
        }


        /// <summary>
        /// Helper method to convert a cell name to the column and row index of spreadsheet
        /// </summary>
        /// <param name="col"></param>
        /// <param name="row"></param>
        public string ConvertCoordinatesToCellName(int col, int row)
        {
            int startingpoint = col < 91 ? 65 : 97;
            // cell name will be a character + a column
            return ((char)(col + startingpoint)).ToString() + (row + 1).ToString();
        }


        /// <summary>
        /// Check if variable is valid
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private bool IsValid(string s)
        {
            if (s == null)
                throw new ArgumentException("Argument is null");
            else if (s.Length < 2)
                return false;
            else if (Char.IsLetter(s[0]))  // first element is a letter
            {
                int letterdigitbreakingpoint = -1; // the index in a string where letter ends and starts with digits 

                for (int i = 1; i < s.Length - 1; i++)
                {
                    if (!Char.IsLetterOrDigit(s[i]))
                    {
                        return false;
                    }
                    else if (Char.IsDigit(s[i]))   // found the frist digit
                    {
                        letterdigitbreakingpoint = i;
                        break;
                    }
                }

                if (letterdigitbreakingpoint != -1)
                {
                    for (int i = letterdigitbreakingpoint; i < s.Length; i++)
                    {
                        if (!Char.IsDigit(s[i]))
                        {
                            return false;
                        }
                    }
                    return true;
                }
                else
                {
                    if (Char.IsDigit(s[s.Length - 1]))
                        return true;
                }
            }
            return false;
        }


        /// <summary>
        /// Normalize the variable
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private string Normalize(string s)
        {
            if (s == null)
            {
                throw new ArgumentException("Argument is null");
            }

            return s.ToUpper();
        }
    }





}