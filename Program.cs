using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Sticky_Notes
{
    public delegate void StickyEventHandler(object obj);
    public delegate void DataEventHandler(string file, Point location, Size size);

    class Program : Form
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Program());
        }

        public Program()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.loadText = new Label();

            //
            // Program Form
            //
            this.ClientSize = new Size(260, 130);
            this.BackColor = SystemColors.ControlLightLight;
            this.ShowIcon = false;
            this.Opacity = 0;
            this.Controls.Add(this.loadText);
            this.Load += new EventHandler(this.Program_Load);
            this.Resize += new EventHandler(this.Program_Resize);
            //
            // Load Label
            //
            this.loadText.Dock = DockStyle.Fill;
            this.loadText.TextAlign = ContentAlignment.MiddleCenter;
            this.loadText.Font = new Font("굴림", 10F);
            this.loadText.Text = "스티커 메모";
        }

        Label loadText;

        private void Data_Write()
        {
            StreamWriter sw = new StreamWriter(datafile, false);
            for (int index = 0; index < sticky.Count; index++)
            {
                sw.WriteLine(filename[index]);
                sw.WriteLine(datalocation[index].X);
                sw.WriteLine(datalocation[index].Y);
                sw.WriteLine(datasize[index].Width);
                sw.WriteLine(datasize[index].Height);
            }
            sw.Dispose();
            sw.Close();
        }

        private void Sticky_Count(object index)
        {
            StickyAdd((int)index);
        }

        private void Sticky_Clear(object name)
        {
            count--;
            Sticky find = sticky.Find(s => s.Name == name.ToString());
            for (int list = 0; list < sticky.Count; list++)
            {
                if (filename[list] == find.Name)
                {
                    filename.RemoveAt(list);
                    datalocation.RemoveAt(list);
                    datasize.RemoveAt(list);
                    break;
                }
            }
            sticky.Remove(find);
            Data_Write();
            if (count == 0) { this.Close(); }
        }

        private void Data_Save(string file, Point location, Size size)
        {
            for (int list = 0; list < sticky.Count; list++)
            {
                if (file == filename[list])
                {
                    datalocation[list] = location;
                    datasize[list] = size;
                }
            }
            Data_Write();
        }

        private void StickyAdd(int index)
        {
            string name = string.Empty;
            Point point = new Point(0, 0);
            Size size = new Size(311, 304);

            count += index;
            for (int number = sticky.Count; number < count; number++)
            {
                if (number < notefile.Count)
                {
                    name = notefile[number];

                    for (int list = 0; list < count; list++)
                    {
                        if (name == filename[list])
                        {
                            point = datalocation[list];
                            size = datasize[list];
                            break;
                        }
                    }
                }
                else
                {
                    int stack = number;
                    string newfile;
                    do
                    {
                        newfile = path + "note(" + stack++ + ").rtf";
                    } while (File.Exists(newfile));
                    name = newfile;
                    filename.Add(name);
                    datalocation.Add(point);
                    datasize.Add(size);
                }
                sticky.Add(new Sticky
                {
                    Owner = this,
                    Name = name,
                    StartPosition = FormStartPosition.Manual,
                    Location = point,
                    Size = size
                });
                sticky[number].CountEvent += new StickyEventHandler(this.Sticky_Count);
                sticky[number].ClearEvent += new StickyEventHandler(this.Sticky_Clear);
                sticky[number].SaveEvent += new DataEventHandler(this.Data_Save);
                sticky[number].Show();
            }
        }

        private void Data_Read()
        {
            FileStream fs = new FileStream(datafile, FileMode.OpenOrCreate, FileAccess.Read);
            fs.Dispose();
            fs.Close();

            StreamReader sr = new StreamReader(datafile);
            while (!sr.EndOfStream)
            {
                filename.Add(sr.ReadLine());
                int x = int.Parse(sr.ReadLine());
                int y = int.Parse(sr.ReadLine());
                int width = int.Parse(sr.ReadLine());
                int height = int.Parse(sr.ReadLine());
                datalocation.Add(new Point(x, y));
                datasize.Add(new Size(width, height));
            }
            sr.Dispose();
            sr.Close();
        }

        private void Data_Load()
        {
            datafile = path + "data.txt";
            filename = new List<string>();
            datalocation = new List<Point>();
            datasize = new List<Size>();
            Data_Read();
        }

        string datafile;
        List<string> filename;
        List<Point> datalocation;
        List<Size> datasize;

        private void File_Load()
        {
            notefile = new List<string>();
            DirectoryInfo directory = new DirectoryInfo(path);

            if (directory.Exists == false)
            {
                directory.Create();
            }
            foreach (var note in directory.GetFiles("*.rtf"))
            {
                if (note.Name == "data.txt") { continue; }
                notefile.Add(note.FullName);
            }
            Data_Load();
            StickyAdd((notefile.Count == 0) ? 1 : notefile.Count);
        }

        private readonly string path = Environment.CurrentDirectory + @"\StickyNotes\";
        List<string> notefile;

        private void Program_Load(object sender, EventArgs e)
        {
            // Sticky Note Count
            count = 0;
            sticky = new List<Sticky>();
            File_Load();
        }

        List<Sticky> sticky;
        int count;

        private void Program_Resize(object sender, EventArgs e)
        {
            FormWindowState state = this.WindowState;
            int opacity = (state == FormWindowState.Minimized) ? 0 : 1;
            for (int index = 0; index < count; index++)
            {
                sticky[index].Opacity = opacity;
                if (opacity == 1)
                {
                    sticky[index].BringToFront();
                }
            }
        }
    }
}