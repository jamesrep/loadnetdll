using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Management.Automation;
using System.Management.Instrumentation;
using System.Collections.ObjectModel;

namespace LoadNetDll
{
    public partial class frmPowershell : Form
    {
        public frmPowershell()
        {
            InitializeComponent();
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                using (PowerShell pwr = PowerShell.Create())
                {
                    pwr.AddScript(textBox2.Text);
                    Collection<PSObject> PSOutput = pwr.Invoke();

                    textBox1.Text += "$ " + textBox2.Text + "\r\n";

                    foreach (PSObject outputItem in PSOutput)
                    {
                        if (outputItem != null)
                        {
                            Console.WriteLine(outputItem.ToString());
                            textBox1.AppendText(outputItem.ToString() + "\r\n");
                        }
                    }

                   
                }

                textBox2.Text = "";
            }
        }

        bool bFirst = false;

        private void textBox2_Click(object sender, EventArgs e)
        {
            if (!bFirst)
            {
                textBox2.Text = "";
                bFirst = true;
            }
        }

        private void frmPowershell_Load(object sender, EventArgs e)
        {


        }
    }
}
