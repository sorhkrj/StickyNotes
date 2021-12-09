using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;

namespace StickyMemos
{
    namespace Manager
    {
        public class DataManager
        {
            public DataManager()
            {
                this.path = AppDomain.CurrentDomain.BaseDirectory;
                directory = new DirectoryInfo(string.Format(@"{0}/Sticky Memos", this.path));
                this.file = new FileInfo(string.Format(@"{0}/data.txt", directory));
                this.list = new List<Data>();

                this.Load();
                this.Read();
            }

            private readonly string path;
            public static DirectoryInfo directory;
            private readonly FileInfo file;

            private static DataManager dataManager = null;
            public static DataManager GetData()
            {
                if (dataManager == null)
                {
                    dataManager = new DataManager();
                }
                return dataManager;
            }
            private List<Data> list;
            public List<Data> GetList
            {
                get { return list; }
            }

            private void Load()
            {
                if (!directory.Exists)
                {
                    directory.Create();
                }
                if (!this.file.Exists)
                {
                    using (FileStream fileStream = this.file.Create())
                    {
                        fileStream.Dispose();
                    }
                }
            }

            private void Read()
            {
                using (StreamReader streamReader = new StreamReader(this.file.FullName))
                {
                    while (!streamReader.EndOfStream)
                    {
                        string value = streamReader.ReadLine();

                        string[] item = value.Split(',');
                        int index = 0;
                        string name = item[index++];
                        if (!File.Exists(string.Format(@"{0}/{1}", directory, name))) { continue; }
                        string color = item[index++];
                        int x = int.Parse(item[index++]);
                        int y = int.Parse(item[index++]);
                        Point point = new Point(x, y);
                        int Width = int.Parse(item[index++]);
                        int Height = int.Parse(item[index++]);
                        Size size = new Size(Width, Height);

                        Data data = new Data(name, color, point, size);
                        this.list.Add(data);
                    }
                    streamReader.Dispose();
                }
            }

            private void Write()
            {
                using (StreamWriter streamWriter = new StreamWriter(this.file.FullName, false))
                {
                    for (int data = 0; data < this.list.Count; data++)
                    {
                        string name = this.list[data].name;
                        string color = this.list[data].color;
                        Point point = this.list[data].point ?? new Point(0, 0);
                        Size size = this.list[data].size;

                        streamWriter.Write(string.Format("{0},{1}", name, color));
                        streamWriter.Write(string.Format("{0},{1}", point.X, point.Y));
                        streamWriter.Write(string.Format("{0},{1}", size.Width, size.Height));
                        streamWriter.WriteLine();
                    }
                    streamWriter.Dispose();
                }
            }

            // 중복되지 않는 파일 이름 생성
            private string GetFileName()
            {
                string name = string.Empty;
                int stack = 0;
                do
                {
                    name = string.Format(@"{0}/note({1}).rtf", directory.FullName, stack++);
                } while (File.Exists(name));

                FileInfo file = new FileInfo(name);
                using (FileStream fileStream = file.Create())
                {
                    fileStream.Dispose();
                }

                return file.Name;
            }

            // 데이터 생성
            private Data Create(string color, Nullable<Point> point, Size size)
            {
                string name = GetFileName();
                return new Data(name, color, point, size);
            }

            // 데이터 추가
            public Data Add(string color, Nullable<Point> point, Size size)
            {
                Data data = this.Create(color, point, size);
                this.list.Add(data);
                this.Write();
                return data;
            }

            // 데이터 수정
            private void Update(Data update)
            {
                Data data = this.list.Find(find => find.name == update.name);
                int index = this.list.IndexOf(data);
                this.list[index] = update;
            }

            // 데이터 저장
            public void Save(Data update)
            {
                this.Update(update);
                this.Write();
            }

            // 데이터 삭제
            public void Remove(Data data)
            {
                FileInfo file = new FileInfo(string.Format(@"{0}/{1}", directory, data.name));
                file.Delete();

                this.list.Remove(data);
                this.Write();
            }
        }

        public class Data
        {
            public Data(string name, string color, Nullable<Point> point, Size size)
            {
                this.name = name;
                this.color = color;
                this.point = point;
                this.size = size;
            }

            public string name;
            public string color;
            public Point? point;
            public Size size;
        }
    }
}