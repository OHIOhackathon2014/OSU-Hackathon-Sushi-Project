using Microsoft.Win32;
using SushiProject.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace SushiProject.Utilities
{
    public class ProjectAS3Compiler
    {
        public ProjectAS3Compiler()
        {
        }

        public void CompileProject(GameProject gameProject)
        {
            var writer = new SushiProject.Utilities.ProjectAS3Writer();
            writer.WriteASFiles(gameProject);

            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = String.Format("/C C:\\AirSDK\\bin\\mxmlc.exe C:\\game\\Main.as -output C:\\game\\game.swf");
            process.StartInfo = startInfo;
            process.Start();
        }
    }
}
