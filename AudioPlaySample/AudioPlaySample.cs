using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AudioPlaySample
{
	public class App : Application
	{
		public interface IMediaPlayer
		{
			Task PlayAsync(string title);
			void Stop();
			void GetStatus();
		}

		public App()
		{
			Button btnPlay = new Button {
				Text = "Play/Pause",
				Command = new Command((obj) => {
					DependencyService.Get<IMediaPlayer>().PlayAsync("shall_we_meet");
				})
			};

			Button btnStop = new Button {
				Text = "Stop",
				Command = new Command((obj) => {
					DependencyService.Get<IMediaPlayer>().Stop();
				})
			};

			Button btnInfo = new Button {
				Text = "GetStatus",
				Command = new Command((obj) => {
					DependencyService.Get<IMediaPlayer>().GetStatus();
				})
			};

			// The root page of your application
			var content = new ContentPage {
				Title = "AudioPlaySample",
				Content = new StackLayout {
					VerticalOptions = LayoutOptions.Center,
					Children = {
						btnPlay,
						btnStop,
						btnInfo
					}
				},
				Padding = new Thickness(10)
			};
			MainPage = new NavigationPage(content);
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}

