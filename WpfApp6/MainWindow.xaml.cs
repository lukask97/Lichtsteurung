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
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenu contextMenu;

        private System.Windows.Forms.MenuItem menuItem0 = new System.Windows.Forms.MenuItem {Index = 0, Text = "Exit" };
        private System.Windows.Forms.MenuItem menuItem1 = new System.Windows.Forms.MenuItem {Index = 1, Text = "Main" };
        private System.Windows.Forms.MenuItem menuItem2 = new System.Windows.Forms.MenuItem {Index = 2, Text = "Settings" };
        private System.Windows.Forms.MenuItem menuItem3 = new System.Windows.Forms.MenuItem {Index = 3, Text = "Off" };
        private System.Windows.Forms.MenuItem menuItem4 = new System.Windows.Forms.MenuItem {Index = 0, Text = "White" };
        private System.Windows.Forms.MenuItem menuItem5 = new System.Windows.Forms.MenuItem {Index = 0, Text = "Red" };
        private System.Windows.Forms.MenuItem menuItem6 = new System.Windows.Forms.MenuItem {Index = 0, Text = "Green" };
        private System.Windows.Forms.MenuItem menuItem7 = new System.Windows.Forms.MenuItem {Index = 0, Text = "Blue" };
        private System.Windows.Forms.MenuItem menuItem8 = new System.Windows.Forms.MenuItem { Index = 0, Text = "Custom1" };
        private System.Windows.Forms.MenuItem menuItem9 = new System.Windows.Forms.MenuItem { Index = 0, Text = "Custom2" };
        private System.Windows.Forms.MenuItem menuItem10 = new System.Windows.Forms.MenuItem { Index = 0, Text = "Custom3" };



        


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
                SerialPort sP = new SerialPort { PortName = "COM5", BaudRate = 9600, ReadTimeout = 500, WriteTimeout = 500 };
                //sP.Open();
            }
            catch (Exception e) { MessageBox.Show(e.Message);}
            


            components = new System.ComponentModel.Container();
            contextMenu = new System.Windows.Forms.ContextMenu();

            notifyIcon = new System.Windows.Forms.NotifyIcon(components);
            notifyIcon.Icon = new System.Drawing.Icon("Icon.ico");
            notifyIcon.Text = "Lichtsteuerung";
            notifyIcon.Visible = true;

            int presetCount = 0;
            
            List<System.Windows.Forms.MenuItem> menuItems = new List<System.Windows.Forms.MenuItem>();


            //Add menuItems 
            {
                
                menuItems.Add(new System.Windows.Forms.MenuItem { Index = 0, Text = "Exit" });
                menuItems.Add(new System.Windows.Forms.MenuItem { Index = 0, Text = "Main" });
                menuItems.Add(new System.Windows.Forms.MenuItem { Index = 0, Text = "Settings" });
                menuItems.Add(new System.Windows.Forms.MenuItem { Index = 0, Text = "OFF" });
                menuItems.Add(new System.Windows.Forms.MenuItem { Index = 0, Text = "White" });
                menuItems.Add(new System.Windows.Forms.MenuItem { Index = 0, Text = "Red" });
                menuItems.Add(new System.Windows.Forms.MenuItem { Index = 0, Text = "Green" });
                menuItems.Add(new System.Windows.Forms.MenuItem { Index = 0, Text = "Blue" });

                for(int i = 0; i < presetCount;i++)
                {
                    menuItems.Add(new System.Windows.Forms.MenuItem { Index = 0, Text = "________________________________" });
                }

            }



            // Initialize contextMenu1
            contextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {menuItem0,menuItem1,menuItem2,menuItem3,menuItem4,menuItem5,menuItem6,menuItem7,menuItem8,menuItem9,menuItem10});
            notifyIcon.ContextMenu = contextMenu;
            contextMenu.MenuItems.Add(menuItems[0]);
          
            

            notifyIcon.DoubleClick += new System.EventHandler(this.GotoMain);
            menuItem0.Click += new EventHandler(Exit);
            menuItem1.Click += new EventHandler(GotoMain);
            menuItem2.Click += new EventHandler(GotoSettings);
            menuItem3.Click += new EventHandler(Exit);
            menuItem4.Click += new EventHandler(Exit);
            menuItem5.Click += new EventHandler(Exit);
            menuItem6.Click += new EventHandler(Exit);
            menuItem7.Click += new EventHandler(Exit);
            menuItem8.Click += new EventHandler(Exit);
            menuItem9.Click += new EventHandler(Exit);











            
        
        }
        

            private void GotoMain (object Sender, EventArgs e)
        {
            
            if (this.WindowState == WindowState.Minimized)
                this.WindowState = WindowState.Normal;

            // Activate the form.
            this.Activate();
        }
        private void GotoSettings(object Sender, EventArgs e)
        {
            menuItem2.Checked = true;
        }

        private void Exit(object Sender, EventArgs e)
        {
            // Close the form, which closes the application.
            this.Close();
        }

        












    }
}
