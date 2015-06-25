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

        private void Form1_Load(object sender, EventArgs e)
        {
            radYTtitle.Checked = true;
            //linkgit.Links.Add(6,4,"http://www.github.com/rnand/");
            if (Clipboard.ContainsText())
            {
                Regex isUrl = new Regex("^https?://"); //if it begins with http:// or https://
                Match URLtest = isUrl.Match(Clipboard.GetText());
                if (URLtest.Success) //copy content from clipboard only if it begins with http:// or https://
                {
                    txtURL.Text = Clipboard.GetText();
                }

                
            }
            
            txtdir.Text = Settings.Default.CustomPath;
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
            else if (chkPlaylst.Checked == true)
            {
                this.Height = 530; //resize the form
                //show the text box
                txtStatus.Visible = true;

                //show the 'hide status' button
                btnHideSt.Visible = true;
                
                
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

                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.CreateNoWindow = false;
                startInfo.UseShellExecute = false;
                startInfo.FileName = ex1;
                //startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                if (txtPLstart.Text=="" && txtPLend.Text=="")
                {
                    startInfo.Arguments = " -o " + "\"" + fdir + "\\" + "%(title)s" + "." + ftype + "\"" + " " + PLurl + " -f " + qlty;
                }
                else
                {
                    string strtNum = txtPLstart.Text;
                    string endNum = txtPLend.Text;
                    startInfo.Arguments = " -o " + "\"" + fdir + "\\" + "%(title)s" + "." + ftype + "\"" + " " + PLurl + " -f " + qlty + " --playlist-start " + strtNum + " --playlist-end " + endNum;//ftype; +" ";
                }

                try
                {
                    // Start the process with the info we specified.
                    // Call WaitForExit and then the using statement will close.
                    using (Process exeProcess = Process.Start(startInfo))
                    {

                        exeProcess.WaitForExit();
                        if (exeProcess.ExitCode == 0)
                        {
                            MessageBox.Show("Download Complete.", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Download Failed.", "Status", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
                catch
                {
                    // Log error.
                }
            }
            else
            {
                
                Regex linkParser = new Regex("^(?:https?\\:\\/\\/)?(?:www\\.)?(?:youtu\\.be\\/|youtube\\.com\\/(?:embed\\/|v\\/|watch\\?v\\=))([\\w-]{10,12})(?:[\\&\\?\\#].*?)*?(?:[\\&\\?\\#]t=([\\dhm]+s))?$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                string vURL = txtURL.Text;
                string vID;//stores video ID
                Match match = linkParser.Match(vURL);//match the pattern
                if (!match.Success)
                {
                    MessageBox.Show("The given URL is not a valid YouTube URL.", "Invalid URL", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    this.Height = 530;
                    if (txtStatus.Visible == false)
                    {
                        txtStatus.Visible = true;

                    }
                    else
                    {
                        txtStatus.Visible = false;
                    }
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

                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.CreateNoWindow = false;
                    startInfo.UseShellExecute = false;
                    startInfo.FileName = ex1;
                    //startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    if (radYTtitle.Checked)
                    {
                        startInfo.Arguments = " -o " + "\"" + fdir + "\\" + "%(title)s" + "." + ftype + "\"" + " " + yURL + " -f " + qlty;
                    }
                    else
                    {
                        startInfo.Arguments = " -o " + "\"" + fdir + "\\" + fname + "." + ftype + "\"" + " " + yURL + " -f " + qlty;//ftype; +" ";
                    }

                    try
                    {
                        // Start the process with the info we specified.
                        // Call WaitForExit and then the using statement will close.
                        using (Process exeProcess = Process.Start(startInfo))
                        {
                        
                            exeProcess.WaitForExit();
                            if (exeProcess.ExitCode == 0)
                            {
                                MessageBox.Show("Download Complete.", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Download Failed.", "Status", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                        }
                    }
                    catch
                    {
                        // Log error.
                    }
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
                Settings.Default.Save();
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
    }
   

}
