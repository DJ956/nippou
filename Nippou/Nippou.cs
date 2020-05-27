using System;
using System.Text;

namespace Nippou{
	public class Nippou{

		public static readonly string[] SPLITOR = new string[]{",,"};

		public string PlanTxt {get; set;}
		public string DoTxt {get; set;}
		public string CheckTxt {get; set;}
		public string ActionTxt {get; set;}
		public string NextPlanTxt {get; set;}

		public int Understand {get; set;}
		public int RangeValue {get; set;}

		public Nippou(){
			Understand = 50;
			RangeValue = 1;
		}


		public string GenerateTxt(){
			var builder = new StringBuilder();

			builder.AppendLine(Understand.ToString());
			builder.AppendLine(SPLITOR[0]);

			builder.AppendLine(RangeValue.ToString());
			builder.AppendLine(SPLITOR[0]);

			builder.AppendLine(PlanTxt);
			builder.AppendLine(SPLITOR[0]);

			builder.AppendLine(DoTxt);
			builder.AppendLine(SPLITOR[0]);

			builder.AppendLine(CheckTxt);
			builder.AppendLine(SPLITOR[0]);

			builder.AppendLine(ActionTxt);
			builder.AppendLine(SPLITOR[0]);

			builder.AppendLine(NextPlanTxt);

			return builder.ToString();
		}

		public string GenerateTxtJar(){
			var builder = new StringBuilder();
			
			builder.AppendLine(PlanTxt);
			builder.AppendLine(SPLITOR[0]);

			builder.AppendLine(DoTxt);
			builder.AppendLine(SPLITOR[0]);

			builder.AppendLine(CheckTxt);
			builder.AppendLine(SPLITOR[0]);

			builder.AppendLine(ActionTxt);
			builder.AppendLine(SPLITOR[0]);

			builder.AppendLine(NextPlanTxt);

			return builder.ToString();
		}
	}
}