using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Web;
using System.Net;
using System.Text.RegularExpressions;
using Microsoft.WindowsAPICodePack.Taskbar;
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
        private bool userCancel = false;
        private bool userPause = false;
        private string ftype;
        private string fdir;
        private string fname;
        
        private void MainForm_Load(object sender, EventArgs e)
        {
            //radYTtitle.Checked = true;
            getClipboardData(); //get the url
                        
            txtdir.Text = Settings.Default.CustomPath; //save the default path to this variable
            if (Settings.Default.CustomPath != "") //check whether there is a default location already set or not
            {
                chkDefLoc.Checked = true;
                
                //MessageBox.Show("OKAY");
            }
   
        }

        private void btnbrowse_Click(object sender, EventArgs e) //browse for a location/dir
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
            txtdir.Text = fbd.SelectedPath;
            
        }

        private void btndwnld_Click(object sender, EventArgs e)
        {
            
            prgrsbr.Value = 0;//reset the progress bar
            txtStatus.Clear();//clear the status box
            if (txtURL.Text == "" ||  txtdir.Text == "")
            {
                MessageBox.Show("Enter the required values.", "Values needed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (chkPlaylst.Checked == true)  
            {
                string qlty;
                if (rdbmaxqlty.Checked)
                {
                    qlty = "best"; //default quality
                    ftype = "mp4";
                }
                else
                {
                    qlty = SetQuality();
                }
                Download(qlty,dType:"pl");
            }
            else if (rdbmp3.Checked)
            {
                ftype = "mp3";
                Download("best",dType:"aud");
            }
            else
            {
                string qlty;
                if (rdbmaxqlty.Checked)
                {
                    qlty = "best"; //default quality
                    ftype = "mp4";
                }
                else
                {
                    qlty = SetQuality();
                }
                Download(qlty, dType: "vid");
            }
        }

        void exeProcess_ExitedHandler(object sender, EventArgs e)
        {
            exeProcess.CancelOutputRead();
            exeProcess.CancelErrorRead();
            if (exeProcess.ExitCode == 0 && userCancel==false)
            {
                                
                MessageBox.Show("Download Complete.", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress);
                //hide the progressbar
                prgrsbr.BeginInvoke(new Action(() =>    //All
                {                                      //of
                    prgrsbr.Visible = false;          //these
                }                                    //lines
                ));                                 //are required to just hide the progress bar!
                btnCancel.BeginInvoke(new Action(() =>
                {
                    btnCancel.Visible = false;
                }
                ));
                btnPause.Invoke((MethodInvoker)delegate
                {
                    btnPause.Visible=false;
                }
                );
            }
            else if(userCancel==true)
            {
                txtStatus.BeginInvoke(new Action(() =>
                    {
                        txtStatus.AppendText("Download cancelled.");
                    }
                ));
                prgrsbr.BeginInvoke(new Action(() =>    //All
                {                                      //of
                    prgrsbr.Visible = false;          //these
                }                                    //lines
                ));                                 //are required to just hide the progress bar!
                TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Paused);
                DialogResult result = MessageBox.Show("Do you want to delete the part downloaded so far?", "Delete file conformation", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    
                    if (File.Exists(Path.Combine(fdir, fname + "." + ftype + ".part")))
                    {
                        File.Delete(Path.Combine(fdir, fname+ "."+ ftype+ ".part")); 
                        txtStatus.BeginInvoke(new Action(() =>
                        {
                            txtStatus.AppendText(Environment.NewLine + "File deleted:" + Path.Combine(fdir, fname + "." + ftype + ".part"));
                        }
                ));
                    }
                }
                TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress);
            }
            else if (userPause == true)
            {
                txtStatus.Invoke((MethodInvoker)delegate
                {
                    txtStatus.Text="Download paused.";
                    
                });
                TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Paused);
            }
            else
            {
                TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Error);
                if (File.Exists(Path.Combine(fdir, fname + "." + ftype + ".part")))
                {
                    MessageBox.Show("Download Failed.\nHowever, the part downloaded so far has been saved so that you can resume it later; just use the same URL and quality and the download will resume.", "Status", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show("Download Failed.", "Status", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                

                //hide the progressbar
                //prgrsbr.Visible = false; //this will not work as this handler is in another thread and progress bar is in ui thread.
                prgrsbr.BeginInvoke(new Action(() =>    //All
                    {                                  //of
                        prgrsbr.Visible = false;      //these
                    }                                //lines
                ));                                 //are required to just hide the progress bar!

                btnCancel.BeginInvoke(new Action(() =>
                    {
                        btnCancel.Visible = false;
                    }
                ));
                btnPause.Invoke((MethodInvoker)delegate
                {
                    btnPause.Visible = false;
                }
                );
                TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress);
            }
            userCancel = false;
        }
        void exeProcess_OutDataReceivedHandler(object sender, DataReceivedEventArgs e) //handle output data coming from the command line process being run
        {
            if (txtStatus.InvokeRequired)  //this is required because the UI thread which contains the textbox is seperate from the process thread
            {
                txtStatus.BeginInvoke(new DataReceivedEventHandler(exeProcess_OutDataReceivedHandler), new[] { sender, e }); //invoke event handler. This is required because txtStatus is in UI thread and the process which invokes it is in another.
            }
            else
            {
                output = Environment.NewLine + e.Data; //add a 'new line' to the status data from the process
                txtStatus.Text=output; //append the status data to the textbox
                TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Normal);
                foreach (Match match in Regex.Matches(output, @"[\.\d]+(?=%)")) //find the 'percentage data' in the output
                {

                    //prgrsbr.PerformStep(); //lol. This is not how you do this, is it? But it works!
                    prgrsbr.Value = (int)(Convert.ToDecimal(match.Value)*10);//now this, is more like it. we need to convert the decimal value to int
                    TaskbarManager.Instance.SetProgressValue(prgrsbr.Value, 1000);
                }
            }
        }
        private void txtfilename_TextChanged(object sender, EventArgs e)
        {
            //lblFileSpChar.Visible = true;
            //radCustomFileName.Checked = true;
        }

        private void radYTtitle_CheckedChanged(object sender, EventArgs e)
        {
            //lblFileSpChar.Visible = false;
        }

        private void radCustomFileName_CheckedChanged(object sender, EventArgs e)
        {
            //lblFileSpChar.Visible = true;
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
                //radCustomFileName.Enabled = false;
                txtfilename.Enabled = false;
            }
            else
            {
                //radCustomFileName.Enabled = true;
                txtfilename.Enabled = true;
                lblPLend.Enabled = false;
                txtPLend.Enabled = false;
                lblPLstart.Enabled = false;
                txtPLstart.Enabled = false;
            }
        }
        public void validate(String url)
        {
            //to implement
        }

        private void linkgit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.github.com/rnand/");
        }

        private void btnHideSt_Click(object sender, EventArgs e) //show/hide the status box
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
            if (this.Height == 450) //resize the form
            {
                this.Height = 490;
            }
            else
            {
                this.Height = 450;
            }
        }

        private void btnRld_Click(object sender, EventArgs e)
        {
            getClipboardData();
            
        }
        private void getClipboardData()
        {

            if (Clipboard.ContainsText())
            {
                Regex isUrl = new Regex("^https?://|^www"); //if it begins with http:// or https:// or www
                Match URLtest = isUrl.Match(Clipboard.GetText());
                if (URLtest.Success) //copy content from clipboard only if it begins with http:// or https:// or www
                {
                    txtURL.Text = Clipboard.GetText();
                }

            }
        }

        private void txtURL_TextChanged(object sender, EventArgs e)
        {
            var urlData = new URLData() { URL = txtURL.Text, title = txtfilename.Text }; //this object is used to send info to the backgroundworker thread
            YTtitlebackgroundWorker.WorkerSupportsCancellation = true;
            if (!YTtitlebackgroundWorker.IsBusy)
            {
                YTtitlebackgroundWorker.RunWorkerAsync(urlData);
            }
            else
            {
                YTtitlebackgroundWorker.CancelAsync();
                YTtitlebackgroundWorker.RunWorkerAsync(urlData);
            }
        }

        private string GetTitle(string URL)//string site
        {
            using (WebClient client = new WebClient())
            {
                try
                {
                    string content = client.DownloadString(URL);
                    var pattern = @"<meta.*property=""og:title"".*content=""(.*)"".*>"; //pattern to match
                    Match patternMatch = Regex.Match(content, pattern);
                    var matchedPart = patternMatch.Groups[1];
                    return matchedPart.Value;
                }
                catch (Exception)
                {
                    MessageBox.Show("Unable to retrieve title. Check your network connection.", "Title error");
                }
                
            
            }
            return "";
        }

        private void YTtitlebackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var urldata = (URLData)e.Argument;
            lblRetrv.Invoke((MethodInvoker)delegate
            {
                lblRetrv.Visible = true;
            });
            txtfilename.Invoke((MethodInvoker)delegate
            {
                txtfilename.Clear();
            });
            urldata.title = GetTitle(urldata.URL);
            e.Result = urldata;
        }

        private void YTtitlebackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var urltitlereturn = (URLData)e.Result;
            lblRetrv.Visible = false;
            if (urltitlereturn.title == null)
            {
                txtfilename.Text = "Unknown video"; //set this if background worker was unable to retrieve the actual title
            }
            else
            {
                txtfilename.Text = SanitizeTitle(urltitlereturn.title); //filter illegal characters form title/filename
            }
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (!exeProcess.HasExited)
            {
                userCancel = true;

                try
                {
                    exeProcess.Kill();
                }
                catch (Exception excep)
                {
                    MessageBox.Show(excep.ToString());
                }
                btnCancel.Visible = false;
                btnPause.Visible = false;
            }

            
        }

        private string SanitizeTitle(string str)
        {
            string saneTitle = Regex.Replace(str, @"[^\w\'&.@-]", " "); //allow only words, letters, single quote, ampersand, period, at symbol, and a dash
            return saneTitle;
        }

        private void Download(string qlty,string dType) 
        {
            btnCancel.Visible = true;
            btnPause.Visible = true;
            this.Height = 490; //resize the form   
            txtStatus.Visible = true;//show the status text box
            btnHideSt.Visible = true;//show the 'hide status' button
            prgrsbr.Visible = true;//show the progressbar
            string url = txtURL.Text;
            fdir = txtdir.Text; //file dir
            fname = txtfilename.Text; //the file name
            string ex1 = Path.Combine(Path.GetTempPath(), "youtube-dl.exe");
            try
            {
                File.WriteAllBytes(ex1, YouTube_downloader.Properties.Resources.youtube_dl);
            }
            catch (Exception resEx)
            {
                MessageBox.Show("Resource write error\n"+resEx.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            exeProcess.StartInfo.RedirectStandardOutput = true;
            exeProcess.StartInfo.RedirectStandardError = true;
            exeProcess.StartInfo.CreateNoWindow = true;
            exeProcess.StartInfo.UseShellExecute = false;
            exeProcess.EnableRaisingEvents = true;
            exeProcess.StartInfo.FileName = ex1;
            if (dType=="pl")
            {
                if (txtPLstart.Text == "" && txtPLend.Text == "")
                {
                    exeProcess.StartInfo.Arguments = " -o " + "\"" + fdir + "\\" + "%(title)s" + "." + ftype + "\"" + " " + url + " -f " + qlty;
                }
                else
                {
                    string strtNum = txtPLstart.Text;
                    string endNum = txtPLend.Text;
                    exeProcess.StartInfo.Arguments = " -o " + "\"" + fdir + "\\" + "%(title)s" + "." + ftype + "\"" + " " + url + " -f " + qlty + " --playlist-start " + strtNum + " --playlist-end " + endNum;//ftype; +" ";
                }
            }
            else if(dType=="vid")
            {
                exeProcess.StartInfo.Arguments = " -o " + "\"" + fdir + "\\" + fname + "." + ftype + "\"" + " " + url + " -f " + qlty;//ftype; +" ";
            }
            else if (dType == "aud")
            {
                exeProcess.StartInfo.Arguments = "--extract-audio --audio-format mp3 --audio-quality 0"+ " -o " + "\"" + fdir + "\\" + fname + "." + ftype + "\"" + " " + url  ; //audio quality, insert a value between 0 (better) and 9 (worse) for VBR
                                                                                                                                                              //audio format: "best", "aac", "vorbis", "mp3", "m4a", "opus", or "wav"; "best" by default
            }

            exeProcess.OutputDataReceived -= exeProcess_OutDataReceivedHandler; //remove event handler if already exists
            exeProcess.OutputDataReceived += exeProcess_OutDataReceivedHandler; // generate event handlers 
            exeProcess.ErrorDataReceived -= exeProcess_OutDataReceivedHandler;  //remove event handler if already exists
            exeProcess.ErrorDataReceived += exeProcess_OutDataReceivedHandler; // generate event handlers
            exeProcess.Exited -= new EventHandler(exeProcess_ExitedHandler);  //remove event handler if already exists
            exeProcess.Exited += new EventHandler(exeProcess_ExitedHandler); //handle process exit

            try
            {
                // Start the process with the info we specified.
                exeProcess.Start();
                exeProcess.BeginOutputReadLine(); //need to call this method to begin the event handling and generation from the process being run
                exeProcess.BeginErrorReadLine();
                while (!exeProcess.HasExited) // Calling WaitForExit() will suspend the UI thread. So don't do that. Instead do this.
                {
                    Application.DoEvents(); // This keeps the form responsive by processing events
                }
            }
            catch (Exception runEx)
            {
                MessageBox.Show("Unable to run the resource.\n"+runEx.ToString(),"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private string SetQuality()
        {
            string qlty = "";
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
            return qlty;
        }


        private void btnPause_Click(object sender, EventArgs e)
        {
            if (!exeProcess.HasExited && btnPause.Text =="Pause")
            {
                userPause = true;

                try
                {
                    exeProcess.Kill(); //we kill the process here instead of suspending the thread because this is more efficient and easier
                }
                catch (Exception excep)
                {
                    MessageBox.Show(excep.ToString());
                }
                btnPause.Text = "Resume";
            }
            else if (btnPause.Text == "Resume")
            {
                userPause = false;
                try
                {
                    btndwnld_Click(sender, e); //call the click event of the download button
                }
                catch (Exception excep)
                {
                    MessageBox.Show(excep.ToString());
                }
                btnPause.Text = "Pause";
            }
        }
    }
    
    class URLData //this is for transferring values to and from the UI thread and BackgroundWorker thread
    {
        public string URL;
        public string title;
        //public string site;
    }
   

}
