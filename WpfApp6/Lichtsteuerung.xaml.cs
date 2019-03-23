using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.IO.Ports;
using System.IO;
using System.ComponentModel;
using Microsoft.Win32;
using forms = System.Windows.Forms; 




namespace Lichtsteuerung
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class Lichtsteuerung : Window
    {

        
        


        private System.Windows.Forms.ColorDialog colorDialog1;

        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenu contextMenu;




        private SerialPort sP;
        List<System.Windows.Forms.MenuItem> menuItems = new List<System.Windows.Forms.MenuItem>();

        




        private System.ComponentModel.IContainer components;

        public Lichtsteuerung()
        {
            InitializeComponent();


            //stream reader.
            try
            {
                using (StreamReader sr = new StreamReader("Settings.txt"))
                {

                    String line = sr.ReadLine();
                    Console.WriteLine(line);
                }
            }
            catch (Exception e) { MessageBox.Show(e.Message); }

            // SerialPort
            try
            {
                sP = new SerialPort { PortName = "COM5", BaudRate = 9600, ReadTimeout = 500, WriteTimeout = 500 };
                sP.Open();
            }
            catch (Exception e) { MessageBox.Show(e.Message); }

            this.colorDialog1 = new System.Windows.Forms.ColorDialog();



            this.StateChanged += new EventHandler(HideMinimized);


            components = new System.ComponentModel.Container();
            contextMenu = new System.Windows.Forms.ContextMenu();

            notifyIcon = new System.Windows.Forms.NotifyIcon(components);
            notifyIcon.Icon = new System.Drawing.Icon("Icon.ico");
            notifyIcon.Text = "Lichtsteuerung";
            notifyIcon.Visible = true;

            int presetCount = 0;




            //Add menuItems 
            {

                menuItems.Add(new System.Windows.Forms.MenuItem { Index = 0, Text = "Exit" });//0
                menuItems.Add(new System.Windows.Forms.MenuItem { Index = 0, Text = "Main" });//1
                menuItems.Add(new System.Windows.Forms.MenuItem { Index = 0, Text = "OFF" });//2
                menuItems.Add(new System.Windows.Forms.MenuItem { Index = 0, Text = "White" });//3
                menuItems.Add(new System.Windows.Forms.MenuItem { Index = 0, Text = "Red" });//4
                menuItems.Add(new System.Windows.Forms.MenuItem { Index = 0, Text = "Green" });//5
                menuItems.Add(new System.Windows.Forms.MenuItem { Index = 0, Text = "Blue" });//6
                menuItems.Add(new System.Windows.Forms.MenuItem { Index = 0, Text = "Rainbow" });//7

                for (int i = 0; i < presetCount; i++)
                {
                    menuItems.Add(new System.Windows.Forms.MenuItem { Index = 0, Text = "" });
                }




            }




            // Initialize contextMenu1

            for (int i = menuItems.Count() - 1; i >= 0; i--)
            {
                contextMenu.MenuItems.Add(menuItems[i]);
            }


            notifyIcon.ContextMenu = contextMenu;


            notifyIcon.DoubleClick += new System.EventHandler(this.GotoMain);


            menuItems[0].Click += new EventHandler(Exit);       //Exit
            menuItems[1].Click += new EventHandler(GotoMain);   //Main
            menuItems[2].Click += new EventHandler(GotoSettings);   //Settings
            menuItems[3].Click += (Sender, e) => SetColor(0,0,0,0);   //OFF
            menuItems[4].Click += (Sender, e) => SetColor(0,0,0,255); //WHITE
            menuItems[5].Click += (Sender, e) => SetColor(255,0,0,0);   //RED
            menuItems[6].Click += (Sender, e) => SetColor(0,255,0,0); //GREEN
            menuItems[7].Click += (Sender, e) => SetColor(0,0,255,0);  // BLUE
                                                                       //menuItems[7].Click += (sender, e) => colorDialog1;

            //List<forms.MenuItem> mt = new List<forms.MenuItem>();
            //mt.Add(new forms.MenuItems);
            //mt[1].Container = Brushes.Aqua;
            //mt[0].Click += (sender, e) => { Exit(sender,e);};

            //for (int i = 0; i < 10; i++)
            //{
            //    forms.Button btn = new forms.Button();
            //    btn.Text = i.ToString();

            //    forms.ContextMenu ct = new forms.ContextMenu();
            //    for (int j = 0; j < menuItems.Count(); j++)
            //    {
            //        ct.MenuItems.Add(menuItems[j]);
            //    }
            //    button1.ContextMenu = ct;
            //    ButtonPanel.Children.Add(btn);
            //}



            int rows = 5, columns = 5;

            for (int i = 0; i < columns; i++) mainBtnGrid.ColumnDefinitions.Add(new ColumnDefinition());
            for (int i = 0; i < rows; i++)mainBtnGrid.RowDefinitions.Add(new RowDefinition());

            List<Button> buttonList = new List<Button>();



            for(int i = 0;i<buttonList.Count();i++)
            {
                Button btn = new Button();
                // btn.Content =
                // btn.Background
                
            }




        }

        






        


        private void GotoMain(object Sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
            if (this.WindowState == WindowState.Minimized)
                this.WindowState = WindowState.Normal;
            this.Activate();
        }
        private void GotoSettings(object Sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 2;
            if (this.WindowState == WindowState.Minimized)
                this.WindowState = WindowState.Normal;
            this.Activate();
        }


        private void Exit(object Sender, EventArgs e)
        {
            // Close the form, which closes the application.
            this.Close();
        }

        private void HideMinimized(object Sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Minimized)
                this.ShowInTaskbar = false;
            else this.ShowInTaskbar = true;

        }

        private void SetColor(int r, int g, int b, int w) //Setcolor("")
        {
            try
            {       
                    sP.WriteLine("set_all_" + r.ToString() + "," + g.ToString() + "," + b.ToString() + "," + w.ToString());            
            }
            catch (Exception e) { MessageBox.Show(e.Message); }
        }




        

        private void SetBrightness(int a) //SetBrigthness("")
        {
            try
            {
                sP.WriteLine("set_brightness_" + a.ToString());
            }
            catch (Exception e) { MessageBox.Show(e.Message); }
        }

        private void SetMode(int mode) //SetBrigthness("")
        {
            mode = 0;
            try
            {
                sP.WriteLine("set_mode_" + mode.ToString());
            }
            catch (Exception e) { MessageBox.Show(e.Message); }
        }



        



        private void Button1_Click(object sender, System.EventArgs e)
        {

            //if (colorDialog1.ShowDialog() == DialogResult)
            {
                colorDialog1.ShowDialog();
                Button btn = sender as Button;
                btn.Background = new SolidColorBrush(Color.FromArgb(255, (byte)colorDialog1.Color.R, (byte)colorDialog1.Color.G, (byte)colorDialog1.Color.B));

            }
        }



        public class preset
        {
            string name;
            static int length, speed, ticks, ledAmount;  // ADD More........


            int[,,] rgbval = new int[ticks,ledAmount,4]; // [tick][LED_Number][R,G,B,Brightness]




        }

}

    
}
