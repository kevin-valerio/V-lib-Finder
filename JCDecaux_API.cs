using System;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.IO;
using System.Net;
using System.Text;

public class JCDecaux_API
{
	private String API_KEY;
	private String CITY;
	private String URL = "https://api.jcdecaux.com/vls/v1/stations?contract={{VILLE}}&apiKey={{API_KEY}}";
	 

	//Constructor. Setting new API key
	public JCDecaux_API(String API_KEY)
	{
		this.API_KEY = API_KEY;
	}


	//Constructor. Setting new API key and city if needed
	public JCDecaux_API(String API_KEY, String city)
	{
		this.setAPI(API_KEY);
		this.setCity(city);
	} 

	//Setting new URL with city
	public void setCity(String city)
    {
		this.CITY = city;
		this.URL.Replace("{{VILLE}}", city);
    } 

	//Setting new URL with API key
	public void setAPI(String API)
	{
		this.API_KEY = API;
		this.URL.Replace("{{API_KEY}}", API);
	}

	public void foo()
    {
		WebRequest request = WebRequest.Create("<URL>");
		WebResponse response = request.GetResponse();

		Console.WriteLine(((HttpWebResponse)response).StatusDescription);

		Stream dataStream = response.GetResponseStream();

		StreamReader reader = new StreamReader(dataStream);

		String responseFromServer = reader.ReadToEnd();

		MessageBox.Show(responseFromServer);

		reader.Close();
		response.Close();
	}


}
