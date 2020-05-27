using System;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace Nippou{
	public class MainForm{

		private static readonly int TEXT_WIDTH = 100;
		private static readonly int LABEL_WIDTH = 35;

		private static readonly Font DEFAULT_FONT = new Font("Console", 12);

		private int boxWidth = 400;
		private int boxHeight = 100;

		private Form form;
		private Label planLabel;
		private TextBox planBox;

		private Label doLabel;
		private TextBox doBox;

		private Label checkLabel;
		private TextBox checkBox;

		private Label actionLabel;
		private TextBox actionBox;

		private Label nextPlanLabel;
		private TextBox nextPlanBox;

		private MainMenu mainMenu;
		private MenuItem fileMenu;
		private MenuItem openFileMenu;
		private MenuItem saveFileMenu;

		private RadioButton lowRadio;
		private RadioButton midRadio;
		private RadioButton highRadio;

		private NumericUpDown understand;

		private TextBox idBox;
		private TextBox	pwBox;

		private Button exeButton;

		public MainForm(){
			Initial();
			if(File.Exists(Utils.USER_PATH)){
				var data = Utils.LoadUser();
				idBox.Text = data[0];
				pwBox.Text = data[1];
			}
		}

		private void Initial(){
			int currentWidth = 0;
			form = new Form();
			form.Size = new Size(800, 750);
			form.Text = "日報アップローダー Version 1.0.0";

			//Menu
			mainMenu = new MainMenu(){};
			fileMenu = new MenuItem(){Text="ファイル"};
			openFileMenu = new MenuItem(){Text = "ファイルを開く..."};
			openFileMenu.Click += new EventHandler(openFileMenu_Click);
			saveFileMenu = new MenuItem(){Text = "ファイルを保存する...", Shortcut = Shortcut.CtrlS};
			saveFileMenu.Click += new EventHandler(saveFileMenu_Click);

			fileMenu.MenuItems.Add(openFileMenu);
			fileMenu.MenuItems.Add(saveFileMenu);
			mainMenu.MenuItems.Add(fileMenu);
			form.Menu = mainMenu;
			
			//plan
			planLabel = new Label(){Text = "Plan:", Location = new Point(0, currentWidth), Font = DEFAULT_FONT};
			planBox = new TextBox(){Font = DEFAULT_FONT, ScrollBars = ScrollBars.Vertical};
			planBox.Width = boxWidth;
			planBox.Height = boxHeight;
			planBox.Multiline = true;
			currentWidth += LABEL_WIDTH;
			planBox.Location = new Point(0, currentWidth);

			form.Controls.Add(planLabel);
			form.Controls.Add(planBox);
			//do
			currentWidth += TEXT_WIDTH;
			doLabel = new Label(){Text = "Do:", Location = new Point(0, currentWidth), Font = DEFAULT_FONT};
			doBox = new TextBox(){Font = DEFAULT_FONT, ScrollBars = ScrollBars.Vertical};
			doBox.Width = boxWidth;
			doBox.Height = boxHeight;
			doBox.Multiline = true;
			currentWidth += LABEL_WIDTH;
			doBox.Location = new Point(0, currentWidth);

			form.Controls.Add(doLabel);
			form.Controls.Add(doBox);
			//check
			currentWidth += TEXT_WIDTH;
			checkLabel = new Label(){Text = "Check", Location = new Point(0, currentWidth), Font = DEFAULT_FONT};
			checkBox = new TextBox(){Font = DEFAULT_FONT, ScrollBars = ScrollBars.Vertical};
			checkBox.Width = boxWidth;
			checkBox.Height = boxHeight;
			checkBox.Multiline = true;
			currentWidth += LABEL_WIDTH;
			checkBox.Location = new Point(0, currentWidth);

			form.Controls.Add(checkLabel);
			form.Controls.Add(checkBox);

			//action
			currentWidth += TEXT_WIDTH;
			actionLabel = new Label(){Text = "Action", Location = new Point(0, currentWidth), Font = DEFAULT_FONT};
			actionBox = new TextBox(){Font = DEFAULT_FONT, ScrollBars = ScrollBars.Vertical};
			actionBox.Width = boxWidth;
			actionBox.Height = boxHeight;
			actionBox.Multiline = true;
			currentWidth += LABEL_WIDTH;
			actionBox.Location = new Point(0, currentWidth);

			form.Controls.Add(actionLabel);
			form.Controls.Add(actionBox);

			//nextplan
			currentWidth += TEXT_WIDTH;
			nextPlanLabel = new Label(){Text = "Next Plan:", Location = new Point(0, currentWidth), Font = DEFAULT_FONT};
			nextPlanBox = new TextBox(){Font = DEFAULT_FONT, ScrollBars = ScrollBars.Vertical};
			nextPlanBox.Width = boxWidth;
			nextPlanBox.Height = boxHeight;
			nextPlanBox.Multiline = true;
			currentWidth += LABEL_WIDTH;
			nextPlanBox.Location = new Point(0, currentWidth);

			form.Controls.Add(nextPlanLabel);
			form.Controls.Add(nextPlanBox);

			int x = boxWidth + 20;
			int y = 10;

			//low-high
			var radioLabel = new Label(){Text = "評価の選択(低[0～4],中[2～4],高[3～4])",
				Location = new Point(x, y), Width = 400, Font = DEFAULT_FONT};

			y += 40; 
			lowRadio = new RadioButton(){Text = "低", Location = new Point(x, y), Font = DEFAULT_FONT};
			midRadio = new RadioButton(){Text = "中", Location = new Point(x + 110, y), Checked = true, Font = DEFAULT_FONT};
			highRadio = new RadioButton(){Text = "高", Location = new Point(x + 220, y), Font = DEFAULT_FONT};

			form.Controls.Add(radioLabel);
			form.Controls.Add(lowRadio);
			form.Controls.Add(midRadio);
			form.Controls.Add(highRadio);

			y += 40;
			//understand
			var underLabel = new Label(){Text = "理解度", Location = new Point(x, y), Font = DEFAULT_FONT};
			y += 30;
			understand = new NumericUpDown(){Location = new Point(x, y), Font = DEFAULT_FONT};
			understand.Value = 50;
			understand.Maximum = 100;
			understand.Minimum = 1;

			form.Controls.Add(underLabel);
			form.Controls.Add(understand);

			//id pw
			y+= 30;
			var idLabel = new Label(){Text = "ID:", Location = new Point(x, y), Width = 30, Font = DEFAULT_FONT};
			idBox = new TextBox(){Location = new Point(x + 40, y), Font = DEFAULT_FONT};
			idBox.Width = 100;
			
			var pwLabel = new Label(){Text = "PW:", Location = new Point(x + 140, y), Width = 40, Font = DEFAULT_FONT};
			pwBox = new TextBox(){Location = new Point(x + 180, y), Font = DEFAULT_FONT};
			pwBox.Width = 100;

			form.Controls.Add(idLabel);
			form.Controls.Add(idBox);
			form.Controls.Add(pwLabel);
			form.Controls.Add(pwBox);

			//exe
			y += 40;
			exeButton = new Button(){Text = "投稿", Location = new Point(x, y), Font = DEFAULT_FONT};
			exeButton.Click += new EventHandler(exeButton_Click);
			form.Controls.Add(exeButton);
		}

		private Nippou CreateNippou(){
			var nippou = new Nippou();
			nippou.PlanTxt = planBox.Text;
			nippou.DoTxt = doBox.Text;
			nippou.CheckTxt = checkBox.Text;
			nippou.ActionTxt = actionBox.Text;
			nippou.NextPlanTxt = nextPlanBox.Text;

			if(lowRadio.Checked) nippou.RangeValue = 0;
			if(midRadio.Checked) nippou.RangeValue = 1;
			if(highRadio.Checked) nippou.RangeValue = 2;

			nippou.Understand = (int)understand.Value;

			return nippou;
		}


		private void openFileMenu_Click(object sender, EventArgs e){
			var dialog = new OpenFileDialog();
			dialog.Filter = "テキストファイル(*.txt)|*.txt";
			try{
				if(dialog.ShowDialog() == DialogResult.OK){
					var nippou = Utils.Load(dialog.FileName);

					planBox.Text = nippou.PlanTxt;
					doBox.Text = nippou.DoTxt;
					checkBox.Text = nippou.CheckTxt;
					actionBox.Text = nippou.ActionTxt;
					nextPlanBox.Text = nippou.NextPlanTxt;

					understand.Value = nippou.Understand;

					int range = nippou.RangeValue;

					lowRadio.Checked = false;
					midRadio.Checked = false;
					highRadio.Checked = false;

					if(range == 0) lowRadio.Checked = true;
					if(range == 1) midRadio.Checked = true;
					if(range == 2) highRadio.Checked = true;
				}
			}catch(Exception ex){
				MessageBox.Show("error", ex.Message);
			}
		}

		private void saveFileMenu_Click(object sender, EventArgs e){
			var dialog = new SaveFileDialog();
			dialog.Filter = "テキストファイル(*.txt)|*.txt";
			if(dialog.ShowDialog() == DialogResult.OK){
				Utils.Save(dialog.FileName, CreateNippou());
				Utils.SaveUser(idBox.Text, pwBox.Text);

				MessageBox.Show("保存しました", dialog.FileName);
			}
		}

		private void exeButton_Click(object sender, EventArgs e){
			Utils.Execute(idBox.Text, pwBox.Text, CreateNippou());
		}

		[STAThread]
		public static void Main(String[] args){
			new MainForm().form.ShowDialog();
		}
	}
}