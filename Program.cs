using System;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using StickyMemos.Manager; // MemoManager.cs

namespace StickyMemos
{
	public class Program
	{
		[STAThreadAttribute]
		public static void Main()
		{
			string AppName = Assembly.GetExecutingAssembly().GetName.Name;
			bool IsCreate;
			Mutex mutex = new Mutex(true, AppName, out IsCreate);
			if (IsCreate)
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new MemoManager());
				mutex.ReleaseMutex();
			}
			else
			{
				MessageBox.Show("이미 실행중 입니다.", AppName, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				Application.Exit();
			}
		}
	}
}