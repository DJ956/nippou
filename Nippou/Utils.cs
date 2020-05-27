using System;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace Nippou{
	public class Utils{

		public static readonly string USER_PATH = "./user.txt";
		private static readonly char USER_SPLIT = ',';

		private static readonly string JAR = "./nippou.jar";
		private static readonly string TEMP_PATH = "./temp.txt";

		public Utils(){

		}

		public static string[] LoadUser(){
			using(var reader = new StreamReader(USER_PATH, Encoding.UTF8)){
				var line = reader.ReadToEnd();
				return line.Split(USER_SPLIT);
			}
		}

		public static void SaveUser(string id, string pw){
			if(File.Exists(USER_PATH)){
				File.Delete(USER_PATH);
			}

			using(var writer = new StreamWriter(USER_PATH, false, Encoding.UTF8)){
				writer.Write(id + USER_SPLIT + pw);
				writer.Flush();
			}
		}

		public static Nippou Load(string path){
			using(var reader = new StreamReader(path, Encoding.GetEncoding("shift_jis"))){
				var line = reader.ReadToEnd();
				var lines = line.Split(Nippou.SPLITOR, StringSplitOptions.None);

				var nippou = new Nippou();

				nippou.Understand = Int32.Parse(lines[0]);
				nippou.RangeValue = Int32.Parse(lines[1]);
				nippou.PlanTxt = lines[2];
				nippou.DoTxt = lines[3];
				nippou.CheckTxt = lines[4];
				nippou.ActionTxt = lines[5];
				nippou.NextPlanTxt = lines[6];

				return nippou;
			}
		}

		public static void Save(string path, Nippou nippou){
			var builder = new StringBuilder();
			using(var writer = new StreamWriter(path, false, Encoding.GetEncoding("shift_jis"))){
				writer.Write(nippou.GenerateTxt());
				writer.Flush();
			}
		}

		public static void Execute(string id, string pw, Nippou nippou){
			if(File.Exists(TEMP_PATH)){
				File.Delete(TEMP_PATH);
			}
			using(var writer = new StreamWriter(TEMP_PATH, false, Encoding.GetEncoding("shift_jis"))){
				writer.Write(nippou.GenerateTxtJar());
				writer.Flush();
			}

			var info = new ProcessStartInfo("java");
			info.UseShellExecute = true;
			info.Arguments =
			 string.Format("{0} {1} {2} {3} {4} {5} {6}", "-jar", JAR, id, pw, nippou.Understand.ToString(),
				TEMP_PATH, nippou.RangeValue.ToString());

			Process.Start(info);
		}
	}
}