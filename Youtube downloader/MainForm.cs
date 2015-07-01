using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using YouTube_downloader.Properties;

namespace Youtube_downloader

{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        private string output;
        private Process exeProcess = new Process();
        private void Form1_Load(object sender, EventArgs e)
        {
            radYTtitle.Checked = true;
            //linkgit.Links.Add(6,4,"http://www.github.com/rnand/");
            if (Clipboard.ContainsText()) //check whether the clipboard contains text data
            {
                Regex isUrl = new Regex("^https?://|^www"); //if it begins with http:// or https:// or www
                Match URLtest = isUrl.Match(Clipboard.GetText());
                if (URLtest.Success) //copy content from clipboard only if it begins with http:// or https://
                {
                    txtURL.Text = Clipboard.GetText();
                }

                
            }
            
            txtdir.Text = Settings.Default.CustomPath; //save the default path to this variable
            if (Settings.Default.CustomPath != "")
            {
                chkDefLoc.Checked = true;
                
                //MessageBox.Show("OKAY");
            }
   
        }

        private void btnbrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
            txtdir.Text = fbd.SelectedPath;
            
        }

        private void btndwnld_Click(object sender, EventArgs e)
        {
            
            if (txtURL.Text == "" ||  txtdir.Text == "")
            {
                MessageBox.Show("Enter the required values.", "Values needed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (chkPlaylst.Checked == true)  //refactoring required in the future
            {
                this.Height = 530; //resize the form        
                //show the text box
                txtStatus.Visible = true;
                
                //show the 'hide status' button
                btnHideSt.Visible = true;

                //show the progressbar
                prgrsbr.Visible = true;
                
                
                //MessageBox.Show("Tjorf frseter cionmf spgen (jeiotlfust). This feature coming soon (playlist)");
                string ex1 = Path.Combine(Path.GetTempPath(), "youtube-dl.exe");
                File.WriteAllBytes(ex1, YouTube_downloader.Properties.Resources.youtube_dl);
                string ftype; //the file type
                string PLurl = txtURL.Text;
                string fdir = txtdir.Text;
                string fname = txtfilename.Text; //the file name

                string qlty = "best"; //default quality
                ////////////////////////////////////////////////////////////////
                //the following options set the quality for mp4

                ftype = "mp4";
                if (rdb4k.Checked && rdbmp4.Checked)
                {
                    qlty = "266+141"; //266 for mp4 video @ 2160p, H.264, Video bitrate (Mbits/s) : 12.5-13.5 
                    //141 for mp4 audio AAC, Bitrate (kbits/s) : 256
                }
                else if (rdbhd1080.Checked && rdbmp4.Checked)
                {
                    qlty = "137+140"; //137 for mp4 video @ 1080p, H.264, Video bitrate (Mbits/s) : 2.5-3
                    //140 for mp4 audio AAC, Bitrate (kbits/s) : 128
                }
                else if (rdbhd720.Checked && rdbmp4.Checked)
                {
                    qlty = "22"; //22 for mp4 video @ 720p, H.264, Video bitrate (Mbits/s) : 2-3
                    //Audio AAC @ 192 kbits/s
                }
                else if (rdbsd480.Checked && rdbmp4.Checked)
                {
                    qlty = "135+140";//135 for mp4 video @ 480p, H.264, Video bitrate (Mbits/s) : 0.5-1
                    //140 for mp4 audio AAC, Bitrate (kbits/s) : 128
                }
                else if (rdbsd360.Checked && rdbmp4.Checked)
                {
                    qlty = "18"; //18 for mp4 video @ 360p, H.264, Video bitrate (Mbits/s) : 0.5
                    //Audio AAC @ 96 kbits/s
                }
                ////////////////////////////////////////////////////////////////
                //the following options set quality for webm

                if (rdbwebm.Checked)
                {
                    ftype = "webm";
                }

                if (rdbhd1080.Checked && rdbwebm.Checked)
                {
                    qlty = "248+140"; //248 for webm video @ 1080p, VP9, Video bitrate (Mbits/s) : 1.5
                    //140 for mp4 audio AAC, Bitrate (kbits/s) : 128
                }
                else if (rdbhd720.Checked && rdbwebm.Checked)
                {
                    qlty = "247+140";//247 for webm video @ 720p, VP9, Video bitrate (Mbits/s) : 0.7-0.8
                    //140 for mp4 audio AAC, Bitrate (kbits/s) : 128
                }
                else if (rdbsd480.Checked && rdbwebm.Checked)
                {
                    qlty = "244+140";//247 for webm video @ 480p, VP9, Video bitrate (Mbits/s) : 0.5
                    //140 for mp4 audio AAC, Bitrate (kbits/s) : 128
                }
                else if (rdbsd360.Checked && rdbwebm.Checked)
                {
                    qlty = "43";  //43 for webm video @ 360p, VP8, Video bitrate (Mbits/s) : 0.5
                    //Audio Vorbis @ 128 kbits/s
                }

                
                //ProcessStartInfo startInfo = new ProcessStartInfo();
                exeProcess.StartInfo.RedirectStandardOutput = true;
                exeProcess.StartInfo.CreateNoWindow = true;
                exeProcess.StartInfo.UseShellExecute = false;
                exeProcess.EnableRaisingEvents = true;
                exeProcess.StartInfo.FileName = ex1;
                //startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                if (txtPLstart.Text=="" && txtPLend.Text=="")
                {
                    exeProcess.StartInfo.Arguments = " -o " + "\"" + fdir + "\\" + "%(title)s" + "." + ftype + "\"" + " " + PLurl + " -f " + qlty;
                }
                else
                {
                    string strtNum = txtPLstart.Text;
                    string endNum = txtPLend.Text;
                    exeProcess.StartInfo.Arguments = " -o " + "\"" + fdir + "\\" + "%(title)s" + "." + ftype + "\"" + " " + PLurl + " -f " + qlty + " --playlist-start " + strtNum + " --playlist-end " + endNum;//ftype; +" ";
                }

                exeProcess.OutputDataReceived += exeProcess_OutDataReceivedHandler; // generate event handlers when 
                exeProcess.ErrorDataReceived += exeProcess_OutDataReceivedHandler; //   data is received from console

                exeProcess.Exited += new EventHandler(exeProcess_ExitedHandler); //event handler to handle process exit

                try
                {
                    
                    
                   
                    // Start the process with the info we specified.
                    exeProcess.Start();
                    exeProcess.BeginOutputReadLine();
                    exeProcess.BeginErrorReadLine();
                    //exeProcess.WaitForExit();   // calling WaitForExit() will suspend the UI thread. So don't do that.
                    while (!exeProcess.HasExited) // Instead do this.
                    {
                        Application.DoEvents(); // This keeps the form responsive by processing events
                    }
                    //if (exeProcess.ExitCode == 0)
                    //{
                    //    MessageBox.Show("Download Complete.", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Download Failed.", "Status", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //}
                    
                }
                catch
                {
                    //MessageBox.Show("ERROR");
                }
            }
            else
            {
                
                Regex linkParser = new Regex("^(?:https?\\:\\/\\/)?(?:www\\.)?(?:youtu\\.be\\/|youtube\\.com\\/(?:embed\\/|v\\/|watch\\?v\\=))([\\w-]{10,12})(?:[\\&\\?\\#].*?)*?(?:[\\&\\?\\#]t=([\\dhm]+s))?$", RegexOptions.Compiled | RegexOptions.IgnoreCase); //the youtube url checker regex
                string vURL = txtURL.Text;
                string vID;//stores video ID
                Match match = linkParser.Match(vURL);//match the pattern
                if (!match.Success)
                {
                    MessageBox.Show("The given URL is not a valid YouTube URL.", "Invalid URL", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    this.Height = 530; //resize the form
                    //show the text box
                    txtStatus.Visible = true;

                    //show the 'hide status' button
                    btnHideSt.Visible = true;

                    //show the progressbar
                    prgrsbr.Visible = true;

                    vID = match.Groups[1].Value;//the extracted video ID is stored in Groups[1]
                    //MessageBox.Show(vID);
                    string yURL = "http://www.youtube.com/watch?v=" + vID;//generate the proper URL
                    string ex1 = Path.Combine(Path.GetTempPath(), "youtube-dl.exe");
                    File.WriteAllBytes(ex1, YouTube_downloader.Properties.Resources.youtube_dl);
                    string ftype; //the file type

                    string fdir = txtdir.Text;
                    string fname = txtfilename.Text; //the file name

                    string qlty = "best"; //default quality
                    ////////////////////////////////////////////////////////////////
                    //the following options set the quality for mp4

                    ftype = "mp4";
                    if (rdb4k.Checked && rdbmp4.Checked)
                    {
                        qlty = "266+141"; //266 for mp4 video @ 2160p, H.264, Video bitrate (Mbits/s) : 12.5-13.5 
                        //141 for mp4 audio AAC, Bitrate (kbits/s) : 256
                    }
                    else if (rdbhd1080.Checked && rdbmp4.Checked)
                    {
                        qlty = "137+140"; //137 for mp4 video @ 1080p, H.264, Video bitrate (Mbits/s) : 2.5-3
                        //140 for mp4 audio AAC, Bitrate (kbits/s) : 128
                    }
                    else if (rdbhd720.Checked && rdbmp4.Checked)
                    {
                        qlty = "22"; //22 for mp4 video @ 720p, H.264, Video bitrate (Mbits/s) : 2-3
                        //Audio AAC @ 192 kbits/s
                    }
                    else if (rdbsd480.Checked && rdbmp4.Checked)
                    {
                        qlty = "135+140";//135 for mp4 video @ 480p, H.264, Video bitrate (Mbits/s) : 0.5-1
                        //140 for mp4 audio AAC, Bitrate (kbits/s) : 128
                    }
                    else if (rdbsd360.Checked && rdbmp4.Checked)
                    {
                        qlty = "18"; //18 for mp4 video @ 360p, H.264, Video bitrate (Mbits/s) : 0.5
                        //Audio AAC @ 96 kbits/s
                    }
                    ////////////////////////////////////////////////////////////////
                    //the following options set quality for webm

                    if (rdbwebm.Checked)
                    {
                        ftype = "webm";
                    }

                    if (rdbhd1080.Checked && rdbwebm.Checked)
                    {
                        qlty = "248+140"; //248 for webm video @ 1080p, VP9, Video bitrate (Mbits/s) : 1.5
                        //140 for mp4 audio AAC, Bitrate (kbits/s) : 128
                    }
                    else if (rdbhd720.Checked && rdbwebm.Checked)
                    {
                        qlty = "247+140";//247 for webm video @ 720p, VP9, Video bitrate (Mbits/s) : 0.7-0.8
                        //140 for mp4 audio AAC, Bitrate (kbits/s) : 128
                    }
                    else if (rdbsd480.Checked && rdbwebm.Checked)
                    {
                        qlty = "244+140";//247 for webm video @ 480p, VP9, Video bitrate (Mbits/s) : 0.5
                        //140 for mp4 audio AAC, Bitrate (kbits/s) : 128
                    }
                    else if (rdbsd360.Checked && rdbwebm.Checked)
                    {
                        qlty = "43";  //43 for webm video @ 360p, VP8, Video bitrate (Mbits/s) : 0.5
                        //Audio Vorbis @ 128 kbits/s
                    }

                    
                    //ProcessStartInfo startInfo = new ProcessStartInfo();
                    exeProcess.StartInfo.RedirectStandardOutput = true;
                    exeProcess.StartInfo.CreateNoWindow = true;
                    exeProcess.StartInfo.UseShellExecute = false;
                    exeProcess.EnableRaisingEvents = true;
                    exeProcess.StartInfo.FileName = ex1;
                    //startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    if (radYTtitle.Checked)
                    { //set the arguments to the process
                        exeProcess.StartInfo.Arguments = " -o " + "\"" + fdir + "\\" + "%(title)s" + "." + ftype + "\"" + " " + yURL + " -f " + qlty;
                    }
                    else
                    {
                        exeProcess.StartInfo.Arguments = " -o " + "\"" + fdir + "\\" + fname + "." + ftype + "\"" + " " + yURL + " -f " + qlty;//ftype; +" ";
                    }

                    exeProcess.OutputDataReceived += exeProcess_OutDataReceivedHandler; // generate event handlers when 
                    exeProcess.ErrorDataReceived += exeProcess_OutDataReceivedHandler; //   data is received from console

                    exeProcess.Exited += new EventHandler(exeProcess_ExitedHandler); //handle process exit
                    try
                    {

                        // Start the process with the info we specified.
                        exeProcess.Start();
                        exeProcess.BeginOutputReadLine();
                        exeProcess.BeginErrorReadLine();
                        //exeProcess.WaitForExit();   // calling WaitForExit() will suspend the UI thread. So don't do that.
                        while (!exeProcess.HasExited) // Instead do this.
                        {
                            Application.DoEvents(); // This keeps the form responsive by processing events
                        }
                        //if (exeProcess.HasExited)
                        //{
                        //    if (exeProcess.ExitCode == 0)
                        //    {
                        //        MessageBox.Show("Download Complete.", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //    }
                        //    else
                        //    {
                        //        MessageBox.Show("Download Failed.", "Status", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        //    }
                        //}
                        

                    }
                    catch
                    {
                        //MessageBox.Show("ERROR");
                    }
                }
            }
        }

        void exeProcess_ExitedHandler(object sender, EventArgs e)
        {
            
            if (exeProcess.ExitCode == 0)
            {
                prgrsbr.BeginInvoke(new Action(() =>    
                {                                      
                    prgrsbr.Value = 100;         //yes this is cheating, but who cares if it works lol!
                }                                  
                ));   
                MessageBox.Show("Download Complete.", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //hide the progressbar
                //prgrsbr.Visible = false; //this will not work as this handler is in another thread and progress bar is in ui thread.
                prgrsbr.BeginInvoke(new Action(() =>    //All
                {                                      //of
                    prgrsbr.Visible = false;          //these
                }                                    //lines
                ));                                 //are required to just hide the progress bar!
            }
            else
            {
                MessageBox.Show("Download Failed.", "Status", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                //hide the progressbar
                //prgrsbr.Visible = false; //this will not work as this handler is in another thread and progress bar is in ui thread.
                prgrsbr.BeginInvoke(new Action(() =>    //All
                    {                                  //of
                        prgrsbr.Visible = false;      //these
                    }                                //lines
                ));                                 //are required to just hide the progress bar!
            }
        }
        void exeProcess_OutDataReceivedHandler(object sender, DataReceivedEventArgs e)
        {
            if (txtStatus.InvokeRequired)
            {
                txtStatus.BeginInvoke(new DataReceivedEventHandler(exeProcess_OutDataReceivedHandler), new[] { sender, e }); //invoke event handler. This is required because txtStatus is in UI thread and the process which invokes it is in another.
            }
            else
            {
                output = Environment.NewLine + e.Data; //add a 'new line' to the status data from the process
                txtStatus.AppendText(output); //append the status data to the textbox

                foreach (Match match in Regex.Matches(output, @"[\.\d]+(?=%)")) //find the percentage data in the output
                {

                    //prgrsbr.PerformStep(); //lol. This is not how you do this, is it? But it works!
                    prgrsbr.Value = (int)Convert.ToDecimal(match.Value);//now this, is more like it.
                }
            }
        }
        private void txtfilename_TextChanged(object sender, EventArgs e)
        {
            lblFileSpChar.Visible = true;
            radCustomFileName.Checked = true;
        }

        private void radYTtitle_CheckedChanged(object sender, EventArgs e)
        {
            lblFileSpChar.Visible = false;
        }

        private void radCustomFileName_CheckedChanged(object sender, EventArgs e)
        {
            lblFileSpChar.Visible = true;
        }

        private void chkDefLoc_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDefLoc.Checked == true&&txtdir.Text!=null)
            {
                Settings.Default.CustomPath = txtdir.Text;
                Settings.Default.Save();    //save the default file location setting
            }
        }

        private void txtdir_TextChanged(object sender, EventArgs e)
        {
            chkDefLoc.Checked = false;
        }

        private void chkPlaylst_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPlaylst.Checked == true)
            {
                lblPLend.Enabled = true;
                txtPLend.Enabled = true;
                lblPLstart.Enabled = true;
                txtPLstart.Enabled = true;
                radCustomFileName.Enabled = false;
                txtfilename.Enabled = false;
            }
            else
            {
                radCustomFileName.Enabled = true;
                txtfilename.Enabled = true;
                lblPLend.Enabled = false;
                txtPLend.Enabled = false;
                lblPLstart.Enabled = false;
                txtPLstart.Enabled = false;
            }
        }
        public void validate(String url)
        {

        }

        private void linkgit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.github.com/rnand/");
        }

        private void btnHideSt_Click(object sender, EventArgs e)
        {
            if (txtStatus.Visible == false)
            {
                txtStatus.Visible = true;
                btnHideSt.Text = "Hide Status";
            }
            else
            {
                txtStatus.Visible = false;
                btnHideSt.Text = "Show Status";
            }
            if (this.Height == 470)
            {
                this.Height = 530;
            }
            else
            {
                this.Height = 470;
            }
        }

        private void btnRld_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                Regex isUrl = new Regex("^https?://|^www"); //if it begins with http:// or https:// or www
                Match URLtest = isUrl.Match(Clipboard.GetText());
                if (URLtest.Success) //copy content from clipboard only if it begins with http:// or https://
                {
                    txtURL.Text = Clipboard.GetText();
                }


            }
        }
    }
   

}
