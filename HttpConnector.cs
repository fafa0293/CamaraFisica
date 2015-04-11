/*
 * Created by SharpDevelop.
 * User: Dylhann
 * Date: 4/11/2015
 * Time: 9:25 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using FireSharp;
using FireSharp.Interfaces;
using FireSharp.Config;
using FireSharp.Response;
using System.IO;
using System.Drawing.Imaging;

namespace CameraPrueba
{
	/// <summary>
	/// Description of HttpConnector.
	/// </summary>
	public static class HttpConnector
	{
		
		public async static void SendImage(Image image){
			IFirebaseConfig config = new FirebaseConfig
			{
		    	AuthSecret = "fcDLLUXTwgABbxvqETwSMuRmJqayhjb2ZsXlfMBt",
		    	BasePath =  "https://dazzling-inferno-9994.firebaseio.com/",
		  	};
			
			IFirebaseClient  client = new FirebaseClient(config);
			var todo = new Images {
				image = ImageToBase64(image)
	        };
			PushResponse response = await client.PushAsync("images/", todo);
			Images result = response.ResultAs<Images>(); //The response will contain the data written
		}		

			
		
		public static string ImageToBase64(Image image)
		{
		  using (var ms = new MemoryStream())
		  {
		  	//var format = new ImageFormat("JPEG");
		    // Convert Image to byte[]
		    image.Save(ms, ImageFormat.Jpeg);
		    byte[] imageBytes = ms.ToArray();
		
		    // Convert byte[] to Base64 String
		    string base64String = Convert.ToBase64String(imageBytes);
		    return base64String;
		  }
		}
	}
}
