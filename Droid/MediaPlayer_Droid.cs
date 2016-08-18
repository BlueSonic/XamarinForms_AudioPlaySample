using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Android.Media;

[assembly: Dependency(typeof(AudioPlaySample.Droid.MediaPlayer_Droid))]
namespace AudioPlaySample.Droid
{
	public class MediaPlayer_Droid : App.IMediaPlayer
	{
		MediaPlayer player = null;

		private async Task StartPlayerAsync(string title)
		{
			var resourceId = (int)typeof(Resource.Raw).GetField(title).GetValue(null);

			await Task.Run(() => {
				if (player == null) {
					player = new MediaPlayer();
					player.SetAudioStreamType(Stream.Music);

					player = MediaPlayer.Create(
								global::Android.App.Application.Context,
								resourceId
							);
					player.Looping = true;
					player.Start();
				} else {
					if (player.IsPlaying == true) {
						player.Pause();
					} else {
						player.Start();
					}
				}
			});
		}

		private void StopPlayer()
		{
			if ((player != null)) {
				if (player.IsPlaying) {
					player.Stop();
				}
				player.Release();
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
			if ((player != null)) {
				if (player.IsPlaying) {
					int total = player.Duration;
					int now = player.CurrentPosition;
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
