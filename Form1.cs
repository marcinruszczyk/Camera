using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DarrenLee.Media;


namespace CameraForms
{
    public partial class Form1 : Form
    {

        int count = 0;
        Camera myCamera = new Camera();


        public Form1()
        {
            InitializeComponent();
            GetInfo();
            myCamera.OnFrameArrived += MyCamera_OnFrameArrived;

        }

        private void GetInfo()
        {
            var cameraDevices = myCamera.GetCameraSources();
            var cameraResolutions = myCamera.GetSupportedResolutions();



            foreach (var d in cameraDevices)
                cmbCameraDevices.Items.Add(d);


            foreach (var r in cameraResolutions)
                cmbCameraResolution.Items.Add(r);
            
                cmbCameraResolution.SelectedIndex = 0;
                cmbCameraDevices.SelectedIndex = 0;
          }



        private void MyCamera_OnFrameArrived(object source, FrameArrivedEventArgs e)
        {
            Image img = e.GetFrame();
            picCamera.Image = img;

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void cmbCameraDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            myCamera.ChangeCamera(cmbCameraDevices.SelectedIndex);
        }

        private void cmbCameraResolution_SelectedIndexChanged(object sender, EventArgs e)
        {
            myCamera.Start(cmbCameraResolution.SelectedIndex);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            myCamera.Stop();
        }

        private void savebtn_Click(object sender, EventArgs e)
        {
            string filename = Application.StartupPath + @"\" + "Image" + count.ToString();
            myCamera.Capture(filename);
            count++;
        }
    }
}
