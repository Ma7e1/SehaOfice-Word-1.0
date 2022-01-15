using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Security.Permissions;
using System.Drawing.Printing;

namespace SOfice
{
    
    public partial class Form1 : Form
    {
        decimal a = 10;
        
        
        object p = FileIOPermissionAccess.Write;
        public Form1()
        {
            InitializeComponent();
            float b = Convert.ToSingle(a);
            textBox1.Font = new Font(textBox1.Font.FontFamily, b);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (PrinterSettings.InstalledPrinters.Count <= 0)
            {
                MessageBox.Show("Printer not found!");
                return;
            }

            //Get all available printers and add them to the combo box  
            foreach (String printer in PrinterSettings.InstalledPrinters)
            {
                printerslist.Items.Add(printer.ToString());
            }
            textBox1.Enabled = true;
            toolStrip1.Dock = DockStyle.Bottom;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

            saveFileDialog1.ShowDialog();
            FileStream file = new FileStream(saveFileDialog1.FileName, FileMode.Create);
            byte[] bdata = Encoding.Default.GetBytes(textBox1.Text);
            file.Write(bdata, 0, bdata.Length);
            file.Close();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            string contet;
            openFileDialog1.ShowDialog();
            FileStream file = new FileStream(openFileDialog1.FileName, FileMode.Open);
            using (StreamReader sr = new StreamReader(file))
            {
                float b = Convert.ToSingle(a);
                contet = sr.ReadToEnd();
                textBox1.Text = contet;
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            
            DialogResult rezult = MessageBox.Show("Are you sure you want to print?", "SOfice print", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rezult == DialogResult.Yes)
            {
                
                PrintDocument p = new PrintDocument();
                p.PrintPage += delegate (object sender1, PrintPageEventArgs e1)
                {
                    float b = Convert.ToSingle(a);
                    e1.Graphics.DrawString(textBox1.Text, new Font("Times New Roman", b), new SolidBrush(Color.Black), new RectangleF(0, 0, p.DefaultPageSettings.PrintableArea.Width, p.DefaultPageSettings.PrintableArea.Height));

                };
                try
                {
                    p.Print();
                }
                catch (Exception ex)
                {
                    throw new Exception("Exception Occured While Printing", ex);
                }
            }
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            a = numericUpDown1.Value;
            float b = Convert.ToSingle(a);
            textBox1.Font = new Font(textBox1.Font.FontFamily, b);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            
        }
    }


}
