using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using AVFoundation;
using Foundation;

[assembly: Dependency(typeof(AudioPlaySample.iOS.MediaPlayer_iOS))]
namespace AudioPlaySample.iOS
{
	public class MediaPlayer_iOS : App.IMediaPlayer
	{
		AVAudioPlayer player = null;

		private async Task StartPlayerAsync(string title)
		{
			await Task.Run(() => {
				try
				{
					if (player == null)
					{
						var url = NSUrl.FromFilename(title + ".mp3");
						NSError _err = null;
				
						player = new AVAudioPlayer(
									url,
									AVFileType.MpegLayer3,
									out _err
							     );

						player.NumberOfLoops = -1;//player.Looping = true;
						player.PrepareToPlay();
						player.Play();
					} else {
						if (player.Playing == true) {
							player.Stop();
						} else {
							player.Play();
						}
					}
				}catch(Exception ex) {
					Console.WriteLine("Error: " + ex.Message);
				}
			});
		}

		private void StopPlayer()
		{
			if (player != null) {
				if (player.Playing) {
					player.Stop();
				}
				player.Dispose();
				player = null;
			}
		}

		public async Task PlayAsync(string title)
		{
			await StartPlayerAsync(title);
		}

		public void Stop()
		{
			StopPlayer();
		}

		public void GetStatus()
		{
			if (player != null) {
				Console.WriteLine("GetStatus: IsPlaying = " + player.Playing);
				if (player.Playing) {
					double total = player.Duration;
					double now = player.CurrentTime;
					string msg = "PlayNow / Total :" + Environment.NewLine
								  + TimeSpan.FromMilliseconds(now) + " / " + TimeSpan.FromMilliseconds(total);

					Console.WriteLine(msg);
					App.Current.MainPage.DisplayAlert("GetStatus", msg, "OK");
				} else {
					App.Current.MainPage.DisplayAlert("GetStatus", "Paused", "OK");
				}
			} else {
				App.Current.MainPage.DisplayAlert("GetStatus", "Stopped", "OK");
			}
		}
	}
}
