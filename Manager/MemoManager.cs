using System;
using System.Reflection;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using StickyMemos.Graphic; // DataMamager.cs

namespace StickyMemos
{
	namespace Manager
	{
    	public class MemoManager : Form
    	{
        	public MemoManager()
        	{
        		this.dataList = this.dataManager.GetList;
           		this.list = new List<Memo>();
           		
        		//
        		// Form
        		//
        		this.FormBorderStyle = FormBorderStyle.Sizable;
        		this.StartPosition = FormStartPosition.CenterScreen;
        		this.BackColor = Color.FromArgb(255, 242, 171);
        		this.Size = new Size(322, 321);
        		this.Icon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location);
        		this.MaximizeBox = false;
        		this.ControlBox = false;
        		this.Opacity = 0;
        		
        		this.Resize += new EventHandler(this.App_Resize);
        		this.FormClosing += new FormClosingEventHandler(this.App_FormClosing);
        		
        		this.MemoLoad(this.dataList);
        	}
        	
        	private DataManager dataManager = DataManager.GetData();
        	private List<Data> dataList;
        	
        	private static MemoManager memoManager = null;
        	public static MemoManager GetMemo()
        	{
        		if (memoManager == null)
    			{
           			memoManager = new MemoManager();
        		}
        		return memoManager;
        	}
        	private List<Memo> list;
        	private List<Memo> GetList
        	{
        		get { return list; }
        	}
        	
        	private void App_Resize(object sender, EventArgs e)
        	{
        		foreach (var memo in list)
        		{
        			memo.Opacity = (this.WindowState == FormWindowState.Normal) ? 1 : 0;
        		}
        	}
        	
        	private void App_FormClosing(object sender, FormClosingEventArgs e)
        	{
        		foreach (var memo in list)
        		{
        			Data data = new Data(memo.Name, memo.ColorName, memo.Location, memo.Size);
        			this.dataManager.Save(data);
        			memo.note.SaveFile(string.Format@"{0}/{1}", DataManager.directory, memo.Name);
        			memo.Close();
        		}
        	}
        	private void MemoLoad(List<Data> dataList)
        	{
        		if (dataList.Count == 0)
        		{
           			this.DataAdd("Yellow");
        		}
        		foreach (var data in dataList)
        		{
           			this.MemoAdd(data);
        		}
        	}
        	
        	private Data DataAdd(string colorName)
        	{
        		string color = colorName;
        		Nullable<Point> point = null;
        		Size size = new Size(322, 321);
        		return this.dataManager.Add(color, point, size);
        	}
        	
        	public void MemoAdd(Data data)
        	{
        		MemoColor.ModeChange(data.color);
        		Memo memo = new Memo
        		{
        			Owner = this,
           			Name = data.name,
           			ColorName = data.color,
           			Location = data ?? new Point(0, 0),
           			Size = data.size
        		};
        		if (data.point != null)
        		{
           			memo.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
        		}
        		
        		memo.Add += new Action<string>(this.Add);
        		memo.Exit += new Action<Memo>(this.Exit);
        		
        		this.list.Add(memo);
        		memo.Show();
        	}
        	
        	private void Add(string colorName)
        	{
        		Data data = this.DataAdd(colorName);
        		MemoAdd(data);
        	}
        	
        	private void Exit(Memo memo)
        	{
        		this.list.Remove(memo);
        		memo.Close();
        		if (this.dataList.Count == 0) { this.Close(); }
        	}
    	}
	}
}