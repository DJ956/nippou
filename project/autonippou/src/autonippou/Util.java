package autonippou;

import java.io.BufferedReader;
import java.io.FileInputStream;
import java.io.IOException;
import java.io.InputStreamReader;

public class Util {

	private static final String SPLITOR = ",,";

	/***
	 *
	 * @param path
	 * @param understand
	 * @return
	 */
	public static Nippou Load(String path, int understand) {
		Nippou nippou = new Nippou();
		var builder = new StringBuilder();

		try(var reader = new BufferedReader(new InputStreamReader(new FileInputStream(path),"Shift-JIS"))) {
			String line;
			while((line = reader.readLine()) != null) {
				builder.append(line);
			}

			var contexts = builder.toString().split(SPLITOR);

			nippou.setPlanTxt(contexts[0]);
			nippou.setDoTxt(contexts[1]);
			nippou.setCheckTxt(contexts[2]);
			nippou.setActionTxt(contexts[3]);
			nippou.setNextPlanTxt(contexts[4]);
			nippou.setUnderstand(understand);
		}catch(IOException ex) {
			ex.printStackTrace();
		}

		return nippou;
	}
}
