using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AMCDatabase;
using System.Threading;
using ClosedXML.Excel;
using System.Collections;

namespace SubComponentExporter
{
    public struct GridValues
    {
        public string sSubValue;
        public string sSerial;
        public string sPartNumber;
        public string sVersion;
        public string sSubSerial;
        public string sSubPartNumber;
        public string sSubVer;
    }

    enum Columns
    {
        Found,
        Sub_Num,
        Serial_Num,
        Part_Num,
        Sub_Serial,
        Sub_Part_Num,
        Version,
        Sub_Version
        
    }
    
    public partial class SubCompExport : Form
    {
        private const string FOUND = "1";
        private const string NOT_FOUND = "0";
        private const string EXCEL_FILENAME = "dgr_RMA_Labels_Multiple_Columns.xlsx";

        private bool _bCanExport = false;
        
        // ja - progress bar
        private int _ProgressBarDelay = 1;
        private bool _bReAdjustProgressBar = false;
        private int _nNewValue = 0; // ja - for progress bar adjustments

        private int _nRowIndex; // ja - for right click events
        
        public SubCompExport()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
#if DEBUG
            string[] row = { FOUND, "0", "<64978-1000>", "N/A", "N/A", "N/A", "0.00", "0.00" };
            rmaDataGridView.Rows.Add(row);

            string[] row1a = { "0", "67862-1111", "N/A", "N/A", "N/A" };
//            rmaDataGridView.Rows.Add(row1a);

            string[] row2 = { FOUND, "0", "<64979-1013>", "N/A", "N/A", "N/A", "0.00", "0.00" };
            rmaDataGridView.Rows.Add(row2);

            string[] row1b = { "0", "67862-0000", "N/A", "N/A", "N/A" };
//            rmaDataGridView.Rows.Add(row1b);

            string[] row2a = { "0", "<64980-1016>", "N/A", "N/A", "N/A" };
//            rmaDataGridView.Rows.Add(row2a);
#endif 
            // ja - this will get rid of the extra row in the data grid
            rmaDataGridView.AllowUserToAddRows = false;

            excelBtn.Enabled = false;
        }

        private string GetPartFromSerial(string sSerial, ref string sVersion)
        {
            // ja - split the serial number (workcode)
            string[] sSerialSplit = sSerial.Split('-');

            // ja - get the workcode information and part number
            Workcode wc = new Workcode(sSerialSplit[0]);
            string sPart = wc.GetValue(wc.MODELLCODE);
            sVersion = wc.GetValue(wc.AMPVERSION);

            return sPart;
        }

        private void UpdateStatus(string sMsg)
        {
            // ja - update the status label (and command line for debugging) 
            Console.WriteLine(sMsg);
            statusLbl.Invoke(new MethodInvoker(() => statusLbl.Text = sMsg));
        }

