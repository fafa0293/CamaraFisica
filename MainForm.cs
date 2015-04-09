/*
 * Created by SharpDevelop.
 * User: Fabian
 * Date: 18/03/2015
 * Time: 06:46 p.m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using AForge;
using AForge.Video;
using AForge.Video.DirectShow;

namespace CameraPrueba
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		void PictureBox2Click(object sender, EventArgs e)
		{
	
		}
		
		private FilterInfoCollection devices;
		private VideoCaptureDevice img;
		private string ruta = "C:\\Documents and Settings\\Fabian\\Documents\\ImgFisica2";
		
		void MainFormLoad(object sender, EventArgs e)
		{
			devices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
			foreach (FilterInfo device in devices) {
				comboBox1.Items.Add(device.Name);
			}
			comboBox1.SelectedIndex = 0;
			img = new VideoCaptureDevice();
		}
		
		
		void imagen_newframe(object sender, NewFrameEventArgs eventArgs){
			pictureBox1.Image =(Bitmap)eventArgs.Frame.Clone();
		}
		void Button1Click(object sender, EventArgs e)
		{
				try {
					img = new VideoCaptureDevice(devices[comboBox1.SelectedIndex].MonikerString);
					img.NewFrame+=new NewFrameEventHandler(imagen_newframe);
					img.Start();
				} catch (Exception) {
					MessageBox.Show("Seleccione una camara válida","Aviso",MessageBoxButtons.OK,MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
					comboBox1.SelectedIndex = 0;
				}
				
		}
		void Button2Click(object sender, EventArgs e)
		{
			try {
				pictureBox2.Image = (Bitmap)pictureBox1.Image.Clone();
				guardarimagen();
			} catch (Exception) {
				
				MessageBox.Show("Active la cámara","Aviso",MessageBoxButtons.OK,MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
				
			}
				
			
		}
		
		void guardarimagen(){
			String nombre = ruta+ "\\"+DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")+".JPG";
			pictureBox2.Image.Save(nombre);
		}
		void MainFormFormClosing(object sender, FormClosingEventArgs e)
		{
			if (img.IsRunning==true){
				img.Stop();
			}
		}
		void Button3Click(object sender, EventArgs e)
		{
			if (img.IsRunning==true){
				img.Stop();
				pictureBox1.Image = null;
			}
		}	
	}
}
