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



namespace WpfApp6
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        

        private System.Windows.Forms.ColorDialog colorDialog1;

        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenu contextMenu;

        //private System.Windows.Forms.MenuItem menuItem0 = new System.Windows.Forms.MenuItem {Index = 0, Text = "Exit" };
        //private System.Windows.Forms.MenuItem menuItem1 = new System.Windows.Forms.MenuItem {Index = 1, Text = "Main" };
        //private System.Windows.Forms.MenuItem menuItem2 = new System.Windows.Forms.MenuItem {Index = 2, Text = "Settings" };
        //private System.Windows.Forms.MenuItem menuItem3 = new System.Windows.Forms.MenuItem {Index = 3, Text = "Off" };
        //private System.Windows.Forms.MenuItem menuItem4 = new System.Windows.Forms.MenuItem {Index = 0, Text = "White" };
        //private System.Windows.Forms.MenuItem menuItem5 = new System.Windows.Forms.MenuItem {Index = 0, Text = "Red" };
        //private System.Windows.Forms.MenuItem menuItem6 = new System.Windows.Forms.MenuItem {Index = 0, Text = "Green" };
        //private System.Windows.Forms.MenuItem menuItem7 = new System.Windows.Forms.MenuItem {Index = 0, Text = "Blue" };
        //private System.Windows.Forms.MenuItem menuItem8 = new System.Windows.Forms.MenuItem { Index = 0, Text = "Custom1" };
        //private System.Windows.Forms.MenuItem menuItem9 = new System.Windows.Forms.MenuItem { Index = 0, Text = "Custom2" };
        //private System.Windows.Forms.MenuItem menuItem10 = new System.Windows.Forms.MenuItem { Index = 0, Text = "Custom3" };



        private SerialPort sP;
        List<System.Windows.Forms.MenuItem> menuItems = new List<System.Windows.Forms.MenuItem>();





        private System.ComponentModel.IContainer components;

        public MainWindow()
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
            catch (Exception e) { MessageBox.Show(e.Message);}

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
                menuItems.Add(new System.Windows.Forms.MenuItem { Index = 0, Text = "Settings" });//2
                menuItems.Add(new System.Windows.Forms.MenuItem { Index = 0, Text = "OFF" });//3
                menuItems.Add(new System.Windows.Forms.MenuItem { Index = 0, Text = "White" });//4
                menuItems.Add(new System.Windows.Forms.MenuItem { Index = 0, Text = "Red" });//5
                menuItems.Add(new System.Windows.Forms.MenuItem { Index = 0, Text = "Green" });//6
                menuItems.Add(new System.Windows.Forms.MenuItem { Index = 0, Text = "Blue" });//7
                menuItems.Add(new System.Windows.Forms.MenuItem { Index = 0, Text = "RGB" });//7

                for (int i = 0; i < presetCount;i++)
                {
                    menuItems.Add(new System.Windows.Forms.MenuItem { Index = 0, Text = "" });
                }

            }


            

            // Initialize contextMenu1

            for (int i = menuItems.Count()-1; i >= 0; i--)
            {
                contextMenu.MenuItems.Add(menuItems[i]);
            }
            
            
            notifyIcon.ContextMenu = contextMenu;
    

            notifyIcon.DoubleClick += new System.EventHandler(this.GotoMain);


            menuItems[0].Click += new EventHandler(Exit);       //Exit
            menuItems[1].Click += new EventHandler(GotoMain);   //Main
            menuItems[2].Click += new EventHandler(GotoSettings);   //Settings
            menuItems[3].Click += (Sender, e) => SetColor(3,"off");   //OFF
            menuItems[4].Click += (Sender, e) => SetColor(4, "white"); //WHITE
            menuItems[5].Click += (Sender, e) => SetColor(5, "red");   //RED
            menuItems[6].Click += (Sender, e) => SetColor(6, "green"); //GREEN
            menuItems[7].Click += (Sender, e) => SetColor(7, "blue");  // BLUE
            //menuItems[7].Click += (sender, e) => colorDialog1;

            

        }
        

            private void GotoMain (object Sender, EventArgs e)
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

        private void SetColor(int menuobjectNR,string color) //Setcolor("")
        {
            try
            {
                if (color == "white")
                {
                    
                    sP.WriteLine("setwhite");
                    
                }
                if (color == "green")
                {
                    sP.WriteLine("set_all_255,000,000");
                }
                if (color == "red")
                {
                    sP.WriteLine("set_all_000,255,000");
                }
                if (color == "blue")
                {
                    sP.WriteLine("set_all_000,000,255");
                }
                if (color == "blue")
                {
                    sP.WriteLine("set_all_000,000,255");
                }
            }
            catch (Exception e) { MessageBox.Show(e.Message); }
        }


        //private void uncheckMenuItems()
        //{
        //    foreach(MenuItem menuItem in MenuItems)
        //}

        private void SetColor(int R,int G, int B) //Setcolor("")
        {
            try
            {
                sP.WriteLine("set_all_"+R.ToString()+"," + G.ToString() + "," + B.ToString());
            }
            catch (Exception e) { MessageBox.Show(e.Message); }
        }





        private void Button1_Click(object sender, System.EventArgs e)
        {
            
           // if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                colorDialog1.ShowDialog();
                //button1.Background = colorDialog1.Color;
                colorDialog1.Color.R;
            }
        }

        
    }
}
