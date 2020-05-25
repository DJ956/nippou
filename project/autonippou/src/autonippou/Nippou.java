package autonippou;

public class Nippou {

	public static final int RADIO_LOW = 0;
	public static final int RADIO_MID = 1;
	public static final int RADIO_HIGH = 2;

	private String planTxt;
	private String doTxt;
	private String checkTxt;
	private String actionTxt;
	private String nextPlanTxt;
	private int understand;
	private int radioValue = RADIO_HIGH;

	public Nippou() {

	}

	public Nippou(String planTxt, String doTxt, String checkTxt, String actionTxt, String nextPlanTxt) {
		setPlanTxt(planTxt);
		setDoTxt(doTxt);
		setCheckTxt(checkTxt);
		setActionTxt(actionTxt);
		setNextPlanTxt(nextPlanTxt);
	}

	public Nippou(String planTxt, String doTxt, String checkTxt, String actionTxt, String nextPlanTxt, int understand) {
		this(planTxt, doTxt, checkTxt, actionTxt, nextPlanTxt);
		setUnderstand(understand);
	}

	public Nippou(String planTxt, String doTxt, String checkTxt, String actionTxt, String nextPlanTxt, int understand, int radioValue) {
		this(planTxt, doTxt, checkTxt, actionTxt, nextPlanTxt, understand);
		this.radioValue = radioValue;
	}

	public String generateNippou() {
		var builder = new StringBuilder();
		builder.append("■本日の目標（PLAN）\n");
		builder.append(planTxt);
		builder.append("\n");

		builder.append("■本日の研修内容と結果（DO）\n");
		builder.append(doTxt);
		builder.append("\n");

		builder.append("■感想、課題と反省点（CHECK）\n");
		builder.append(checkTxt);
		builder.append("\n");

		builder.append("■対策／解決策（ACTION）\n");
		builder.append(actionTxt);
		builder.append("\n");

		builder.append("■明日の目標（PLAN）\n");
		builder.append(nextPlanTxt);
		builder.append("\n");

		return builder.toString();
	}

	public int getRadioValue() {
		return radioValue;
	}

	public void setRadioValue(int radioValue) {
		this.radioValue = radioValue;
	}

	public int getUnderstand() {
		return understand;
	}

	public void setUnderstand(int understand) {
		if(understand < 0) {
			this.understand = 0;
		}else if(understand > 100) {
			this.understand = 100;
		}else {
			this.understand = understand;
		}
	}

	public String getPlanTxt() {
		return planTxt;
	}

	public void setPlanTxt(String planTxt) {
		this.planTxt = planTxt;
	}

	public String getDoTxt() {
		return doTxt;
	}

	public void setDoTxt(String doTxt) {
		this.doTxt = doTxt;
	}

	public String getCheckTxt() {
		return checkTxt;
	}

	public void setCheckTxt(String checkTxt) {
		this.checkTxt = checkTxt;
	}

	public String getActionTxt() {
		return actionTxt;
	}

	public void setActionTxt(String actionTxt) {
		this.actionTxt = actionTxt;
	}

	public String getNextPlanTxt() {
		return nextPlanTxt;
	}

	public void setNextPlanTxt(String nextPlanTxt) {
		this.nextPlanTxt = nextPlanTxt;
	}


	public String toString() {
		return generateNippou();
	}
}