        private int GetAmplifsInfo(string sSerial, int nGridRow)
        {
            int nOrgPos = nGridRow;
            bool bFirstTime = true;
            GridValues gv = new GridValues();
            gv.sSerial = sSerial;

            // ja - update the status label (and command line for debugging) 
            string sMsg = "Getting Amplifs Info for - " + gv.sSerial;
            UpdateStatus(sMsg);
                        
            // ja - get the amp information (this takes some time)
            Amplifs amp = new Amplifs(gv.sSerial);

            // ja - if amp is not found display error
            if (!amp.KeyFound())
            {
                // ja - update the status label (and command line for debugging) 
                string sMsg2 = "Amp: " + gv.sSerial + " Not Found..";
                UpdateStatus(sMsg2);

                // ja - add serial Number to list for painting row red   
                rmaDataGridView.Invoke(new MethodInvoker(() => rmaDataGridView.Rows[nGridRow].Cells[(int)Columns.Found].Value = NOT_FOUND));
                rmaDataGridView.Invoke(new MethodInvoker(() => rmaDataGridView.InvalidateRow(nGridRow)));                
                
                return nGridRow;
            }

            // ja - call function to get the part number from the Workcode table
            gv.sPartNumber = GetPartFromSerial(gv.sSerial, ref gv.sVersion);

            // ja - there are 15 columns in the table so enumerate through all of them to get the Sub Component
            for (int i = 1; i <= 15; i++)
            {
                // ja - concatenate the string for sub component 1..15
                gv.sSubValue = i.ToString();
                string sSubCompName = "Sub_comp" + gv.sSubValue;
                
                // ja - get the sub component value from the amplifs table
                gv.sSubSerial = amp.GetValue(sSubCompName).Trim();
                
                // ja - update status 
                UpdateStatus("Processing Sub Component - " + gv.sSubValue);

                // ja - if the value is not empty grab the part number
                if (!string.IsNullOrWhiteSpace(gv.sSubSerial))
                {
                    // ja - get the part number from the Workcode table
                    gv.sSubPartNumber = GetPartFromSerial(gv.sSubSerial, ref gv.sSubVer);

                    // ja - insert or replace (bFirstTime = true) a row, return the offset in the case of an insert 
                    nGridRow = AddRow(gv, ref bFirstTime, nGridRow);
                }           
            }

            // ja - return the offset
            return nGridRow - nOrgPos;
        }

