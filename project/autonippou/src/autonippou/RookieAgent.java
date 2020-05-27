package autonippou;

import java.util.Date;
import java.util.Random;

import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.chrome.ChromeDriver;
import org.openqa.selenium.chrome.ChromeOptions;
import org.openqa.selenium.support.ui.ExpectedCondition;
import org.openqa.selenium.support.ui.WebDriverWait;

public class RookieAgent {

	private WebDriver driver;

	private static final String LOGIN_URL = "https://rookie.i-learning.jp/Identity/Account/Login?ReturnUrl=%2F";
	private static final String NIPPOU_URL = "https://rookie.i-learning.jp/reports/traineeindex/";

	public RookieAgent() {
		System.setProperty("webdriver.chrome.driver", "./exe/chromedriver.exe");

		var options = new ChromeOptions();
		driver = new ChromeDriver(options);
	}

	public void login(String id, String pw) {
		driver.get(LOGIN_URL);
		driver.manage().window().maximize();

		new WebDriverWait(driver, 10).until((ExpectedCondition<Boolean>) webdriver -> webdriver.getTitle().startsWith("ログイン"));

		driver.findElement(By.id("Input_TraineeUserName")).sendKeys(id);
		driver.findElement(By.id("Input_TraineePassword")).sendKeys(pw);

		driver.findElement(By.className("submit")).submit();
	}

	public void writeNipppou(Nippou nippou) {
		driver.get(NIPPOU_URL);
		new WebDriverWait(driver, 10).until((ExpectedCondition<Boolean>) webdriver -> webdriver.getTitle().startsWith("日報一覧"));

		moveReportURL();

		var random = new Random(new Date().getTime());

		//understand
		var underElm = driver.findElement(By.className("understanding"));
		underElm.clear();
		underElm.sendKeys(String.valueOf(nippou.getUnderstand()));

		//text
		var textArea = driver.findElement(By.className("report-body"));
		textArea.clear();
		textArea.sendKeys(nippou.generateNippou());

		//radio
		var rows = driver.findElements(By.className("mdl-data-table__cell--non-numeric"));
		for(var row : rows) {
			var labels = row.findElements(By.tagName("label"));

			var detect = generateValue(random, nippou.getRadioValue());

			for(int i = 0; i < labels.size(); i++) {
				var label = labels.get(i);
				var input = label.findElement(By.tagName("input"));

				var labelValue = Integer.parseInt(input.getAttribute("value"));
				if(labelValue == detect) {
					input.click();
					break;
				}
			}
		}
	}

	private int generateValue(Random random, int radioValue) {
		switch(radioValue) {
			case Nippou.RADIO_LOW:{
				return random.nextInt(5); //0~4
			}
			case Nippou.RADIO_MID:{
				return random.nextInt(3) + 2; //2~4
			}
			case Nippou.RADIO_HIGH:{
				return random.nextInt(2) + 3; //3~4
			}default:{
				return random.nextInt(5);
			}
		}
	}

	private void moveReportURL() {
		var elems = driver.findElements(By.className("create-report"));
		WebElement targetElm = null;
		for(var elm : elems) {
			if(elm.getText().equals("create日報を書く")) {
				targetElm = elm;
				break;
			}
		}

		if(targetElm == null) {
			return;
		}

		targetElm.click();

		new WebDriverWait(driver, 10).until((ExpectedCondition<Boolean>) webdriver -> webdriver.getTitle().length() > 0);

		//編集するボタン押す
		var url = driver.getCurrentUrl();
		if(url.contains("details")) {
			url = url.replace("details", "edit");
			driver.get(url);

			new WebDriverWait(driver, 10).until((ExpectedCondition<Boolean>) webdriver -> webdriver.getTitle().startsWith("Edit"));
		}
	}

	public void quit() {
		driver.quit();
	}
}
