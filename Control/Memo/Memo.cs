using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using StickyMemos.Manager; // DataManager.cs

namespace StickyMemos
{
	partial class Memo 
    {
    	public Memo()
    	{
    		InitializeComponent();
    		
    		this.dataList = this.dataManager.GetList;
    	}
    	
    	private DataManager dataManager = DataManager.GetData();
    	private List<Data> dataList;
    	
    	public string ColorName;
    	
    	private void Memo_Load(object sender, EventArgs e)
    	{
    		this.title.Visible = false;
    		this.menubar.Visible = IsMenubar();
    		
    		string path = string.Format(@"{0}/{1}", DataManager.directory, this.Name);
    		if (System.IO.File.ReadAllText(path) != string.Empty)
    		{
    			this.note.LoadFile(path, RichTextBoxStreamType.RichText);
    		}
    		
    		if (this.ColorName == "Black")
    		{
    			WhiteForeColor();
    		}
    	}
    	
    	private bool IsMenubar() // 메뉴바 상태
    	{
    		return (this.Size.Width < 270 || this.Size.Height < 200 ? false : true);
    	}
    	private int MenubarShow() // 메뉴바 보이기
    	{
    		return this.menubar.Location.Y - this.title.Size.Height;
    	}
    	private int MenubarHide() // 메뉴바 숨기기
    	{
    		return this.ClientSize.Height - this.title.Size.Height;
    	}
    	
    	private void Memo_Activated(object sender, EventArgs e)
    	{
    		this.title.Visible = true;
    		this.menubar.Visible = IsMenubar();
    		int height = (this.menubar.Visible == true) ? MenubarShow() : MenubarHide();
    		this.note.Size = new Size(this.ClientSize.Width, height);
    	}
    	
    	private void Memo_Deactivate(object sender, EventArgs e)
    	{
    		this.title.Visible = false;
    		this.menubar.Visible = false;
    		int height = MenubarHide();
    		this.note.Size = new Size(this.ClientSize.Width, height);
    	}
    	
    	private void Memo_SizeChanged(object sender, EventArgs e)
    	{
    		int width = this.title.Size.Width;
    		for (int index = 0; index < this.title.RightMenu.Count; index++)
    		{
    			int length = this.title.RightMenu[index].Size.Width;
    			int x = width - length - (length * index);
    			this.title.RightMenu[index].Location = new Point(x, 0);
    		}
    		
    		this.menubar.Visible = false;
    		int height = MenubarHide();
    		this.note.Size = new Size(this.ClientSize.Width, height);
    	}

    	private void Memo_Resize(object sender, EventArgs e)
    	{
    		Size size = this.Size;
    		string text = "Memo";
    		if (this.note.Lines.Length > 0)
    		{
    			text = this.note.Lines[0];
    			text = (text.Length >= 12) ? text.Substring(0, 12) : text;
    		}
    		this.Text = (this.WindowState == FormWindowState.Minimized) ? text : string.Empty;
    		this.Size = size;
    	}
    	
    	private void Memo_ResizeEnd(object sender, EventArgs e)
    	{
    		this.menubar.Visible = IsMenubar();
    		this.menubar.Line.Size = new Size(this.ClientSize.Width, 1);
    		int height = (this.menubar.Visible == true) ? MenubarShow() : MenubarHide();
    		this.note.Size = new Size(this.ClientSize.Width, height);
    		
    		Data data = new Data(this.Name, this.ColorName, this.Location, this.Size);
    		this.dataManager.Save(data);
    	}
    	
    	protected override bool ProcessCmdKey(ref Message message, Keys keyData)
    	{
    		switch (keyData)
    		{
    			case Keys.Control | Keys.N :
    				this.Add(this.ColorName);
    			break;
    			case Keys.Control | Keys.D :
    				this.ClearMessage();
    			break;
    			case Keys.Alt | Keys.D :
    				this.WindowState = (this.WindowState == FormWindowState.Normal) ? FormWindowState.Minimized : FormWimdowState.Normal;
    				this.Activate();
    			break;
    			default :
    			break;
    		}
    		return base.ProcessCmdKey(ref message, keyData);
    	}
    	
    	private void ClearMessage()
    	{
    		ClearMessageBox messageBox = new ClearMessageBox
    		{
    			TopMost = true,
    			ClientSize = this.ClientSize,
    			Tag = this.ColorName
    		};
    		
    		DialogResult dialogResult = messageBox.ShowDialog();
    		if (dialogResult == DialogResult.Yes)
    		{
    			Data data = dataList.Find(find => find.name == this.Name);
    			this.dataManager.Remove(data);
    			this.Exit(this);
    		}
    		else
    		{
    			this.note.Focus();
    		}
    	}
    }
}