        //private int AddRow(string sSubValue, string sSerial, string sPartNumber, string sSubSerial, string sSubPartNumber, string sVersion, string sSubVer, ref bool bFirstTime, int nPos)
        private int AddRow(GridValues gv, ref bool bFirstTime, int nPos)
        {
            // ja - if this is the first time edit row info otherwise add new row
            // ja - they would rather have the Empty Row so I will comment this code out for now
#if DONT_REPLACE_HEADER
            if (bFirstTime)
            {
                try
                {
                    // ja - replace the information at the current index in the grid
                    rmaDataGridView.Invoke(new MethodInvoker(() => rmaDataGridView.Rows[nPos].Cells[(int)Columns.Sub_Num].Value = gv.sSubValue));
                    rmaDataGridView.Invoke(new MethodInvoker(() => rmaDataGridView.Rows[nPos].Cells[(int)Columns.Serial_Num].Value = gv.sSerial));
                    rmaDataGridView.Invoke(new MethodInvoker(() => rmaDataGridView.Rows[nPos].Cells[(int)Columns.Part_Num].Value = gv.sPartNumber));
                    rmaDataGridView.Invoke(new MethodInvoker(() => rmaDataGridView.Rows[nPos].Cells[(int)Columns.Sub_Serial].Value = gv.sSubSerial));
                    rmaDataGridView.Invoke(new MethodInvoker(() => rmaDataGridView.Rows[nPos].Cells[(int)Columns.Sub_Part_Num].Value = gv.sSubPartNumber));
                    rmaDataGridView.Invoke(new MethodInvoker(() => rmaDataGridView.Rows[nPos].Cells[(int)Columns.Version].Value = gv.sVersion));
                    rmaDataGridView.Invoke(new MethodInvoker(() => rmaDataGridView.Rows[nPos].Cells[(int)Columns.Sub_Version].Value = gv.sSubVer));

                    bFirstTime = false;
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }                        
            }
            else // ja - insert a new row 
            {
#endif
            try 
            {
                if (bFirstTime)
                {
                    // ja - replace the <> for the header row to mark as processed and so it will export to the excel file
                    rmaDataGridView.Invoke(new MethodInvoker(() => rmaDataGridView.Rows[nPos].Cells[(int)Columns.Serial_Num].Value = gv.sSerial));
                    rmaDataGridView.Invoke(new MethodInvoker(() => rmaDataGridView.Rows[nPos].Cells[(int)Columns.Part_Num].Value = gv.sPartNumber));
                    rmaDataGridView.Invoke(new MethodInvoker(() => rmaDataGridView.Rows[nPos].Cells[(int)Columns.Found].Value = FOUND));
                }
                
                // ja - add a new row and increment the index so the offset is modified
                string[] row = { FOUND, gv.sSubValue, gv.sSerial, gv.sPartNumber, gv.sSubSerial, gv.sSubPartNumber, gv.sVersion, gv.sSubVer };
                rmaDataGridView.Invoke(new MethodInvoker(() => rmaDataGridView.Rows.Insert((nPos++ + 1), row)));
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
//            }

            return nPos;
        }

        private void ProccessData()
        {            
            int nIndex = 0;
            string sSerial = "";
            _nNewValue = 0;
            
            Dictionary<int, string> unProcessedSerials = new Dictionary<int, string>();

            // ja - enumerate through the rows and add to the map
            foreach (DataGridViewRow row in rmaDataGridView.Rows)
            {
                try
                {
                    sSerial = row.Cells[(int)Columns.Serial_Num].Value.ToString();

                    // ja - test for not processed flag (<>)
                    if (sSerial.StartsWith("<") && sSerial.EndsWith(">"))
                    {
                        sSerial = sSerial.Trim(new Char[] { '<', '>' });
                        
                        // ja - add to map with the indexed position as the key
                        unProcessedSerials.Add(nIndex, sSerial);
                     
                    }
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                nIndex++;
            }

            _ProgressBarDelay = unProcessedSerials.Count;
            
            int nCounter = 1;
            int nOffset = 0;
            foreach (var val in unProcessedSerials)
            {
                Console.WriteLine("Index is - " + (val.Key).ToString());
                Console.WriteLine("Offset is - " + (nOffset).ToString());
                
                // ja - get the sub components and part numbers from Amplifs
                // ja - the offset is used to keep the index of the grid aligned when adding more than one sub component 
                nOffset += GetAmplifsInfo(val.Value, (val.Key + nOffset));

                _nNewValue = (int)((100 / _ProgressBarDelay) * nCounter); 
                nCounter++;
                _bReAdjustProgressBar = true;
            }

            _bCanExport = true;
            
            UpdateUI();            
        }

        private void UpdateUI(bool bEnabled = true)
        {
            addBtn.Invoke(new MethodInvoker(() => addBtn.Enabled = bEnabled));
            rmaDataGridView.Invoke(new MethodInvoker(() => rmaDataGridView.Enabled = bEnabled));
            populateBtn.Invoke(new MethodInvoker(() => populateBtn.Enabled = bEnabled));

            if (bEnabled)
            {
                if (_bCanExport)
                    excelBtn.Invoke(new MethodInvoker(() => excelBtn.Enabled = bEnabled));
            }
            else
                excelBtn.Invoke(new MethodInvoker(() => excelBtn.Enabled = bEnabled));
            
            if (bEnabled)
            {
                statusLbl.Invoke(new MethodInvoker(() => statusLbl.Text = "Processing Complete you can now Export"));
                CancelProgressBar();
            }
            else
            {
                // ja - start the background task to update the progress bar
                StartPBBackGround(1);
            }

        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            //  ja - verify valid serial number format?
            if (serialNumMTB.Text.Trim().Length != 10)
            {
                MessageBox.Show("Invalid Format for Serial Number", "Invalid Format");
                return;
            }

            // ja - check for duplicate entries
            try
            {
                foreach (DataGridViewRow row in rmaDataGridView.Rows)
                {
                    string sRowValue = row.Cells[(int)Columns.Serial_Num].Value.ToString();
                    if (sRowValue.Equals("<" + serialNumMTB.Text + ">") || sRowValue.Equals(serialNumMTB.Text))
                    {
                        MessageBox.Show("Serial Number " + serialNumMTB.Text + " is already in List" ,"Duplicate Serial");
                        return;
                    }                                    
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            string[] newRow = { FOUND, "0", "<" + serialNumMTB.Text + ">", "N/A", "N/A", "N/A", "0.00", "0.00" };
            int nRow = rmaDataGridView.Rows.Add(newRow);

            _bCanExport = false;
            excelBtn.Enabled = false;
            populateBtn.Enabled = true;

            UpdateStatus("Please Process the Serial Numbers before Exporting...");
        }

        private void exportBtn_Click(object sender, EventArgs e)
        {
            // ja - disable the controls and start the progress bar background task
            UpdateUI(false);           

            // ja - Launch Batch Thread for processing the items in the grid
            Thread batchThread = new Thread(ProccessData);
            batchThread.Start();            
        }

#region ExcelExport

        private void excelBtn_Click(object sender, EventArgs e)
        {
            ExportToExcel();    
        }

        private void ExportToExcel()
        {
            // ja - create a new workbook and update the worksheet title (this cannot change)
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("dgr_RMA_Labels_Multiple_Columns");

            // ja - add the headers
            worksheet.Cell("A1").Value = "RMA_Number";
            worksheet.Cell("B1").Value = "Serial_Number";
            worksheet.Cell("C1").Value = "Part_Number";
            worksheet.Cell("D1").Value = "Part_Number_Ver";
            worksheet.Cell("E1").Value = "SubComp_Serial_No";
            worksheet.Cell("F1").Value = "SubComp_Part_No";
            worksheet.Cell("G1").Value = "SubComp_Part_No_Ver";
            worksheet.Cell("H1").Value = "RMA_ReceivedDate";

            // ja - start at row 2
            int nIndex = 2;

            // ja - enumerate through the data grid and add the values to the worksheet
            foreach (DataGridViewRow row in rmaDataGridView.Rows)
            {
                try
                {
                    string sSerial = row.Cells[(int)Columns.Serial_Num].Value.ToString();
                    string sSubSerial = row.Cells[(int)Columns.Sub_Serial].Value.ToString();

                    // ja - add yellow highlight to header row
                    if ((highlightChk.Checked) && (sSubSerial.Equals("N/A")))
                    {
                        worksheet.Rows(nIndex, nIndex).Style.Fill.BackgroundColor = XLColor.Yellow;
                    }

                    // ja - filter out Not Serialized
                    if ((filterChk.Checked) && (sSubSerial.Equals("NOT SERIALIZED")))
                        continue;
                    
                    // ja - if the serial number has not been processed don't export
                    if (!sSerial.StartsWith("<"))
                    {
                        
                        // ja - add Columns B (Serial), C (PartNumber), E (Sub Serial) and F (Sub Serial PartNumber)
                        worksheet.Cell("B" + nIndex.ToString()).Value = sSerial;
                        worksheet.Cell("C" + nIndex.ToString()).Value = row.Cells[(int)Columns.Part_Num].Value.ToString();
                        worksheet.Cell("D" + nIndex.ToString()).Value = row.Cells[(int)Columns.Version].Value.ToString(); // ja - version
                        worksheet.Cell("E" + nIndex.ToString()).Value = sSubSerial;
                        worksheet.Cell("F" + nIndex.ToString()).Value = row.Cells[(int)Columns.Sub_Part_Num].Value.ToString();
                        worksheet.Cell("G" + nIndex.ToString()).Value = row.Cells[(int)Columns.Sub_Version].Value.ToString(); // ja - sub version

                        // ja - Move to next row
                        nIndex++;
                    }                    
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            worksheet.Columns().AdjustToContents();

            try
            {
                string sFileName = "";
                
                //  ja - display a folder dialog
                FolderBrowserDialog dirDialog = new FolderBrowserDialog();
                dirDialog.Description = "Please choose a Directory to save the Excel file to..";
                
                // ja - if ok then save at the dialogs location
                if (dirDialog.ShowDialog() == DialogResult.OK)
                {
                    // ja - this filename cannot be changed!
                    sFileName = dirDialog.SelectedPath + "\\" + EXCEL_FILENAME;
                    
                    // ja - save new excel spreadsheet
                    workbook.SaveAs(sFileName);

                    if (openChk.Checked)
                    {
                        System.Diagnostics.Process.Start(sFileName);
                    }
                }                                
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);	
            }            
        }
#endregion

#region ProgressBar

        private void CancelProgressBar()
        {
            try
            {
                // ja - send a cancel event to the Background Worker Thread
                backgroundWorker.CancelAsync();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void StartPBBackGround(int nColor)
        {
            progressBar.ForeColor = Color.DodgerBlue;
            
            try
            {
                if (backgroundWorker.IsBusy)
                {
                    // ja - cancel background worker
                    backgroundWorker.CancelAsync();
                    Thread.Sleep(25);
                }

                backgroundWorker.RunWorkerAsync();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        // ja - called when Background Worker 
        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 1; i <= 100; i++)
            {
                //  ja - about 4 seconds for each Serial Number in Amplifs
                Thread.Sleep(415 * _ProgressBarDelay);

                try
                {
                    if (backgroundWorker.CancellationPending)
                    {
                        backgroundWorker.ReportProgress(100);
                        break;
                    }
                    else
                    {
                        if (_bReAdjustProgressBar)
                        {
                            i = _nNewValue;
                            _bReAdjustProgressBar = false;
                        }
                        
                        backgroundWorker.ReportProgress(i);
                    }
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        // ja - gets called by Report Progress function of Background Worker
        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                // ja - report progress in progress bar as %
                progressBar.Invoke(new MethodInvoker(() => progressBar.Value = e.ProgressPercentage));
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

#endregion

#region GridContexMenu

        private void dataGridView1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0)
                {
                    rmaDataGridView.Rows[e.RowIndex].Selected = true;
                    _nRowIndex = e.RowIndex;
                    rmaDataGridView.CurrentCell = this.rmaDataGridView.Rows[e.RowIndex].Cells[1];
                    contextMenuStrip1.Show(this.rmaDataGridView, e.Location);
                    contextMenuStrip1.Show(Cursor.Position);
                }
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!rmaDataGridView.Rows[_nRowIndex].IsNewRow)
            {
                rmaDataGridView.Rows.RemoveAt(_nRowIndex);
            }
        }

        private void clearAllItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                rmaDataGridView.Rows.Clear();                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            UpdateStatus("All Rows Cleared..");
            populateBtn.Enabled = false;
            excelBtn.Enabled = false;
        }
#endregion

        private void rmaDataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                // ja - check the hidden found column to set the error flag on or off
                bool bSerialFound = (rmaDataGridView.Rows[e.RowIndex].Cells[(int)Columns.Found].Value.ToString() == FOUND);
                 
                // ja - alternate colors in grid
                if (e.RowIndex == -1)
                {
                    SolidBrush br = new SolidBrush(Color.Gainsboro);//Gainsboro
                    e.Graphics.FillRectangle(br, e.CellBounds);
                    e.PaintContent(e.ClipBounds);
                    e.Handled = true;
                    return;
                }
                    
                if (e.RowIndex % 2 == 0)
                {
                    SolidBrush br = new SolidBrush(Color.LightSeaGreen);//Gainsboro
                    e.Graphics.FillRectangle(br, e.CellBounds);
                    e.PaintContent(e.ClipBounds);
                    e.Handled = true;
                }
                else
                {
                    SolidBrush br = new SolidBrush(Color.LightBlue);//white
                    e.Graphics.FillRectangle(br, e.CellBounds);
                    e.PaintContent(e.ClipBounds);
                    e.Handled = true;
                }

                // ja - paint any unprocessed red
                if (!bSerialFound)
                {
                    SolidBrush br = new SolidBrush(Color.Red);
                    e.Graphics.FillRectangle(br, e.CellBounds);
                    e.PaintContent(e.ClipBounds);
                    e.Handled = true;
                }                
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
