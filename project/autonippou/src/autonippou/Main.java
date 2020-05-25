package autonippou;

public class Main {

	private static final String SPLIT = "--------------------------------------------------------------";

	public static void main(String[] args) {
		if(args.length != 5) {
			System.out.println("Usage:java -jar nippou.jar id pass understand(0~100) src_path range(0~2)");
			return;
		}

		var id = args[0];
		var pw = args[1];
		var understand = Integer.valueOf(args[2]);
		var path = args[3];
		var range = Integer.valueOf(args[4]);

		var nippou = Util.Load(path, understand);
		if(range == Nippou.RADIO_HIGH) {
			nippou.setRadioValue(Nippou.RADIO_HIGH);
		}else if(range == Nippou.RADIO_MID) {
			nippou.setRadioValue(Nippou.RADIO_MID);
		}else if(range == Nippou.RADIO_LOW) {
			nippou.setRadioValue(Nippou.RADIO_LOW);
		}

		System.out.println(SPLIT);
		System.out.println(nippou.toString());
		System.out.println(SPLIT);

		RookieAgent agent = new RookieAgent();
		agent.login(id, pw);

		agent.writeNipppou(nippou);

		System.out.println("done");
	}
}
