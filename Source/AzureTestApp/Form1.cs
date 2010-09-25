using System;
using System.Threading;
using System.Windows.Forms;
using ThePhotoProject.Uploader;
using Timer = System.Threading.Timer;

namespace AzureTestApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Click event of the button1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void button1_Click(object sender, EventArgs e)
        {
           //DialogResult result = openFileDialog1.ShowDialog();

           // if(result == DialogResult.OK)
           // {
           //     byte[] bytes = openFileDialog1.OpenFile().ReadToEnd();

           //     IFileService service = new AzureFileService();
           //     service.CreateBucket("test");
           //    // service.AddFile("test", Path.GetFileName(openFileDialog1.FileName), bytes);
           // }

            MediaUploaderService mediaUploaderService = new MediaUploaderService();
            TimerCallback timerDelegate = mediaUploaderService.Run;
            Timer _timer = new Timer(timerDelegate, null, 0, 60000); //every minute
        }
    }
}
